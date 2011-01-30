/*
 * Created by: M. Sigalin
 * Created: Tuesday, October 24, 2006
 */

using System;

namespace Knowledge.KIF.Converter.model.kif {
    
    public abstract class KifVariable: KifWord {
        public KifVariable(string name) : base(name) {
        }

        protected abstract string getPrefix();

        public override string ToString() {
            return getPrefix() + base.ToString();
        }

        public override bool Equals(Object o) {//TODO:
            KifVariable variable = o as KifVariable;
            return variable != null && getName().Equals(variable.getName()) && getPrefix().Equals(variable.getPrefix());
        }          
    }
}