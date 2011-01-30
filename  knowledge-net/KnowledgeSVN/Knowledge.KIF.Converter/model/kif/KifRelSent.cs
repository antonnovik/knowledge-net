using System;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifRelSent : KifSentence {
        private String _relreadonly;
        private KifSequence<KifObject> _termSeq;


        public KifRelSent(String relreadonly, KifSequence<KifObject> termSeq) {
            _relreadonly = relreadonly;
            _termSeq = termSeq;
        }


        public KifRelSent(String relreadonly) {
            _relreadonly = relreadonly;
            _termSeq = null;
        }


        public virtual String relreadonly() {
            return _relreadonly;
        }


        public virtual KifSequence<KifObject> termSeq() {
            return _termSeq;
        }


        public override String ToString() {
            if (_termSeq == null)
                return KifObject.LEFT_BRACKET + _relreadonly + KifObject.RIGHT_BRACKET;

            return KifObject.LEFT_BRACKET + _relreadonly + KifObject.SPACE + _termSeq.ToString() + KifObject.RIGHT_BRACKET;
        }
    }
}