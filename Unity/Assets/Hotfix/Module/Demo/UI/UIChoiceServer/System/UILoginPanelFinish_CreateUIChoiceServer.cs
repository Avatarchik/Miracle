using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.UILoginPanelFinish)]
    class UILoginPanelFinish_CreateUIChoiceServer : AEvent
    {
        public override void Run()
        {
            UI ui = UIChoiceServerFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}
