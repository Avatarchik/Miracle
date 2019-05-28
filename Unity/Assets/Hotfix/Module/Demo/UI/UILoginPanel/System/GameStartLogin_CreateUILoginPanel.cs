using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.GameStartLogin)]
    class GameStartLogin_CreateUILoginPanel : AEvent
    {
        public override void Run()
        {
            UI ui = UILoginPanelFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}
