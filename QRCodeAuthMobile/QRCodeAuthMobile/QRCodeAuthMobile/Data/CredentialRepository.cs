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
		protected static SQLiteAsyncConnection dbconn;
		public static string StatusMessage { get; set; }

		public CredentialRepository(string dbPath)
		{
			dbconn = new SQLiteAsyncConnection(dbPath);
			dbconn.CreateTableAsync<Credential>();
		}

		public static async Task AddCredentialAsync(Credential cred)
		{
			int result = 0;
			try
			{
				result = await dbconn.InsertAsync(new Credential
				{
					Name = cred.Name,
					CredentialType = cred.CredentialType,
					IssueDate = cred.IssueDate,
					ExpirationDate = cred.ExpirationDate,
					Value = cred.Value,
					IsValid = cred.IsValid,
					Issuer = cred.Issuer,
					Owner = cred.Owner
				});

				StatusMessage = string.Format("Success! Added credential {0}. You now have {1} credentials.", cred.Name, result);
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
				cred = await dbconn.FindAsync<Credential>(id);
				result = await dbconn.DeleteAsync<Credential>(id);

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
			int result = 0;


				foreach (Credential c in creds)
				{
					try
					{
						result = await dbconn.InsertAsync(new Credential
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

						StatusMessage = string.Format("Success! Added Credential {0}. You now have {1} credentials.", c.Name, result);
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
			int result = 0;
				foreach (Credential c in creds)
				{
					try
					{
						oldCredential = await dbconn.FindAsync<Credential>(c.CredentialId);

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

						result = await dbconn.UpdateAsync(oldCredential);

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
				return await dbconn.Table<Credential>().ToListAsync();
			}
			catch (Exception ex)
			{
				StatusMessage = string.Format("Failed to get credentials. {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
				return null;
			}			
		}

        public static async Task DeleteAllCredentials()
        {
            int result = 0;
            try
            {
                result = await dbconn.DeleteAllAsync<Credential>();
            }
            catch (Exception ex)
            {
				StatusMessage = string.Format("Failed to get credentials. {0}", ex.Message);
				System.Diagnostics.Debug.WriteLine(StatusMessage);
			}
        }

    }
}
