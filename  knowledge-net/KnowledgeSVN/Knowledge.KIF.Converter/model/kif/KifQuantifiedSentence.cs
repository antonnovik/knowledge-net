/*
 * Created by: M. Sigalin
 * Created: Tuesday, October 24, 2006
 */

namespace Knowledge.KIF.Converter.model.kif {
    
    public abstract class KifQuantifiedSentence: KifSentence {

        private KifSequence<KifVariableSpecification> _varspec;
        private KifSentence _sentence;
        public abstract string getName();

        protected KifQuantifiedSentence(KifSequence<KifVariableSpecification> varspec, KifSentence sentence) {
            _varspec = varspec;
            _sentence = sentence;
        }

        public KifSentence sentence {
            get { return _sentence; }
        }

        public KifSequence<KifVariableSpecification> varspec {
            get { return _varspec; }
        }

        public override string ToString() {
            return LEFT_BRACKET + getName() + SPACE +
                LEFT_BRACKET + _varspec.ToString() + RIGHT_BRACKET + 
                SPACE + _sentence.ToString() + RIGHT_BRACKET;
        }
    }
}