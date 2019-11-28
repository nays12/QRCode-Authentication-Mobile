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

		protected static SQLiteAsyncConnection db;
		public static string StatusMessage { get; set; }

		public MobileAccountRepository(string dbPath)
		{
			db = new SQLiteAsyncConnection(dbPath);
			// db.CreateTableAsync<MobileAccount>();
		}

		public static async Task InitializeTableAsync()
		{
			try
			{
				await db.CreateTableAsync<MobileAccount>();
				StatusMessage = string.Format("Success! Created a table for MobileAccounts.");
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not create the MobileAccounts table. Error: {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
		}

		public static async Task AddAccountAsync(MobileAccount m)
		{
			try
			{				
				await db.InsertAsync(m);

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
				MobileAccount m = await db.Table<MobileAccount>().FirstAsync();
				StatusMessage = string.Format("Success! Added Mobile Token Account belonging to user {0}", m.MobileId);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return m;
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get the Mobile Token Account. Error: {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return null;
			}
		}

	}
}
