using System;
using ETModel;

namespace ETHotfix
{
    public static class TestCallHelper
    {
        //不懂TODO
        public static async ETVoid OnTestCall()
        {
            try
            {
                // 创建一个ETModel层的Session
                ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);
				
                // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
                Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
                R2C_TestCall r2CTestCall = (R2C_TestCall) await realmSession.Call(new C2R_TestCall() );
                realmSession.Dispose();

                //处理回调
                Log.Debug($"Error={r2CTestCall.Error},消息是={r2CTestCall.Message}");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        } 
    }
}