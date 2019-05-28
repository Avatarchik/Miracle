
using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIChoiceCharacterPanelComponentSystem : AwakeSystem<UIChoiceCharacterPanelComponent>
    {
        public override void Awake(UIChoiceCharacterPanelComponent self)
        {
            self.Awake();
        }
    }

    public class UIChoiceCharacterPanelComponent : Component
    {
        public RawImage characterShowRawImage;
        public Button character1Button;
        public Button character2Button;
        public Button character3Button;
        public Button character4Button;
        public Button character5Button;
        public Button beginButton;
        public Text yournameText;
        public Text youChoiceText;
        public Button backChoiceButton;
        public Text combatNumberText;
        public Button removeCharacterButton;

        public void Awake()
        {

            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            characterShowRawImage = rc.Get<GameObject>("characterShowRawImage").GetComponent<RawImage>();
            character1Button = rc.Get<GameObject>("character1Button").GetComponent<Button>();
            character2Button = rc.Get<GameObject>("character2Button").GetComponent<Button>();
            character3Button = rc.Get<GameObject>("character3Button").GetComponent<Button>();
            character4Button = rc.Get<GameObject>("character4Button").GetComponent<Button>();
            character5Button = rc.Get<GameObject>("character5Button").GetComponent<Button>();
            beginButton = rc.Get<GameObject>("beginButton").GetComponent<Button>();
            yournameText = rc.Get<GameObject>("yournameText").GetComponent<Text>();
            youChoiceText = rc.Get<GameObject>("youChoiceText").GetComponent<Text>();
            backChoiceButton = rc.Get<GameObject>("backChoiceButton").GetComponent<Button>();
            combatNumberText = rc.Get<GameObject>("combatNumberText").GetComponent<Text>();
            removeCharacterButton = rc.Get<GameObject>("removeCharacterButton").GetComponent<Button>();

        }


    }
}
