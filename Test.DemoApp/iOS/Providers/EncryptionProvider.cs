using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Test.DemoApp.Common.Interfaces;

namespace Test.DemoApp.iOS.Providers
{
	/// <summary>
	/// Provide AES based encryption and descryption using 32 bit.
	/// </summary>
	public class EncryptionProvider : IEncryptionProvider
	{
		public string Encrypt(string dataString, string password, string saltString)
		{
			using (Aes encryptor = Aes.Create())
			{
				InitilizeEncryptor(encryptor, password, saltString);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
					{
						byte[] data = ConvertToBytesArray(dataString);
						cs.Write(data, 0, data.Length);
						cs.Close();
					}

					return Convert.ToBase64String(ms.ToArray());
				}
			}
		}

		public string Decrypt(string dataString, string password, string saltString)
		{
			using (Aes encryptor = Aes.Create())
			{
				InitilizeEncryptor(encryptor, password, saltString);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
					{
						byte[] data = Convert.FromBase64String(dataString);
						cs.Write(data, 0, data.Length);
						cs.Close();
					}

					return ConvertToString(ms.ToArray());
				}
			}
		}

		private void InitilizeEncryptor(Aes encryptor, string password, string saltString)
		{
			Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, ConvertToBytesArray(saltString));
			encryptor.Key = pdb.GetBytes(32);
			encryptor.IV = pdb.GetBytes(16);
		}

		private byte[] ConvertToBytesArray(string input)
		{
			return Encoding.Unicode.GetBytes(input);
		}

		private string ConvertToString(byte[] input)
		{
			return Encoding.Unicode.GetString(input, 0, input.Length);
		}
	}
}
