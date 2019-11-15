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

	}
}
