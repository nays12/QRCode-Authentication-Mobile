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

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public Home()
		{
			InitializeComponent();
		}

		private async void BtnUpdateCredentials_Clicked(object sender, EventArgs e)
		{
			User user = await App.UserRepo.GetUserbyId("123456");

			//if (user.group == "Student")
			//{
			//	await DisplayAlert("Update Credentials", "Please go to the Office of Admissions to update your credentials", "OK");
			//}
			//else
			//{
			//	await DisplayAlert("Update Credentials", "Please go to the Human Resources Office to update your credentials", "OK");
			//}


		}

		private void BtnWebLogIn_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new WebAppLogin();
        }

        private void BtnRecordAttendance_Clicked(object sender, EventArgs e)
        {
            
        }

        private void BtnShareCredentials_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}