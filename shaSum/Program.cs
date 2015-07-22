using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace shaSum
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Usage();
                Environment.Exit(1);
            }
            var fileName = args[0];
            try {
                var hashFile = new HashFile(GetHashingAlgorithm());
                var hash = hashFile.Calculate(fileName);
                Console.WriteLine(string.Format("{0}: {1}", fileName, hash));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("An exception occurred: {0}: {1}", ex.GetType().Name, ex.Message);
                Console.Error.WriteLine(WriteException(ex));
                Environment.Exit(1);
            }

            Environment.Exit(0);
        }

        static string WriteException(Exception exception)
        {
            if (exception != null)
            {
                StringBuilder error = new StringBuilder();
                string stackTrace = exception.StackTrace;
                do
                {
                    error.AppendLine(string.Format("{0}: {1}", exception.GetType().Name, exception.Message));

                    exception = exception.InnerException;
                }
                while (exception != null);

                error.AppendLine("stack trace:" + stackTrace);
                return error.ToString();
            }
            return string.Empty;
        }

        static HashAlgorithm GetHashingAlgorithm(string hashName = "sha256")
        {
            switch (hashName.ToUpperInvariant())
            {
                case "SHA256":
                    return new SHA256CryptoServiceProvider();
                default:
                    throw new NotImplementedException(string.Format("support for hash algorithm '{0}'  is not implemented yet.", hashName));
            }
        }

        static void Usage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("shaSum.exe <fileName>");
        }
    }
}
