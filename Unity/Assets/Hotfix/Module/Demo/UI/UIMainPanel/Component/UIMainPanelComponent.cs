using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UIMainPanelComponentSystem : AwakeSystem<UIMainPanelComponent>
    {
        public override void Awake(UIMainPanelComponent self)
        {
            self.Awake();
        }
    }

    public class UIMainPanelComponent : Component
    {
        public RockerCotroller joystic; //RockerCotroller.target
        public Text chatText;
        public Button skill1;
        public Button skill2;
        public Button skill3;
        public Button skill4;
        public Button skill5;
        public Button drawInButton;
        public Button teamButton;
        public Button taskButton;
        public Button screenshotButton;
        public Button lockButton;
        public Button autoFightButton;
        public Button targetButton;
        public Button liveButton;
        public Button bagButton;
        public Button voiceChatButton;
        public Button friendButton;
        public Text timeText;
        public Image wifiIntensityImage;
        public Slider powerSlider;
        public Text expText;
        public Slider expSlider;
        public Button dungeonButton;
        public Button newServerActivityButton;
        public Button communityButton;
        public Button storeButton;
        public Button firstPayButton;
        public Button newServerButton;
        public Button wealButton;
        public Button activityButton;
        public Button hideOtherButton;
        public Button emailButton;
        public RawImage mapRawImage;
        public Text professionText;
        public Text levelText;
        public Slider mpSlider;
        public Text hpText;
        public Slider hpSlider;
        public Text combatNumberText;
        public Image headImage;

        public void Awake()
        {

            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            chatText = rc.Get<GameObject>("chatText").GetComponent<Text>();
            skill1 = rc.Get<GameObject>("skill1").GetComponent<Button>();
            skill2 = rc.Get<GameObject>("skill2").GetComponent<Button>();
            skill3 = rc.Get<GameObject>("skill3").GetComponent<Button>();
            skill4 = rc.Get<GameObject>("skill4").GetComponent<Button>();
            skill5 = rc.Get<GameObject>("skill5").GetComponent<Button>();
            drawInButton = rc.Get<GameObject>("drawInButton").GetComponent<Button>();
            teamButton = rc.Get<GameObject>("teamButton").GetComponent<Button>();
            taskButton = rc.Get<GameObject>("taskButton").GetComponent<Button>();
            screenshotButton = rc.Get<GameObject>("screenshotButton").GetComponent<Button>();
            lockButton = rc.Get<GameObject>("lockButton").GetComponent<Button>();
            autoFightButton = rc.Get<GameObject>("autoFightButton").GetComponent<Button>();
            targetButton = rc.Get<GameObject>("targetButton").GetComponent<Button>();
            liveButton = rc.Get<GameObject>("liveButton").GetComponent<Button>();
            bagButton = rc.Get<GameObject>("bagButton").GetComponent<Button>();
            voiceChatButton = rc.Get<GameObject>("voiceChatButton").GetComponent<Button>();
            friendButton = rc.Get<GameObject>("friendButton").GetComponent<Button>();
            timeText = rc.Get<GameObject>("timeText").GetComponent<Text>();
            wifiIntensityImage = rc.Get<GameObject>("wifiIntensityImage").GetComponent<Image>();
            powerSlider = rc.Get<GameObject>("powerSlider").GetComponent<Slider>();
            expText = rc.Get<GameObject>("expText").GetComponent<Text>();
            expSlider = rc.Get<GameObject>("expSlider").GetComponent<Slider>();
            dungeonButton = rc.Get<GameObject>("dungeonButton").GetComponent<Button>();
            newServerActivityButton = rc.Get<GameObject>("newServerActivityButton").GetComponent<Button>();
            storeButton = rc.Get<GameObject>("storeButton").GetComponent<Button>();
            firstPayButton = rc.Get<GameObject>("firstPayButton").GetComponent<Button>();
            newServerButton = rc.Get<GameObject>("newServerButton").GetComponent<Button>();
            wealButton = rc.Get<GameObject>("wealButton").GetComponent<Button>();
            activityButton = rc.Get<GameObject>("activityButton").GetComponent<Button>();
            hideOtherButton = rc.Get<GameObject>("hideOtherButton").GetComponent<Button>();
            emailButton = rc.Get<GameObject>("emailButton").GetComponent<Button>();
            mapRawImage = rc.Get<GameObject>("mapRawImage").GetComponent<RawImage>();
            professionText = rc.Get<GameObject>("professionText").GetComponent<Text>();
            levelText = rc.Get<GameObject>("levelText").GetComponent<Text>();
            mpSlider = rc.Get<GameObject>("mpSlider").GetComponent<Slider>();
            hpText = rc.Get<GameObject>("hpText").GetComponent<Text>();
            hpSlider = rc.Get<GameObject>("hpSlider").GetComponent<Slider>();
            combatNumberText = rc.Get<GameObject>("combatNumberText").GetComponent<Text>();
            headImage = rc.Get<GameObject>("headImage").GetComponent<Image>();
        }


    }
}
