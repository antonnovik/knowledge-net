/*
 * Created by: M. Sigalin
 * Created: Tuesday, October 24, 2006
 */

namespace Knowledge.KIF.Converter.model.kif {
    public class KifUniversallyQuantifiedSentence : KifQuantifiedSentence {
        public static readonly string NAME = "forall";

        public KifUniversallyQuantifiedSentence(KifSequence<KifVariableSpecification> varspec, KifSentence sentence)
            : base(varspec, sentence) {
        }

        public override string getName() {
            return NAME;
        }
    }
}