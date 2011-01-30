using System;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifInteger : KifTerm {


        public KifInteger(String intString) : base(intString) {
        }

        public KifInteger(int intValue) {
            _termString = StringUtils.valueOf(intValue);
        }
    }
}