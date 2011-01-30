/*
 * Created by: M. Sigalin
 * Created: Tuesday, October 24, 2006
 */

namespace Knowledge.KIF.Converter.model.kif {
    public class KifExistentiallyQuantifiedSentence: KifQuantifiedSentence {
        public static readonly string NAME = "exists";

        public KifExistentiallyQuantifiedSentence(KifSequence<KifVariableSpecification> varspec, KifSentence sentence)
            : base(varspec, sentence) {
        }

        public override string getName() {
            return NAME;
        }        
    }
}