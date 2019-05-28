using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.ChoiceCharacterFinish)]
    class ChoiceCharacterFinish_CreateUIMainPanel : AEvent
    {
        public override void Run()
        {
            UI ui = UIMainPanelFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}
