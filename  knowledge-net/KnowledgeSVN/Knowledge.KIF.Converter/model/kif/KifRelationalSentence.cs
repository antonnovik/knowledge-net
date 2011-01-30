/*
 * Created by: 
 * Created: Tuesday, October 24, 2006
 */

using System;
using System.Collections.Generic;

namespace Knowledge.KIF.Converter.model.kif {
    
    public class KifRelationalSentence: KifSentence {//Implicit form

        private KifConstant _constant;
        private KifSequenceVariable _variable;
        private KifSequence<KifTerm> _terms;

        public KifRelationalSentence(KifConstant constant, KifSequence<KifTerm> terms, KifSequenceVariable variable) {
            if (constant == null)
                throw new ArgumentNullException("Constant cann't be null");
            _constant = constant;
            _variable = variable;
            _terms = new KifSequence<KifTerm>(terms);
        }

        public KifConstant Constant {
            get { return _constant; }
        }

        public KifSequenceVariable SequenceVariable {
            get { return _variable; }
        }

        public KifSequence<KifTerm> Terms {
            get { return new KifSequence<KifTerm>(_terms); }
        }

        public KifRelationalSentence(KifConstant constant, KifSequence<KifTerm> terms): this(constant, terms, null) {
        }

        public KifRelationalSentence(KifConstant constant, KifTerm term)
            : this(constant, new KifSequence<KifTerm>(term), null) {
        }

        public override IList<IModelItem> getChildren() {
            List<IModelItem> list = new List<IModelItem>();
            list.Add(LexemFactory.getInstance().getLexem(LEFT_BRACKET));
            list.Add(_constant);
            if (!_terms.isEmpty()) {
                list.Add(DELIMITER);
                list.AddRange(_terms.getChildren());
            }
            if (_variable != null) {
                list.Add(DELIMITER);
                list.Add(_variable);
            }
            list.Add(LexemFactory.getInstance().getLexem(RIGHT_BRACKET));
            return list;
        }
    }
}