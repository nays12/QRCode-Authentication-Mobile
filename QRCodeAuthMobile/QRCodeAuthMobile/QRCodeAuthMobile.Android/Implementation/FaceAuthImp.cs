using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRCodeAuthMobile.Droid.Implementation;
using QRCodeAuthMobile.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(FaceAuthImp))]
namespace QRCodeAuthMobile.Droid.Implementation
{
    class FaceAuthImp : IFaceAuth
    {
        public void FaceAuthentication()
        {
            //FaceID authentification not avaialbe for our mobile app. 
            //Display appropriate message. 
            Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Authentication Failed", "Face ID Authentification not available for Android", "Ok");
        }
    }
}