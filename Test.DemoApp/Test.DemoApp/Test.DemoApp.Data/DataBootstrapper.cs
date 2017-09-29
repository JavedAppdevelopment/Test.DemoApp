using Microsoft.Practices.Unity;
using Test.DemoApp.Common.DB;
using Test.DemoApp.Common.Interfaces;
using Test.DemoApp.Common.Logging;
using Test.DemoApp.Common.Providers;
using Test.DemoApp.Data.Database;
using Test.DemoApp.Data.Interfaces;
using Test.DemoApp.Data.Repositories;
using Test.DemoApp.Data.Repositories.Interfaces;

namespace Test.DemoApp.Data
{
	public class DataBootstrapper
	{		
		public static void Initialize(IUnityContainer container)
		{
			// Interface and Class Registration goes here.
			container.RegisterType<ISqLiteConnectionFactory, SqLiteConnectionFactory>(new ContainerControlledLifetimeManager());
			container.RegisterType<ILogsInternalDatabase, LogsInternalDatabase>(new HierarchicalLifetimeManager());
			container.RegisterType<IAppInternalDatabase, ServiceClientInternalDatabase>(new HierarchicalLifetimeManager());
			container.RegisterType<IServiceClientInternalDatabase, ServiceClientInternalDatabase>(new HierarchicalLifetimeManager());
			container.RegisterType<IServiceClientDatabase, ServiceClientDatabase>(new HierarchicalLifetimeManager());
			container.RegisterType<ILoggingProvider, LogglyLocalDatabaseLoggingProvider>("LogglyLogger",new ContainerControlledLifetimeManager());
			container.RegisterType<ILogger, Logger>();
			container.RegisterType<IConnectivityHelper, ConnectivityHelper>(new ContainerControlledLifetimeManager());
			container.RegisterType<ILogsDatabase, LogsDatabase>(new HierarchicalLifetimeManager());

			// Repository Registration goes here.
			container.RegisterType<ITestRepository, TestRepository>(new TransientLifetimeManager());
		}
	}
}
