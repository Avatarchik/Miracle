using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.CreateCharacterBegin)]
    class CreateCharacterBegin_CreateUICreateCharacterPanel : AEvent
    {
        public override void Run()
        {
            UI ui = UICreateCharacterPanelFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}
