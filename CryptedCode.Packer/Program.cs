using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CryptedCode.Packing;

namespace CryptedCode.Packer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string directory = Environment.CurrentDirectory;
            IEnumerable<string> files = Directory.EnumerateFiles(directory, "*.dll").Where(f => !f.EndsWith("CryptedCode.Packing.dll"));
            
            var packer = new DefaultPacker();

            var filesBytes = ReadFiles(files).ToArray();

            var result = packer.Pack(filesBytes);

            File.WriteAllText("AppData.pak", result);
        }

        private static IEnumerable<byte[]> ReadFiles(IEnumerable<string> pathes)
        {
            foreach (var file in pathes)
            {
                yield return File.ReadAllBytes(file);
            }
        } 
    }
}