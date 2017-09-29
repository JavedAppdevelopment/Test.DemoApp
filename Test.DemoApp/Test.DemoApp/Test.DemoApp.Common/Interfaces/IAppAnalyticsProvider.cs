using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test.DemoApp.Common.Interfaces
{
	public interface IAppAnalyticsProvider
	{
		/// <summary>
		/// Sends the manually error to raygun.
		/// </summary>
		/// <param name="ex">Exception that need to be send to raygun</param>
		void SendError(Exception ex);

		/// <summary>
		/// this will add analytics for api with it's type to raygun
		/// </summary>
		/// <param name="apiUrl">Api url to be calling.</param>
		/// <param name="apiMethodType">Type of the method like GET/POST.</param>
		/// <param name="time">Time when Api called</param>
		void SendApiCallInformation(string apiUrl, HttpMethod apiMethodType, DateTime time);

		/// <summary>
		/// Send the View information to raygun.
		/// </summary>
		/// <param name="key">Name of the view.</param>
		/// <param name="time">Time when view called or appear.</param>
		void SendViewInformation(string key, DateTime time);

		/// <summary>
		/// Adds the user information in analytics.
		/// </summary>
		/// <param name="emailAddress">Email address.</param>
		/// <param name="firstName">First name.</param>
		/// <param name="fullName">Full name.</param>
		void AddUserInformationInAnalytics(string emailAddress, string firstName, string fullName);
	}
}
