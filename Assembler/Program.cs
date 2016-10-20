using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    class Program
    {
        static List<Assembly> assembies = new List<Assembly>();
        static TextReader input = Console.In;
        static void Main(string[] args)
        {
            Input(args);
            Process();
        }

        private static void Input(string[] args)
        {
            ReadFromFile(args);
        }

        private static void ReadFromFile(string[] args)
        {
            if (args.Any())
            {
                var path = args[0];
                if (File.Exists(path))
                {
                    input = File.OpenText(path);
                }
            }
            
            for (string line; (line = input.ReadLine()) != null;)
            {
                SplitText(line);
            }
        }

        private static void SplitText(string inputText)
        {

            assembies.Add(new Assembly());
        }

        private static void Process()
        {
            foreach (Assembly assembly in assembies)
            {
                Console.WriteLine(assembly.ToMachine());
            }
        }
    }
}
