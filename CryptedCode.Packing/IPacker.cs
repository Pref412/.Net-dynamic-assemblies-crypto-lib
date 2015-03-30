using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptedCode.Packing
{
    public interface IPacker
    {
        string Pack(IList<byte[]> data);

        IEnumerable<byte[]> Unpack(string packed);
    }
}
