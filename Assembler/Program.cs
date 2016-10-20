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


            String Instruction_Type = "";
            char[] in_derim = { ' ', '\t' };
            string[] Sub_text = inputText.Split(in_derim);


            //List<string> Instruction_Sub_text = new List<string>(); // Type + lable + Instruction_name +  field0-2 

            String In_type = "";
            String Instruc = "";
            String label = "";
            String f1 = "";
            String f2 = "";
            String f3 = "";


            int Index_Of_Instruce_Name = 0;
            // check Type
            if (Check_Instruction(Sub_text[0])) // instruct
            {
                Instruction_Type = Check_Instruction_type(Sub_text[0]);
                Index_Of_Instruce_Name = 0;


                In_type = Instruction_Type; // get type
            }
            else if (Check_Instruction(Sub_text[1]))  // Label + instruct
            {
                Instruction_Type = Check_Instruction_type(Sub_text[1]);
                Index_Of_Instruce_Name = 1;

                label = Sub_text[0];
                In_type = Instruction_Type; // get type

            }


            if (Instruction_Type == "O" )
            {
                Instruc = Sub_text[Index_Of_Instruce_Name];
            }
            else if (Instruction_Type == "J" || Instruction_Type == "F")  // get only 1 field
            {
                Instruc = Sub_text[Index_Of_Instruce_Name];
                f1 = Sub_text[Index_Of_Instruce_Name + 1];

            }
            else  // of R and I type get 3 field
            {
                Instruc = Sub_text[Index_Of_Instruce_Name];
                f1 = Sub_text[Index_Of_Instruce_Name + 1];
                f2 = Sub_text[Index_Of_Instruce_Name + 2];
                f3 = Sub_text[Index_Of_Instruce_Name + 3];
            }


            assembies.Add(new Assembly(label,Instruc,f1, f2, f3, In_type));
        }

        private static bool Check_Instruction(String text)
        {
            bool check_instruction = false;

            if (text == "add") { check_instruction = true; }
            else if (text == "nand") { check_instruction = true; }
            else if (text == "lw") { check_instruction = true; }
            else if (text == "sw") { check_instruction = true; }
            else if (text == "beq") { check_instruction = true; }
            else if (text == "jalr") { check_instruction = true; }
            else if (text == "halt") { check_instruction = true; }
            else if (text == "noop") { check_instruction = true; }
            else if (text == ".fill") { check_instruction = true; }

            return check_instruction;
        }

        private static string Check_Instruction_type(String text)
        {
            string instruction = "R";

            if (text == "add") { instruction = "R"; }
            else if (text == "nand") { instruction = "R"; }
            else if (text == "lw") { instruction = "I"; }
            else if (text == "sw") { instruction = "I"; }
            else if (text == "beq") { instruction = "I"; }
            else if (text == "jalr") { instruction = "J"; }
            else if (text == "halt") { instruction = "O"; }
            else if (text == "noop") { instruction = "O"; }
            else if (text == ".fill") { instruction = "F"; }

            return instruction;
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
