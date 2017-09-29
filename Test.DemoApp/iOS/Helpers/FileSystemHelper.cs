using System;
using System.IO;
using System.Threading.Tasks;
using Test.DemoApp.Common.Interfaces;

namespace Test.DemoApp.iOS.Helpers
{
	public class FileSystemHelper : IFileSystemHelper
	{
		public string GetCurrentDirectlyPath()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		}

		public void CopyFile(string source, string destination, bool canOverwrite)
		{
			File.Copy(source, destination, canOverwrite);
		}

		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public Task SaveStreamToFileAsync(Stream stream, string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				return null;

			if (!File.Exists(filePath))
				throw new FileNotFoundException("File Not Exist");

			using (var fileSteam = File.Open(filePath, FileMode.OpenOrCreate))
			{
				stream.Seek(0, SeekOrigin.Begin);
				stream.CopyTo(fileSteam);
				fileSteam.Close();
			}

			return null;
		}

		public Stream GetStreamFromFile(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				return null;

			if (!File.Exists(filePath))
				throw new FileNotFoundException("File Not Exist");

			Stream stream = File.OpenRead(filePath);

			return stream;
		}
	}
}
