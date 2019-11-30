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



        private async void BtnShareCredentials_Clicked(object sender, EventArgs e)
        {
            var defenition = new
            {
                informationCollector = "",
                department = "",
                requestedCredentials = new[] { "", "", "", "", "", "", "" }
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
                    string message = "Information Collector : \n" + obj.informationCollector + "\n\nDepartment: \n" + obj.department + "\n\nRequesting Credentials:\n" + getListOfRequestedCredentials(obj.requestedCredentials) + "\n\nWould you like to share your credentials?";

                    var decision = await DisplayAlert("Share Credentials", message, "Yes", "No");
                    if (decision)
                    {
                        //Code - send information to the information collector 

                        await DisplayAlert("Successful!!!", "Your credentials have been shared with " + obj.informationCollector, "ok");
                    }
                    else
                    {
                        await DisplayAlert("Cancelled", "You have chosen not to share your credentials. ", "ok");
                    }
                });
            };

            ////TESTING - DELETE LATER 
            //var obj = JsonConvert.DeserializeAnonymousType(getCredentialRequest(), defenition);
            //string message = "Information Collector: \n" + obj.informationCollector + "\n\nDepartment: \n" + obj.department + "\n\nRequesting Credentials:\n" + getListOfRequestedCredentials(obj.requestedCredentials) + "\n\nWould you like to share your credentials?";

            //var decision = await DisplayAlert("Share Credentials", message, "Yes", "No");
            //if (decision)
            //{
            //    await DisplayAlert("Successful!!!", "Your credentials have been shared with " + obj.informationCollector, "ok");
            //}
            //else
            //{
            //    await DisplayAlert("Cancelled", "You have chosen not to share your credentials. ", "ok");
            //}


        }

        private async void PutUserinSessionState()
		{
			User user = await UserRepository.GetAccountOwnerAsync();

			Application.Current.Properties["LoggedInUser"] = user;

			lblWelcome.Text = string.Format("Welcome {0}!", user.FirstName);
		}


        public string getListOfRequestedCredentials(string[] a)
        {
            string str = "";
            for(int i = 0; i < a.Length; i++)
            {
                if(a[i] != "")
                {
                    str += a[i] + "\n";
                }
            }
            return str;

        }

        //Testing - DELETE LATER (This will be in the web app code) 
        public string getCredentialRequest()
        {
            var credentialRequest = new
            {
                informationCollector = "Alredo Davila",
                department = "College of Science and Engineering",
                requestedCredentials = new[] { "Name", "Major", "Email", "", "", "", "" }
            };

            var result = JsonConvert.SerializeObject(credentialRequest);

            return result;
        }

    }
}