using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

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
            if (await CrossFingerprint.Current.IsAvailableAsync())
            {
                FingerprintAuthenticationResult result = await CrossFingerprint.Current.AuthenticateAsync("Provide fingerprint to sign in.");
                if (result.Authenticated)
                {
                    App.Current.MainPage = new SelectType();
                }
            }
            else
            {
                await DisplayAlert("Authentication Failed", "Fingerprint Authentication Failed", "OK");
            }
        }

		private async void BtnFaceID_Clicked(object sender, EventArgs e)
		{


		}
		
	}
}
