using System;
using Test.DemoApp.Common.Enums;

namespace Test.DemoApp.Common.Interfaces
{	
	public interface INetworkStatusService
	{		
		event EventHandler NetworkStatusChanged;

		NetworkStatus NetworkStatus();
	}
}
