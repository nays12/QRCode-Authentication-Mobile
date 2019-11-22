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
                result = await dbconn.InsertAsync(new User
                {
                    UserId = mobileUser.UserId,
                    LastName = mobileUser.LastName,
                    FirstName = mobileUser.FirstName,
                    UserType = mobileUser.UserType,
                });

                StatusMessage = string.Format("Welcome to the mobile token app, {0}!", mobileUser.FirstName);
            }
            catch (Exception ex)
            {
				StatusMessage = string.Format("Sorry we could not add you. Error: {0}.", ex.Message);
			}
		}

		public static async Task<User> GetUserbyId(string id)
		{
			return await dbconn.Table<User>().Where(i => i.UserId == id).FirstOrDefaultAsync();
		}

        public static async Task<int> GetRowCount()
		{
			return await dbconn.Table<User>().CountAsync();	     
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

        public static async Task<User> GetUserbyIndex()
        {
            return await dbconn.Table<User>().FirstAsync();
        }
    }
}
