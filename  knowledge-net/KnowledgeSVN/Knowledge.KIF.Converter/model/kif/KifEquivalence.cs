namespace Knowledge.KIF.Converter.model.kif {
    public class KifEquivalence : KifLogicalSentence {
        public static readonly string NAME = "<=>";

        private KifSentence _leftSentence, _rightSentence;

        public KifEquivalence(KifSentence leftSentence, KifSentence rightSentence) {
            _leftSentence = leftSentence;
            _rightSentence = rightSentence;
        }


        public virtual KifSentence getLeftSentence() {
            return _leftSentence;
        }

        public virtual KifSentence getRightSentence() {
            return _rightSentence;
        }

        protected override string getName() {
            return NAME;
        }

        protected override KifSequence<KifSentence> getSequence() {
            return new KifSequence<KifSentence>(getLeftSentence(), new KifSequence<KifSentence>(getRightSentence()));
        }
    }
}