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
	public partial class SelectType : ContentPage
	{
		public SelectType()
		{
			InitializeComponent();
		}

		public async void SubmitButtonClicked(object sender, EventArgs args)
		{

			statusMessage.Text = "";



			User mobileUser = new User
			{
				userId = schoolId.Text,
				last_name = lastName.Text,
				first_name = firstName.Text,
				group = group.SelectedItem.ToString()
			};

			await App.UserRepo.AddUserAsync(mobileUser);
			statusMessage.Text = App.UserRepo.StatusMessage;
		}
	}
}