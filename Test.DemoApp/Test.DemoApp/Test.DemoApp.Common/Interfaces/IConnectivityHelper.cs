using System;
using System.Threading.Tasks;
using Test.DemoApp.Common.Enums;

namespace Test.DemoApp.Common.Interfaces
{
	public interface IConnectivityHelper
	{
		bool IsConnected { get; }

		NetworkStatus NetworkStatus { get; }

		Task InitiateCheckingAsync();

		void ContinueChecking();

		void PauseChecking();

		Task SetConnectionAsync();
	}
}
