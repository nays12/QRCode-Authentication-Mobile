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
		protected static SQLiteAsyncConnection db;
		public static string StatusMessage { get; set; }

		public CredentialRepository(string dbPath)
		{
			db = new SQLiteAsyncConnection(dbPath);
			//db.CreateTableAsync<Credential>();
		}

		public static async Task InitializeTableAsync()
		{
			try
			{
				await db.CreateTableAsync<Credential>();
				StatusMessage = string.Format("Success! Created a table for Credentials.");
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not create the Credentials table. Error: {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}			
		}

		public static async Task AddCredentialAsync(Credential cred)
		{
			try
			{
				await db.InsertAsync(cred);

				StatusMessage = string.Format("Success! Added credential {0}.", cred.Name);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to add credential {0}. Error: {1}", cred.Name, ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
		}

		public static async Task DeleteCredentialById(int id)
		{
			int result = 0;
			Credential cred = new Credential();
			try
			{
				cred = await db.FindAsync<Credential>(id);
				result = await db.DeleteAsync<Credential>(id);

				StatusMessage = string.Format("Success! Deleted Credential '{0}' in Mobile Account belonging to {1}.", cred.Name, cred.Owner);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failure. Could not find Credential '{0}' to Mobile Account belonging to {1} for deletion. Error: {2}", cred.Name, cred.Owner, ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
		}

		public static async Task AddNewCredentialsAsync(List<Credential> creds)
		{
				foreach (Credential c in creds)
				{
					try
					{
						await db.InsertAsync(new Credential
						{
							CredentialId = c.CredentialId,
							Name = c.Name,
							CredentialType = c.CredentialType,
							IssueDate = c.IssueDate,
							ExpirationDate = c.ExpirationDate,
							Value = c.Value,
							IsValid = c.IsValid,
							Issuer = c.Issuer,
							Owner = c.Owner
						});

						StatusMessage = string.Format("Success! Added Credential {0}.", c.Name);
						System.Diagnostics.Debug.WriteLine(StatusMessage);
					}
			
					catch (Exception ex)
					{
						StatusMessage = string.Format("Failed to add Credential {0}. Error: {1}", c.Name, ex.Message);
						System.Diagnostics.Debug.WriteLine(StatusMessage);
					}
				}
		}

		public static async Task UpdateCredentialsAsync(List<Credential> creds)
		{
			Credential oldCredential = new Credential();
				foreach (Credential c in creds)
				{
					try
					{
						oldCredential = await db.FindAsync<Credential>(c.CredentialId);

						// Update old credential with new credential values
						oldCredential.CredentialId = c.CredentialId;
						oldCredential.Name = c.Name;
						oldCredential.CredentialType = c.CredentialType;
						oldCredential.IssueDate = c.IssueDate;
						oldCredential.ExpirationDate = c.ExpirationDate;
						oldCredential.Value = c.Value;
						oldCredential.IsValid = c.IsValid;
						oldCredential.Issuer = c.Issuer;
						oldCredential.Owner = c.Owner;

						await db.UpdateAsync(oldCredential);

						StatusMessage = string.Format("Success! Updated Credential {0}.", oldCredential.Name);
						System.Diagnostics.Debug.WriteLine(StatusMessage);
					}
					catch (Exception ex)
					{
						StatusMessage = string.Format("Failure. Could not update Credential {0}. Error: {0}", oldCredential.Name, ex.Message);
						System.Diagnostics.Debug.WriteLine(StatusMessage);
					}
				}
		}

		public static async Task<List<Credential>> GetAllCredentialsAsync()
		{
			try
			{
				return await db.Table<Credential>().ToListAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get credentials. Error: {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return null;
			}			
		}

		public static async Task<Credential> GetCredentialByType(CredentialType ct)
		{
			Credential cred = new Credential();
			try
			{
				cred = await db.Table<Credential>().Where(i => i.CredentialType == ct).FirstOrDefaultAsync();
				StatusMessage = string.Format("Success! Found Credential {0} of Type {1}.", cred.Name, cred.CredentialType);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return cred;
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Could not get Credential of Type {0}. Error: {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return null;
			}
		}

		public static async Task DeleteAllCredentials()
        {
            try
            {
                await db.DeleteAllAsync<Credential>();
            }
            catch (Exception ex)
            {
				StatusMessage = string.Format("Failed to get credentials. {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
        }

    }
}
