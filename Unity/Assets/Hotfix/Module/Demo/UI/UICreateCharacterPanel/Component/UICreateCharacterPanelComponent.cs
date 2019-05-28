using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UICreateCharacterPanelComponentSystem : AwakeSystem<UICreateCharacterPanelComponent>
    {
        public override void Awake(UICreateCharacterPanelComponent self)
        {
            self.Awake();
        }
    }

    public class UICreateCharacterPanelComponent : Component
    {
        public Button createButton;
        public RawImage characterShowRawImage;
        public Button backCreateButton;
        public Button randomNameButton;
        public Text yourChoiceText;
        public Text capacityShowText;
        public InputField nicknameInputField;
        public Button toxotaeButton;
        public Button magicianButton;
        public Button swordmanButton;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            createButton = rc.Get<GameObject>("createButton").GetComponent<Button>();
            characterShowRawImage = rc.Get<GameObject>("characterShowRawImage").GetComponent<RawImage>();
            backCreateButton = rc.Get<GameObject>("backCreateButton").GetComponent<Button>();
            randomNameButton = rc.Get<GameObject>("randomNameButton").GetComponent<Button>();
            yourChoiceText = rc.Get<GameObject>("yourChoiceText").GetComponent<Text>();
            capacityShowText = rc.Get<GameObject>("capacityShowText").GetComponent<Text>();
            nicknameInputField = rc.Get<GameObject>("nicknameInputField").GetComponent<InputField>();
            toxotaeButton = rc.Get<GameObject>("toxotaeButton").GetComponent<Button>();
            magicianButton = rc.Get<GameObject>("magicianButton").GetComponent<Button>();
            swordmanButton = rc.Get<GameObject>("swordmanButton").GetComponent<Button>();
        }


    }
}
