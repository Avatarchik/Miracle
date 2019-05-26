﻿    using System;
using ETModel;
using PF;
using UnityEngine;

namespace ETHotfix
{
	[MessageHandler(AppType.Map)]
	public class G2M_CreateUnitHandler : AMRpcHandler<G2M_CreateUnit, M2G_CreateUnit>
	{
		protected override void Run(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}
		
		protected async ETVoid RunAsync(Session session, G2M_CreateUnit message, Action<M2G_CreateUnit> reply)
		{
			M2G_CreateUnit response = new M2G_CreateUnit();
			try
			{
                //通过组件工厂创建了Unit
				Unit unit = ComponentFactory.CreateWithId<Unit>(IdGenerater.GenerateId());
                //添加移动组件
				unit.AddComponent<MoveComponent>();
                //添加A*寻路算法组件
				unit.AddComponent<UnitPathComponent>();
                //设置出生点
				unit.Position = new Vector3(-10, 0, -10);
				
                //添加了一个收消息
				await unit.AddComponent<MailBoxComponent>().AddLocation();

				unit.AddComponent<UnitGateComponent, long>(message.GateSessionId);
				Game.Scene.GetComponent<UnitComponent>().Add(unit);
				response.UnitId = unit.Id;
				
				
				// 广播创建的unit
				M2C_CreateUnits createUnits = new M2C_CreateUnits();
				Unit[] units = Game.Scene.GetComponent<UnitComponent>().GetAll();
				foreach (Unit u in units)
				{
					UnitInfo unitInfo = new UnitInfo();
					unitInfo.X = u.Position.x;
					unitInfo.Y = u.Position.y;
					unitInfo.Z = u.Position.z;
					unitInfo.UnitId = u.Id;
					createUnits.Units.Add(unitInfo);   //把刚生成的玩家id和位置响应回去
				}
				MessageHelper.Broadcast(createUnits);
				
				
				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}