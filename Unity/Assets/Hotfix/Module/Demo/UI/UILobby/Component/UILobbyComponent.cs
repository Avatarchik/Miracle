using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	[ObjectSystem]
	public class UiLobbyComponentSystem : AwakeSystem<UILobbyComponent>
	{
		public override void Awake(UILobbyComponent self)
		{
			self.Awake();
		}
	}
	

	public class UILobbyComponent : Component
	{
		private GameObject enterMap;
		private Text text;

		public void Awake()
		{
			ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
			
			enterMap = rc.Get<GameObject>("EnterMap");
			enterMap.GetComponent<Button>().onClick.Add(this.EnterMap);

			this.text = rc.Get<GameObject>("Text").GetComponent<Text>();
		}

		private void EnterMap()
		{
            //异步进入Map
			MapHelper.EnterMapAsync().Coroutine();
		}
        //客户端逻辑：个人猜测
        //点击UI通过Helper执行逻辑发送消息或者跳转场景，或在Helper中通过消息机制执行方法创建界面
		

	}
}
