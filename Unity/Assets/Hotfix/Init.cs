using System;
using ETModel;

namespace ETHotfix
{
	public static class Init
	{
		public static void Start()
		{
#if ILRuntime
			if (!Define.IsILRuntime)
			{
				Log.Error("mono层是mono模式, 但是Hotfix层是ILRuntime模式");
			}
#else
			if (Define.IsILRuntime)
			{
				Log.Error("mono层是ILRuntime模式, Hotfix层是mono模式");
			}
#endif
			
			try
			{
				// 注册热更层回调事件
				ETModel.Game.Hotfix.Update = () => { Update(); };
				ETModel.Game.Hotfix.LateUpdate = () => { LateUpdate(); };
				ETModel.Game.Hotfix.OnApplicationQuit = () => { OnApplicationQuit(); };
				
                //添加热更层组件
				Game.Scene.AddComponent<UIComponent>();
				Game.Scene.AddComponent<OpcodeTypeComponent>();
				Game.Scene.AddComponent<MessageDispatcherComponent>();

				// 对游戏的配置文件进行热更
				ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
				Game.Scene.AddComponent<ConfigComponent>();
				ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");

                //测试加载的配置  测试代码
                //UnitConfig unitConfig = (UnitConfig)Game.Scene.GetComponent<ConfigComponent>().Get(typeof(UnitConfig), 1001);
                //Log.Debug($"config {JsonHelper.ToJson(unitConfig)}");

                //调用\Unity\Assets\Hotfix\Module\Demo\UI\UILogin\System\InitSceneStart_CreateLoginUI.cs    创建UILogin 
                //生成初始UI界面
                Game.EventSystem.Run(EventIdType.InitSceneStart);
                /*
                 * 目前有三个入口:
                 * 1. EventIdType.InitSceneStart 可以进入熊猫ET自带的多人联机Demo，这个算是官方案例，极好的熟悉框架的demo
                 * 2. EventIdType.CreateLoginPanel 这是一个极度简单的登录，能够和服务器进行简单到不能再简单的通信，整个简单到不能再简单
                 * 3. EventIdType.GameStartLogin 一个正在做的小游戏，预期是ARPG或者RPG，emmm目前已经过了一周零2天了，
                 *    进度UI刚搭到主界面，底下各个分支还没有搭建，并且没有整体的模块图（2019年5月28日14点39分）
                 */
            }
            catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void Update()
		{
			try
			{
				Game.EventSystem.Update();
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void LateUpdate()
		{
			try
			{
				Game.EventSystem.LateUpdate();
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void OnApplicationQuit()
		{
			Game.Close();
		}
	}
}