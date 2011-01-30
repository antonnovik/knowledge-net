using System;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifImplication : KifLogicalSentence {

        public static readonly string NAME = "=>";

        private KifSentence _consequent;
        private KifSequence<KifSentence> _antecedents;

        public KifImplication(KifSentence consequent, KifSequence<KifSentence> antecedents) {
            _antecedents = antecedents;
            _consequent = consequent;
        }

        public KifImplication(KifSentence consequent) {
            _consequent = consequent;
        }


        public virtual KifSequence<KifSentence> getAntecedents() {
            return _antecedents;
        }

        public virtual KifSentence getConsequent() {
            return _consequent;
        }

        protected override string getName() {
            return NAME;
        }

        protected override KifSequence<KifSentence> getSequence() {
            return new KifSequence<KifSentence>(getAntecedents(), getConsequent());
        }
    }
}