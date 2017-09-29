using System;
using Test.DemoApp.Common.DB;
using Test.DemoApp.Common.Entities;
using Test.DemoApp.Common.Interfaces;

namespace Test.DemoApp.Common.Logging
{
	public class LogsInternalDatabase : AppInternalDatabase, ILogsInternalDatabase
	{
		public LogsInternalDatabase(ISqLiteConnectionFactory connectionFactory) : base(connectionFactory.GetLogsConnection())
		{
			// Create tables if they don't exist.
			Connection.CreateTable<LogEntry>();

			Connection.CreateIndex<LogEntry>(x => x.SentToServerAtUtc);
		}
	}
}
