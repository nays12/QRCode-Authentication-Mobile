/*
 * Purpose: Allows the user to access the camera feature of thier mobile device
 * in order to scan a QR code
 */

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Camera : ContentPage
	{
		public Camera()
		{
			InitializeComponent();

			CameraButton.Clicked += CameraButton_Clicked;
		}

		private async void CameraButton_Clicked(object sender, EventArgs e)
		{
			var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

			if (photo != null)
				PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
		}
	}
}