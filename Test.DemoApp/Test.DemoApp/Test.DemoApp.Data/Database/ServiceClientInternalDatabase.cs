using Test.DemoApp.Common.DB;
using Test.DemoApp.Common.Interfaces;
using Test.DemoApp.Data.Entities;
using Test.DemoApp.Data.Interfaces;

namespace Test.DemoApp.Data.Database
{
    public class ServiceClientInternalDatabase : AppInternalDatabase, IServiceClientInternalDatabase
	{
		public ServiceClientInternalDatabase(ISqLiteConnectionFactory connectionFactory) : base(connectionFactory.GetAppDBConnection())
		{
			// Create tables here, using following Way,
			Connection.CreateTable<TestEntity>();
		}
	}
}
