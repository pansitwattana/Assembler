using System;

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
        public string Field0 { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Type { get; set; }
        public override string ToString()
        {
            return Label + "\t" + Instruction + "\t" + Field0 + "\t" + Field1 + "\t" + Field2 + "\t" + Comment;
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
                    return "Null";
            }
        }

        private string ConvertTypeO()
        {
            return "";
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
            ResultJ += DecToBin(Field0);
            return ResultJ;
        }

        private string ConvertTypeI()
        {
            string ResultI = "";
            switch (Instruction)
            {
                case "lW":
                    ResultI += "010";
                    break;
                case "sw":
                    ResultI += "011";
                    break;
                case "beq":
                    ResultI += "100";
                    break;

            }
            ResultI += DecToBin(Field0);
            ResultI += ExtendZero(DecToBin(Field1));
            return ResultI;
        }

        private string ConvertTypeR()
        {
            string ResultR = "000";
            switch (Instruction)
            {
                case "add":
                    ResultR += "000";
                    break;
                case "nand":
                    ResultR += "001";
                    break;
            }
            ResultR += DecToBin(Field0);
            ResultR += DecToBin(Field1);
            ResultR += ExtendZero(DecToBin(Field2));
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
        private string ExtendZero(string bin)
        {
            return "";
        }
    }
}