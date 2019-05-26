using System;
using System.Collections.Generic;
using System.Net;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Realm)]
	public class C2R_TestCallHandler : AMRpcHandler<C2R_TestCall, R2C_TestCall>
	{
		protected override void Run(Session session, C2R_TestCall message, Action<R2C_TestCall> reply)
		{
			RunAsync(session, message, reply);
		}

		private void RunAsync(Session session, C2R_TestCall message, Action<R2C_TestCall> reply)
		{
            R2C_TestCall response = new R2C_TestCall();
			try
			{
                response.Error = 0;
                response.Message = "服务器处理成功消息了";
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}