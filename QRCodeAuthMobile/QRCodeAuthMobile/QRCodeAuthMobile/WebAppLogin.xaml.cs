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
		protected User accountOwner = new User();
		public WebAppLogin()
		{
			InitializeComponent();
			GetLoggedInUserInfo();
			GetCodeFromService();
			SendUserInfo();
		}

		public async void GetCodeFromService()
		{
			var code = await DataService.GetWebCode();
			WebCode.Text = code.ToString();
		}

		public async void SendUserInfo()
		{
			await DataService.SendAccountId(accountOwner);
			//await DataService.SendAccountId(accountOwner.UserId);
		}

		public void GetLoggedInUserInfo()
		{
			accountOwner = (User)(Application.Current.Properties["LoggedInUser"]);
		}

	}
}