using ETModel;
namespace ETHotfix
{
    [Event(EventIdType.CreateCharacterFinish)]
    class CreateCharacterFinish_RemoveUICreateCharacterPanel : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UICreateCharacterPanel);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UICreateCharacterPanel);
        }
    }
}
