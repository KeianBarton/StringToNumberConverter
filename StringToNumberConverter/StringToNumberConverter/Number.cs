using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringToNumberConverter
{
    public class Number
    {
        private readonly string stringValue;
        private readonly double numberValue;

        public Number(string value)
        {
            stringValue = value.ToLower();
            numberValue = ConverterHelper.Convert(stringValue);
        }

        public override string ToString()
        {
            return stringValue;
        }

        public double ToNumber()
        {
            return numberValue;
        }
    }
}
