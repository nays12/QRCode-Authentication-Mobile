using QRCodeAuthMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeAuthMobile.Data
{
	public class MobileAccountRepository
	{

		protected static SQLiteAsyncConnection dbconn;
		public static string StatusMessage { get; set; }

		public MobileAccountRepository(string dbPath)
		{
			dbconn = new SQLiteAsyncConnection(dbPath);
			dbconn.CreateTableAsync<MobileAccount>();
		}

		public static async Task AddAccountAsync(MobileAccount m)
		{
			int result = 0;
			try
			{
				result = await dbconn.InsertAsync(m);

				StatusMessage = string.Format("Success! Added Mobile Token Account belonging to user {0}", m.MobileId);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not add Mobile Token Account fo user {0}. Error: {1}", m.MobileId, ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
		}

		public static async Task<MobileAccount> GetMobileTokenAccountAsync()
		{
			try
			{
				return await dbconn.Table<MobileAccount>().FirstAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get the Mobile Token Account. Error: {0}", ex.Message);
				return null;
			}
		}

	}
}
