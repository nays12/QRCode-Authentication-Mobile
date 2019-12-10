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
			// define anonoymous object
			var defenition = new
			{
				attendaceManager = "",
				department = "",
				eventId = "",
				eventName = "",
				eventLocation = "",
				eventType = "",
				eventDate = "",
				eventStart = "",
				eventEnd = "",
				evDescription = "",
				evOwner = "",
				requestedCredentials = new List<CredentialType>() { },
			};

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

					//Save the scanned anonymous object into the obj variable. 
					var obj = JsonConvert.DeserializeAnonymousType(result.ToString(), defenition);

					// Get the Event object
					Event ev = new Event
					{
						EventId = Convert.ToInt32(obj.eventId),
						Name = obj.eventName,
						Location = obj.eventLocation,
						EventType = (EventType)Convert.ToInt32(obj.eventType),
						Date = DateTime.Parse(obj.eventDate),
						StartTime = DateTime.Parse(obj.eventStart),
						EndTime = DateTime.Parse(obj.eventEnd),
						Description = obj.evDescription,
						Owner = obj.evOwner
					};

					// Get Other Info
					string amName = obj.attendaceManager;
					string amDepartment = obj.department;
					List<CredentialType> requestedCredentials = obj.requestedCredentials;

					System.Diagnostics.Debug.WriteLine(obj);

					ConfirmAttendance(ev, amName, amDepartment, requestedCredentials);
				});
			};
		}

		public async void ConfirmAttendance(Event ev, string amName, string amDepartment, List<CredentialType> requestedCredentials)
		{
			// Show user event details
			var message = "Attendance Manager: " + amName + "\n Department: " + amDepartment + "\n Event Name: " + ev.Name + "\n Event Location: " + ev.Location + "\n Event Type: " + ev.EventType +
						  "\n Date: " + ev.Date + "\n Start Time: " + ev.StartTime + "\n End Time: " + ev.EndTime + "\n Description : " + ev.Description;
			var message2 = "Name \nEmail";
			await DisplayAlert("Event Details", message, "OK");
			bool answer = await DisplayAlert("Send Credentials?", message2, "Yes", "No");

			if (requestedCredentials != null && requestedCredentials.Count > 0)
			{
				foreach (CredentialType c in requestedCredentials)
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
				await EventRepository.AddEventAsync(ev);
				attendanceEvents.Add(ev);
				AttendanceViewList.ItemsSource = attendanceEvents;

				// Send user Credentials to Web App
				SendCredentials(requestedCredentials);
			}
			else
			{
				await DisplayAlert("Declined Attendance", "You have decided not to attend this event", "OK");
			}
		}


		public async void SendCredentials(List<CredentialType> creds)
		{
			List<Credential> eventCreds = new List<Credential>();

			foreach (CredentialType ct in creds) // get credentials of type requested in event
			{
				Credential cred = await CredentialRepository.GetCredentialByType(ct);
				if (cred != null)
				{
					eventCreds.Add(cred);
				}
			};
			await DataService.SendEventCredentials(eventCreds);
			GetAttendanceHistory();
		}

    }
}