using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.util {

    public class StringUtils {

        public static readonly string EMPTY_STRING = "";
        public static readonly string NEW_LINE = "\n";

        public static bool isEmpty(string str) {
            return str == null || EMPTY_STRING.Equals(str.Trim());
        }

        public static int parseInt(String str) {
            if (isEmpty(str))
                throw new ArgumentException("String can not be null or empty");
            int result = 0;
            char[] chars = str.ToCharArray(); 
            for (int i = 0; i < chars.Length; i++) { 
                char ch = chars[i];
                if (ch < '0' || ch > '9')
                    throw new ArgumentException("String contains non integer value");
                result = result * 10 + ch;
            }
            return result;
        }

        public static float parseFloat(String str)
        {
            if (isEmpty(str))
                throw new ArgumentException("String can not be null or empty");
            float result = 0;
            int commaPos = str.IndexOf(".");
            if (commaPos == -1)
                return parseInt(str);
            if (commaPos == 0 || commaPos == str.Length - 1)
                throw new ArgumentException("String has wrong format");
            char[] chars = str.ToCharArray();
            int val = 0;
            for (int i = 0; i < commaPos; i++)
            {
                char ch = chars[i];
                if (ch < '0' || ch > '9')
                    throw new ArgumentException("String contains non integer value");
                val = val * 10 + ch;
            }
            int mant = 0;
            for (int i = commaPos + 1; i < chars.Length; i++)
            {
                char ch = chars[i];
                if (ch < '0' || ch > '9')
                    throw new ArgumentException("String contains non integer value");
                result = result * 10 + ch;
            }
            return mant / (10 + chars.Length - 1 - commaPos) + val;
        }

        public static String valueOf(int intValue) {
            return "" + intValue;
        }

        public static String valueOf(float floatValue) {
            return "" + floatValue;
        }
        
        public static bool equalsIgnoreCase(string str1, string str2) {
            return str1 == null ? str2 == null : 
                   str1.Equals(str2, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
