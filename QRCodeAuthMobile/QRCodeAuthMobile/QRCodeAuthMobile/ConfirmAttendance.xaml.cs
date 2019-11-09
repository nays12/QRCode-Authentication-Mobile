/*
 * Purpose: Displays details about the Event the user sent their digital credentials to.
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
	public partial class ConfirmAttendance : ContentPage
	{
		public ConfirmAttendance()
		{
			InitializeComponent();
		}
	}
}