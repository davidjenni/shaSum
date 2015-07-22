using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace shaSum
{
    public class HashFile
    {
        HashAlgorithm hashAlgorithm;

        public HashFile(HashAlgorithm hashAlgorithm)
        {
            this.hashAlgorithm = hashAlgorithm;
        }

        public string Calculate(string fileName)
        {
            string fullPathFileName = Path.GetFullPath(fileName);
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("File '{0}' not found.", fullPathFileName);
            }
            using (var fileStream = new FileStream(fullPathFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Calculate(fileStream);
            }
        }

        public string Calculate(Stream stream)
        {
            var hashedData = this.hashAlgorithm.ComputeHash(stream);
            return ToHexString(hashedData);
        }

        static string ToHexString(byte[] value)
        {
            return new System.Runtime.Remoting.Metadata.W3cXsd2001.SoapHexBinary(value).ToString();
        }
    }
}
