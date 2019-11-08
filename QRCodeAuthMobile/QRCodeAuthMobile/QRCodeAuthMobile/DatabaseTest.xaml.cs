using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCodeAuthMobile.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DatabaseTest : ContentPage
	{
		public DatabaseTest()
		{
			InitializeComponent();
		}

		public async void OnNewButtonClicked(object sender, EventArgs args)
		{
			statusMessage.Text = "";
			
			//Credential cred1 = new Credential {
			//		name = "Email",
			//		issuer = "UHCL UCT",
			//		issue_date = Convert.ToDateTime("01/15/2016"),
			//		expiration_date = Convert.ToDateTime("12/21/2019"),
			//		value = "WigginsN7499@uhcl.edu",
			//		is_valid = true
			//	};

			//await App.CredentialRepo.AddCredentialAsync(cred1);
			//statusMessage.Text = App.CredentialRepo.StatusMessage;
		}

		public async void OnGetButtonClicked(object sender, EventArgs args)
		{
			statusMessage.Text = "";

			//List<Credential> credentials = await App.CredentialRepo.GetAllCredentialsAsync();
			//credentialsList.ItemsSource = credentials;
		}


	}
}