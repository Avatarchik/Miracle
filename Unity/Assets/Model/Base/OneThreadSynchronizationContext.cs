using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ETModel
{
    //OneThreadSynchronizationContext类继承自SynchronizationContext。内部维护了一个ConcurrentQueue<Action> queue对象。当中有一个Updata()方法，不断的执行队列内部的委托方法。同时重写了Post()方法，在线程通信的时候，可以向队列中添加元素。
	public class OneThreadSynchronizationContext : SynchronizationContext
	{
        //直接声明了一个静态的实例
		public static OneThreadSynchronizationContext Instance { get; } = new OneThreadSynchronizationContext();

        //获取主线程唯一标识id
		private readonly int mainThreadId = Thread.CurrentThread.ManagedThreadId;

        // 线程同步队列,发送接收socket回调都放到该队列,由poll线程统一执行
        //c#高效的线程安全队列ConcurrentQueue：https://blog.csdn.net/liunianqingshi/article/details/79025818
        //该队列使用了分段存储的概念。ConcurrentQueue分配内存时以段(Segment)为单位，一个段内部含有一个默认长度为32的数组和执行下一个段的指针，有个和Head和Tail指针分别指向了起始段和结束段（这种结构有点像操作系统的段式内存管理和页式内存管理策略）。
        //这种分配内存的实现方式不但减轻的GC的压力而且调用者也不用显式的调用TrimToSize()方法回收内存（在某段内存为空时，会由GC来回收该段内存）。
        private readonly ConcurrentQueue<Action> queue = new ConcurrentQueue<Action>();


		private Action a;


        //轮询调用队列中的事件
		public void Update()
		{
			while (true)
			{
				if (!this.queue.TryDequeue(out a))
				{
					return;
				}
				a();
			}
		}



        /// <summary>
        /// 父类中的Virtual Post()注释： When overridden in a derived class, dispatches an asynchronous message to a synchronization context.
        /// 大意是：在派生类中重写时，将异步消息分派到同步上下文。</summary>
        /// 重写了SynchronizationContext的Post()方法，在线程通信的时候，可以向队列中添加元素
        /// <param name="callback">System.Threading要调用的SendOrPostCallback委托。 主线程要回调的委托事件？</param>
        /// <param name="state">传递给委托的对象。（参数吧？）</param>
        public override void Post(SendOrPostCallback callback, object state)
		{
            //如果是主线程中直接调用了 那就直接执行方法
			if (Thread.CurrentThread.ManagedThreadId == this.mainThreadId)
			{
				callback(state);
				return;
			}
			
            //否则就加入到线程同步队列
			this.queue.Enqueue(() => { callback(state); });
		}
	}
}
