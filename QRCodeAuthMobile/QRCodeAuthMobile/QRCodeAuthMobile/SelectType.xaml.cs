/*
 * Purpose: Gathers information from user to create a mobile account for them 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCodeAuthMobile.Models;
using QRCodeAuthMobile.Data;
using QRCodeAuthMobile.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectType : ContentPage
	{
		public SelectType()
		{
			InitializeComponent();
		}

		public async void SubmitButtonClicked(object sender, EventArgs args)
		{
			statusMessage.Text = "";
			User systemUser = new User();

			systemUser = await DataService.GetUserAccount(schoolId.Text);

			if (systemUser != null)
			{
				string userComfirmMsg = string.Format("Are you {0} {1}?", systemUser.FirstName, systemUser.LastName);

				bool answer = await DisplayAlert("Account Found.", userComfirmMsg, "Yes", "No");

				if (answer)
				{
					// Add user information to their local database
					await UserRepository.AddUserAsync(systemUser);
					System.Diagnostics.Debug.WriteLine(UserRepository.StatusMessage);

					// Create a Mobile Account for the User
					MobileAccount account = new MobileAccount()
					{
						MobileId = systemUser.UserId,
						IsActive = true
					};
					await MobileAccountRepository.AddAccountAsync(account);
					await DisplayAlert("Success", "Your Mobile Token Account has been activated.", "OK");

					AddTestData(systemUser);
				}
				else
				{
					statusMessage.Text = "Please see a UHCL Credential Authority for further assitance.";
				}
			}
			else
			{
				await DisplayAlert("Account Not Found", "Sorry, we could not find your information in the school system. You cannot make a Mobile Account as this time.", "OK");
				statusMessage.Text = "Please see a UHCL Credential Authority for further assitance";
			}

			// add logic to display information about credential authority
			if (systemUser.UserType == UserType.Student)
			{
				statusMessage.Text = "Please go to the Office of Admissions to add credentials to your account";
			}
			else
			{
				statusMessage.Text = "Please go to the Human Resources Office to add credentials to your account";				
			}
		
		}

		public async void AddTestData(User u) // Add test data to get user familiar with system 
		{

			// Add Test Credential to User Account
			Credential testCredential = new Credential
			{
				CredentialId = 1,
				Name = "First Name",
				CredentialType = CredentialType.Name,
				IssueDate = DateTime.UtcNow,
				ExpirationDate = DateTime.UtcNow,
				Value = u.FirstName,
				IsValid = false,
				Owner = schoolId.Text,
				Issuer = "TEST_CA"
			};
			await CredentialRepository.AddCredentialAsync(testCredential);

			// Add test Event to User Account
			Event testEvent = new Event
			{
				EventId = 1,
				Name = "Test Event",
				Location = "Credential Authority Office",
				EventType = EventType.Meeting,
				StartTime = DateTime.UtcNow,
				EndTime = DateTime.UtcNow,
				Description = u.FirstName + "'s meeting with the Credential Authority to Add Credentials",
				Owner = schoolId.Text
			};
			await EventRepository.AddEventAsync(testEvent);

            await Navigation.PushAsync(new Home());
        }
	
	}
}