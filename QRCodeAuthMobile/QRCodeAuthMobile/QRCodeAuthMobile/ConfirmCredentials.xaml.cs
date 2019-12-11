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
using System.Linq;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmCredentials : ContentPage
	{
		protected List<CredentialType> credentialsNeeded = new List<CredentialType>();
		public ConfirmCredentials()
		{
			InitializeComponent();
		}

		private async void BtnComfirm_Clicked(object sender, EventArgs e)
		{
			scanQRCode();
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
				credentialsNeeded = obj.requestedCredentials;

					credentialTypes.ItemsSource = credentialsNeeded;

					//string message = obj.informationCollector + "from " + obj.department + " is requesting the the CredentialTypes: \n";
					//string message2 = cred0 + "\n" + cred1 + "\n" + cred2 + "\n" + cred3 + "\n" + cred4 + "\n" + cred5 + "\n" + cred6 + "\n" + cred7 + "\n" + cred8;

					var decision = false; // await DisplayAlert("Share Credentials", "test", "Yes", "No");
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

			if (requestedCredentials != null && requestedCredentials.Count > 0)
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