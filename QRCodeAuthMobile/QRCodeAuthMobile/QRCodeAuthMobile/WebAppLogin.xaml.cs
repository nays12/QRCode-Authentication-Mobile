/*
 * Purpose: Displays generated Access code from Web Application
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QRCodeAuthMobile.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WebAppLogin : ContentPage
	{
		public WebAppLogin()
		{
			InitializeComponent();

			string result = CallWebCodeService();

//			WebCode.Text = result;

		}

		public static string CallWebCodeService()
		{
			string code;

			code = WebLoginCodeService.GetWebCode().ToString();
			System.Diagnostics.Debug.WriteLine(code);
			//DisplayAlert("Test", "", "OK");
			return code;
		}
	}

}