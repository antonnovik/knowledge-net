/*
 * Created by: M. Sigalin
 * Created: Tuesday, October 24, 2006
 */

namespace Knowledge.KIF.Converter.model.kif {
    public class KifReverseImplication: KifImplication {

        public static readonly new string NAME = "<=";
        
        public KifReverseImplication(KifSentence consequent, KifSequence<KifSentence> antecedents) : base(consequent, antecedents) {
        }
        public KifReverseImplication(KifSentence consequent)
            : base(consequent) {
        }
        protected override string getName() {
            return NAME;
        }

        protected override KifSequence<KifSentence> getSequence() {
            return new KifSequence<KifSentence>(getConsequent(), getAntecedents());
        }        
    }
}