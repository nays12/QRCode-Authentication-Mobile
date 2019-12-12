/*
 * Purpose: First screen after login that allows user to select which action they would like to take
 * 
 * Contributors: 
 * Marilin Ortuno
 * Naomi Wiggins
 * 
 */

using System;
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
			await Navigation.PushAsync(new ManageCredentials());
			//Application.Current.MainPage = new NavigationPage(new ManageCredentials());
		}

		private async void BtnWebLogIn_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new WebAppLogin());
			//Application.Current.MainPage = new NavigationPage(new WebAppLogin());
		}

        private async void BtnManageAttendance_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new ManageAttendance());
			//Application.Current.MainPage = new NavigationPage(new ManageAttendance());
		}

        private async void BtnShareCredentials_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new ConfirmCredentials());
        }

        private async void PutUserinSessionState()
        {
            User user = await UserRepository.GetAccountOwnerAsync();

            Application.Current.Properties["LoggedInUser"] = user;

            lblWelcome.Text = string.Format("Welcome {0}!", user.FirstName);
        }
    }
}