using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ZipFileTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var fileName = "HelloWorld.txt";
            var bytes = Encoding.UTF8.GetBytes("HelloWorld");

            using (var zipStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Update))
                {
                    var entry = zipArchive.CreateEntry(fileName);
                    using (var entryStream = entry.Open())
                    {
                        var buff = bytes;
                        entryStream.Write(buff, 0, buff.Length);
                    }
                }

                Console.WriteLine(BitConverter.ToString(zipStream.ToArray()));

                File.WriteAllBytes("HelloWorld.zip", zipStream.ToArray());
            }
        }
    }
}