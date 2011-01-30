using System;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifString : KifTerm {//TODO: another types???
        
        public static readonly string DOUBLE_QUOTE = "\"";
        
        public override string ToString() {
            return DOUBLE_QUOTE + base.ToString() + DOUBLE_QUOTE;
        }

        public KifString(String str) : base(str) {
        }
    }
}