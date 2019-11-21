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
	public partial class ManageCredentials : ContentPage
	{
        List<Credential> cList = new List<Credential>(); // TESTING -- DELETE LATER

		public ManageCredentials ()
		{
			InitializeComponent ();

            displayCredentialList();
        }

        public async void displayCredentialList()
        {
            generateList(); // TESTING -- DELETE LATER
            credentialList.ItemsSource = cList; // TESTING -- DELETE LATER

            //This is the CREDENTIAL code. Cannot implement yet. 
            //List<Credential> credentialList = await App.CredentialRepo.GetAllCredentialsAsync();
            //credentialList.ItemsSource = credentialList;

        }


        private async void BtnSetUpCredentials_Clicked(object sender, EventArgs e)
        {
            //TESTING - DELETE LATER 
            User user = new User();   //DELETE LATER 
            user.UserType = UserType.Student; //DELETE LATER 
            var office = user.UserType == UserType.Student ? "Admissions Office" : "Human Resourse Office"; //DELETE LATER 


            //User user = await App.UserRepo.GetUserbyIndex();                                                   //CORRECT CODE - CAN'T IMPLEMENT YET. 
            //var office = user.UserType == UserType.Student ? "Admissions Office" : "Human Resourse Office";   //CORRECT CODE - CAN'T IMPLEMENT YET. 
            await DisplayAlert("Set Up Credentials", "Please visit the " + office + " to set up your credentials.", "OK");

        }

        private void BtnFetchCredentials_Clicked(object sender, EventArgs e)
        {

        }



        private void generateList() // TESTING -- DELETE LATER
        {
            //TESTING - DELETE LATER 
            Credential cred1 = new Credential();
            cred1.CredentialId = 1;
            cred1.Name = "Student Full Name";
            cred1.CredentialType = CredentialType.Name;
            cred1.IssueDate = Convert.ToDateTime("01/15/2016");
            cred1.ExpirationDate = Convert.ToDateTime("12/21/2019");
            cred1.Value = "Naomi S. Wiggins";
            cred1.CredentialOwner = "1304693";
            cred1.CredentialIssuer = "Admission";
            cList.Add(cred1);

            Credential cred2 = new Credential();
            cred2.CredentialId = 2;
            cred2.Name = "Student Major";
            cred2.CredentialType = CredentialType.Major;
            cred2.IssueDate = Convert.ToDateTime("01/15/2016");
            cred2.ExpirationDate = Convert.ToDateTime("12/21/2019");
            cred2.Value = "Computer Science";
            cred2.CredentialOwner = "1304693";
            cred2.CredentialIssuer = "Admission";
            cList.Add(cred2);

            Credential cred3 = new Credential();
            cred3.CredentialId = 3;
            cred3.Name = "Student Email";
            cred3.CredentialType = CredentialType.Email;
            cred3.IssueDate = Convert.ToDateTime("01/15/2016");
            cred3.ExpirationDate = Convert.ToDateTime("12/21/2019");
            cred3.Value = "studentname@uhcl.edu";
            cred3.CredentialOwner = "1304693";
            cred3.CredentialIssuer = "Admission";
            cList.Add(cred3);

            Credential cred4 = new Credential();
            cred4.CredentialId = 4;
            cred4.Name = "Student ID#";
            cred4.CredentialType = CredentialType.IdNumber;
            cred4.IssueDate = Convert.ToDateTime("01/15/2016");
            cred4.ExpirationDate = Convert.ToDateTime("12/21/2019");
            cred4.Value = "1304693";
            cred4.CredentialOwner = "1304693";
            cred4.CredentialIssuer = "Admission";
            cList.Add(cred4);
        }

    }
}