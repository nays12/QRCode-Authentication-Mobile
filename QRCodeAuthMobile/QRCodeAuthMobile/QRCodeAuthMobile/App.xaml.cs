using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace QRCodeAuthMobile
{
	public partial class App : Application
	{

		string dbPath => FileAccessHelper.GetLocalFilePath("UserData.db3");
		public App()
		{
			InitializeComponent();

			//MainPage = new MainPage();
            //  MainPage = new SelectType();
            //  MainPage = new Home();
            //  MainPage = new ConfirmCredentials();
            //  MainPage = new ConfirmMessage();
            //  MainPage = new ConfirmAttendance();
              MainPage = new WebAppLogin();
        }

        protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
