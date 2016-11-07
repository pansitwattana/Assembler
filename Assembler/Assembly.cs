using System;
using System.Text;

namespace Assembler
{
    public class Assembly
    {

        public Assembly(string label, string inst, string f0, string f1, string f2, string type)
        {
            Label = label;
            Instruction = inst;
            Field0 = f0;
            Field1 = f1;
            Field2 = f2;
            Type = type;
        }

        public string Label { get; set; }
        public string Instruction { get; set; }
        private string field0;

        public string Field0
        {
            get { return field0; }
            set
            {
                field0 = GetValue(value);
            }
        }

        private bool isNumeric(string value)
        {
            int f = 0;
            return int.TryParse(value, out f);
        }

        private string field1;

        public string Field1
        {
            get { return field1; }
            set
            {
                field1 = GetValue(value);
            }
        }



        private string GetValue(string value)
        {
            return CheckIfFillValue(value);
        }

        private string field2;

        public string Field2
        {
            get { return field2; }
            set
            {
                field2 = GetValue(value);
            }
        }

        public string Type { get; set; }
        public override string ToString()
        {
            return Label + "\t" + Instruction + "\t" + Field0 + "\t" + Field1 + "\t" + Field2 + "\t";
        }

        public string ToMachine()
        {
            switch (Type)
            {
                case "R":
                    return ConvertTypeR();
                case "I":
                    return ConvertTypeI();
                case "J":
                    return ConvertTypeJ();
                case "O":
                    return ConvertTypeO();
                default:
                    return "Not a type";
            }
        }

        private string ConvertTypeO()
        {
            string ResultO = "";
            switch (Instruction)
            {
                case "halt":
                    ResultO += "110";
                    break;
                case "noop":
                    ResultO += "111";
                    break;
            }
            ResultO += ExtendZero(DecToBin(Field0), 22);
            return ResultO;
        }

        private string ConvertTypeJ()
        {
            string ResultJ = "";
            switch (Instruction)
            {
                case "jair":
                    ResultJ += "101";
                    break;
            }
            ResultJ += ExtendZero(DecToBin(Field0), 3);
            ResultJ += ExtendZero(DecToBin(Field1), 3);
            ResultJ += ExtendZero(DecToBin(Field2), 16);
            return ResultJ;
        }

        private string ConvertTypeI()
        {
            string ResultI = "";
            switch (Instruction)
            {
                case "lw":
                    ResultI += "010";
                    break;
                case "sw":
                    ResultI += "011";
                    break;
                case "beq":
                    ResultI += "100";
                    break;

            }
            ResultI += ExtendZero(DecToBin(Field0), 3);
            ResultI += ExtendZero(DecToBin(Field1), 3);
            ResultI += ExtendZero(DecToBin(Field2), 16);
            return ResultI;
        }

        private string ConvertTypeR()
        {
            string ResultR = "";
            switch (Instruction)
            {
                case "add":
                    ResultR += "000";
                    break;
                case "nand":
                    ResultR += "001";
                    break;
            }
            ResultR += ExtendZero(DecToBin(Field0), 3);
            ResultR += ExtendZero(DecToBin(Field1), 3);
            ResultR += ExtendZero("0", 13);
            ResultR += ExtendZero(DecToBin(Field2), 3);
            return ResultR;
        }

        //Dec to Bin
        private string DecToBin(string Binary)
        {
            //convert string to integer
            //int m = Int32.Parse("abc");
            int value = Int32.Parse(Binary);
            string bin = Convert.ToString(value, 2);
            return bin;
        }

        //zeroextend
        private string ExtendZero(string bin, int max)
        {
            string str;
            char pad = '0';
            str = bin.PadLeft(max, pad);
            return str;
        }

        private string CheckIfFillValue(string f)
        {
            int str = 0;
            /* check fill in dictionary
            if (Program.Global.fillValues.ContainsKey(f) == false)
             {
                return "undefine";
            }
             else if (Program.Global.fillValues.ContainsKey(f) == true)*/
            {
            int str1 = 0;
            bool isNumeric = int.TryParse(f, out str1);
            if (isNumeric == false)
            {
                str = Program.Global.fillValues[f];
            }
            else if (isNumeric == true)
            {
                str = str1;
            }
            /*int n;
             bool isNumeric = int.TryParse("123", out n);*/
                   
            return str + "";
        }
    }
}