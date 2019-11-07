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
            var myReason = new NSString("Athenticate");
            NSError AuthError;

            //Check if Face ID is available on mobile device. 
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
            {
                var replyHandler = new LAContextReplyHandler((success, error) => {
                    Device.BeginInvokeOnMainThread(() => {

                        //If authetification is successfull navigate to Select Type page. 
                        if (success)
                        {
                            Xamarin.Forms.Application.Current.MainPage = new SelectType();
                        }
                    });
                });

                //Handle the result of Face ID authentification (Sucess/Failure) according to replyHandler above.  
                context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, myReason, replyHandler);
            }
            else
            {
                //If FaceID NOT available on mobiel device, display appropriate error message. 
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Authentication", "Authentication Failed", "Ok");
            }
        }
    }
}