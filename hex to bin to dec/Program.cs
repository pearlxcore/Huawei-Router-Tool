using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hex_to_bin_to_dec
{
    class Program
    {
        static List<string> mylist = new List<string>(new string[] { "0002000000000000" });
        

        static void Main(string[] args)
        {
            long total = 0;

            foreach (var value in mylist)
            {
                //long longval = Convert.ToInt64(value);
                long decValue = Int64.Parse(value, System.Globalization.NumberStyles.HexNumber);
                total = total + decValue;
            }

            //dec to hex
            string hexValue = total.ToString("X");

            Console.WriteLine(hexValue);
            Console.ReadKey();
        }
    }
}
