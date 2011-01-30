using System;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifLogicalConstant : KifSentence {//TODO: or constant???

        public static readonly string TRUE = "true";
        public static readonly string FALSE = "false";

        private bool _boolValue;

        public KifLogicalConstant(string value) /*throws KifRTException*/ {
            if (TRUE.Equals(value.ToLower()))
                _boolValue = true;
            else if (FALSE.Equals(value.ToLower()))
                _boolValue = false;

            else
                throw (new KifRTException("Illegal logical constant"));
        }


        public KifLogicalConstant(bool value) {
            _boolValue = value;
        }


        public virtual bool boolValue() {
            return _boolValue;
        }


        public override String ToString() {
            return _boolValue ? TRUE : FALSE;
        }
    }
}