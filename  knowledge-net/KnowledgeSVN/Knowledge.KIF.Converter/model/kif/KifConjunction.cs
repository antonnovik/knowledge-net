namespace Knowledge.KIF.Converter.model.kif {
    public class KifConjunction : KifLogicalSentence {
        public static readonly string NAME = "AND";

        private KifSequence<KifSentence> _params;

        public KifConjunction(KifSequence<KifSentence> sentSeq) {
            _params = sentSeq;
        }

        public KifSequence<KifSentence> getConjuncts() {
            return _params;
        }

        protected override string getName() {
            return NAME;
        }

        protected override KifSequence<KifSentence> getSequence() {
            return getConjuncts();
        }
    }
}