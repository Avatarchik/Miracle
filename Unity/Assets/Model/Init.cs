using System;
using System.Threading;
using UnityEngine;

namespace ETModel
{
	public class Init : MonoBehaviour
	{
		private void Start()
		{
			this.StartAsync().Coroutine();
		}
		
		private async ETVoid StartAsync()
		{
			try
			{
                //异步Socket每次接收一步就会单独分出一个线程
                //这里使异步方法全部会回调到主线程，进行Socket线程队列同步
                //最后让收到的所有消息都是有序的
                SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);

                //挂载DebuggerComponent
                gameObject.AddComponent<DebuggerComponent>();

                //通知Unity这个GameObject永远不会销毁
                DontDestroyOnLoad(gameObject);

                //反射当前所有dll,添加到反射系统        Model.dll      类型Init的程序集
                Game.EventSystem.Add(DLLType.Model, typeof(Init).Assembly);

                //计时器
                Game.Scene.AddComponent<TimerComponent>();
                //全局配置数据（服务器的链接IP、资源服务器的HTTP地址）
                Game.Scene.AddComponent<GlobalConfigComponent>();
                //联网组件（进行服务器通讯用的组件）
                Game.Scene.AddComponent<NetOuterComponent>();
                //热更新资源（资源管理，AB包）
                Game.Scene.AddComponent<ResourcesComponent>();
                //所有玩家（用户信息、等）
                Game.Scene.AddComponent<PlayerComponent>();
                //游戏单元 （Demo用的:骷髅模型） 
                Game.Scene.AddComponent<UnitComponent>();
                //ET的UI框架组件（掌管着ET的UI）
                Game.Scene.AddComponent<UIComponent>();

                

                // 下载ab包
                await BundleHelper.DownloadBundle();

                //读取热更代码 ILRuntime VM
                Game.Hotfix.LoadHotfixAssembly();

                // 加载配置 描所有的带config.unity3d的ConfigAttribute标签的配置包（配置文件在Res/Config文件夹，并带有config.untiy3d标签）
                Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
                Game.Scene.AddComponent<ConfigComponent>();
                Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");
                
                //消息代码识别码组件（操作码等）
                Game.Scene.AddComponent<OpcodeTypeComponent>();

                //消息分发组件    收到消息后由哪个handler调用处理
                Game.Scene.AddComponent<MessageDispatcherComponent>();

                //进入热更层，执行热更Init
                Game.Hotfix.GotoHotfix();

                //测试    分发数值监听事件
                Game.EventSystem.Run(EventIdType.TestHotfixSubscribMonoEvent, "TestHotfixSubscribMonoEvent");
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

        //按标准帧更新
        private void Update()
		{
			OneThreadSynchronizationContext.Instance.Update();
			Game.Hotfix.Update?.Invoke();
			Game.EventSystem.Update();
		}

		private void LateUpdate()
		{
			Game.Hotfix.LateUpdate?.Invoke();
			Game.EventSystem.LateUpdate();
		}

		private void OnApplicationQuit()
		{
			Game.Hotfix.OnApplicationQuit?.Invoke();
			Game.Close();
		}
	}
}