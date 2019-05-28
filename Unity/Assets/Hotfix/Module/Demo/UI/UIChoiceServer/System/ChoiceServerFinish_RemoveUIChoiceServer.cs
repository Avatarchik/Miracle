using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;

namespace ETHotfix
{
    [Event(EventIdType.ChoiceServerFinish)]
    class ChoiceServerFinish_RemoveUIChoiceServer : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIChoiceServer);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UIChoiceServer.StringToAB());

        }
    }
}
