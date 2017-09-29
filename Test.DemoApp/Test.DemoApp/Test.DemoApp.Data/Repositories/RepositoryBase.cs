using Test.DemoApp.Common.Logging;
using Test.DemoApp.Data.Interfaces;

namespace Test.DemoApp.Data.Repositories
{
	public class RepositoryBase
	{
		public RepositoryBase(IServiceClientDatabase database, ILogger logger)
		{
			Database = database;
			Logger = logger;
		}

		protected IServiceClientDatabase Database { get; set; }

		protected ILogger Logger { get; set; }
	}
}
