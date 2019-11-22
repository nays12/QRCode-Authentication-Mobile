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
			//generateList();
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
			await DisplayAlert("New Credentials", "Sucess! Your new Credentials have been added to your account!", "OK");

			await CredentialRepository.AddMultipleCredentialsAsync(fetchedCredentials); // Add new Credentials to DB

			displayCredentialList(); // Update the credential list
		}

		public void GetLoggedInUserInfo()
		{
			accountOwner.UserId = Convert.ToString(Application.Current.Properties["UserId"]);
			accountOwner.LastName = Convert.ToString(Application.Current.Properties["LastName"]);
			accountOwner.FirstName = Convert.ToString(Application.Current.Properties["FirstName"]);
			accountOwner.UserType = (UserType)Convert.ToInt32(Application.Current.Properties["UserType"]);

			System.Diagnostics.Debug.WriteLine(accountOwner);
			System.Diagnostics.Debug.WriteLine(accountOwner.LastName);
			System.Diagnostics.Debug.WriteLine(accountOwner.FirstName);
			System.Diagnostics.Debug.WriteLine(accountOwner.UserType);
			System.Diagnostics.Debug.WriteLine(accountOwner.UserId);
		}

		//     private void generateList() // TESTING -- DELETE LATER
		//     {
		//         //TESTING - DELETE LATER 
		//         Credential cred1 = new Credential();
		//         cred1.CredentialId = 1;
		//         cred1.Name = "Student Full Name";
		//         cred1.CredentialType = CredentialType.Name;
		//         cred1.IssueDate = Convert.ToDateTime("01/15/2016");
		//         cred1.ExpirationDate = Convert.ToDateTime("12/21/2019");
		//         cred1.Value = "Naomi S. Wiggins";
		//         cred1.Owner = "1304693";
		//         cred1.Issuer = "Admission";
		//userCredentials.Add(cred1);

		//         Credential cred2 = new Credential();
		//         cred2.CredentialId = 2;
		//         cred2.Name = "Student Major";
		//         cred2.CredentialType = CredentialType.Major;
		//         cred2.IssueDate = Convert.ToDateTime("01/15/2016");
		//         cred2.ExpirationDate = Convert.ToDateTime("12/21/2019");
		//         cred2.Value = "Computer Science";
		//         cred2.Owner = "1304693";
		//         cred2.Issuer = "Admission";
		//userCredentials.Add(cred2);

		//         Credential cred3 = new Credential();
		//         cred3.CredentialId = 3;
		//         cred3.Name = "Student Email";
		//         cred3.CredentialType = CredentialType.Email;
		//         cred3.IssueDate = Convert.ToDateTime("01/15/2016");
		//         cred3.ExpirationDate = Convert.ToDateTime("12/21/2019");
		//         cred3.Value = "studentname@uhcl.edu";
		//         cred3.Owner = "1304693";
		//         cred3.Issuer = "Admission";
		//userCredentials.Add(cred3);

		//         Credential cred4 = new Credential();
		//         cred4.CredentialId = 4;
		//         cred4.Name = "Student ID#";
		//         cred4.CredentialType = CredentialType.IdNumber;
		//         cred4.IssueDate = Convert.ToDateTime("01/15/2016");
		//         cred4.ExpirationDate = Convert.ToDateTime("12/21/2019");
		//         cred4.Value = "1304693";
		//         cred4.Owner = "1304693";
		//         cred4.Issuer = "Admission";
		//userCredentials.Add(cred4);
		//     }

	}
}