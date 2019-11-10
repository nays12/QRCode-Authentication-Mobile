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
		public async Task<string> GetWebCode()
		{
			HttpClient client = new HttpClient();
			string response = await client.GetStringAsync("http://localhost:60933/api/Events/Test");
			var code = JsonConvert.DeserializeObject<string>(response);
			
			return code;
		}

		private async Task<List<Event>> GetAllEvents()
		{
			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync("http://localhost:60933/api/Events/GetAll");
			var eventsList = JsonConvert.DeserializeObject<List<Event>>(response);

			return eventsList;
		}

	}
}
