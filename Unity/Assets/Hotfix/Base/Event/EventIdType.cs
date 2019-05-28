namespace ETHotfix
{
    public static class EventIdType
    {
        public const string InitSceneStart = "InitSceneStart";
        public const string LoginFinish = "LoginFinish";
        public const string EnterMapFinish = "EnterMapFinish";


        public const string CreateLoginPanel = "CreateLoginPanel";



        //----------游戏中需要的事件ID-------------这个在正式开发时需要提前准备好预留
        //感觉ET的UI框架部分简直太舒服了，模块划分明确，实在太适合团队开发了（个人见解）而且事件调用实在是方便


        //用于进入游戏的开始画面UILoginPanel
        public const string GameStartLogin = "GameStartLogin";
        //用于在登录界面点击注册进入注册界面
        public const string RegisterBegin = "RegisterBegin";
        //用于在注册界面注册完成后返回登录界面使用
        public const string RegisterFinish = "RegisterFinish";
        //用于登录成功后关闭登录界面进入选择大区界面
        public const string UILoginPanelFinish = "UILoginPanelFinish";
        //用于选择大区后关闭选择大区进入选择角色界面
        public const string ChoiceServerFinish = "ChoiceServerFinish";
        //用于选择人物后关闭选择人物界面进入主界面
        public const string ChoiceCharacterFinish = "ChoiceCharacterFinish";
        //用于在选择人物界面时点击创建人物进入创建人物界面
        public const string CreateCharacterBegin = "CreateCharacterBegin";
        //用于在创建人物界面时创建人物玩完成后跳转到选择人物界面
        public const string CreateCharacterFinish = "CreateCharacterFinish";


    }
}