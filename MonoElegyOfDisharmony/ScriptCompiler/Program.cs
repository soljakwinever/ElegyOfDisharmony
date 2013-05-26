using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ScriptCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new Exception("Invalid params passed");
            }

            using (Utilities.FormattedFile file = new Utilities.FormattedFile())
            {
                //Read in the .ell script format to compile to a .ecs file
                file.ReadBegin(args[0] + ".ell");


                file.ReadEnd();
            }
        }
    }
}
