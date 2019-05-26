using System;

namespace ETModel
{
	public static class ComponentFactory
	{
		public static Component CreateWithParent(Type type, Component parent, bool fromPool = true)
		{
			Component component;
			if (fromPool)
			{
				component = Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (Component)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);

			component.Parent = parent;
			if (component is ComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			Game.EventSystem.Awake(component);
			return component;
		}

		public static T CreateWithParent<T>(Component parent, bool fromPool = true) where T : Component
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);
			
			component.Parent = parent;
			if (component is ComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			Game.EventSystem.Awake(component);
			return component;
		}

		public static T CreateWithParent<T, A>(Component parent, A a, bool fromPool = true) where T : Component
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);
			
			component.Parent = parent;
			if (component is ComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			Game.EventSystem.Awake(component, a);
			return component;
		}

		public static T CreateWithParent<T, A, B>(Component parent, A a, B b, bool fromPool = true) where T : Component
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);
			
			component.Parent = parent;
			if (component is ComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			Game.EventSystem.Awake(component, a, b);
			return component;
		}

		public static T CreateWithParent<T, A, B, C>(Component parent, A a, B b, C c, bool fromPool = true) where T : Component
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);
			
			component.Parent = parent;
			if (component is ComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			Game.EventSystem.Awake(component, a, b, c);
			return component;
		}

		public static T Create<T>(bool fromPool = true) where T : Component
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);

			if (component is ComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			Game.EventSystem.Awake(component);
			return component;
		}

		public static T Create<T, A>(A a, bool fromPool = true) where T : Component
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);

			if (component is ComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			Game.EventSystem.Awake(component, a);
			return component;
		}

		public static T Create<T, A, B>(A a, B b, bool fromPool = true) where T : Component
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
                //fromPool为true，则通过对象池获取
                component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
                //否则就创建一个实例
				component = (T)Activator.CreateInstance(type);	
			}
			
            //将获取到的组件添加到事件系统
			Game.EventSystem.Add(component);
            
            //这里不太懂 TODO   组件继承了Entity，Entity继承了ComponentWithId，这里同步了一下？
            if (component is ComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
            //事件系统的Awake方法  例如创建ui时传入的是 组件，name，gameobject引用
			Game.EventSystem.Awake(component, a, b);
            //返回组件
			return component;
		}

		public static T Create<T, A, B, C>(A a, B b, C c, bool fromPool = true) where T : Component
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);

			if (component is ComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			Game.EventSystem.Awake(component, a, b, c);
			return component;
		}

		public static T CreateWithId<T>(long id, bool fromPool = true) where T : ComponentWithId
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);
			
			component.Id = id;
			Game.EventSystem.Awake(component);
			return component;
		}

		public static T CreateWithId<T, A>(long id, A a, bool fromPool = true) where T : ComponentWithId
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);
			
			component.Id = id;
			Game.EventSystem.Awake(component, a);
			return component;
		}

		public static T CreateWithId<T, A, B>(long id, A a, B b, bool fromPool = true) where T : ComponentWithId
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);
			
			component.Id = id;
			Game.EventSystem.Awake(component, a, b);
			return component;
		}

		public static T CreateWithId<T, A, B, C>(long id, A a, B b, C c, bool fromPool = true) where T : ComponentWithId
		{
			Type type = typeof (T);
			
			T component;
			if (fromPool)
			{
				component = (T)Game.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);	
			}
			
			Game.EventSystem.Add(component);
			
			component.Id = id;
			Game.EventSystem.Awake(component, a, b, c);
			return component;
		}
	}
}
