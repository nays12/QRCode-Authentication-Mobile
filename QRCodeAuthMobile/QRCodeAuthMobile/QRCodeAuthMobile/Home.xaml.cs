/*
 * Purpose: First screen after login that allows user to select which action they would like to take
 */

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QRCodeAuthMobile.Models;
using QRCodeAuthMobile.Data;

namespace QRCodeAuthMobile
{
	
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		protected string selectedId;
		public Home()
		{
			InitializeComponent();
			GetUserAccounts();
		}

		private async void GetUserAccounts()
		{
			lblOptions.Text = "Please select the Account you would like to Login with";

			List<User> userAccounts = new List<User>();
			userAccounts = await UserRepository.GetAllUsersAsync();

			if (userAccounts.Count > 1)
			{
				accountUserId.ItemsSource = userAccounts; // bind picker to accounts found from database
			}
			else
			{
				SetUserPage();
			}

		}

		private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
		{
			// Get account from picker
			User u = new User();
			Picker picker = sender as Picker;
			var selectedItem = picker.SelectedItem; 
			u = (User)selectedItem;

			PutUserinSessionState(u);
			SetUserPage();
		}

		private async void BtnManagedCredentials_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageCredentials());
        }

		private async void BtnWebLogIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WebAppLogin());
        }

        private async void BtnManageAttendance_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManageAttendance());          
        }

        private void BtnShareCredentials_Clicked(object sender, EventArgs e)
        {
            
        }

		private void PutUserinSessionState(User u)
		{
			Application.Current.Properties["LoggedInUser"] = u;
			lblGreeting.Text = string.Format("Welcome, {0}!", u.FirstName);
			lblOptions.Text = "What would you like to do?";
		}

		private void SetUserPage()
		{
			// hide picker
			accountUserId.IsVisible = false;

			// show buttons
			btnManageCredentials.IsVisible = true;
			btnShareCredentials.IsVisible = true;
			btnManageAttendance.IsVisible = true;
			btnWebLogin.IsVisible = true;
		}

	}
}