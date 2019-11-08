using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using QRCodeAuthMobile.Interfaces;
using QRCodeAuthMobile.Models;

namespace QRCodeAuthMobile
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void BtnFingerPrint_Clicked(object sender, EventArgs e)
		{
            //Check if Fingerprint ID is available on mobile device. 
			if (await CrossFingerprint.Current.IsAvailableAsync())
			{
                //If avaialbe authenticate by Fingerprint ID. 
                FingerprintAuthenticationResult result = await CrossFingerprint.Current.AuthenticateAsync("Provide fingerprint to sign in.");

                //If authentication is successful continue to next page. 
                if (result.Authenticated)
				{
					//If is authenticated successfully and has an account, navigate to the Select type page. 
					if (await App.UserRepo.GetUserbyId("test") != null) // I will add logic to get current user if they exist here
					{
						App.Current.MainPage = new Home();
					}
					else
					{
						App.Current.MainPage = new SelectType();
					}

					
				}
			}
            else
            {
                //If FingerprintID is NOT availabe on mobile device, display appropriate error message. 
                await DisplayAlert("Authentication Failed", "Fingerprint Authentication Failed", "OK");
            }
        }

		private void BtnFaceID_Clicked(object sender, EventArgs e)
		{
            //If face ID authentification is selected handle the action with platform specific code. 
            //See Android and IOS project implementation folders for code. 
			DependencyService.Get<IFaceAuth>().FaceAuthentication();
		}
	}
}