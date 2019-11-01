using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using QRCodeAuthMobile.Models;

namespace QRCodeAuthMobile.Services
{
	class EventService
	{
		private async void GetAllEvents()
		{
			HttpClient client = new HttpClient();
			var response = await client.GetStringAsync("http://localhost:60933/api/Events/GetAll");
			JsonConvert.DeserializeObject<List<Event>>(response);

		}
	}
}
