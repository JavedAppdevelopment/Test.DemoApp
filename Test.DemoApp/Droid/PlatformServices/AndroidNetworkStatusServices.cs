using System;
using Android.App;
using Android.Content;
using Android.Net;
using Plugin.Connectivity;
using Test.DemoApp.Common.Enums;
using Test.DemoApp.Common.Interfaces;

namespace Test.DemoApp.Forms.Droid.PlatformServices
{
	public class AndroidNetworkStatusServices : INetworkStatusService
	{
		private NetworkStatus _currentNetworkStatus = Common.Enums.NetworkStatus.NotReachable;

		public AndroidNetworkStatusServices()
		{
			_currentNetworkStatus = GetActiveNetworkStatus();
			CrossConnectivity.Current.ConnectivityTypeChanged += Current_ConnectivityTypeChanged;
		}

		public event EventHandler NetworkStatusChanged;

		public NetworkStatus NetworkStatus()
		{			
			return GetActiveNetworkStatus();
		}

		void Current_ConnectivityTypeChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityTypeChangedEventArgs e)
		{
			NetworkStatus status = GetActiveNetworkStatus();
			System.Diagnostics.Debug.WriteLine("Connectivity Type Changed");
			System.Diagnostics.Debug.WriteLine($"Android Network Status : {status} {_currentNetworkStatus}");
			if (_currentNetworkStatus != status)
			{
				_currentNetworkStatus = status;
				if (NetworkStatusChanged != null)
					NetworkStatusChanged(this, EventArgs.Empty);
			}
		}

		private NetworkStatus GetActiveNetworkStatus()
		{
			ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
			NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;

			if (activeConnection == null)
				return Common.Enums.NetworkStatus.NotReachable;

			if (activeConnection.IsConnected)
			{
				switch (activeConnection.Type)
				{
					case ConnectivityType.Wifi:
						return Common.Enums.NetworkStatus.ReachableViaWiFiNetwork;
					case ConnectivityType.Mobile:
					case ConnectivityType.MobileDun:
					case ConnectivityType.MobileHipri:
					case ConnectivityType.MobileMms:
					case ConnectivityType.MobileSupl:
						return Common.Enums.NetworkStatus.ReachableViaCarrierDataNetwork;
					default:
						return Common.Enums.NetworkStatus.NotReachable;
				}
			}

			return Common.Enums.NetworkStatus.NotReachable;
		}
	}
}
