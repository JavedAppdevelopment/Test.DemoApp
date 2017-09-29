using System;
namespace Test.DemoApp.Common.Interfaces
{
	public interface IStackFrameHelper
	{
		string GetCallerFullTypeName(int? skipFrames = null);

		Type GetCallerFullType(int? skipFrames = null);
	}
}
