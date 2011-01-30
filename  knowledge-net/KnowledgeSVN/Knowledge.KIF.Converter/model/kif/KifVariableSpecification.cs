/*
 * Created by: M. Sigalin
 * Created: Tuesday, October 24, 2006
 */

namespace Knowledge.KIF.Converter.model.kif {
    public class KifVariableSpecification: KifObject {
        
        public KifVariableSpecification(KifVariable variable): this(variable, null) {
        }

        public KifVariableSpecification(KifVariable variable, KifConstant constant) {
            _variable = variable;
            _constant = constant;
        }

        private KifVariable _variable;
        private KifConstant _constant;

        public KifVariable variable {
            get { return _variable; }
        }

        public KifConstant constant {
            get { return _constant; }
        }

        public override string ToString() {
            return _constant == null ? _variable.ToString() : 
                   KifObject.LEFT_BRACKET + _variable.ToString() + 
                   KifObject.SPACE + _constant.ToString() + 
                   KifObject.RIGHT_BRACKET;
        }
    }
}