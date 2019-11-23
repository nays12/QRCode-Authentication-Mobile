using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QRCodeAuthMobile.Models;
using QRCodeAuthMobile.Data;
using QRCodeAuthMobile.Services;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManageCredentials : ContentPage
	{
        protected List<Credential> userCredentials = new List<Credential>();
		protected User accountOwner = new User();

		public ManageCredentials ()
		{
			InitializeComponent();
			GetLoggedInUserInfo();
			displayCredentialList();
        }

        public async void displayCredentialList()
        {
			userCredentials = await CredentialRepository.GetAllCredentialsAsync();
			if (userCredentials.Count > 0)
			{
				credentialList.ItemsSource = userCredentials;
			}
			else
			{
				await DisplayAlert("No Credentials Found", "Looks like you don't have any Credential yet. Visit your Credential Authority to add some!", "OK");
			}
		}

        private async void BtnSetUpCredentials_Clicked(object sender, EventArgs e)
        {
			var office = accountOwner.UserType == UserType.Student ? "Admissions Office" : "Human Resourses Office"; 
                                         
            await DisplayAlert("Set Up Credentials", "Please visit the " + office + " to set up your credentials.", "OK");

        }

        private void BtnFetchCredentials_Clicked(object sender, EventArgs e)
        {
			if (userCredentials.Count > 0)
			{
				
				UpdatedCredentialsCheck();
				displayCredentialList(); // Update the credential list
			}
			else
			{
				NewCredentialsCheck();
			}
		}

		public void GetLoggedInUserInfo()
		{
			accountOwner.UserId = Convert.ToString(Application.Current.Properties["UserId"]);
			accountOwner.LastName = Convert.ToString(Application.Current.Properties["LastName"]);
			accountOwner.FirstName = Convert.ToString(Application.Current.Properties["FirstName"]);
			accountOwner.UserType = (UserType)Convert.ToInt32(Application.Current.Properties["UserType"]);
		}

		private async void NewCredentialsCheck()
		{
			List<Credential> newCredentials = new List<Credential>();
			newCredentials = await DataService.GetIssuedCredentials(); // Call API

			if (newCredentials.Count > 0)
			{
				await CredentialRepository.AddNewCredentialsAsync(newCredentials); 
				await DisplayAlert("New Credentials", "Sucess! Your new Credentials have been added to your account!", "OK");
			}
			else
			{
				await DisplayAlert("No New Credentials", "There were no Credentials found to add to your account.", "OK");
			}		
		}

		private async void UpdatedCredentialsCheck()
		{
			List<Credential> updatedCredentials = new List<Credential>();
			updatedCredentials = await DataService.GetUpdatedCredentials(); // Call API

			if (updatedCredentials.Count > 0)
			{
				await CredentialRepository.UpdateCredentialsAsync(updatedCredentials); 
				await DisplayAlert("Updated Credentials", "Sucess! Your updated Credentials have been added to your account!", "OK");
			}
			else
			{
				await DisplayAlert("No New Updates", "Looks like none of your credentials were updated.", "OK");
			}			
		}


	}
}