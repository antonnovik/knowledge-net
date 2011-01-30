using System;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifObjConst : KifTerm {
        public KifObjConst(String objreadonly) : base(objreadonly) {
        }

        public virtual String objreadonly() {
            return termString();
        }
    }
}