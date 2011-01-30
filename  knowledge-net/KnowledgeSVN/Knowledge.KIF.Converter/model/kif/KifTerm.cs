using System;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.model.kif {//TODO: need refactoring
    public abstract class KifTerm : KifObject {

        protected String _termString;

        public KifTerm(String termString) {
            if (StringUtils.isEmpty(termString))
                throw new ArgumentException("termString cann't be null or empty");
            
            _termString = termString;
        }

        protected KifTerm() {
            _termString = null;
        }
        
        public virtual String termString() {
            return _termString;
        }


        //TODO:
        public virtual int intValue() {
            return StringUtils.parseInt(_termString);
        }


        //TODO:
        public virtual float floatValue() {
            return StringUtils.parseFloat(_termString);
        }

        public override String ToString() {
            return _termString;
        }
    }
}