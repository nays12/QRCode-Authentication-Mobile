using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;


using QRCodeAuthMobile.Interfaces;
using QRCodeAuthMobile.iOS.Implementations;
using Xamarin.Forms;
using LocalAuthentication;


[assembly: Xamarin.Forms.Dependency(typeof(FaceAuthImp))]
namespace QRCodeAuthMobile.iOS.Implementations
{
    class FaceAuthImp : IFaceAuth
    {
        public void FaceAuthentication()
        {
            var context = new LAContext();
            NSError AuthError;
            var myReason = new NSString("Athenticate");

            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
            {
                var replyHandler = new LAContextReplyHandler((success, error) => {
                    Device.BeginInvokeOnMainThread(() => {
                        if (success)
                        {
                            Xamarin.Forms.Application.Current.MainPage = new SelectType();
                        }
                    });
                });
                context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, myReason, replyHandler);
            }
            else
            {
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Authentication", "Authentication Failed", "Ok");
            }
        }
    }
}