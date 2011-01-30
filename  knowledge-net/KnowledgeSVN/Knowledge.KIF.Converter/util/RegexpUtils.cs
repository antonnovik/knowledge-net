/*
 * Created by: M. Sigalin
 * Created: Sunday, October 29, 2006
 */

using System.Text.RegularExpressions;

namespace Knowledge.KIF.Converter.util {//TODO: delete
    public class RegexpUtils {
        private RegexpUtils() {} //
        
        public static bool IsMatch(string str, Regex regex) {//TRUE, if whole string matches with pattern
            Match match = regex.Match(str);
            return match.Success && str.Equals(match.Value);
        }

        public static bool IsMatch(string str, string regexp) {
            return IsMatch(str, new Regex(regexp));
        }
    
    }
}