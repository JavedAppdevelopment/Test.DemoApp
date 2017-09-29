using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Mindscape.Raygun4Net;
using Test.DemoApp.Forms.iOS;
using UIKit;

namespace Test.DemoApp.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			var iosAppConfig = new IosAppConfig();


#if (APPSTORE || PLAYSTORE)
            RaygunClient.Initialize(iosAppConfig.StoreAppRaygunKey).AttachCrashReporting().AttachPulse();
#elif (BETA)
			RaygunClient.Initialize(iosAppConfig.BetaAppRaygunKey).AttachCrashReporting().AttachPulse();
#else
			RaygunClient.Initialize(iosAppConfig.TestAppRaygunKey).AttachCrashReporting().AttachPulse();
#endif

			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
