using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using QRCodeAuthMobile.Models;
using QRCodeAuthMobile.Data;

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

        public void GetAttendanceHistory()
        {
            //attendanceEvents = await EventRepository.GetAllEventsAsync();
            //AttendanceList.ItemsSource = attendanceEvents;

            //Testing - DELETE LATER
            CreateAttendanceEvents();
            AttendanceList.ItemsSource = attendanceEvents;
        }

        private void BtnRecordAttendance_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new Camera();
        }


        // TESTING - DELETE LATER 
        public void CreateAttendanceEvents() // TESTING - DELETE LATER 
        {
            Event e1 = new Event();
            e1.Name = "Delta Waffle Day";
            e1.Location = "Delta Building Lobby";
            e1.EventType = EventType.Campus;
            e1.StartTime = Convert.ToDateTime("10/30/2019 2:30:00 PM");
            e1.EndTime = Convert.ToDateTime("10/30/2019 2:30:00 PM");
            e1.Description = "Free Waffles at the Delta building!";
            e1.Owner = "8764710";
            attendanceEvents.Add(e1);

            Event e2 = new Event();
            e2.Name = "Senior Class";
            e2.Location = "Delta Building 242";
            e2.EventType = EventType.Lecture;
            e2.StartTime = Convert.ToDateTime("10/30/2019 7:00:00 PM");
            e2.EndTime = Convert.ToDateTime("10/30/2019 9:50:00 PM");
            e2.Description = "Class";
            e2.Owner = "8764710";
            attendanceEvents.Add(e2);

            Event e3 = new Event();
            e3.Name = "Study PAWS";
            e3.Location = "Naumann Library Main Floor";
            e3.EventType = EventType.Campus;
            e3.StartTime = Convert.ToDateTime("11/20/2019 3:00:00 PM");
            e3.EndTime = Convert.ToDateTime("11/20/2019 4:30:00 PM");
            e3.Description = "Pet Away Worry and Stress (PAWS) during your studies for mid-terms at this Neumann Library fall event.";
            e3.Owner = "646825";
            attendanceEvents.Add(e3);
        }
    }
}