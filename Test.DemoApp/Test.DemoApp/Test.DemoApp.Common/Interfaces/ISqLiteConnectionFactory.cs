using System;
using SQLite.Net;

namespace Test.DemoApp.Common.Interfaces
{
	public interface ISqLiteConnectionFactory
	{
		SQLiteConnectionWithLock GetLogsConnection();

		SQLiteConnectionWithLock GetAppDBConnection();
	}
}
