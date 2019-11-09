/*
 * Purpose: Authenticates User and sends them to the appropiate screen once authenticated
 * 
 * Algorithm:
 * 
 */

using System;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using QRCodeAuthMobile.Interfaces;

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
                    //If is authenticated successfully navigate to the Select type page. 
					App.Current.MainPage = new Home();
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
            // See Android and iOS project implementation folders for code. 
			DependencyService.Get<IFaceAuth>().FaceAuthentication();
		}
	}
}