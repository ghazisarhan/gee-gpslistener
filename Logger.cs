// Todo: enable activating and deactivating appender at runtime
// Todo: Code cleaneup
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;

public class Logger
{
	public static void Setup()
	{
		Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

		PatternLayout patternLayout = new PatternLayout();
		//patternLayout.ConversionPattern = "%date [Thread:%thread] %-5level %logger - %message%newline";
		patternLayout.ConversionPattern = "%date [Thread:%thread] %-5level - %message%newline";
		patternLayout.ActivateOptions();

		// TODO: make file name is the date, a file for each date
		// TODO: Or batter use one file and log-rotate
		RollingFileAppender roller = new RollingFileAppender();
		roller.AppendToFile = true;
		roller.File = @"log.log";
		roller.PreserveLogFileNameExtension = true;
		roller.ImmediateFlush = true;
		roller.Layout = patternLayout;
		roller.MaxSizeRollBackups = 5;
		roller.MaximumFileSize = "1GB";
		roller.RollingStyle = RollingFileAppender.RollingMode.Size;
		roller.StaticLogFileName = true;            
		roller.ActivateOptions();
		hierarchy.Root.AddAppender(roller);

		ConsoleAppender console = new ConsoleAppender ();
		console.Layout = patternLayout;
		console.ActivateOptions ();
		hierarchy.Root.AddAppender (console);

//		MemoryAppender memory = new MemoryAppender();
//		memory.ActivateOptions();
//		hierarchy.Root.AddAppender(memory);

		hierarchy.Root.Level = Level.Debug; // TODO: Change back to Info
		hierarchy.Configured = true;
	}
}