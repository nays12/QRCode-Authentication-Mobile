using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRCodeAuthMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AESHelper : ContentPage
    {
        public AESHelper()
        {
            InitializeComponent();

            string original = "We need to encrypt!";
            string k = "1111111111111111";
            string en_res = "";
            string de_res = "";

            Console.WriteLine("original string: " + original);  //ouput the org string
            en_res = AesEncrypt(original,k);
            Console.WriteLine("After encrypt: " + en_res);  //output the encryption
            de_res = AesDecrypt(en_res,k);
            Console.WriteLine("After decrypt: " + de_res);  //output the encryption

        }

        public string AesEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;  //no string sent handle
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray);

        }

        public string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;  //no string sent handle
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);

        }
    }
}