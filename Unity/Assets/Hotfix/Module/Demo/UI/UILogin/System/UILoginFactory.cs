using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class UILoginFactory
    {
        public static UI Create()
        {
	        try
	        {
                //从模型层的资源组件，然后加载Login的AB包
				ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
				resourcesComponent.LoadBundle(UIType.UILogin.StringToAB());   //StringToAB() 变小写加unity3d  如：uilogin.unity3d

                //拿到预制体
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.UILogin.StringToAB(), UIType.UILogin);
				GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

                //通过组件化工厂创建UI
		        UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UILogin, gameObject, false);

                //添加UILogin的逻辑组件
				ui.AddComponent<UILoginComponent>();
				return ui;
	        }
	        catch (Exception e)
	        {
				Log.Error(e);
		        return null;
	        }
		}
    }
}