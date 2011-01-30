namespace Knowledge.KIF.Converter.model.kif {
    
    public class KifDisjunction : KifLogicalSentence {
        
        public static readonly string NAME = "OR";

        private KifSequence<KifSentence> _params;

        public KifDisjunction(KifSequence<KifSentence> sentSeq) {
            _params = sentSeq;
        }

        public KifSequence<KifSentence> getDisjuncts() {
            return _params;
        }

        protected override string getName() {
            return NAME;
        }

        protected override KifSequence<KifSentence> getSequence() {
            return getDisjuncts();
        } 
    }
}