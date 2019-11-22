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
			if (events.Count > 0)
			{
				foreach (Event ev in events)
				{
					System.Diagnostics.Debug.WriteLine("Event ID Number: " + ev.EventId);
					System.Diagnostics.Debug.WriteLine("Name: " + ev.Name);
					System.Diagnostics.Debug.WriteLine("Location: " + ev.Location);
					System.Diagnostics.Debug.WriteLine("Type: " + ev.EventType);
					System.Diagnostics.Debug.WriteLine("Start Time: " + ev.StartTime);
					System.Diagnostics.Debug.WriteLine("End Time: " + ev.EndTime);
					System.Diagnostics.Debug.WriteLine("Description: " + ev.Description);
					System.Diagnostics.Debug.WriteLine("Owner: " + ev.Owner);
					System.Diagnostics.Debug.WriteLine("-----------------------");
				};
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("This account has no Events.");
			}
		}

		public async void GetCredentialsButtonClicked(object sender, EventArgs args)
		{
			List<Credential> credentials = new List<Credential>();
			credentials = await CredentialRepository.GetAllCredentialsAsync();
			if (credentials.Count > 0)
			{
				foreach (Credential cred in credentials)
				{
					System.Diagnostics.Debug.WriteLine("Credential ID Number: " + cred.CredentialId);
					System.Diagnostics.Debug.WriteLine("Name: " + cred.Name);
					System.Diagnostics.Debug.WriteLine("Issue Date: " + cred.IssueDate);
					System.Diagnostics.Debug.WriteLine("Expiration Date: " + cred.ExpirationDate);
					System.Diagnostics.Debug.WriteLine("Value: " + cred.Value);
					System.Diagnostics.Debug.WriteLine("Is Value: " + cred.IsValid);
					System.Diagnostics.Debug.WriteLine("Owner: " + cred.Owner);
					System.Diagnostics.Debug.WriteLine("Issuer: " + cred.Issuer);
					System.Diagnostics.Debug.WriteLine("-----------------------");
				};
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("This account has no Credentials.");
			}
		}

		public async void GetUsersButtonClicked(object sender, EventArgs args)
		{
			List<User> users = new List<User>();
			users = await UserRepository.GetAllUsersAsync();

			if (users.Count > 0)
			{
				foreach (User u in users)
			{
				System.Diagnostics.Debug.WriteLine("User Id: " + u.UserId);
				System.Diagnostics.Debug.WriteLine("Last Name: " + u.LastName);
				System.Diagnostics.Debug.WriteLine("First Name: " + u.FirstName);
				System.Diagnostics.Debug.WriteLine("Type: " + u.UserType);
				System.Diagnostics.Debug.WriteLine("-----------------------");
			};
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("This User does not have a Mobile Account yet.");
			}
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