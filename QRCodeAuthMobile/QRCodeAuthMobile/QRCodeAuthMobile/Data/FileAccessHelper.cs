/*
 * Purpose: Used to map and create filepath for the SQLite database on a user's mobile device
 * 
 * Algorithm: 
 * Get the local file path
 */

using Xamarin.Essentials;

namespace QRCodeAuthMobile.Data
{
	public class FileAccessHelper
	{
		public static string GetLocalFilePath(string filename)
		{
			return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
		}
	}
}
