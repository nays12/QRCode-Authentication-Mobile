/*
 * Purpose: Displays details about the Information Collector the user shared their credentials with.
 */

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QRCodeAuthMobile.Models;
using System.Collections.Generic;
using System;
using ZXing.Net.Mobile.Forms;
using Newtonsoft.Json;
using QRCodeAuthMobile.Services;
using QRCodeAuthMobile.Data;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmCredentials : ContentPage
	{
		protected List<CredentialType> credentialsNeeded = new List<CredentialType>();
		protected string nameIC;
		protected string departmentIC;
		public ConfirmCredentials()
		{
			InitializeComponent();
			scanQRCode();
		}

		private async void scanQRCode()
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
					credentialsNeeded = obj.requestedCredentials;
					nameIC = obj.informationCollector;
					departmentIC = obj.department;

					lblName.Text = "Infomration Collector: " + obj.informationCollector;
					lblDepartment.Text = "Department: " + obj.department;

					credentialTypes.ItemsSource = credentialsNeeded;
				});
			};
		}

		private async void BtnComfirm_Clicked(object sender, EventArgs e)
		{
			string message = "Are you sure you want to send your Credentials to " + nameIC + " from the " + departmentIC + "?";
			var decision = await DisplayAlert("Confirm Credentials Share", message, "Yes", "No");
			if (decision)
			{
				// send information to the information collector 
				SendRequestedCredentials(credentialsNeeded);
			}
			else
			{
				await DisplayAlert("Cancelled", "You have chosen not to share your Credentials. ", "ok");
			}			
		}

		public async void SendRequestedCredentials(List<CredentialType> types)
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

			if (requestedCredentials != null && requestedCredentials.Count > 0)
			{
				await DataService.SendRequestedCredentials(requestedCredentials);
				await DisplayAlert("Success!", "Your Credentials have been shared!", "OK");
			}
			else
			{
				await DisplayAlert("Failure", "You do not hold one or more of the requested Credentials. Visit your Credential Authority to add more Credentials to your Mobile Account.", "OK");
			}
		}


	}
}