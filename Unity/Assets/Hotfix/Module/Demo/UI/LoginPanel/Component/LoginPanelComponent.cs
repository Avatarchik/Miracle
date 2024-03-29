﻿using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class LoginPanelComponentSystem : AwakeSystem<LoginPanelComponent>
    {
        public override void Awake(LoginPanelComponent self)
        {
            self.Awake();
        }
    }

    public class LoginPanelComponent : Component
    {
        public InputField usernameInput;
        public Button loginButton;

        public void Awake()
        {

            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            usernameInput = rc.Get<GameObject>("usernameInputField").GetComponent<InputField>();
            loginButton = rc.Get<GameObject>("loginButton").GetComponent<Button>();

            loginButton.onClick.Add(() =>
            {
                
                Log.Debug("向服务器发送登录消息");
                Game.Scene.GetComponent<UIComponent>().Remove(UIType.LoginPanel);
                ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.LoginPanel.StringToAB());
            });

            loginButton.onClick.Add(TestCall);
        }

        private void TestCall()
        {
            TestCallHelper.OnTestCall().Coroutine();
        }
    }
}
