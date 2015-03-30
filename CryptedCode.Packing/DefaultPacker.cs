using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptedCode.Packing
{
    public class DefaultPacker : IPacker
    {
        private const byte EncryptXorKey = 17;

        public string Pack(IList<byte[]> data)
        {
            StringBuilder packer = new StringBuilder();

            for (int i = 0; i < data.Count; i++)
            {
                byte[] encryptedArray = XorArray(data[i]).ToArray();
                string text = Convert.ToBase64String(encryptedArray);

                packer.Append(text).Append(Environment.NewLine);
            }

            return packer.ToString();
        }

        public IEnumerable<byte[]> Unpack(string packed)
        {
            string[] items = packed.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string encryptedData in items)
            {
                byte[] bytes = Convert.FromBase64String(encryptedData);

                yield return XorArray(bytes).ToArray();
            }
        }

        private IEnumerable<byte> XorArray(byte[] array)
        {
            foreach (byte value in array)
            {
                yield return Xor(value);
            }
        }

        private static byte Xor(byte @byte)
        {
            checked
            {
                return (byte) (@byte ^ EncryptXorKey);
            }
        }
    }
}