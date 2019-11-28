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
			if (userCredentials != null && userCredentials.Count > 0)
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

        private void BtnUpdateCredentials_Clicked(object sender, EventArgs e)
        {
			if (userCredentials != null && userCredentials.Count > 0)
			{
				DeletedCredentialsCheck();
				UpdatedCredentialsCheck();
				NewCredentialsCheck();
				displayCredentialList();
			}
			else
			{
				displayCredentialList();
			}
		}

		public void GetLoggedInUserInfo()
		{
			accountOwner = (User)(Application.Current.Properties["LoggedInUser"]);
		}

		private async void NewCredentialsCheck()
		{
			List<Credential> newCredentials = new List<Credential>();
			newCredentials = await DataService.GetIssuedCredentials(); // Call API

			if (newCredentials != null && newCredentials.Count > 0)
			{
				await CredentialRepository.AddNewCredentialsAsync(newCredentials); 
				await DisplayAlert("New Credentials", "Success! Your new Credentials have been added to your account! Please restart the app to view them.", "OK");
			}
			else
			{
				System.Diagnostics.Debug.WriteLine(CredentialRepository.StatusMessage);
			}		
		}

		private async void UpdatedCredentialsCheck()
		{
			List<Credential> updatedCredentials = new List<Credential>();
			updatedCredentials = await DataService.GetUpdatedCredentials(); // Call API

			if (updatedCredentials != null && updatedCredentials.Count > 0)
			{
				await CredentialRepository.UpdateCredentialsAsync(updatedCredentials); 
				await DisplayAlert("Updated Credentials", "Success! Your Credentials have been updated! Please restart the app to see the new values.", "OK");
			}
			else
			{
				System.Diagnostics.Debug.WriteLine(CredentialRepository.StatusMessage);
			}			
		}

		private async void DeletedCredentialsCheck()
		{
			int deletedCredentialId = await DataService.GetCredentialIdToDelete(); // Call API
			if (deletedCredentialId == 0)
			{
				System.Diagnostics.Debug.WriteLine(CredentialRepository.StatusMessage);
			}
			else
			{
				await CredentialRepository.DeleteCredentialById(deletedCredentialId);
				await DisplayAlert("Deleted Credential", CredentialRepository.StatusMessage + "Please restart to application to remove the Credential from your list.", "OK");
			}
		}


	}
}