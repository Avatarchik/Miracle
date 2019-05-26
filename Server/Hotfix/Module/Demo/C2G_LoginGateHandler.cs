using System;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Gate)]
	public class C2G_LoginGateHandler : AMRpcHandler<C2G_LoginGate, G2C_LoginGate>
	{
		protected override void Run(Session session, C2G_LoginGate message, Action<G2C_LoginGate> reply)
		{
			G2C_LoginGate response = new G2C_LoginGate();
			try
			{
                //获取key
				string account = Game.Scene.GetComponent<GateSessionKeyComponent>().Get(message.Key);
				if (account == null)
				{
					response.Error = ErrorCode.ERR_ConnectGateKeyError;
					response.Message = "Gate key验证失败!";
					reply(response);
					return;
				}

                //创建玩家
				Player player = ComponentFactory.Create<Player, string>(account);

                //放到Player组件中
				Game.Scene.GetComponent<PlayerComponent>().Add(player);

                //会话存放玩家
				session.AddComponent<SessionPlayerComponent>().Player = player;

                //因为服务器负载均衡，并不知到具体在哪一个服务器，但服务器是明确的知道客户端的ip和端口号的
                //模仿erlang的actor通信机制，实现跨进程的通信（实现原理：有一个位置服务器）
                //收信件的功能
				session.AddComponent<MailBoxComponent, string>(MailboxType.GateSession);

				response.PlayerId = player.Id;
				reply(response);

                //发送一个登录成功的消息
				session.Send(new G2C_TestHotfixMessage() { Info = "recv hotfix message success" });
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}