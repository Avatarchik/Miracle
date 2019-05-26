using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    
	public static class BundleHelper
	{
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public static async ETTask DownloadBundle()
		{
            //通过预定义符判断是否是异步下载
			if (Define.IsAsync)
			{
				try
				{
                    //获取AssetsBudle下载组件，从web端下载，并对比md5码，对比资源
					using (BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.AddComponent<BundleDownloaderComponent>())
					{
                        //拿到远程和本地所有不需要热更的bundle
                        await bundleDownloaderComponent.StartAsync();
						
                        //通过事件系统调用显示加载界面
						Game.EventSystem.Run(EventIdType.LoadingBegin);
						
						await bundleDownloaderComponent.DownloadAsync();
					}
					
					Game.EventSystem.Run(EventIdType.LoadingFinish);
					
					Game.Scene.GetComponent<ResourcesComponent>().LoadOneBundle("StreamingAssets");
					ResourcesComponent.AssetBundleManifestObject = (AssetBundleManifest)Game.Scene.GetComponent<ResourcesComponent>().GetAsset("StreamingAssets", "AssetBundleManifest");
				}
				catch (Exception e)
				{
					Log.Error(e);
				}

			}
		}

		public static string GetBundleMD5(VersionConfig streamingVersionConfig, string bundleName)
		{
			string path = Path.Combine(PathHelper.AppHotfixResPath, bundleName);
			if (File.Exists(path))
			{
				return MD5Helper.FileMD5(path);
			}
			
			if (streamingVersionConfig.FileInfoDict.ContainsKey(bundleName))
			{
				return streamingVersionConfig.FileInfoDict[bundleName].MD5;	
			}

			return "";
		}
	}
}
