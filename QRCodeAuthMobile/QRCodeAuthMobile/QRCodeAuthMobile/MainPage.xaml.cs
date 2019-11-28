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


namespace QRCodeAuthMobile
{

	//[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			InitializeDatabases();
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
					await UserRepository.DeleteUserbyId("1304693");
					await CredentialRepository.DeleteAllCredentials();

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

		private async void InitializeDatabases()
		{
			await UserRepository.InitializeTableAsync();
			await MobileAccountRepository.InitializeTableAsync();
			await CredentialRepository.InitializeTableAsync();
			await EventRepository.InitializeTableAsync();
		}

	}
}