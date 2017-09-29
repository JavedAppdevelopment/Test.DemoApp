using System;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Test.DemoApp.Common.Enums;
using Test.DemoApp.Common.Interfaces;
using Test.DemoApp.Common.Messages;

namespace Test.DemoApp.Common.Providers
{
	public class ConnectivityHelper : IConnectivityHelper
	{
		private readonly IMessagingService _messagingService;
		private readonly INetworkStatusService _networkStatusService;
		private readonly IThreadExecutionProvider _threadExecutionProvider;
		private bool _isKeepChecking;
		private bool _isConnected;
		private NetworkStatus _networkStatus;

		public ConnectivityHelper(IMessagingService messagingService,
								  IThreadExecutionProvider threadExecutionProvider,
								 INetworkStatusService networkStatusService)
		{
			_messagingService = messagingService;
			_threadExecutionProvider = threadExecutionProvider;
			_networkStatusService = networkStatusService;
			_isConnected = CrossConnectivity.Current.IsConnected;
			CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChanged;
			_networkStatusService.NetworkStatusChanged += NetworkStatusService_NetworkStatusChanged;
			_networkStatus = _networkStatusService.NetworkStatus();
		}

		public bool IsConnected
		{
			get
			{
				return _isConnected;
			}

			private set
			{				
				if (_isConnected == value)
					return;

				_isConnected = value;

				_threadExecutionProvider.RunOnMainThread(() =>
				{
					_messagingService.Send(new ConnectivityChangedMessage(_isConnected));
				});
			}
		}

		public NetworkStatus NetworkStatus
		{
			get
			{
				return _networkStatus;
			}
		}

		public async Task InitiateCheckingAsync()
		{
			_isKeepChecking = true;

			while (true)
			{
				await Task.Delay(5000);
				if (_isKeepChecking)
					await SetConnectionAsync();
			}
		}

		public void ContinueChecking()
		{
			_isKeepChecking = true;
		}

		public void PauseChecking()
		{
			_isKeepChecking = false;
		}

		public async Task SetConnectionAsync()
		{						
			await Task.Run(async () =>
			{
				IsConnected = CrossConnectivity.Current.IsConnected && (await CrossConnectivity.Current.IsRemoteReachable("www.google.com"));
			});
		}

		private async void OnConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
		{			
			await SetConnectionAsync();

			System.Diagnostics.Debug.WriteLine($"Common In Connectivity Change Network Status : {_networkStatus} {_networkStatusService.NetworkStatus()}");
			if (_networkStatus != _networkStatusService.NetworkStatus())
			{
				_networkStatus = _networkStatusService.NetworkStatus();
				_messagingService.Send(new NetworkStatusChangedMessage());
			}
		}

		private void NetworkStatusService_NetworkStatusChanged(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine($"Common Network status changed : {_networkStatusService.NetworkStatus()}");
			_networkStatus = _networkStatusService.NetworkStatus();
			_messagingService.Send(new NetworkStatusChangedMessage());
		}
	}
}
