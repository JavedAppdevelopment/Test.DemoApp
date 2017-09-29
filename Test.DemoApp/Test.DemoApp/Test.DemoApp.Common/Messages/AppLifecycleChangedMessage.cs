using System;
using Test.DemoApp.Common.Enums;
using Test.DemoApp.Common.Interfaces;

namespace Test.DemoApp.Common.Messages
{
	public class AppLifecycleChangedMessage : IMessage
	{
        public AppLifecycleChangedMessage(AppLifecycleState lifecyleState)
		{
			CurrentLifecyleState = lifecyleState;
		}

        public AppLifecycleState CurrentLifecyleState { get; set; }
	}
}
