/*
 * Purpose: First screen after login that allows user to select which action they would like to take
 */

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QRCodeAuthMobile.Models;
using QRCodeAuthMobile.Data;
using ZXing.Net.Mobile.Forms;
using Newtonsoft.Json;
using QRCodeAuthMobile.Services;

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
			lblOptions.Text = "Please select the Account you would like to Login with.";

			List<User> userAccounts = new List<User>();
			userAccounts = await UserRepository.GetAllUsersAsync();

			accountUserId.ItemsSource = userAccounts; // bind picker to accounts found from database		

		}

		private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
		{
			// Get account from picker
			User u = new User();
			Picker picker = sender as Picker;
			var selectedItem = picker.SelectedItem; 
			u = (User)selectedItem;

			PutUserinSessionState(u);
			SetUserPage(u);
		}

		private async void BtnManagedCredentials_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ManageCredentials());
          Application.Current.MainPage = new NavigationPage(new ManageCredentials());
        }

		private async void BtnWebLogIn_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new WebAppLogin());
          Application.Current.MainPage = new NavigationPage(new WebAppLogin());
        }

        private async void BtnManageAttendance_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ManageAttendance());
			      Application.Current.MainPage = new NavigationPage(new ManageAttendance());
		    }

        private void BtnShareCredentials_Clicked(object sender, EventArgs e)
        {
            scanQRCode();
        }

        private async void PutUserinSessionState()
        {
            User user = await UserRepository.GetAccountOwnerAsync();

            Application.Current.Properties["LoggedInUser"] = user;

            lblWelcome.Text = string.Format("Welcome {0}!", user.FirstName);
        }

        public async void scanQRCode()
        {
            var defenition = new
            {
                informationCollector = "",
                department = "",
                requestedCredentials = new List<CredentialType>() { },
            };

            //Create a scan page. 
            var scanPage = new ZXingScannerPage();
            scanPage.DefaultOverlayShowFlashButton = true;
            scanPage.HasTorch = true;

            // Navigate to scan page
            await Navigation.PushModalAsync(scanPage);

            //Event Handler
            scanPage.OnScanResult += (result) =>
            {
                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Stop scanning and dimiss the modal page
                    scanPage.IsScanning = false;
                    await Navigation.PopModalAsync();

                    //Save the scanned anonymous object into the obj variable. 
                    var obj = JsonConvert.DeserializeAnonymousType(result.ToString(), defenition);
                    string message = "Information Collector : \n" + obj.informationCollector + "\n\nDepartment: \n" + obj.department + "\n\nRequesting Credentials:\n" + obj.requestedCredentials + "\n\nWould you like to share your credentials?";

                    var decision = await DisplayAlert("Share Credentials", message, "Yes", "No");
                    if (decision)
                    {
                        // send information to the information collector 
                        sendRequestedCredentials(obj.requestedCredentials);
                    }
                    else
                    {
                        await DisplayAlert("Cancelled", "You have chosen not to share your credentials. ", "ok");
                    }
                });
            };
        }

		private async void PutUserinSessionState(User u)
		{
			User user = await UserRepository.GetAccountOwnerAsync();

			Application.Current.Properties["LoggedInUser"] = u;
			lblWelcome.Text = string.Format("Welcome, {0}!", u.FirstName);
			lblOptions.Text = "What would you like to do?";
		}

		private void SetUserPage(User u)
		{
			// hide picker
			accountUserId.IsVisible = false;

			lblWelcome.Text = string.Format("Welcome {0}!", u.FirstName);
			// show buttons
			btnManageCredentials.IsVisible = true;
			btnShareCredentials.IsVisible = true;
			btnManageAttendance.IsVisible = true;
			btnWebLogin.IsVisible = true;
		}
        public async void sendRequestedCredentials(List<CredentialType> types)
        {
            List<Credential> requestedCredentials = new List<Credential>();

            foreach (CredentialType credentialType in types)
            {
                Credential cred = await CredentialRepository.GetCredentialByType(credentialType);
                if (cred != null)
                {
                    requestedCredentials.Add(cred);
                    System.Diagnostics.Debug.WriteLine(cred.CredentialType);
                }            
            }

            if(requestedCredentials != null && requestedCredentials.Count > 0)
            {
                await DataService.SendRequestedCredentials(requestedCredentials); 
                await DisplayAlert("Successful!!!", "Your credentials have been shared", "ok");
            }
            else
            {
                await DisplayAlert("Unsuccessful", "You do not hold one or more of the requested credentials. Therefore, unable to share credentials with information collector. ", "ok");
            }
        }

    }
}