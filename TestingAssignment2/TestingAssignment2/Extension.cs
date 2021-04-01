using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAssignment2
{
    public static class Extension
    {
        public static int WordCount(this string inputStr)
        {
            string[] words = inputStr.Split(' ');

            return words.Length;
        }

        public static bool NumberValidation(this string inputStr)
        {
            int n;
            bool isNum = int.TryParse(inputStr, out n);
            return isNum;
        }

        public static string ConvertLowerCase(this string inputStr)
        {
            StringBuilder sb = new StringBuilder(inputStr);
            int stringLength = sb.Length;

            for (int j = 0; j < stringLength; j++)
            {
                if (sb[j] >= 'A' && sb[j] <= 'Z')
                    sb[j] = (char)(sb[j] + 32);
            }
            return sb.ToString();
        }

        public static string ConvertUpperCase(this string inputStr)
        {
            StringBuilder sb = new StringBuilder(inputStr);
            int stringLength = sb.Length;

            for (int j = 0; j < stringLength; j++)
            {
                if (sb[j] >= 'a' && sb[j] <= 'z')
                    sb[j] = (char)(sb[j] - 32);
            }
            return sb.ToString();
        }

        public static bool CheckLowerCase(this string inputStr)
        {
            int stringLength = inputStr.Length;

            for (int j = 0; j < stringLength; j++)
            {
                if (inputStr[j] >= 'A' && inputStr[j] <= 'Z')
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CheckUpperCase(this string inputStr)
        {
            int stringLength = inputStr.Length;

            for (int j = 0; j < stringLength; j++)
            {
                if (inputStr[j] >= 'a' && inputStr[j] <= 'z')
                {
                    return false;
                }
            }
            return true;
        }

        public static string FirstUpperLetter(this string inputStr)
        {
            if (inputStr == null)
                return null;

            if (inputStr.Length > 1)
                return char.ToUpper(inputStr[0]) + inputStr.Substring(1);

            return inputStr.ToUpper();
        }

        public static string LastCharacterRemove(this string inputStr)
        {
            if (inputStr == null)
                return null;
            else
                return inputStr.Substring(0, inputStr.Length - 1);
        }

        public static int StringToInt(this string inputStr)
        {
            int x = 0;

            Int32.TryParse(inputStr, out x);
            return x;
        }

        public static string ConvertTitleCase(this string inputStr)
        {
            TextInfo textInfo = new CultureInfo("en-us", false).TextInfo;
            return textInfo.ToTitleCase(inputStr);
        }
    }
}
