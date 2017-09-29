using System;
using System.Net.Http;
using Mindscape.Raygun4Net;
using Test.DemoApp.Common.Interfaces;

namespace Test.DemoApp.Droid.Providers
{
	public class RaygunAppAnalyticsProvider : IAppAnalyticsProvider
	{
		public void SendApiCallInformation(string apiUrl, HttpMethod apiMethodType, DateTime time)
		{
			RaygunClient.Current.SendPulseTimingEvent(RaygunPulseEventType.NetworkCall, $"{apiMethodType.ToString()} {apiUrl}", new DateTime(1,1,1, time.Hour, time.Minute, time.Second).Ticks);
		}

		public void SendError(Exception ex)
		{
			RaygunClient.Current.SendInBackground(ex);
		}

		public void SendViewInformation(string key, DateTime time)
		{
			RaygunClient.Current.SendPulseTimingEvent(RaygunPulseEventType.ViewLoaded, key, new DateTime(1,1,1, time.Hour, time.Minute, time.Second).Ticks);
		}

		public void AddUserInformationInAnalytics(string emailAddress, string firstName, string fullName)
		{
			RaygunClient.Current.UserInfo = new Mindscape.Raygun4Net.Messages.RaygunIdentifierMessage(emailAddress)
			{
				FirstName = firstName,
				FullName = fullName
			};
		}
	}
}
