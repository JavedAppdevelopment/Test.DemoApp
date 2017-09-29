using System;
using System.Threading.Tasks;
using Test.DemoApp.Common.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace Test.DemoApp.Forms.iOS.Utils
{
	public class ThreadExecutionProvider : IThreadExecutionProvider
	{
		public async Task RunOnThreadPoolThreadAsync(Action action)
		{
			await Task.Run(action);
		}

		public void RunOnBackgroundThread(Action action, bool isShowNetworkIndicator, double finishWithDelay = 0)
		{
			nint taskId = UIApplication.SharedApplication.BeginBackgroundTask(() => { });
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = isShowNetworkIndicator;

			action();
			Device.StartTimer(TimeSpan.FromSeconds(finishWithDelay), () =>
			{
				UIApplication.SharedApplication.EndBackgroundTask(taskId);
				return false;
			});
		}

		public void RunOnMainThread(Action action)
		{
			Device.BeginInvokeOnMainThread(action);
		}
	}
}
