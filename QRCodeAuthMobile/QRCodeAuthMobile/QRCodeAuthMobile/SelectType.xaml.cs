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

			User mobileUser = new User
			{
				UserId = schoolId.Text,
				LastName = lastName.Text,
				FirstName = firstName.Text,
				UserType = (UserType)userType.SelectedIndex
			};

			await UserRepository.AddUserAsync(mobileUser);
			statusMessage.Text = UserRepository.StatusMessage;

			// add logic to display popup about credential authority
			if (mobileUser.UserType == UserType.Student)
			{
				await DisplayAlert("Complete Setup", "Please go to the Office of Admissions to add credentials to your account", "OK");
			}
			else
			{
				await DisplayAlert("Complete Setup", "Please go to the Human Resources Office to add credentials to your account", "OK");				
			}
			AddTestData();
		}

		public  async void AddTestData() // Add test data to get user familiar with system 
		{

			// Add Test Credential to User Account
			Credential testCredential = new Credential
			{
				CredentialId = 1,
				Name = "First Name",
				CredentialType = CredentialType.Name,
				IssueDate = DateTime.UtcNow,
				ExpirationDate = DateTime.UtcNow,
				Value = firstName.Text,
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
				Description = firstName.Text + "'s meeting with the Credential Authority to Add Credentials",
				Owner = schoolId.Text
			};
			await EventRepository.AddEventAsync(testEvent);
		}
	
	}
}