using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;

namespace Shengtai
{
    public class Zip : IDisposable
    {
        private readonly ZipFile zip;

        public Zip(string password = null)
        {
            this.zip = new ZipFile { CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression };
            if (!string.IsNullOrEmpty(password))
                zip.Password = password;
        }

        public Zip AddEntry(string entryName, byte[] byteContent)
        {
            this.zip.AddEntry(entryName, byteContent);

            return this;
        }

        public byte[] Save()
        {
            MemoryStream outputStream = new MemoryStream();
            this.zip.Save(outputStream);

            byte[] outputStreamBytes = outputStream.ToArray();
            if (outputStreamBytes == null)
                throw new Exception();

            return outputStreamBytes;
        }

        //public static IList<byte[]> Extract(Stream zipStream, string password = null)
        //{
        //    IList<byte[]> result = new List<byte[]>();

        //    using (ZipFile zip = ZipFile.Read(zipStream))
        //    {
        //        if (!string.IsNullOrEmpty(password))
        //            zip.Password = password;

        //        foreach(var entry in zip)
        //        {
        //            // todo: 可以知道 ZipEntry 之檔名已組成新設計的物件(預期包裝最外層壓縮檔內之所有檔案)
        //            MemoryStream stream = new MemoryStream();
        //            entry.Extract(stream);

        //            result.Add(stream.ToArray());
        //        }
        //    }

        //    if (result.Count == 0)
        //        throw new Exception();

        //    return result;
        //}

        public static IDictionary<string, byte[]> Extract(Stream zipStream, string password = null)
        {
            IDictionary<string, byte[]> result = new Dictionary<string, byte[]>();

            using (ZipFile zip = ZipFile.Read(zipStream))
            {
                if (!string.IsNullOrEmpty(password))
                    zip.Password = password;

                foreach (var entry in zip)
                {
                    MemoryStream stream = new MemoryStream();
                    entry.Extract(stream);

                    result.Add(entry.FileName, stream.ToArray());
                }
            }

            if (result.Count == 0)
                return null;

            return result;
        }

        public void Dispose()
        {
            this.zip.Dispose();
        }
    }
}