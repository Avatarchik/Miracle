using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIChoiceServerComponentSystem : AwakeSystem<UIChoiceServerComponent>
    {
        public override void Awake(UIChoiceServerComponent self)
        {
            self.Awake();
        }
    }

    public class UIChoiceServerComponent : Component
    {
        public Text selectLabel;
        public Dropdown serversDropdown;
        public Button noticeButton;
        public Button accountButton;
        public Button beginButton;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            selectLabel = rc.Get<GameObject>("selectLabel").GetComponent<Text>();
            serversDropdown = rc.Get<GameObject>("serversDropdown").GetComponent<Dropdown>();
            noticeButton = rc.Get<GameObject>("noticeButton").GetComponent<Button>();
            accountButton = rc.Get<GameObject>("accountButton").GetComponent<Button>();
            beginButton = rc.Get<GameObject>("beginButton").GetComponent<Button>();
        }

    }
}
