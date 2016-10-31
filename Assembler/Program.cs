﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler
{
    class Program
    {
        public static class Global
        {
            public static Dictionary<string, int> fillValues = new Dictionary<string, int>();
            public static Dictionary<string, int> addressValues = new Dictionary<string, int>();
        }

        public static List<Assembly> assembies = new List<Assembly>();

        static void Main(string[] args)
        {
            Input(args);
            Process();

            Console.WriteLine("Show .fill values");
            foreach(KeyValuePair<string, int> value in Global.fillValues)
            {
                Console.WriteLine(value.Key + " have " + value.Value);
            }

            Console.WriteLine("Show address label value");
            foreach(KeyValuePair<string, int> value in Global.addressValues)
            {
                Console.WriteLine(value.Key + " is address " + value.Value);
            }
        }

        private static void Input(string[] args)
        {
            ReadFromFileAndSplit(args);
        }

        private static void ReadFromFileAndSplit(string[] args)
        {
            TextReader input = Console.In;
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

            String In_type = "";
            String Instruc = "";
            String label = "";
            String rs = "";
            String rt = "";
            String rd = "";

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

                if (label != "" && label != " " && label != "\t")
                {
                    Global.addressValues.Add(label, assembies.Count);
                }

                In_type = Instruction_Type; // get type
            }

            if (Instruction_Type == "O" )
            {
                Instruc = Sub_text[Index_Of_Instruce_Name];
            }
            else if (Instruction_Type == "J" || Instruction_Type == "F")  // get only 1 field
            {
                Instruc = Sub_text[Index_Of_Instruce_Name];
                rs = Sub_text[Index_Of_Instruce_Name + 1];

            }
            else  // of R and I type get 3 field
            {
                Instruc = Sub_text[Index_Of_Instruce_Name];
                rs = Sub_text[Index_Of_Instruce_Name + 1];
                rt = Sub_text[Index_Of_Instruce_Name + 2];
                rd = Sub_text[Index_Of_Instruce_Name + 3];
            }

            if (In_type == "F")
            {
                int value = 0;
                if (int.TryParse(rs, out value))
                {
                    Global.fillValues.Add(label, value);
                }
                else
                {
                    Global.fillValues.Add(label, Global.addressValues[rs]);
                }
            }

            assembies.Add(new Assembly(label,Instruc,rs, rt, rd, In_type));
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
