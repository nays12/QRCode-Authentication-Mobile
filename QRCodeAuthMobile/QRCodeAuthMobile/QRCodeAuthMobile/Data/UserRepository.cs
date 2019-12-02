using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using QRCodeAuthMobile.Models;

namespace QRCodeAuthMobile.Data
{
	public class UserRepository
	{
		protected static SQLiteAsyncConnection db;
		public static string StatusMessage { get; set; }

		public UserRepository(string dbPath)
		{
			db = new SQLiteAsyncConnection(dbPath);
			//db.CreateTableAsync<User>();
		}
		public static async Task InitializeTableAsync()
		{
			try
			{
				await db.CreateTableAsync<User>();
				StatusMessage = string.Format("Success! Created a table for Users.");
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not create the Users table. Error: {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
		}

		public static async Task AddUserAsync(User mobileUser)
		{
            try
            {
				await db.InsertAsync(mobileUser);

                StatusMessage = string.Format("Success. Added new User {0}!", mobileUser.FirstName);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
            catch (Exception ex)
            {
				StatusMessage = string.Format("Failure. Could not add new User. Error: {0}.", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
		}

		public static async Task<User> GetUserbyId(string id)
		{
			User u = new User();
			try
			{
				u = await db.Table<User>().Where(i => i.UserId == id).FirstOrDefaultAsync();
				StatusMessage = string.Format("Success. Found User {0}!", u.FirstName);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return u;
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not find User. Error: {0}.", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return null;
			}
		}

        public static async Task<int> GetRowCount()
		{
			try
			{
				int count = await db.Table<User>().CountAsync();
				StatusMessage = string.Format("Success. There are {0} records in the Users table.", count);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return count;
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not Users table count. Error: ", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return 0;
			}
		}

		public static async Task<List<User>> GetAllUsersAsync()
		{
			List<User> users = new List<User>();
			try
			{
				return await db.Table<User>().ToListAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not retrieve user accounts. Error: {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return null;
			}
		}

		public static async Task<User> GetAccountOwnerAsync()
		{
			User u = new User();
			try
			{
				u = await db.Table<User>().FirstAsync();
				StatusMessage = string.Format("Success. Retrieved User {0}!", u.FirstName);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return u;
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not retrieve User account. {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return null;
			}
		}

		public static async Task DeleteUserbyId(string id)
		{
			try
			{
				await db.DeleteAsync<User>(id);

				StatusMessage = string.Format("Successfully deleted {0}!", id);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to delete student with id {0}. Error: {1}", id, ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
		}

    }
}
