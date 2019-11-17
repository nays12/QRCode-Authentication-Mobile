/*
 * Purpose: Test page that allows the developers to test operations on the database as well 
 * review query syntax.
 * 
 * Algorithm: 
 * OnNewButtonClicked - create new object and save it to database
 * OnGetButtonClicked - get objects from the database
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCodeAuthMobile.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DatabaseTest : ContentPage
	{
		public DatabaseTest()
		{
			InitializeComponent();
		}


		public void OnDeleteButtonClicked(object sender, EventArgs args)
		{
			//string userId = deleteUser.Text;

			//App.UserRepo.DeleteUserbyId(userId);
			//statusMessage.Text = App.UserRepo.StatusMessage;
		}

		public async void OnNewButtonClicked(object sender, EventArgs args)
		{
			statusMessage.Text = "";

			Credential cred1 = new Credential
			{
				Name = "Email",
				CredentialType = CredentialType.Email,
				Issuer = null,
				Owner = null,
				IssueDate = Convert.ToDateTime("01/15/2016"),
				ExpirationDate = Convert.ToDateTime("12/21/2019"),
				Value = "WigginsN7499@uhcl.edu",
				IsValid = true
			};

			await App.CredentialRepo.AddCredentialAsync(cred1);
			statusMessage.Text = App.CredentialRepo.StatusMessage;
		}

		public async void OnGetButtonClicked(object sender, EventArgs args)
		{
			statusMessage.Text = "";

			//List<Credential> credentials = await App.CredentialRepo.GetAllCredentialsAsync();
			//credentialsList.ItemsSource = credentials;

			List<User> users = await App.UserRepo.GetAllUsersAsync();
			credentialsList.ItemsSource = users;

			statusMessage.Text = App.UserRepo.StatusMessage;
			RowCount.Text = App.UserRepo.GetRowCount().ToString();
		}


	}
}