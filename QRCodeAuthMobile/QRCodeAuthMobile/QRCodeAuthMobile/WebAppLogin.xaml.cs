/*
 * Purpose: Displays generated Access code from Web Application
 * 
 * Contributors: 
 * Marilin Ortuno
 * Naomi Wiggins
 * 
 */

using QRCodeAuthMobile.Services;
using QRCodeAuthMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
		}

		public void GetLoggedInUserInfo()
		{
			accountOwner = (User)(Application.Current.Properties["LoggedInUser"]);
		}

	}
}