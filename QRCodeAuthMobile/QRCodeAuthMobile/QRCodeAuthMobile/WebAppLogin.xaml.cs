/*
 * Purpose: Displays generated Access code from Web Application
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QRCodeAuthMobile.Services;
using QRCodeAuthMobile.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebAppLogin : ContentPage
	{
		public WebAppLogin()
		{
			InitializeComponent();
			GetCodeFromService();		
		}

		public async void GetCodeFromService()
		{
			var code = await DataService.GetWebCode();
			WebCode.Text = code.ToString();
		}

	}
}