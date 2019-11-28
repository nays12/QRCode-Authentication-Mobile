using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QRCodeAuthMobile.Models;
using QRCodeAuthMobile.Data;
using QRCodeAuthMobile.Services;
using ZXing.Net.Mobile.Forms;
using Newtonsoft.Json;

namespace QRCodeAuthMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManageAttendance : ContentPage
	{
        // List to hold all attendance events. 
        protected List<Event> attendanceEvents = new List<Event>();

        public ManageAttendance ()
		{
			InitializeComponent();
			GetAttendanceHistory();
        }

        public async void GetAttendanceHistory()
        {
            //CORRECT code
            attendanceEvents = await EventRepository.GetAllEventsAsync();
			if (attendanceEvents != null && attendanceEvents.Count > 0)
			{
				AttendanceViewList.ItemsSource = attendanceEvents;
			}
			else
			{
				await DisplayAlert("No Events Found", "Looks like you haven't attended any events yet. Find a QR Code and hit the Record Attendance button to add some!", "OK");
			}
        }


        private async void BtnRecordAttendance_Clicked(object sender, EventArgs e)
        {
			//Create a scan page. 
			var scanPage = new ZXingScannerPage();
			scanPage.DefaultOverlayShowFlashButton = true;
			scanPage.HasTorch = true;

			// Navigate to scan page
			await Navigation.PushModalAsync(scanPage);

			//Event Handler
			scanPage.OnScanResult += (result) =>
			{
				// Pop the page and show the result
				Device.BeginInvokeOnMainThread(async () =>
				{
					// Stop scanning and dimiss the modal page
					scanPage.IsScanning = false;
					await Navigation.PopModalAsync();

					//Save the scanned event object into the eventObject variable. 
					Event eventQR = JsonConvert.DeserializeObject<Event>(Convert.ToString(result));
					System.Diagnostics.Debug.WriteLine(eventQR);

					ConfirmAttendance(eventQR);
				});
			};
		}

        public async void ConfirmAttendance(Event e1) 
        {

			// Show user event details
			var message = "Name: " + e1.Name + "\n Location: " + e1.Location + "\n Event Type: " + e1.EventType + "\n Start Time: " + e1.StartTime.ToString() + "\n End Time: " + e1.EndTime.ToString() + "\n Description : " + e1.Description;
			bool answer = await DisplayAlert("Attend Event?", message, "Yes", "No");

			if (e1.CredentialsNeeded != null && e1.CredentialsNeeded.Count > 0)
			{
				foreach (CredentialType c in e1.CredentialsNeeded)
				{
					System.Diagnostics.Debug.WriteLine(c);
				}
			}
			else
			{
				await DisplayAlert("Required Credentials", "This Event does not require any credentials to attendance.", "OK");
			}

			// Add event to database if user chooses to attend
			if (answer)
			{
				//Add new attendace event to List and database and refresh ListView. 
				await EventRepository.AddEventAsync(e1);
				attendanceEvents.Add(e1);
				AttendanceViewList.ItemsSource = attendanceEvents;

				// Send user Credentials to Web App
				SendCredentials(e1);
			}
			else
			{
				await DisplayAlert("Declined Attendance", "You have decided not to attend this event", "OK");
			}
		}

		public async void SendCredentials(Event ev)
		{
			List<Credential> eventCreds = new List<Credential>();
			Credential cred = new Credential();

			foreach (CredentialType ct in ev.CredentialsNeeded) // get credentials of type requested in event
			{
				cred = await CredentialRepository.GetCredentialByType(ct);
				if (cred != null)
				{
					eventCreds.Add(cred);
				}
			};

			await DataService.SendEventCredentials(eventCreds);
		}


    }
}