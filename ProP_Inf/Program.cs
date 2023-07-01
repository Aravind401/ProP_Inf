using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
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
            int count = args.Count();
            if (count > 0)
            {
                GetFileDescription(args[0]);
            }
            else Console.WriteLine("exe path required");
            Console.ReadKey();
        }
        private static void GetFileDescription(string path = "C:\\Users\\aravi\\OneDrive\\Desktop\\EXE\\AsusAgni.exe")
        {
            GetVersionDetails(path);
            GetSigningDetails(path);
            getFileInformation(path);
            Console.ReadKey();
        }
        static void GetVersionDetails(string FilePath)
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(FilePath);

            var in_fo = new
            {
               @info,
                //version = info.FileVersion,
                //ProductVersion = info.ProductVersion,
                //ProductName = info.ProductName
            };
            Console.WriteLine(in_fo.ToString()); Console.WriteLine();

        }
        static void GetSigningDetails(string FilePath)
        {

            X509Certificate signer = X509Certificate.CreateFromSignedFile(FilePath);
            X509Certificate2 certificate = new X509Certificate2(signer);
            var certificateChain = new X509Chain
            {
                ChainPolicy = { RevocationFlag = X509RevocationFlag.EntireChain, RevocationMode = X509RevocationMode.Online, UrlRetrievalTimeout = new TimeSpan(0, 1, 0), VerificationFlags = X509VerificationFlags.NoFlag }
            };

            var chainIsValid = certificateChain.Build(certificate);

            var info = new
            {
               @certificate,
                //Certificate = certificate.Version,
                //Issuer = certificate.Issuer,
                //Subject = certificate.Subject,
                //FriendlyName = certificate.FriendlyName,

                //SerialNumber = certificate.SerialNumber,
                //NotAfter = certificate.NotAfter,
                //NotBefore = certificate.NotBefore,
                //SignatureAlgorithm = certificate.SignatureAlgorithm,
                certificateisvalid = chainIsValid
            };
            Console.WriteLine(info);
            Console.WriteLine();
        }
        static void getFileInformation(string FilePath)
        {

            FileInfo fi = new FileInfo(FilePath);
            var info = new
            {
                // @fi,
                name = fi.Name,
                CreationTime = fi.CreationTime.ToString(),
                modifiedTime = fi.LastWriteTime.ToString(),
                lastAccessTime = fi.LastAccessTime.ToString(),
                Size = SizeSuffix(fi.Length)

            };
            Console.WriteLine(info
            );

        }


        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(long value, int decimalPlaces = 2)
        {
            if (value < 0)
            {
                throw new ArgumentException("Bytes should not be negative", "value");
            }
            var mag = (int)Math.Max(0, Math.Log(value, 1024));
            var adjustedSize = Math.Round(value / Math.Pow(1024, mag), decimalPlaces);
            return String.Format("{0} {1}", adjustedSize, SizeSuffixes[mag]);
        }

    }
}
