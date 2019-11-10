/*
 * Purpose: Displays generated Access code from Web Application
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QRCodeAuthMobile.Services;
using QRCodeAuthMobile.Models;

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

			var result1 = DataService.GetAllEvents();
			var result2 = DataService.GetWebCode();

			WebCode.Text = result2.ToString();

		}

		//public static List<Event> GetEvents()
		//{
		//	List<Event> events = DataService.GetAllEvents();

		//	return events;
		//}

	}

}