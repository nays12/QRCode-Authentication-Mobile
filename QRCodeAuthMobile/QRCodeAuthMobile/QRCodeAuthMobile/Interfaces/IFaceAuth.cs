/*
 * Purpose: To declare FaceAuthorization interface to use iOS FaceID capabilities
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace QRCodeAuthMobile.Interfaces
{
    public interface IFaceAuth
    {
        void FaceAuthentication();
    }
}
