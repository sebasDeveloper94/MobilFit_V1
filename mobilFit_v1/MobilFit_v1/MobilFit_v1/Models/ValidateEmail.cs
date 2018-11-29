using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MobilFit_v1.Models
{
    class ValidateEmail
    {
        public static bool Validate(string inputText)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(inputText, expresion))
            {
                if (Regex.Replace(inputText, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
