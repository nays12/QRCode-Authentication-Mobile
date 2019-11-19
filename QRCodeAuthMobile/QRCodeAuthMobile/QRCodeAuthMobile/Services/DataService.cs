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

		public static async Task<int> GetWebCode()
		{
			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync("https://qrcodemobileauthenticationweb.azurewebsites.net/api/OPT/RandCode");
			var code = JsonConvert.DeserializeObject<int>(response);

			return code;
		}

		public static async Task<List<Event>> GetAllEvents()
		{
			HttpClient client = new HttpClient();
			try
			{
				var response = await client.GetStringAsync("https://qrcodemobileauthenticationweb.azurewebsites.net/api/Events/GetAll");
				var eventsList = JsonConvert.DeserializeObject<List<Event>>(response);

				return eventsList;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

        public static async Task<Credential> GetCredential(string id)
        {
            //string url = "http://localhost:60933/api/Credentials/GetOwnerCredentials?id=" + id;

            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync("https://qrcodemobileauthenticationweb.azurewebsites.net/api/Credentials/GetOwnerCredentials?id=1304693");
            var credential = JsonConvert.DeserializeObject<Credential>(response);

            return credential;
        }



    }
}
