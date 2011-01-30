/*
 * Created by: M. Sigalin
 * Created: Tuesday, October 24, 2006
 */

namespace Knowledge.KIF.Converter.model.kif {
    public class KifSequenceVariable: KifVariable {
        public static readonly string PREFIX = "@";

        public KifSequenceVariable(string name): base(name) {
        }

        protected override string getPrefix() {
            return PREFIX;
        }        
    }
}