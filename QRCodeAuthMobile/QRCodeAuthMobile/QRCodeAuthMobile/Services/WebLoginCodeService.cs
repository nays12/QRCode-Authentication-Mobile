using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace QRCodeAuthMobile.Services
{
	public class WebLoginCodeService
	{
		public static async Task<string> GetWebCode()
		{
			HttpClient client = new HttpClient();
			string response = await client.GetStringAsync("http://localhost:60933/api/Events/Test");
			
			return response;
		}

	}
}
