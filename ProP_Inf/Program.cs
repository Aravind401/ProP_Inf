using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProP_Inf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetFileDescription();
        }
        private static void GetFileDescription()
        {
            // Get the file version for the notepad.
            var pth = @"C:\\Users\\aravi\\OneDrive\\Desktop\\EXE\\AsusAgni.exe";

            X509Certificate signer = X509Certificate.CreateFromSignedFile(pth);
            X509Certificate2 certificate = new X509Certificate2(signer);
            var certificateChain = new X509Chain
            {

                ChainPolicy = {
        RevocationFlag = X509RevocationFlag.EntireChain,
        RevocationMode = X509RevocationMode.Online,
        UrlRetrievalTimeout = new TimeSpan(0, 1, 0),
        VerificationFlags = X509VerificationFlags.NoFlag }
            };

            var chainIsValid = certificateChain.Build(certificate);


            FileVersionInfo info =
            FileVersionInfo.GetVersionInfo(pth);

            // Print the file description.
            var v = "File description: " + info.FileDescription;
            Console.WriteLine(v + Environment.NewLine);
            Console.WriteLine(info.InternalName + Environment.NewLine);
            Console.WriteLine(info.Language + Environment.NewLine);
            Console.WriteLine(info.FileVersion + Environment.NewLine);
            Console.WriteLine(info.CompanyName + Environment.NewLine);
            Console.WriteLine(info.FileName + Environment.NewLine);
            //info.



            Console.ReadKey();
        }


    }
}
