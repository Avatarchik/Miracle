using System;
using UnityEngine;

namespace ETModel
{
    public static class UILoadingFactory
    {
        //获取热更加载进度界面的引用KV
        public static UI Create()
        {
	        try
	        {
				GameObject bundleGameObject = ((GameObject)ResourcesHelper.Load("KV")).Get<GameObject>(UIType.UILoading);
				GameObject go = UnityEngine.Object.Instantiate(bundleGameObject);
				go.layer = LayerMask.NameToLayer(LayerNames.UI);
				UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UILoading, go, false);

				ui.AddComponent<UILoadingComponent>();
				return ui;
	        }
	        catch (Exception e)
	        {
				Log.Error(e);
		        return null;
	        }
		}

	    public static void Remove(string type)
	    {
	    }
    }
}