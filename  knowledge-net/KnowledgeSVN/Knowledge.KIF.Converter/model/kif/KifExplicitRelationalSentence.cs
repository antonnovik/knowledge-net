/*
 * Created by: M. Sigalin
 * Created: Thursday, October 26, 2006
 */

using System;

namespace Knowledge.KIF.Converter.model.kif {
    
    public class KifExplicitRelationalSentence: KifRelationalSentence {
        public static readonly string CONSTANT_NAME = "holds";
        
        public KifExplicitRelationalSentence(KifSequence<KifTerm> terms, KifSequenceVariable variable) : base(new KifConstant(CONSTANT_NAME), terms, variable) {
            if (terms.isEmpty())
                throw new ArgumentException("Sequence of terms cann't be empty");
        }

        public KifExplicitRelationalSentence(KifSequence<KifTerm> terms)
            : base(new KifConstant(CONSTANT_NAME), terms) {
        }
        
        
    }
}