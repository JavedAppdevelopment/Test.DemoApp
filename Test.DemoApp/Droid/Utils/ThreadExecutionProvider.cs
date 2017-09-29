using System;
using System.Threading;
using System.Threading.Tasks;
using Java.Lang;
using Test.DemoApp.Common.Interfaces;
using Xamarin.Forms;

namespace Test.DemoApp.Forms.Droid.Utils
{
	public class ThreadExecutionProvider : IThreadExecutionProvider
	{
		public void RunOnBackgroundThread(Action action, bool canShowNetworkIndicator, double finishWithDelay = 0)
		{
			CancellationTokenSource tcs = new CancellationTokenSource(TimeSpan.FromSeconds(finishWithDelay));
			Task task = Task.Factory.StartNew(action, tcs.Token);

			Device.StartTimer(TimeSpan.FromSeconds(finishWithDelay), () => {
				tcs.Cancel();
				return false;
			});
		}

		public void RunOnMainThread(Action action)
		{
			Device.BeginInvokeOnMainThread(action);
		}

		public async Task RunOnThreadPoolThreadAsync(Action action)
		{
			await Task.Run(action);
		}
	}
}
