/*
 * Purpose: Service that allows the mobile application to consume Http request for Event objects
 * 
 * Algorithm:
 * Get all events
 */

using System.Collections.Generic;
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
			var response = await client.GetStringAsync("https://qrcodemobileauthenticationweb.azurewebsites.net/api/Events/GetAll");
			JsonConvert.DeserializeObject<List<Event>>(response);
		}
	}
}
