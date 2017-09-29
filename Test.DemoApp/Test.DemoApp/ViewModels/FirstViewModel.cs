using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.DemoApp.Common.Enums;
using Test.DemoApp.Common.Interfaces;
using Test.DemoApp.Common.Logging;
using Test.DemoApp.Common.Messages;
using Test.DemoApp.Data.Repositories.Interfaces;
using Xamarin.Forms;

namespace Test.DemoApp.Forms.ViewModels
{
	public class FirstViewModel : BaseViewModel
	{
		private const string TestPassword = "ABCDefghi";
		private const string TestSalt = "1234567890";

		private readonly ITestRepository _testRepository;
		private readonly IPlatformService _platformService;
		private readonly IMessagingService _messagingService;
		private readonly IConnectivityHelper _connectivityHelper;
		private readonly IDialogService _dialogService;
		private readonly IAppAnalyticsProvider _appAnalyticsProvider;
		private readonly IFileSystemHelper _fileSystemHelper;
		private readonly IEncryptionProvider _encryptionProvider;

		private string _version;
		private bool _isConnected;
		private bool _okCancelAlertResponse;
		private string _textToBeEncrypt;
		private string _encryptedText;
		private string _decryptedText;
		private string _sSIDName;
		private string _wifiName;
		private NetworkStatus _networkStatus;

		public FirstViewModel(
			INavigationService navigationService,
			ILogger logger,
			ITestRepository testRepository,
			IPlatformService platformService,
			IMessagingService messagingService,
			IConnectivityHelper connectivityHelper,
			IDialogService dialogService,
			IAppAnalyticsProvider appAnalyticProvider,
			IFileSystemHelper fileSystemHelper,
			IEncryptionProvider encryptionProvider) : base(navigationService, logger)
		{
			_testRepository = testRepository;
			_platformService = platformService;
			_messagingService = messagingService;
			_connectivityHelper = connectivityHelper;
			_dialogService = dialogService;
			_appAnalyticsProvider = appAnalyticProvider;
			_fileSystemHelper = fileSystemHelper;
			_encryptionProvider = encryptionProvider;
			_sSIDName = _platformService.SsidName;
			_wifiName = _platformService.WifiName;

			OkCancelAlertResponse = false;
			OSVersion = $"{_platformService.OsVersion.Major}.{_platformService.OsVersion.Minor}.{_platformService.OsVersion.Build}";
			IsConnected = connectivityHelper.IsConnected;
			NetworkStatus = _connectivityHelper.NetworkStatus;

			logger.Verbose("First ViewModel", null, new[] { LoggerConstants.UiAction });
			logger.Verbose("OS Version", new { OSVersion }, new[] { LoggerConstants.UiAction });

			ShowDialog = new Command(DisplayProgressDialog);
			DisplayCancelAlert = new Command(DisplayAlertWithCancleButton);
			DisplayOkCancelAlert = new Command(DisplayAlertWithOkCancleButton);
			ShowModalPopupDialog = new Command(ShowModalPopup);
			CrashAppCommand = new Command(() => { throw new Exception("Test Exception to crash app"); });
			EncryptCommand = new Command(EncryptText);
			DecryptCommand = new Command(DecryptText);

			Logger.Verbose($"Current Folder Path : {_fileSystemHelper.GetCurrentDirectlyPath()}");
		}

		public async void OnAppear()
		{
			Logger.Start(new { Data = "ViewModel OnAppear Started" }, new[] { LoggerConstants.UiAction });
			Logger.Info("OnAppear", new { Test = "TestData" }, new[] { LoggerConstants.UiAction });
			Logger.Warning("Warning Message", new { Data = "Warning Data" }, new[] { LoggerConstants.UiAction });
			Logger.Error("Some Error Message", new { Error = "Some Error" }, new[] { LoggerConstants.UiAction });
			Logger.Exception(new Exception("Some Exception Message"), new[] { LoggerConstants.UnhandledException });

			_messagingService.Subscribe<ConnectivityChangedMessage>(this, OnConnectivityChange);
			_messagingService.Subscribe<NetworkStatusChangedMessage>(this, OnNetworkStatusChanged);

			_appAnalyticsProvider.AddUserInformationInAnalytics("test@cawstudio.com", "Test", "Test User");
			_appAnalyticsProvider.SendViewInformation("FirstView", DateTime.UtcNow);
			_appAnalyticsProvider.SendApiCallInformation("www.google.co.in", HttpMethod.Post, DateTime.UtcNow);
			Logger.End(null, new { Data = "ViewModel OnAppear Ended" }, new[] { LoggerConstants.UiAction });
		}

		public async void OnDisappear()
		{
			Logger.Info("OnDisappear", new { Test = "TestData" }, new[] { LoggerConstants.UiAction });
			_messagingService.Unsubscribe<ConnectivityChangedMessage>(this);
		}

		public ICommand ShowDialog { get; set; }

		public ICommand DisplayCancelAlert { get; set; }

