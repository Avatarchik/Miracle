using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.RegisterFinish)]
    class RegisterFinish_RemoveUIRegisterPanel:AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIRegisterPanel);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UIRegisterPanel);
        }        
    }
}
