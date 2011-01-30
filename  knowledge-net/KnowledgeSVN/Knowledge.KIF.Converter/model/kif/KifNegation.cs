using System;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifNegation : KifLogicalSentence {

        public static readonly string NAME = "not";
        
        private KifSentence _sentence;
        
        public KifNegation(KifSentence sentence) {
            _sentence = sentence;
        }

        public virtual KifSentence getNegation() {
            return _sentence;
        }

        protected override string getName() {
            return NAME;
        }

        protected override KifSequence<KifSentence> getSequence() {
            return new KifSequence<KifSentence>(_sentence);
        }
    }
}