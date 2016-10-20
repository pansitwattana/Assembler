using System;

namespace Assembler
{
    public class Assembly
    {
        public string Label { get; set; }
        public string Instruction { get; set; }
        public string Field0 { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Comment { get; set; }
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
            return "";
        }

        private string ConvertTypeI()
        {
            return "";
        }

        private string ConvertTypeR()
        {
            return "";
        }
    }
}