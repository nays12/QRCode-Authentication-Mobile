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
	public partial class ViewCredentials : ContentPage
	{
		public ViewCredentials ()
		{
			InitializeComponent ();
            displayCredentialList();
        }

        public async void displayCredentialList()
        {
            //This is the CREDENTIAL code. Cannot implement yet. 
            //List<Credential> cred = await App.CredentialRepo.GetAllCredentialsAsync();
            //credentialList.ItemsSource = cred;


            //Used USER local records instead for testing. Will delete this code once credential class can be implemented. 
            List<User> users = await App.UserRepo.GetAllUsersAsync();
            credentialList.ItemsSource = users;
        }

		private async void CredentialList_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			// This is the CREDENTIAL code.Cannot implement yet.
			var cred = e.Item as Credential;
			string name = cred.Name;
			// string credentialType = "Name";
			string value = cred.Value;
			//string message = "ID : " + id + "\n" + "Name : " + name + "\n" + "Credential Type : " + credentialType + "\n" + "Vlue : " + value;
			//await DisplayAlert("Credential", message, "ok");



			//Used USER local records instead for testing. Will delete this code once credential class can be implemented. 
			var user = e.Item as User;

			//string id = user.userId;
			//string firstName = user.first_name;
			//string lastName = user.last_name;
			//string group = user.group;

			//string message = "ID : " + id + "\n" + "First Name : " + firstName + "\n" + "Last Name : " + lastName + "\n" + "Group : " + group;

			//await DisplayAlert("Credential", message, "ok");

		}
	}
}