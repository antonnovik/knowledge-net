using System;
using System.Collections.Generic;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifFunctionalTerm : KifTerm {//descr. implicit functional term
        
        private KifSequence<KifTerm> _termSeq;
        private KifSequenceVariable _sequenceVar;

        public KifFunctionalTerm(String constantName): this(constantName, null, null) {
        }

        public KifFunctionalTerm(String constantName, KifSequence<KifTerm> termSeq, KifSequenceVariable sequenceVariable): base(constantName) {
            _termSeq = new KifSequence<KifTerm>(termSeq);
            _sequenceVar = sequenceVariable;
        }


        public virtual string getConstant() {
            return termString();
        }

        public virtual KifSequence<KifTerm> termSeq() {
            return new KifSequence<KifTerm>(_termSeq);
        }

        public KifSequenceVariable SequenceVariable {
            get { return _sequenceVar; }
        }

        public override String ToString() {
            if (_termSeq != null)
                return (LEFT_BRACKET + _termString + SPACE + _termSeq.ToString() + RIGHT_BRACKET);

            return LEFT_BRACKET + _termString + RIGHT_BRACKET;
        }

        public override IList<IModelItem> getChildren() {
            List<IModelItem> result = new List<IModelItem>();
            LexemFactory lexemFactory = LexemFactory.getInstance();
            result.Add(lexemFactory.getLexem(LEFT_BRACKET));
            result.Add(new KifConstant(getConstant()));
            if (!(_termSeq == null || _termSeq.isEmpty())) {
                result.Add(DELIMITER);
                result.AddRange(_termSeq.getChildren());
            }
            if (_sequenceVar != null) {
                result.Add(DELIMITER);
                result.Add(_sequenceVar);
            }
            result.Add(lexemFactory.getLexem(RIGHT_BRACKET));
            return result;
        }
    }
}