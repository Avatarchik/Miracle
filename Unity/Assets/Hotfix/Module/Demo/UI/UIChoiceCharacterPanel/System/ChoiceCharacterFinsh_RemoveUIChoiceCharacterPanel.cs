using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.CreateCharacterBegin)]
    [Event(EventIdType.ChoiceCharacterFinish)]
    class ChoiceCharacterFinsh_RemoveUIChoiceCharacterPanel : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIChoiceCharacterPanel);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UIChoiceCharacterPanel.StringToAB());
        }
    }
}
