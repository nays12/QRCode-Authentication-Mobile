/*
 * Purpose: First screen after login that allows user to select which action they would like to take
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QRCodeAuthMobile.Models;
using QRCodeAuthMobile.Data;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public Home()
		{
			InitializeComponent();
			PutUserinSessionState();
		}
        private async void BtnManagedCredentials_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new ManageCredentials();
            await Navigation.PushAsync(new ManageCredentials());
        }

		private async void BtnWebLogIn_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new WebAppLogin();
            await Navigation.PushAsync(new WebAppLogin());

        }

        private async void BtnManageAttendance_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new ManageAttendance();
            await Navigation.PushAsync(new ManageAttendance());
            
        }

        private void BtnShareCredentials_Clicked(object sender, EventArgs e)
        {
            
        }

		private async void PutUserinSessionState()
		{
			User user = new User();
			user = await UserRepository.GetAccountOwnerAsync();

			Application.Current.Properties["LoggedInUser"] = user;

			lblWelcome.Text = string.Format("Welcome {0}!", user.FirstName);
		}

	}
}