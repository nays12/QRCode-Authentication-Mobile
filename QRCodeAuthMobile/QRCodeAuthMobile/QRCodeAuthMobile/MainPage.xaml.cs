/*
 * Purpose: Authenticates User and sends them to the appropiate screen once authenticated
 * 
 * Contributors: 
 * Marilin Ortuno
 * Naomi Wiggins
 */

using System;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using QRCodeAuthMobile.Interfaces;
using QRCodeAuthMobile.Data;

namespace QRCodeAuthMobile
{

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

					int count = await UserRepository.GetRowCount();


					if (count > 0) // If record count for User table is > 0 then an account exist
					{
						await Navigation.PushAsync(new Home());
					}
					else // If the record count is 0 then no User account exist
					{
						await Navigation.PushAsync(new SelectType());
					}
				}
			}
			else
			{
				// If FingerprintID is NOT availabe on mobile device, display appropriate error message. 
				await DisplayAlert("Authentication Not Possible", "Fingerprint authentication is not available on your device", "OK");
			}
		}

		private void BtnFaceID_Clicked(object sender, EventArgs e)
		{
			// If face ID authentification is selected handle the action with platform specific code. 
			// See Android and iOS project implementation folders for code. 
			DependencyService.Get<IFaceAuth>().FaceAuthentication();
		}

	}
}