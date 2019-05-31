using System;
using System.Collections.Generic;
using System.Net;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    class C2R_UILoginPanelHandler : AMRpcHandler<C2R_Login, R2C_Login>
    {
        protected override void Run(Session session, C2R_Login message, Action<R2C_Login> reply)
        {

        }

        private async ETVoid RunAsync(Session session, C2R_Login message, Action<R2C_Login> reply)
        {
            R2C_Login response = new R2C_Login();
            try
            {
                DBProxyComponent dbproy = Game.Scene.GetComponent<DBProxyComponent>();
                List<ComponentWithId> accounts = await dbproy.Query<Account>($"{{\'username\':\'{message.Account}\'}}");
                if (accounts.Count==0)
                {
                    Account account = new Account();
                    Log.Debug("账号不存在，正在保存账号");
                    await dbproy.Save(account);
                }
                else
                {
                    Account account = (Account)accounts[0];
                    Log.Debug("查找到了" + accounts);
                    if (message.Password!=account.password)
                    {
                        Log.Debug("密码正确");
                        return;
                    }
                }


                StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
                Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
                IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
                Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

                G2R_GetLoginKey g2R_GetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey() { Account = message.Account });

                string outerAddress = config.GetComponent<OuterConfig>().Address2;

                response.Address = outerAddress;
                response.Key = g2R_GetLoginKey.Key;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
        
    }
}
