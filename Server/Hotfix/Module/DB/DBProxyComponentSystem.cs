using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using ETModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ETHotfix
{
	[ObjectSystem]
	public class DbProxyComponentSystem : AwakeSystem<DBProxyComponent>
	{
		public override void Awake(DBProxyComponent self)
		{
			self.Awake();
		}
	}
	
	/// <summary>
	/// 用来与数据库操作代理
	/// </summary>
	public static class DBProxyComponentEx
	{
		public static void Awake(this DBProxyComponent self)
		{
			StartConfig dbStartConfig = StartConfigComponent.Instance.DBConfig;
			self.dbAddress = dbStartConfig.GetComponent<InnerConfig>().IPEndPoint;
		}

		public static async ETTask Save(this DBProxyComponent self, ComponentWithId component)
		{
			Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
			await session.Call(new DBSaveRequest { Component = component });
		}

		public static async ETTask SaveBatch(this DBProxyComponent self, List<ComponentWithId> components)
		{
			Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
			await session.Call(new DBSaveBatchRequest { Components = components });
		}

		public static async ETTask Save(this DBProxyComponent self, ComponentWithId component, CancellationToken cancellationToken)
		{
			Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
			await session.Call(new DBSaveRequest { Component = component }, cancellationToken);
		}


		public static async ETVoid SaveLog(this DBProxyComponent self, ComponentWithId component)
		{
			Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
			await session.Call(new DBSaveRequest { Component = component, CollectionName = "Log" });
		}

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="id"></param>
        /// <returns></returns>
		public static async ETTask<T> Query<T>(this DBProxyComponent self, long id) where T: ComponentWithId
		{
			Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
			DBQueryResponse dbQueryResponse = (DBQueryResponse)await session.Call(new DBQueryRequest { CollectionName = typeof(T).Name, Id = id });
			return (T)dbQueryResponse.Component;
		}
		
		/// <summary>
		/// 根据查询表达式查询
		/// </summary>
		/// <param name="self"></param>
		/// <param name="exp"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static async ETTask<List<ComponentWithId>> Query<T>(this DBProxyComponent self, Expression<Func<T ,bool>> exp) where T: ComponentWithId
		{
			ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
			IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
			IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
			string json = filter.Render(documentSerializer, serializerRegistry).ToJson();
			return await self.Query<T>(json);
		}


        /// <summary>
        /// 根据ID列表查询多个？
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
		public static async ETTask<List<ComponentWithId>> Query<T>(this DBProxyComponent self, List<long> ids) where T : ComponentWithId
		{
			Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
			DBQueryBatchResponse dbQueryBatchResponse = (DBQueryBatchResponse)await session.Call(new DBQueryBatchRequest { CollectionName = typeof(T).Name, IdList = ids });
			return dbQueryBatchResponse.Components;
		}

		/// <summary>
		/// 根据json查询条件查询
		/// </summary>
		/// <param name="self"></param>
		/// <param name="json"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static async ETTask<List<ComponentWithId>> Query<T>(this DBProxyComponent self, string json) where T : ComponentWithId
		{
			Session session = Game.Scene.GetComponent<NetInnerComponent>().Get(self.dbAddress);
			DBQueryJsonResponse dbQueryJsonResponse = (DBQueryJsonResponse)await session.Call(new DBQueryJsonRequest { CollectionName = typeof(T).Name, Json = json });
			return dbQueryJsonResponse.Components;
		}



        public static async ETTask Delete<T>(this DBProxyComponent self, long id)
        {
            DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
            await dbComponent.GetCollection(typeof(T).Name).DeleteOneAsync(i => i.Id == id);
        }

        /// <summary>
        /// 清空表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask DeleteAll<T>(this DBProxyComponent self)
        {
            DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
            var filter = Builders<ComponentWithId>.Filter.Empty;
            await dbComponent.GetCollection(typeof(T).Name).DeleteManyAsync(filter);
        }

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static async ETTask DeleteAll<T>(this DBProxyComponent self, Expression<Func<T, bool>> exp)
        {
            //拉姆塔表达式非常好用
            DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
            ExpressionFilterDefinition<T> filter = new ExpressionFilterDefinition<T>(exp);
            IBsonSerializerRegistry serializerRegistry = BsonSerializer.SerializerRegistry;
            IBsonSerializer<T> documentSerializer = serializerRegistry.GetSerializer<T>();
            string json = filter.Render(documentSerializer, serializerRegistry).ToJson();
            await dbComponent.GetCollection(typeof(T).Name).FindOneAndDeleteAsync(json);
        }

        public static void Update<T>(this DBProxyComponent self, string fieldname, string fieldvalue, string aaa)
        {
            //第一个参数是需要更改的在数据库中的对象  第二个是要更改的对象的值  第三个是更改后的对象的值 对象也可以改 多传点参数就行
            DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
            var filter1 = Builders<ComponentWithId>.Filter.Eq(fieldname, fieldvalue);
            var updata1 = Builders<ComponentWithId>.Update.Set(fieldname, aaa);
            dbComponent.GetCollection(typeof(T).Name).UpdateOne(filter1, updata1);

        }
    }
}