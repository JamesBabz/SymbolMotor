using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymbolMotor
{
    public static class StringLibrary
    {
        public static bool StartsWithUpper(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            var ch = str[0];
            return char.IsUpper(ch);
        }

        public static int RandomFromString(this string str)
        {
            var rand = new Random();
            Console.WriteLine(str.Length);
            return rand.Next();
        }

        public static bool Tester(this object sm)
        {
            var type = sm.GetType();
            while (true)
            {
                if (type == null || type.ToString().Contains(".Object")) return false;
                if (type.ToString().Contains(".SymbolModel")) return true;
                type = type.BaseType;
            }
        }

    }
}
