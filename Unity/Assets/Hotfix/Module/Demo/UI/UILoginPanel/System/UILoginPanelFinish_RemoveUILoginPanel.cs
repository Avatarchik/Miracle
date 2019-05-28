using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.RegisterBegin)]
    [Event(EventIdType.UILoginPanelFinish)]
    class UILoginPanelFinish_RemoveUILoginPanel : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UILoginPanel);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UILoginPanel.StringToAB());
        }
    }
}
