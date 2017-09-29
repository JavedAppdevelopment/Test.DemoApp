using System;
using System.IO;
using System.Threading.Tasks;

namespace Test.DemoApp.Common.Interfaces
{
	public interface IFileSystemHelper
	{
		string GetCurrentDirectlyPath();

		void CopyFile(string source, string destination, bool canOverwrite);

		bool DirectoryExists(string path);

		bool FileExists(string path);

		Task SaveStreamToFileAsync(Stream stream, string path);

		Stream GetStreamFromFile(string path);
	}
}
