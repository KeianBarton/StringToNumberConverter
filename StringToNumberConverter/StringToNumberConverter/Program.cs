using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringToNumberConverter
{
    public class Program
    {
        private static readonly ConverterHelper converterHelper = new ConverterHelper();

        static void Main(string[] args)
        {
            string consoleInput;
            Console.Write("The following console application can be used to "+
                "convert a number between minus -99,999,999,999 and 99,999,999,999." +
                "\nType \"exit\" to exit the applicaiton."+
                "\nNote that hyphenated words are currently not supported e.g. sixty-four should be written as sixty four"+
                "\n -------------------------------------------------------"+
                "-----------------------------------------------------------"+
                "\n\nPlease type in the number you wish to convert: ");
            consoleInput = Console.ReadLine();
            while (true && !string.Equals(consoleInput.ToLower(),"exit",StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    var numberToConvert = new Number(consoleInput, converterHelper).ToNumber();
                    string formattedOutput;
                    if (numberToConvert == 0)
                    {
                        formattedOutput = "0";
                    }
                    else
                    {
                        formattedOutput = String.Format("{0:##,###,###,###}", numberToConvert);
                    }
                    Console.Write(formattedOutput+"\n");
                }
                catch (Exception)
                {
                    Console.Write("\nThere was an error converting the number " + consoleInput + "." +
                        "\nAre you sure it is in the correct format?\n");
                }
                Console.Write("\nPlease type in the number you wish to convert: ");
                consoleInput = Console.ReadLine();
            }
        }
    }
}
