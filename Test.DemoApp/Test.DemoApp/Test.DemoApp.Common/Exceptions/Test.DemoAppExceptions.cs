using System;
namespace Test.DemoApp.Common.Exceptions
{
	public class Test.DemoAppExceptions : Exception
	{
		public Test.DemoAppExceptions(string errorCode, string errorMessage, Exception innerException = null) : base($"ErrorCode={errorCode}, ErrorMessage={errorMessage}", innerException)
		{
			ErrorCodeAsString = errorCode ?? string.Empty;
            ErrorMessage = errorMessage;
		}

		public string ErrorCodeAsString { get; set; }

		public string ErrorMessage { get; set; }
	}
}
