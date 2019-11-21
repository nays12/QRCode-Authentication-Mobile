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
using QRCodeAuthMobile.Services;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		public Home()
		{
			InitializeComponent();
		}

        private void BtnManagedCredentials_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new ManageCredentials();
        }

		private void BtnWebLogIn_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new WebAppLogin();
        }

        private void BtnRecordAttendance_Clicked(object sender, EventArgs e)
        {
            
        }

        private void BtnShareCredentials_Clicked(object sender, EventArgs e)
        {
            
        }


    }
}