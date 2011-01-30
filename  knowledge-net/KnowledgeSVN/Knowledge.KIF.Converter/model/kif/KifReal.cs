using System;
using System.Collections.Generic;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifReal : KifTerm {
        /**
	   Create an instance of KifReal(real number) from a float number
           in String format.
           @param floatString the float number in string format.
        */

        public KifReal(String floatString): base(floatString) {
        }


        /**
	   Create an instance of KifReal(real number) from a float number.
           @param floatValue the float number.
        */

        public KifReal(float floatValue) {
            _termString = StringUtils.valueOf(floatValue);
        }

        public override IEnumerator<IModelItem> getChildrenIterator() {
            throw new NotImplementedException();
        }
    }
}