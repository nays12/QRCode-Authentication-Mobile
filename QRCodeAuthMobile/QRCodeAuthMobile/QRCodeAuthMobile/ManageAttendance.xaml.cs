using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QRCodeAuthMobile.Models;
using QRCodeAuthMobile.Data;
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
			if (attendanceEvents.Count > 0)
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

            Event eventObject = new Event();

            //Create a scan page. 
            var scanPage = new ZXingScannerPage();

            // Navigate to scan page
            await Navigation.PushAsync(scanPage);

            //Clear the list 
            AttendanceViewList.ItemsSource = null;
           
            //Event Handler
            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();

                    //Save the scanned event object into the eventObject variable. 
                    eventObject = JsonConvert.DeserializeObject<Event>(result.ToString());

                    ConfirmAttendance(eventObject);
                });
            };
        }

        public async void ConfirmAttendance(Event e1)
        {
			// Show user event details
            var message = "EventID:  " + e1.EventId + "Name: " + e1.Name + "\n Location: " + e1.Location + "\n Event Type: " + e1.EventType + "\n Start Time: " + e1.StartTime.ToString() + "\n End Time: " + e1.EndTime.ToString() + "\n Description : " + e1.Description;
            bool answer = await DisplayAlert("Attend Event?", message, "Yes", "No");

			// Add event to database if user chooses to attend
			if (answer)
			{
				//Add new attendace event to List and database and refresh ListView. 
				await EventRepository.AddEventAsync(e1);
				attendanceEvents.Add(e1);				
				AttendanceViewList.ItemsSource = attendanceEvents;
			}
			else
			{
				await DisplayAlert("Declined Attendance", "You have decided not to attend this event", "OK");
			}

		}

        //TEsting - DELETE LATER
        public void debug(List<Event> attendanceEvents)
        {
            foreach(Event ev in attendanceEvents)
            {
                System.Diagnostics.Debug.WriteLine("AFTER ADD");
                System.Diagnostics.Debug.WriteLine("Event ID Number: " + ev.EventId);
                System.Diagnostics.Debug.WriteLine("Name: " + ev.Name);
                System.Diagnostics.Debug.WriteLine("Location: " + ev.Location);
                System.Diagnostics.Debug.WriteLine("Type: " + ev.EventType);
                System.Diagnostics.Debug.WriteLine("Start Time: " + ev.StartTime);
                System.Diagnostics.Debug.WriteLine("End Time: " + ev.EndTime);
                System.Diagnostics.Debug.WriteLine("Description: " + ev.Description);
                System.Diagnostics.Debug.WriteLine("Owner: " + ev.Owner);
                System.Diagnostics.Debug.WriteLine("-----------------------");
            }
        }

    }
}