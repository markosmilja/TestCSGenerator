using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestCSGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            string applicationDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath) + "\\";
            //File.WriteAllText(applicationDir + "Properties.txt", "E");
            var protpertiaes = File.ReadAllLines(applicationDir + "Properties.txt");
        }
    }
}
