using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.RegisterBegin)]
    class RegisterBegin_CreateUIRegisterPanel : AEvent
    {
        public override void Run()
        {
            UI ui = UIRegisterPanelFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}
