/*
 * Purpose: Authenticates User and sends them to the appropiate screen once authenticated
 * 
 * Algorithm:
 * Check if fingerprint capability is available for device
 * Authenticate user via fingerprint
 * If user has a mobile account, direct them home page, else direct them to create account
 * Authenticate user via faceId
 */

using System;
using System.ComponentModel;
using Xamarin.Forms;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using QRCodeAuthMobile.Interfaces;
using QRCodeAuthMobile.Models;
using QRCodeAuthMobile.Data;
using SQLite;
using System.Collections.Generic;

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
					List<User> userAccounts = new List<User>();
					userAccounts = await UserRepository.GetAllUsersAsync();

					if (userAccounts.Count > 0) // If record count for User table is > 0 then an account exist
					{
						bool answer = await DisplayAlert("Account Found.", "Would you like to make another account?", "Yes", "No");
						if (answer) 
						{ 
							await Navigation.PushAsync(new Home()); 
						} 
						else 
						{ 
							await Navigation.PushAsync(new SelectType()); 
						}					
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
                await DisplayAlert("Authentication Failed", "Fingerprint Authentication Failed", "OK");
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