		public ICommand DisplayOkCancelAlert { get; set; }

		public ICommand ShowModalPopupDialog { get; set; }

		public ICommand CrashAppCommand { get; set; }

		public ICommand EncryptCommand { get; set; }

		public ICommand DecryptCommand { get; set; }

		public string OSVersion
		{
			get { return _version; }

			private set { SetProperty(ref _version, value); }
		}

		public bool IsConnected
		{
			get { return _isConnected; }

			set { SetProperty(ref _isConnected, value); }
		}

		public bool OkCancelAlertResponse
		{
			get { return _okCancelAlertResponse; }

			set { SetProperty(ref _okCancelAlertResponse, value); }
		}

		public string TextToBeEncrypt
		{
			get { return _textToBeEncrypt; }

			set { SetProperty(ref _textToBeEncrypt, value); }
		}

		public string EncryptedText
		{
			get { return _encryptedText; }

			set { SetProperty(ref _encryptedText, value); }
		}

		public string DecryptedText
		{
			get { return _decryptedText; }

			set { SetProperty(ref _decryptedText, value); }
		}

		public NetworkStatus NetworkStatus
		{
			get { return _networkStatus; }

			set { SetProperty(ref _networkStatus, value); }
		}

		public string SSIDName
		{
			get { return _sSIDName; }

			private set { SetProperty(ref _sSIDName, value); }
		}

		public string WifiName
		{
			get { return _wifiName; }

			private set { SetProperty(ref _wifiName, value); }
		}

		public string AppName => _platformService.AppName;

		public string AppVersion => _platformService.AppVersion;

		public string BundleId => _platformService.BundleId;

		public string DeviceName => _platformService.DeviceName;

		public string DeviceId => _platformService.DeviceUuid;

		public string IPAddress => _platformService.IpAddress;

		public bool IsSimulator => _platformService.IsSimulator;

		public double SCreenScale => _platformService.ScreenScale;

		public double ScreenWidth => _platformService.ScreenSize.Width;

		public double ScreenHeight => _platformService.ScreenSize.Height;

		public double BatteryLevel => _platformService.BatteryLevel;

		private void OnConnectivityChange(object sender, ConnectivityChangedMessage message)
		{
			IsConnected = message.IsConnected;
			WifiName = _platformService.WifiName;
			SSIDName = _platformService.SsidName;
			Logger.Info("Connectivity State Changed", new { IsConnected = IsConnected }, new[] { LoggerConstants.Messaging });
		}

		private void OnNetworkStatusChanged(object sender, NetworkStatusChangedMessage e)
		{
			System.Diagnostics.Debug.WriteLine($"On Network Status changed {_connectivityHelper.NetworkStatus}");
			NetworkStatus = _connectivityHelper.NetworkStatus;
			WifiName = _platformService.WifiName;
			SSIDName = _platformService.SsidName;
		}

		private async void DisplayProgressDialog()
		{
			_dialogService.ShowProgress("Loading...");
			await Task.Delay(3000);
			_dialogService.HideProgress();
		}

		private async void DisplayAlertWithCancleButton()
		{
			await _dialogService.DisplayAlertAsync("First Page", "This is alert dialog with cancel button", "Cancel");
		}

		private async void DisplayAlertWithOkCancleButton()
		{
			OkCancelAlertResponse = await _dialogService.DisplayAlertAsync("First Page", "This is alert dialog with Ok and Cancel button", "Ok", "Cancel");
		}

		private async void ShowModalPopup()
		{
			StackLayout sampleStack = new StackLayout()
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Aqua,
				Margin = new Thickness(20),
				WidthRequest = _platformService.ScreenSize.Width,
				HeightRequest = 150,
			};

			Label lblTitle = new Label()
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Title",
				TextColor = Color.Blue,
				WidthRequest = _platformService.ScreenSize.Width,
				HeightRequest = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center
			};

			Label lblMessage = new Label()
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Some Message or Content",
				TextColor = Color.Red,
				WidthRequest = _platformService.ScreenSize.Width,
				HeightRequest = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center
			};

			Button btnClose = new Button()
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Text = "Close",
				TextColor = Color.Green,
				WidthRequest = _platformService.ScreenSize.Width,
				HeightRequest = 40,
			};

			btnClose.Clicked += (sender, e) =>
			{
				_dialogService.HideModalPopup();
			};

			sampleStack.Children.Add(lblTitle);
			sampleStack.Children.Add(lblMessage);
			sampleStack.Children.Add(btnClose);

			_dialogService.ShowModalPopup(sampleStack);
		}

		private void EncryptText()
		{
			if (string.IsNullOrEmpty(TextToBeEncrypt))
				return;

			EncryptedText = _encryptionProvider.Encrypt(TextToBeEncrypt, TestPassword, TestSalt);
		}

		private void DecryptText()
		{
			if (string.IsNullOrEmpty(EncryptedText))
				return;

			DecryptedText = _encryptionProvider.Decrypt(EncryptedText, TestPassword, TestSalt);
		}
	}
}
