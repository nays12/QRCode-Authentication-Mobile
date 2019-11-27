/*
 * Purpose: First screen after login that allows user to select which action they would like to take
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			List<User> userAccounts = new List<User>();
			userAccounts = await UserRepository.GetAllUsersAsync();

			accountUserId.ItemsSource = userAccounts; // bind picker to accounts found from database


		}

		private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
		{
			User u = new User();
			Picker picker = sender as Picker;
			var selectedItem = picker.SelectedItem; // This is the model selected in the picker

			//selectedId = selectedItem;
			u = (User)selectedItem;

			System.Diagnostics.Debug.WriteLine(u.UserId);

			PutUserinSessionState(u);
		}

		private async void BtnManagedCredentials_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new ManageCredentials();
            await Navigation.PushAsync(new ManageCredentials());
        }

		private async void BtnWebLogIn_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new WebAppLogin();
            await Navigation.PushAsync(new WebAppLogin());

        }

        private async void BtnManageAttendance_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new ManageAttendance();
            await Navigation.PushAsync(new ManageAttendance());
            
        }

        private void BtnShareCredentials_Clicked(object sender, EventArgs e)
        {
            
        }

		private void PutUserinSessionState(User u)
		{

			Application.Current.Properties["LoggedInUser"] = u;

			welcomeUser.Text = string.Format("Welcome {0}!", u.FirstName);
		}

	}
}