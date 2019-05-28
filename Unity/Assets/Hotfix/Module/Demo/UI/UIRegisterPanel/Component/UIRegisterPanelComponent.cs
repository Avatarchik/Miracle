using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIRegisterPanelComponentSystem : AwakeSystem<UIRegisterPanelComponent>
    {
        public override void Awake(UIRegisterPanelComponent self)
        {
            self.Awake();
        }
    }

    public class UIRegisterPanelComponent : Component
    {
        public Button noticeButton;
        public Button accountButton;
        public Button backButton;
        public Button registerButton;
        public InputField passwordInputField;
        public InputField usernameInputField;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            noticeButton = rc.Get<GameObject>("noticeButton").GetComponent<Button>();
            accountButton = rc.Get<GameObject>("accountButton").GetComponent<Button>();
            backButton = rc.Get<GameObject>("backButton").GetComponent<Button>();
            registerButton = rc.Get<GameObject>("registerButton").GetComponent<Button>();
            passwordInputField = rc.Get<GameObject>("passwordInputField").GetComponent<InputField>();
            usernameInputField = rc.Get<GameObject>("usernameInputField").GetComponent<InputField>();
        }


    }
}
