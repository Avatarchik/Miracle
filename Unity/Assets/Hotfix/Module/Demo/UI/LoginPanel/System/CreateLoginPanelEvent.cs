using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.CreateLoginPanel)]
	public class CreateLoginPanelEvent: AEvent
	{
		public override void Run()
		{
			UI ui = LoginPanelFactory.Create();
			Game.Scene.GetComponent<UIComponent>().Add(ui);
		}
	}
}
