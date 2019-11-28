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
		protected static SQLiteAsyncConnection dbconn;
		public static string StatusMessage { get; set; }

		public UserRepository(string dbPath)
		{
			dbconn = new SQLiteAsyncConnection(dbPath);
			dbconn.CreateTableAsync<User>();
		}

		public static async Task AddUserAsync(User mobileUser)
		{
            int result = 0;
            try
            {
				result = await dbconn.InsertAsync(mobileUser);

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
			return await dbconn.Table<User>().Where(i => i.UserId == id).FirstOrDefaultAsync();
		}

        public static async Task<int> GetRowCount()
		{
			int count = 0;
			try
			{
				count = await dbconn.Table<User>().CountAsync();
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
			try
			{
				return await dbconn.Table<User>().ToListAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get users. {0}", ex.Message);
				return null;
			}
		}

		public static async Task<User> GetAccountOwnerAsync()
		{
			try
			{
				return await dbconn.Table<User>().FirstAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get the User account. {0}", ex.Message);
				return null;
			}
		}

		public static async void DeleteUserbyId(string id)
		{
			int result = 0;
			try
			{
				result = await dbconn.DeleteAsync<User>(id);

				StatusMessage = string.Format("Successfully deleted {0}!", id);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to delete student with id {0}. Error: {1}", id, ex.Message);
			}
		}
    }
}
