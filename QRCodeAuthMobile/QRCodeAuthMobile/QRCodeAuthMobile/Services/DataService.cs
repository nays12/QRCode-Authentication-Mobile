using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using QRCodeAuthMobile.Models;

namespace QRCodeAuthMobile.Services
{
	public class DataService
	{
		protected static string url = "https://qrcodemobileauthenticationweb.azurewebsites.net/";
		protected static HttpClient client = new HttpClient();
		public static async Task<int> GetWebCode()
		{
			var response = await client.GetStringAsync(url + "api/Data/GetLoginCode"); 
			var code = JsonConvert.DeserializeObject<int>(response);

			return code;
		}

		public static async Task<List<Event>> GetAllEvents()
		{
			try
			{
				var response = await client.GetStringAsync(url + "api/Events/GetAll");
				var eventsList = JsonConvert.DeserializeObject<List<Event>>(response);

				return eventsList;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}

        public static async Task<List<Credential>> GetAllCredentials(string id)
        {
            try
            {
                var response = await client.GetStringAsync(url + "api/Credentials/GetAllCredentials" + "?id=" + id);            
                var credentialList = JsonConvert.DeserializeObject<List<Credential>>(response);

                return credentialList;
            }
            catch(Exception ex)
            {
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
        }

		public static async Task<List<Credential>> GetIssuedCredentials()
		{
			try
			{
				var response = await client.GetStringAsync(url + "api/Credentials/GetIssuedCredentials");
				var credentialList = JsonConvert.DeserializeObject<List<Credential>>(response);

				return credentialList;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public static async Task<List<Credential>> GetUpdatedCredentials()
		{
			try
			{
				var response = await client.GetStringAsync(url + "api/Credentials/GetUpdatedCredentials");
				var credentialList = JsonConvert.DeserializeObject<List<Credential>>(response);

				return credentialList;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public static async Task<int> GetCredentialIdToDelete()
		{
			try
			{
				var response = await client.GetStringAsync(url + "api/Credentials/GetCredentialIdToDelete");
				var deletedCredentialId = JsonConvert.DeserializeObject<int>(response);

				return deletedCredentialId;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return 0;
			}
		}


	}
}
