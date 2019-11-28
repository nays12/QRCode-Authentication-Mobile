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
		public static string StatusMessage { get; set; }
		public DatabaseTest()
		{
			InitializeComponent();
		}

		public async void GetEventsButtonClicked(object sender, EventArgs args)
		{
			List<Event> events = new List<Event>();
			events = await EventRepository.GetAllEventsAsync();
			if (events != null && events.Count > 0)
			{
				try
				{
					foreach (Event ev in events)
					{
						System.Diagnostics.Debug.WriteLine("-----------------------");
						System.Diagnostics.Debug.WriteLine("Event ID Number: " + ev.EventId);
						System.Diagnostics.Debug.WriteLine("Name: " + ev.Name);
						System.Diagnostics.Debug.WriteLine("Location: " + ev.Location);
						System.Diagnostics.Debug.WriteLine("Type: " + ev.EventType);
						System.Diagnostics.Debug.WriteLine("Start Time: " + ev.StartTime);
						System.Diagnostics.Debug.WriteLine("End Time: " + ev.EndTime);
						System.Diagnostics.Debug.WriteLine("Description: " + ev.Description);
						System.Diagnostics.Debug.WriteLine("Owner: " + ev.Owner);
						System.Diagnostics.Debug.WriteLine("-----------------------");

						StatusMessage = string.Format("Success! Found Event {0}.", ev.Name);
						System.Diagnostics.Debug.WriteLine(StatusMessage);
					};
				}
				catch (Exception ex)
				{
					StatusMessage = string.Format("Failure. Could not find any Events. Error: {0}.", ex.Message);
					System.Diagnostics.Debug.WriteLine(StatusMessage);
				}			
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
			if (credentials != null && credentials.Count > 0)
			{
				try
				{
					foreach (Credential cred in credentials)
					{
					System.Diagnostics.Debug.WriteLine("-----------------------");
					System.Diagnostics.Debug.WriteLine("Credential ID Number: " + cred.CredentialId);
					System.Diagnostics.Debug.WriteLine("Name: " + cred.Name);
					System.Diagnostics.Debug.WriteLine("Issue Date: " + cred.IssueDate);
					System.Diagnostics.Debug.WriteLine("Expiration Date: " + cred.ExpirationDate);
					System.Diagnostics.Debug.WriteLine("Value: " + cred.Value);
					System.Diagnostics.Debug.WriteLine("Is Value: " + cred.IsValid);
					System.Diagnostics.Debug.WriteLine("Owner: " + cred.Owner);
					System.Diagnostics.Debug.WriteLine("Issuer: " + cred.Issuer);
					System.Diagnostics.Debug.WriteLine("-----------------------");

					StatusMessage = string.Format("Success! Found Credential {0}.", cred.Name);
					System.Diagnostics.Debug.WriteLine(StatusMessage);
					};
				}
				catch (Exception ex)
				{
					StatusMessage = string.Format("Failure. Could not find any Credentials. Error: {0}.", ex.Message);
					System.Diagnostics.Debug.WriteLine(StatusMessage);
				}
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
			if (users != null && users.Count > 0)
			{

			{
				try
				{
					foreach (User u in users)
					{
						System.Diagnostics.Debug.WriteLine("-----------------------");
						System.Diagnostics.Debug.WriteLine("User Id: " + u.UserId);
						System.Diagnostics.Debug.WriteLine("Last Name: " + u.LastName);
						System.Diagnostics.Debug.WriteLine("First Name: " + u.FirstName);
						System.Diagnostics.Debug.WriteLine("Type: " + u.UserType);
						System.Diagnostics.Debug.WriteLine("-----------------------");

						StatusMessage = string.Format("Success! Found User with Id {0}.", u.UserId);
						System.Diagnostics.Debug.WriteLine(StatusMessage);
					};
				}
				catch (Exception ex)
				{
					StatusMessage = string.Format("Failure. Could not find any Users. Error: {0}.", ex.Message);
					System.Diagnostics.Debug.WriteLine(StatusMessage);
				}
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("This User does not have a Mobile Account yet.");
			}
		}


	}
}