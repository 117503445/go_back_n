using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace go_back_n
{
   public static class Padding
    {
        public static string GetPaddingString(string s)
        {
            while (s.Length < 46)
            {
                s += '\0';
            }
            return s;
        }
        public static string GetRawString(string s)
        {
            while (s[s.Length - 1] == '\0')
            {
                s = s.Remove(s.Length - 1, 1);
            }
            return s;
        }
    }
}
