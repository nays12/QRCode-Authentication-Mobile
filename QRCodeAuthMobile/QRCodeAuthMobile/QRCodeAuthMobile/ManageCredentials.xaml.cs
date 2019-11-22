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
			InitializeComponent ();
			GetLoggedInUserInfo();
			displayCredentialList();
        }

        public async void displayCredentialList()
        {
			userCredentials = await CredentialRepository.GetAllCredentialsAsync();
			credentialList.ItemsSource = userCredentials;
		}

        private async void BtnSetUpCredentials_Clicked(object sender, EventArgs e)
        {
			var office = accountOwner.UserType == UserType.Student ? "Admissions Office" : "Human Resourses Office"; 
                                         
            await DisplayAlert("Set Up Credentials", "Please visit the " + office + " to set up your credentials.", "OK");

        }

        private async void BtnFetchCredentials_Clicked(object sender, EventArgs e)
        {
			List<Credential> fetchedCredentials = new List<Credential>();

			fetchedCredentials = await DataService.GetIssuedCredentials(); // Call API

			if (fetchedCredentials.Count > 0)
			{
				await DisplayAlert("New Credentials", "Sucess! Your new Credentials have been added to your account!", "OK");
			}
			else
			{
				await DisplayAlert("No New Credentials", "There were no Credentials found to add to your account.", "OK");
			}

			await CredentialRepository.AddMultipleCredentialsAsync(fetchedCredentials); // Add new Credentials to DB

			displayCredentialList(); // Update the credential list
		}

		public void GetLoggedInUserInfo()
		{
			accountOwner.UserId = Convert.ToString(Application.Current.Properties["UserId"]);
			accountOwner.LastName = Convert.ToString(Application.Current.Properties["LastName"]);
			accountOwner.FirstName = Convert.ToString(Application.Current.Properties["FirstName"]);
			accountOwner.UserType = (UserType)Convert.ToInt32(Application.Current.Properties["UserType"]);
		}

	}
}