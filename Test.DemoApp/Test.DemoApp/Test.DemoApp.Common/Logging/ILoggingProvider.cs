using System.Threading.Tasks;

namespace Test.DemoApp.Common.Logging
{
    public interface ILoggingProvider
    {
        void Log(
            string message,
            LogLevel level,
            object data,
            string[] tags,
            string callerFullTypeName,
            string callerMemberName,
            long? timeInMilliseconds = null);

        Task ClearLogsAsync();
    }
}
