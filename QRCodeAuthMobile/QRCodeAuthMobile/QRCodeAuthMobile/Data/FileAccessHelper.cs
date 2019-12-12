/*
 * Purpose: Used to map and create filepath for the SQLite database on a User's mobile device
 * 
 * Contributors:
 * Naomi Wiggins 
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
