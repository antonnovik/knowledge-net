using System;
using System.Text.RegularExpressions;

namespace Knowledge.KIF.Converter.model.kif {
    
    public class KifWord : KifObject {
        
        public static readonly string UPPER_CHAR_REG = "[A-Z]";
        public static readonly string LOWER_CHAR_REG = "[a-z]";
        public static readonly string DIGIT_CHAR_REG = @"\d";
        public static readonly string ALPHA_CHAR_REG = @"[\!\$%&\*\+\-\./<=>\?\@_~]";
        public static readonly string WHITE_CHAR_REG = "[\f\n\r\t\v]"; //TODO: \s and ECMAScript
        public static readonly string SPECIAL_CHAR_REG = "[{\"#'(),\\^`]"; //TODO: how escape()?

        public static readonly string NORMAL_CHAR_REG = "(" + UPPER_CHAR_REG + "|" + LOWER_CHAR_REG + "|" +
                                                        DIGIT_CHAR_REG + "|" + ALPHA_CHAR_REG + ")";

        public static readonly Regex WORD_REG = new Regex("^" + NORMAL_CHAR_REG + "+$"); //TODO: Multiline

        private string _name;

        public static bool isWord(string str) {//TODO: add escaping ch
            return str != null && WORD_REG.IsMatch(str);
        }

        private void checkString(string str) {
            if (!isWord(str))
                throw new ArgumentException("Illegal string"); //TODO: inform
            //TODO: need real check and refactoring
        }

        public KifWord(string name) {
            checkString(name);
            _name = name;
        }

        public virtual string getName() {
            return _name;
        }

        public override string ToString() {
            return _name;
        }

        public override bool Equals(Object o) {
            KifWord word = o as KifWord;
            return word != null && _name.Equals(word.getName());
        }
    }
}