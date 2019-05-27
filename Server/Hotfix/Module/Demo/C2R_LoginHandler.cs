using System;
using System.Collections.Generic;
using System.Net;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Realm)]
	public class C2R_LoginHandler : AMRpcHandler<C2R_Login, R2C_Login>
	{
		protected override void Run(Session session, C2R_Login message, Action<R2C_Login> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}

		private async ETVoid RunAsync(Session session, C2R_Login message, Action<R2C_Login> reply)
		{
			R2C_Login response = new R2C_Login();
			try
			{
                //数据库检测（可删）
                //if (message.Account != "abcdef" || message.Password != "111111")
                //{
                //	response.Error = ErrorCode.ERR_AccountOrPasswordError;
                //	reply(response);
                //	return;
                //}




                //TODO 数据库相关功能   mangodb数据库根据json查询条件查询
                DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();
                List<ComponentWithId> accounts= await dbProxy.Query<Account>($"{{\'username\':\'{message.Account}\'}}");
                //如果没有查找到玩家
                if (accounts.Count==0)
                {
                    Account account = new Account();
                    Log.Debug("账号不存在，正在保存账号");
                    //保存账号
                    await dbProxy.Save(account);
                }
                else
                {
                    Account account = (Account)accounts[0];
                    Log.Debug("查找到了" +accounts);
                    if (message.Password!=account.password)
                    {
                    Log.Debug("密码正确");
                        return;
                    }
                }



                // 随机分配一个Gate
                StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
				//Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
				IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
				Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

				// 向gate请求一个key,客户端可以拿着这个key连接gate
				G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey() {Account = message.Account});

				string outerAddress = config.GetComponent<OuterConfig>().Address2;

				response.Address = outerAddress;
				response.Key = g2RGetLoginKey.Key;
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}