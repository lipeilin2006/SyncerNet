namespace SyncerNet.Logging
{
	public class Logger
	{
		public static LogLevel LogLevel { get; set; }
		public static void Debug(object obj)
		{
			if (LogLevel <= LogLevel.Debug)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"[Debug][{DateTime.UtcNow.ToString("MM-dd HH:mm:ssfff")}]{obj}");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
		public static void Info(object obj)
		{
			if (LogLevel <= LogLevel.Info)
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine($"[Debug][{DateTime.UtcNow.ToString("MM-dd HH:mm:ssfff")}]{obj}");
			}
		}
		public static void Warn(object obj)
		{
			if (LogLevel <= LogLevel.Warn)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($"[Debug][{DateTime.UtcNow.ToString("MM-dd HH:mm:ssfff")}]{obj}");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
		public static void Error(object obj)
		{
			if (LogLevel <= LogLevel.Warn)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"[Debug][{DateTime.UtcNow.ToString("MM-dd HH:mm:ssfff")}]{obj}");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}
		public static void Fatal(object obj)
		{
			if (LogLevel <= LogLevel.Fatal)
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine($"[Fatal][{DateTime.UtcNow.ToString("MM-dd HH:mm:ssfff")}]{obj}");
				Console.ForegroundColor = ConsoleColor.White;
			}
		}

	}
	public enum LogLevel
	{
		Debug,
		Info,
		Warn,
		Error,
		Fatal
	}
}
