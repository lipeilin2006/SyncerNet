using kcp2k;
using System.Reflection;

#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
#pragma warning disable CS8602 // 解引用可能出现空引用。
#pragma warning disable CS8604 // 引用类型参数可能为 null。
#pragma warning disable CS8974 // 将方法组转换为非委托类型
#pragma warning disable IL2026
#pragma warning disable IL2058
#pragma warning disable IL2072
#pragma warning disable IL2075

namespace SyncerNet
{
	/// <summary>
	/// 用于热更新
	/// </summary>
	public class HotfixLoader
	{
		public static void Fix(NetworkServer server)
		{
			string dllPath = $".{Path.DirectorySeparatorChar}SyncerNet.Hotfix.dll";
			Assembly assembly = Assembly.Load(File.ReadAllBytes(dllPath));
			Type gameType = assembly.GetType("SyncerNet.Hotfix.Game");
			object game = assembly.CreateInstance("SyncerNet.Hotfix.Game");
			server.OnConnected = gameType.
				GetRuntimeMethod("OnConnected", [typeof(int)]).
				CreateDelegate<Action<int>>(game);
			server.OnDisconnected = gameType.
				GetRuntimeMethod("OnDisconnected", [typeof(int)]).
				CreateDelegate<Action<int>>(game);
			server.ProcessMessage = gameType.
				GetRuntimeMethod("ProcessMessage", [typeof(int), typeof(ArraySegment<byte>), typeof(KcpChannel)]).
				CreateDelegate<Action<int, ArraySegment<byte>, KcpChannel>>(game);
			server.OnError = gameType.
				GetRuntimeMethod("OnError", [typeof(int), typeof(ErrorCode), typeof(string)]).
				CreateDelegate<Action<int, ErrorCode, string>>(game);
			gameType.GetProperty("SendAction").SetValue(game, server.Send);
		}
	}
}
