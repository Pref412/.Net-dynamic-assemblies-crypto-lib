using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hidderReference;

namespace hidden
{
    public static class Runner
    {
        public static void Init()
        {
            Console.WriteLine("Hello from the hell!");

            Outputer outputer = new Outputer();
            outputer.Out();

            Console.ReadKey();
        }
    }
}
