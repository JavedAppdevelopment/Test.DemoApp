using System;
using Test.DemoApp.Common.Interfaces;
using Test.DemoApp.Common.DB;

namespace Test.DemoApp.Common.Logging
{
	public class LogsDatabase : AppDatabase, ILogsDatabase
	{
		public LogsDatabase(ILogsInternalDatabase database) : base(database)
		{
		}
	}
}
