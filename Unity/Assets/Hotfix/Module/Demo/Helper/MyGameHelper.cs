using System;
using ETModel;

namespace ETHotfix
{
    /// <summary>
    /// 联网demo的切换场景
    /// </summary>
    public static class MyGameHelp
    {
        /// <summary>
        /// 联网demo的切换场景
        /// </summary>
        /// <returns></returns>
        public static async ETVoid EnterMyGameAsync()
        {
            try
            {
                // 获取加载ResourcesComponent组件   加载Unit资源(放一些模型)
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>(); 
                await resourcesComponent.LoadBundleAsync($"unit.unity3d");   //异步加载

                // 加载map场景资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("mygame.unity3d");
                // 切换到map场景
                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(SceneType.MyGame);
                }

                //发送消息到服务器
                G2C_EnterMyGame g2CEnterMyGame = await ETModel.SessionComponent.Instance.Session.Call(new C2G_EnterMyGame()) as G2C_EnterMyGame;
                //把自己的ID设置为刚生成传过来的ID
                PlayerComponent.Instance.MyPlayer.UnitId = g2CEnterMyGame.UnitId;

                Game.Scene.AddComponent<OperaComponent>();
				
                Game.EventSystem.Run(EventIdType.ChoiceCharacterFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}