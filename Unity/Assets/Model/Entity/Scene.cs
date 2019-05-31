namespace ETModel
{
    /// <summary>
    /// 场景枚举
    /// </summary>
	public static class SceneType
	{
		public const string Share = "Share";
		public const string Game = "Game";
		public const string Login = "Login";
		public const string Lobby = "Lobby";
		public const string Map = "Map";
		public const string Launcher = "Launcher";
		public const string Robot = "Robot";
		public const string RobotClient = "RobotClient";
		public const string Realm = "Realm";

        //------游戏中需要的场景
        public const string MyGame = "MyGame";
	}


    /// <summary>
    /// Scene类中定义了一个场景枚举。
    /// Scene类继承自Entity类，Entity继承自ComponentWithId类，ComponentWithId继承自Component类
    /// </summary>
    public sealed class Scene: Entity
	{
		public string Name { get; set; }

		public Scene()
		{
		}

		public Scene(long id): base(id)
		{
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();
		}
	}
}