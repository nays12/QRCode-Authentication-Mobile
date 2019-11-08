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
	}
}
