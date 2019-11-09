/*
 * Purpose: Displays details about the Information Collector the user shared their credentials with.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfirmCredentials : ContentPage
	{
		public ConfirmCredentials()
		{
			InitializeComponent();
		}
	}
}