using System;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifSeqvar : KifObject {
        private String _varName;

        public KifSeqvar(String varName) {
            _varName = varName;
        }

        public virtual String varName() {
            return _varName;
        }

        public override String ToString() {
            return _varName;
        }
    }
}