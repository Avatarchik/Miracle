using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ETModel
{
	[ObjectSystem]
	public class UiBundleDownloaderComponentAwakeSystem : AwakeSystem<BundleDownloaderComponent>
	{
		public override void Awake(BundleDownloaderComponent self)
		{
			self.bundles = new Queue<string>();
			self.downloadedBundles = new HashSet<string>();
			self.downloadingBundle = "";
		}
	}

	/// <summary>
	/// 用来对比web端的资源，比较md5，对比下载资源
	/// </summary>
	public class BundleDownloaderComponent : Component
	{
		private VersionConfig remoteVersionConfig;
		
		public Queue<string> bundles;

		public long TotalSize;

		public HashSet<string> downloadedBundles;

		public string downloadingBundle;

		public UnityWebRequestAsync webRequest;


        /// <summary>
        /// 拿到远程和本地不需要热更的bundle，添加到bundles
        /// </summary>
        /// <returns></returns>
		public async ETTask StartAsync()
		{
			// 获取远程的Version.txt
			string versionUrl = "";
			try
			{
				using (UnityWebRequestAsync webRequestAsync = ComponentFactory.Create<UnityWebRequestAsync>())
				{
                    //web资源服务器地址 本地为 http://127.0.0.1：8080/PC/    （PC为平台）
					versionUrl = GlobalConfigComponent.Instance.GlobalProto.GetUrl() + "StreamingAssets/" + "Version.txt";
					Log.Debug("web资源服务器地址" + versionUrl);
                    //开始下载版本信息
					await webRequestAsync.DownloadAsync(versionUrl);
                    //将下载到的远程版本配置信息反序列化为VersionConfig
                    remoteVersionConfig = JsonHelper.FromJson<VersionConfig>(webRequestAsync.Request.downloadHandler.text);
					Log.Debug("热更新到的VersionConfig：" + JsonHelper.ToJson(this.remoteVersionConfig));
				}

			}
			catch (Exception e)
			{
				throw new Exception($"url: {versionUrl}", e);
			}

            // 获取客户端的Version.txt
            // 获取streaming目录的Version.txt
            VersionConfig streamingVersionConfig;
            //地址合并  拿到本地版本信息文件路径
			string versionPath = Path.Combine(PathHelper.AppResPath4Web, "Version.txt");
            Log.Debug("本地Version.txt地址：" + versionPath);
            //
			using (UnityWebRequestAsync request = ComponentFactory.Create<UnityWebRequestAsync>())
			{
				await request.DownloadAsync(versionPath);
				streamingVersionConfig = JsonHelper.FromJson<VersionConfig>(request.Request.downloadHandler.text);
			}
			

			// 删掉远程不存在的文件
			DirectoryInfo directoryInfo = new DirectoryInfo(PathHelper.AppHotfixResPath);
            Log.Debug("应用程序外部资源路径存放路径(热更新资源路径):"+PathHelper.AppHotfixResPath);
			if (directoryInfo.Exists)
			{
                //有此AppHotfixResPath我呢见驾就获取其中文件
                FileInfo[] fileInfos = directoryInfo.GetFiles();

				foreach (FileInfo fileInfo in fileInfos)
				{
                    //远程版本文件中如果有此文件直接跳过
					if (remoteVersionConfig.FileInfoDict.ContainsKey(fileInfo.Name))
					{
						continue;
					}
                    //是远程版本文件也跳过
                    if (fileInfo.Name == "Version.txt")
					{
						continue;
					}
					//删除远程版本文件中不存在的资源
					fileInfo.Delete();
				}
			}
			else
			{
                //热更新资源路径不存在则创建此路径
				directoryInfo.Create();
			}

			// 对比MD5
			foreach (FileVersionInfo fileVersionInfo in remoteVersionConfig.FileInfoDict.Values)
			{
				// 对比md5      （远程文件对应的本地文件的MD5）
				string localFileMD5 = BundleHelper.GetBundleMD5(streamingVersionConfig, fileVersionInfo.File);
				if (fileVersionInfo.MD5 == localFileMD5)
				{
					continue;
				}
                //添加到bundles队列
				this.bundles.Enqueue(fileVersionInfo.File);
                //总计大小
				this.TotalSize += fileVersionInfo.Size;
			}
		}

		public int Progress
		{
			get
			{
				if (this.TotalSize == 0)
				{
					return 0;
				}

				long alreadyDownloadBytes = 0;
				foreach (string downloadedBundle in this.downloadedBundles)
				{
					long size = this.remoteVersionConfig.FileInfoDict[downloadedBundle].Size;
					alreadyDownloadBytes += size;
				}
				if (this.webRequest != null)
				{
					alreadyDownloadBytes += (long)this.webRequest.Request.downloadedBytes;
				}
				return (int)(alreadyDownloadBytes * 100f / this.TotalSize);
			}
		}

		public async ETTask DownloadAsync()
		{
			if (this.bundles.Count == 0 && this.downloadingBundle == "")
			{
				return;
			}

			try
			{
				while (true)
				{
					if (this.bundles.Count == 0)
					{
						break;
					}

					this.downloadingBundle = this.bundles.Dequeue();

					while (true)
					{
						try
						{
							using (this.webRequest = ComponentFactory.Create<UnityWebRequestAsync>())
							{
								await this.webRequest.DownloadAsync(GlobalConfigComponent.Instance.GlobalProto.GetUrl() + "StreamingAssets/" + this.downloadingBundle);
								byte[] data = this.webRequest.Request.downloadHandler.data;

								string path = Path.Combine(PathHelper.AppHotfixResPath, this.downloadingBundle);
								using (FileStream fs = new FileStream(path, FileMode.Create))
								{
									fs.Write(data, 0, data.Length);
								}
							}
						}
						catch (Exception e)
						{
							Log.Error($"download bundle error: {this.downloadingBundle}\n{e}");
							continue;
						}

						break;
					}
					this.downloadedBundles.Add(this.downloadingBundle);
					this.downloadingBundle = "";
					this.webRequest = null;
				}
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}
	}
}
