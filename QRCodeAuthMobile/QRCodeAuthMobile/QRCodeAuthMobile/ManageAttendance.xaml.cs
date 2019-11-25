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
			InitializeComponent ();
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

                    //Add new attendace event to List and database. 
                    attendanceEvents.Add(eventObject);
                    await EventRepository.AddEventAsync(eventObject);

                    //Re-Add the list of events to ListView, so it can refresh. 
                    AttendanceViewList.ItemsSource = attendanceEvents;

                    //await DisplayAlert("Scanned Barcode", result.Text, "OK");
                    ConfirmAttendance(eventObject);

                });
            };
        }


        public async void ConfirmAttendance(Event e1)
        {
            String message = "EventID:  " + e1.EventId + "Name: " + e1.Name + "\n Location: " + e1.Location + "\n Event Type: " + e1.EventType + "\n Start Time: " + e1.StartTime.ToString() + "\n End Time: " + e1.EndTime.ToString() + "\n Description : " + e1.Description + "\n Owner: " + e1.Owner;
            await DisplayAlert("Scanned Barcode", message, "OK");
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