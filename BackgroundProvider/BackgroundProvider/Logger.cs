using System;
using System.IO;
using System.Reflection;

using Serilog;

namespace BackgroundProvider
{
	internal static class Logger
	{
		static Logger()
		{
			var loggerConfiguration = GetConfiguration();
			Instance = loggerConfiguration.CreateLogger();

			Instance.Information("Logger was create successfuly.");
		}

		public static ILogger Instance { get; private set; }

		private static LoggerConfiguration GetConfiguration()
		{
			const string LogDirName = "logs";
			const string LogFileName = "log-.log";

			var appName = Assembly.GetExecutingAssembly().GetName();
			var domainPath = AppDomain.CurrentDomain.BaseDirectory;

			var logFilePath = Path.Combine(
				domainPath, appName.Name, LogDirName, LogFileName);

			return new LoggerConfiguration()
				.MinimumLevel.Information()
				.WriteTo.Async(innerConfig =>
					innerConfig.File(logFilePath, rollingInterval: RollingInterval.Hour),
					bufferSize: 500);
		}
	}
}
