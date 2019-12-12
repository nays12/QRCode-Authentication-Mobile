/*
 * Purpose: To implement the FaceAuthorization interface to use iOS FaceID capabilities
 * 
 * Contributors:
 * Marilin Ortuno
 * 
 */
using Foundation;
using QRCodeAuthMobile.Interfaces;
using QRCodeAuthMobile.Data;
using QRCodeAuthMobile.iOS.Implementations;
using Xamarin.Forms;
using LocalAuthentication;


[assembly: Xamarin.Forms.Dependency(typeof(FaceAuthImp))]
namespace QRCodeAuthMobile.iOS.Implementations
{
    class FaceAuthImp : IFaceAuth
    {
		int count;
		public async void GetTableCount()
		{
			count = await UserRepository.GetRowCount();
		}
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

						//If authentication is successful continue to next page. 
						if (success)
						{
							if (count > 0) // If record count for User table is > 0 then an account exist
							{
								Xamarin.Forms.Application.Current.MainPage = new Home();
							}
							else // If the record count is 0 then no User account exist
							{
								Xamarin.Forms.Application.Current.MainPage = new SelectType();
							}
						}
                    });
                });

                //Handle the result of Face ID authentification (Sucess/Failure) according to replyHandler above.  
                context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, myReason, replyHandler);
            }
            else
            {
                //If FaceID NOT available on mobile device, display appropriate error message.
                Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Authentication Failed", "Could not Authenticate with FaceID", "Ok");
            }
        }
    }
}