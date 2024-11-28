using SyncerNet.Logging;

namespace SyncerNet
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//设置log等级
			Logger.LogLevel = LogLevel.Debug;

			NetworkServer server = new NetworkServer();
			//加载热更程序集，并将热更调用绑定到NetworkServer
			HotfixLoader.Fix(server);
			server.Init(12345);
			server.ServerLoop();
		}
	}
}
