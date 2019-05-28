using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UILoginPanelComponentSystem : AwakeSystem<UILoginPanelComponent>
    {
        public override void Awake(UILoginPanelComponent self)
        {
            self.Awake();
        }
    }

    public class UILoginPanelComponent : Component
    {
        public InputField usernameInput;
        public InputField passwordInput;
        public Button loginButton;
        public Button registerButton;
        public Button accountButton;
        public Button noticeButton;

        public void Awake()
        {

            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            usernameInput = rc.Get<GameObject>("usernameInputField").GetComponent<InputField>();
            passwordInput = rc.Get<GameObject>("passwordInputField").GetComponent<InputField>();
            loginButton = rc.Get<GameObject>("loginButton").GetComponent<Button>();
            registerButton = rc.Get<GameObject>("registerButton").GetComponent<Button>();
            accountButton = rc.Get<GameObject>("accountButton").GetComponent<Button>();
            noticeButton = rc.Get<GameObject>("noticeButton").GetComponent<Button>();

            loginButton.onClick.Add(Login);
        }

        private void Login()
        {
            throw new NotImplementedException();
        }



    }
}
