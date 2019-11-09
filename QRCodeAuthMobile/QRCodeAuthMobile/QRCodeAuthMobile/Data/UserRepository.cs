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
		SQLiteAsyncConnection dbconn;
		public string StatusMessage { get; set; }

		public UserRepository(string dbPath)
		{
			dbconn = new SQLiteAsyncConnection(dbPath);
			dbconn.CreateTableAsync<User>();
		}

		public async Task AddUserAsync(User mobileUser)
		{
			int result = 0;
			try
			{
				result = await dbconn.InsertAsync(new User
				{
					userId = mobileUser.userId,
					last_name = mobileUser.last_name,
					first_name = mobileUser.first_name,
					group = mobileUser.group
				}) ;

				StatusMessage = string.Format("Welcome to the mobile token app, {0}!", mobileUser.first_name);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Sorry we could not add you, {0}. Error: {1}", mobileUser.first_name, ex.Message);
			}
		}

		public async Task<User> GetUserbyId(string id)
		{
			return await dbconn.Table<User>()
								.Where(i => i.userId == id)
								.FirstOrDefaultAsync();
		}

		public async Task<int> GetRowCount()
		{
			StatusMessage = "Success";
			return await dbconn.Table<User>().CountAsync();			
		}


		public async Task<List<User>> GetAllUsersAsync()
		{
			try
			{
				return await dbconn.Table<User>().ToListAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get users. {0}", ex.Message);
			}

			return new List<User>();
		}
	}
}
