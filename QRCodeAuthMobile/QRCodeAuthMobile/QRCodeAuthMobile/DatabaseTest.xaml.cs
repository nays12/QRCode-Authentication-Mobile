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
using QRCodeAuthMobile.Data;

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

		public async void GetEventsButtonClicked(object sender, EventArgs args)
		{
			List<Event> events = new List<Event>();
			events = await EventRepository.GetAllEventsAsync();
			System.Diagnostics.Debug.WriteLine(events);
		}

		public async void GetCredentialsButtonClicked(object sender, EventArgs args)
		{
			List<Credential> credentials = new List<Credential>();
			credentials = await CredentialRepository.GetAllCredentialsAsync();
			System.Diagnostics.Debug.WriteLine(credentials);
		}

		public async void GetUserssButtonClicked(object sender, EventArgs args)
		{
			List<User> users = new List<User>();
			users = await UserRepository.GetAllUsersAsync();
			System.Diagnostics.Debug.WriteLine(users);
		}

		//public void OnDeleteButtonClicked(object sender, EventArgs args)
		//{
		//	string userId = deleteUser.Text;

		//	App.UserRepo.DeleteUserbyId(userId);
		//	statusMessage.Text = App.UserRepo.StatusMessage;
		//}

		//public async void OnGetButtonClicked(object sender, EventArgs args)
		//{
		//	statusMessage.Text = "";

		//	//List<Credential> credentials = await App.CredentialRepo.GetAllCredentialsAsync();
		//	//credentialsList.ItemsSource = credentials;

		//	List<User> users = await App.UserRepo.GetAllUsersAsync();
		//	credentialsList.ItemsSource = users;

		//	statusMessage.Text = App.UserRepo.StatusMessage;
		//	RowCount.Text = App.UserRepo.GetRowCount().ToString();
		//}


	}
}