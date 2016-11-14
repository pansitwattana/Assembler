using System;
using System.Text;
using System.Collections.Generic;

namespace Assembler
{
    public class Assembly
    {
        Dictionary<string, int> fillValues;
        Dictionary<string, int> addressValues;
        public Assembly(string label, string inst, string f0, string f1, string f2, string type, Dictionary<string, int> fillValues, Dictionary<string, int> addressValues)
        {
            Label = label;
            Instruction = inst;
            Field0 = f0;
            Field1 = f1;
            Field2 = f2;
            Type = type;
            this.fillValues = fillValues;
            this.addressValues = addressValues;
        }

        public string MachineCode { get; set; }
        public string Label { get; set; }
        public string Instruction { get; set; }
        private string field0;

        public string Field0
        {
            get { return field0; }
            set
            {
                field0 = (value);
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
                field1 = (value);
            }
        }

        public string GetMachine()
        {
            return Convert.ToInt32(MachineCode, 2).ToString();
        }

        private string GetValue(string value)
        {
            return (value);
        }

        private string field2;

        public string Field2
        {
            get { return field2; }
            set
            {
                field2 = (value);
            }
        }

        public string Type { get; set; }
        public override string ToString()
        {
            return Label + "\t" + Instruction + "\t" + Field0 + "\t" + Field1 + "\t" + Field2 + "\t";
        }

        public void ToMachine()
        {
            switch (Type)
            {
                case "R":
                    MachineCode = ConvertTypeR();
                    break;
                case "I":
                    MachineCode = ConvertTypeI();
                    break;
                case "J":
                    MachineCode = ConvertTypeJ();
                    break;
                case "O":
                    MachineCode = ConvertTypeO();
                    break;
                default:
                    MachineCode = DecToBinaryWithMaxBit(CheckFillValue(Field0), 25);
                    break;
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
            ResultO += DecToBinaryWithMaxBit("0", 22);
            return ResultO;
        }

        private string ConvertTypeJ()
        {
            string ResultJ = "";
            switch (Instruction)
            {
                case "jalr":
                    ResultJ += "101";
                    break;
            }

            /*int value = Int32.Parse(Field0);
            if (CheckJalr(value))
            {
                //regA
                //regB
                
            }
            else
            {
                Console.WriteLine("404 Your address not found");
            }*/

            ResultJ += DecToBinaryWithMaxBit(Field0, 3);
            ResultJ += DecToBinaryWithMaxBit(Field1, 3);
            ResultJ += DecToBinaryWithMaxBit("0", 16);
            return ResultJ;
        }

        private string ConvertTypeI()
        {
            string ResultI = "";
            switch (Instruction)
            {
                case "lw":
                    ResultI += "010";
                    Field2 = CheckFillValue(Field2);
                    break;
                case "sw":
                    ResultI += "011";
                    Field2 = CheckFillValue(Field2);
                    break;
                case "beq":
                    ResultI += "100";
                    Field2 = CheckBranch(Field2);
                    break;

            }
            ResultI += DecToBinaryWithMaxBit(Field0, 3);
            ResultI += DecToBinaryWithMaxBit(Field1, 3);
            ResultI += DecToBinaryWithMaxBit(Field2, 16);
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

            //regA
            ResultR += DecToBinaryWithMaxBit(Field0, 3);    
            //regB      
            ResultR += DecToBinaryWithMaxBit(Field1, 3);
            ResultR += DecToBinaryWithMaxBit("0", 13);
            ResultR += DecToBinaryWithMaxBit(Field2, 3);      
            //test
            //ResultR += " " + DecToBinaryWithMaxBit("-2",10);
            return ResultR;
        }

        //Dec to Bin
       /* private string DecToBin(string Binary)
        {

            //convert string to integer
            //int m = Int32.Parse("abc");
            int value = Int32.Parse(Binary);
            //string str = Convert.ToString(value, 2);
            //str = str.Substring(Math.Max(str.Length - 8, 0)).PadLeft(8, '0');
            string bin = Convert.ToString(value, 2);
            return bin;
        }*/

        //zeroextend
        /*private string ExtendZero(string bin, int max)
        {
            string str;
            char pad = '0';
            str = bin.PadLeft(max, pad);
            return str;
        }*/
        
        private string DecToBinaryWithMaxBit(string dec, int bit)
        {
            
            int value = Int32.Parse(dec);
            if (value >= 0)
            {
                string result = Convert.ToString(value, 2).PadLeft(bit, '0');
                return result;
            }
            else
            {
                string result = Convert.ToString(value, 2);
                result = result.Substring(Math.Max(result.Length - bit, 0)).PadLeft(bit, '0');
                return result;
            }
            
        }

        private string CheckBranch(string field2)
        {
            int str1 = 0;
            bool isNumberic = int.TryParse(field2, out str1);   
            if (isNumberic == false && field2 != "")
            {
                int currentAddr = Program.assembies.IndexOf(this);
                int branchAddr = 0;
                if (addressValues.ContainsKey(field2))
                {
                    branchAddr = addressValues[field2];
                }
                int branchResult = branchAddr - currentAddr - 1;
                return "" + branchResult;
            }
            else
            {
                return field2;
            }
        }

        private string CheckFillValue(string f)
        {
            string str = "";
            {
                int str1 = 0;
                bool isNumeric = int.TryParse(f, out str1);
                if (isNumeric == false && f != "")
                {
                    if (fillValues.ContainsKey(f))
                        str = "" + addressValues[f];
                    //    str = "" + fillValues[f];
                    //else if(addressValues.ContainsKey(f))
                    //    str = "" + addressValues[f];
                    //else
                        //Console.WriteLine("Something wrong in code (CheckIfFillValue)");
                        
                }
                else if (isNumeric == true)
                {
                    str = f;
                }

                return str;
            }
        }
        /*
        //check -> can jump? + jump addr.  from field1
        private bool CheckJalr(int f0)
        {
          
            //int value = Int32.Parse(f0);
            int CountLabel = Program.assembies.Count - Global.fillValues.Count;
            if (f0 > 0 && f0 <= CountLabel)
            {
                return true;
            }else
            {               
                return false;
            }
         
        }

        //check Branch from field2
        private bool CheckBranch(int f2)
        {
            int CountLabel = Program.assembies.Count - Global.fillValues.Count;
            int UpLabel = 0 - Program.assembies.IndexOf(this);
            int DownLabel = CountLabel - Program.assembies.IndexOf(this) - 1;
            //check top label of this label
            if (f2 < 0 && f2 >= UpLabel)
            {
                return true;
            }
            else if (f2 <= DownLabel && f2 >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
             
           
        }*/
    }
}