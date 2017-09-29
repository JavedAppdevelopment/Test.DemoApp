using Test.DemoApp.Common.Interfaces;
using Test.DemoApp.Common.Logging;
using XLabs.Forms.Mvvm;

namespace Test.DemoApp.Forms.ViewModels
{
	public class BaseViewModel : ViewModel
	{
		protected readonly INavigationService NavigationService;
		protected readonly ILogger Logger;

		public BaseViewModel(INavigationService navigationService, ILogger logger)
		{
			NavigationService = navigationService;
			Logger = logger;
		}
	}
}
