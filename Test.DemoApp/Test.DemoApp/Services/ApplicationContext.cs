using Test.DemoApp.Common.Interfaces;
using Test.DemoApp.Common.Logging;

namespace Test.DemoApp.Forms.Services
{
	public class ApplicationContext : IApplicationContext
	{		
		public string CurrentLoggedInUserName { get; set; }

		public LogLevel LogLevel { get; set; }
	}
}
