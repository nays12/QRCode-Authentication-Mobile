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
using QRCodeAuthMobile.Services;

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
            //List<User> users = await App.UserRepo.GetAllUsersAsync();
            //string userID = users[0].UserId;                                                      
            //string office = users[0].UserType == UserType.Student ? "Office of Admissions " : "Human Resorce Office ";

            string userID = "1344328";
            string office = UserType.Staff == UserType.Student ? "Office of Admissions " : "Human Resorce Office ";

            List<Credential> apiCredentials = new List<Credential>();                               // Create an empty list to store credentials fetch from web app.  
            apiCredentials = await DataService.GetCredentials(userID);                              //Save list fetched credentials from api into apiCredentials list. 

            if (apiCredentials.Any())                                                               // If list is NOT empty (api did NOT return an empty list) 
            {
                foreach (Credential c in apiCredentials)
                {
                    await App.CredentialRepo.AddCredentialAsync(c);
                }
                await DisplayAlert("Update Credentials", "Successful!!!" + "\n\n" + "To make changes to existing credentials, please visit the " + office + "to update credentials.", "OK");
            }
            else
            {
                await DisplayAlert("Update Credentials", "Unsuccessful!!!" + "\n\n" + "Please visit the " + office + "to set up credentials.", "OK");
            }

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