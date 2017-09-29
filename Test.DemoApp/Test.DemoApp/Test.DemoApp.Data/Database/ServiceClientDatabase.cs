using System;
using Test.DemoApp.Common.DB;
using Test.DemoApp.Data.Interfaces;

namespace Test.DemoApp.Data.Database
{
	public class ServiceClientDatabase : AppDatabase, IServiceClientDatabase
	{
		public ServiceClientDatabase(IServiceClientInternalDatabase database) : base(database)
		{
		}
	}
}
