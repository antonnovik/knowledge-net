/*
 * Created by: M. Sigalin
 * Created: Monday, November 06, 2006
 */

using System;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifExplicitFunctionalTerm: KifFunctionalTerm {
        
        public static readonly string CONSTANT_NAME = "value";

        private static KifSequence<KifTerm> checkSeqTerm(KifSequence<KifTerm> termSeq) {
            if (termSeq == null || termSeq.isEmpty())
                throw new ArgumentException("Sequence of terms cann't be null or empty");
            return termSeq;
        }
        
        public KifExplicitFunctionalTerm(KifSequence<KifTerm> termSeq, KifSequenceVariable sequenceVariable) : base(CONSTANT_NAME, checkSeqTerm(termSeq), sequenceVariable) {
        }
    }
}