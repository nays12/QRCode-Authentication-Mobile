/*
 * Purpose: Displays page requesting comfirmation to share credentials
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
	public partial class ConfirmationMessage : ContentPage
	{
		public ConfirmationMessage()
		{
			InitializeComponent();
		}
	}
}