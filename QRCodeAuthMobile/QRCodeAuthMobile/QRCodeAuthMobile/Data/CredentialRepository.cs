/*
 * Purpose: Contains all methods needed to perform CRUD operations on the objects in the "credentials" Table
 * 
 * Algorithm: 
 * Construct class and pass in the database path and create an instance of the credentials table
 * Add a Credential object to the table
 * Get all Credential objects from the table and convert them to a list
 * 
 * Notes: All methods in this class are asynchronous
 */

using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QRCodeAuthMobile.Models;

namespace QRCodeAuthMobile.Data
{
	public class CredentialRepository
	{
		SQLiteAsyncConnection dbconn;
		public string StatusMessage { get; set; }

		public CredentialRepository(string dbPath)
		{
			dbconn = new SQLiteAsyncConnection(dbPath);
			dbconn.CreateTableAsync<Credential>();
		}

		public async Task AddCredentialAsync(Credential cred)
		{
			//int result = 0;
			//try
			//{
			//	result = await dbconn.InsertAsync(new Credential
			//	{	Name = cred.Name,
			//		Issuer = cred.Issuer,
			//		Owner = cred.Owner,
			//		IssueDate = cred.IssueDate,
			//		ExpirationDate = cred.ExpirationDate,
			//		Value = cred.Value,
			//		IsValid = cred.IsValid
			//	});

			//	StatusMessage = string.Format("Success! Added credential {0}. You now have {1} credentials.", cred.Name, result);
			//}
			//catch (Exception ex)
			//{
			//	StatusMessage = string.Format("Failed to add credential {0}. Error: {1}", cred.Name, ex.Message);
			//}
		}

		public async Task<List<Credential>> GetAllCredentialsAsync()
		{
			try
			{
				return await dbconn.Table<Credential>().ToListAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get credentials. {0}", ex.Message);
			}

			return new List<Credential>();
		}

	}
}
