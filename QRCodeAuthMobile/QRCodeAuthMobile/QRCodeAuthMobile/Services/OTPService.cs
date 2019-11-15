using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using QRCodeAuthMobile.Models;

namespace QRCodeAuthMobile.Services
{
    class OTPService
    {
        public static async Task<string> GetWebCode()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://qrcodemobileauthenticationweb.azurewebsites.net/api/Otp");
            var code = JsonConvert.DeserializeObject<string>(response);

            return code;
        }
    }
}
