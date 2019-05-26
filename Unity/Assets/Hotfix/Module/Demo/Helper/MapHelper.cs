using System;
using ETModel;

namespace ETHotfix
{
    public static class MapHelper
    {
        public static async ETVoid EnterMapAsync()
        {
            try
            {
                // 加载Unit资源
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>(); 
                await resourcesComponent.LoadBundleAsync($"unit.unity3d");   //异步加载

                // 加载场景资源
                await ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundleAsync("map.unity3d");
                // 切换到map场景
                using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
                {
                    await sceneChangeComponent.ChangeSceneAsync(SceneType.Map);
                }
				
                //发送消息到服务器
                G2C_EnterMap g2CEnterMap = await ETModel.SessionComponent.Instance.Session.Call(new C2G_EnterMap()) as G2C_EnterMap;
                //把自己的ID设置为刚生成传过来的ID
                PlayerComponent.Instance.MyPlayer.UnitId = g2CEnterMap.UnitId;
				
                Game.Scene.AddComponent<OperaComponent>();
				
                Game.EventSystem.Run(EventIdType.EnterMapFinish);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}