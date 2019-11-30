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
using ZXing.Net.Mobile.Forms;
using Newtonsoft.Json;
using QRCodeAuthMobile.Services;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{

        public Home()
		{
			InitializeComponent();
			PutUserinSessionState();
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
                    string message = "Information Collector : \n" + obj.informationCollector + "\n\nDepartment: \n" + obj.department + "\n\nRequesting Credentials:\n" + getRequestedCredentials(obj.requestedCredentials) + "\n\nWould you like to share your credentials?";

                    var decision = await DisplayAlert("Share Credentials", message, "Yes", "No");
                    if (decision)
                    {
                        //Code - send information to the information collector 
                        sendRequestedCredentials(obj.requestedCredentials);
                    }
                    else
                    {
                        await DisplayAlert("Cancelled", "You have chosen not to share your credentials. ", "ok");
                    }
                });
            };
        }

        public string getRequestedCredentials(List<CredentialType> types)
        {
            string str = "";
            foreach(CredentialType ct in types)
            {
                str += ct.ToString() + "\n";
            }
            return str;
        }

        public async void sendRequestedCredentials(List<CredentialType> types)
        {
            List<Credential> requestedCredentials = new List<Credential>();
            Credential cred = new Credential();

            foreach (CredentialType credentialType in types)
            {
                cred = await CredentialRepository.GetCredentialByType(credentialType);
                if (cred != null)
                {
                    requestedCredentials.Add(cred);
                    System.Diagnostics.Debug.WriteLine(cred + "\n");
                }            
            }

            if(requestedCredentials != null && requestedCredentials.Count == types.Count)
            {
                //await DataService.SendRequestedCredentials(requestedCredentials); 
                await DisplayAlert("Successful!!!", "Your credentials have been shared", "ok");
            }
            else
            {
                await DisplayAlert("Unsuccessful", "You do not hold one or more of the requested credentials. Therefore, unable to share credentials with information collector. ", "ok");
            }

        }

    }
}