using Microsoft.Practices.Unity;
using Test.DemoApp.Common.Logging;

namespace Test.DemoApp.Common
{
	public class CommonBootstrapper
	{
		public static void Initialize(IUnityContainer container)
		{
			// Interface and Class Registration goes here.
			container.RegisterType<ILoggingProvider, ConsoleLoggingProvider>("ConsoleLogger", new ContainerControlledLifetimeManager());
		}
	}
}
