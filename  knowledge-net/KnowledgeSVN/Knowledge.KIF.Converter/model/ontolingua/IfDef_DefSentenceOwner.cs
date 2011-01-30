/*
 * Created by: M. Sigalin
 * Created: Thursday, October 26, 2006
 */

using System.Collections.Generic;
using Knowledge.KIF.Converter.model.kif;

namespace Knowledge.KIF.Converter.model.ontolingua {
    public abstract class IfDef_DefSentenceOwner : OntolinguaDefinition {
        public static readonly string DEF_SENTENCE = ":DEF";
        public static readonly string IF_DEF_SENTENCE = ":IF-DEF";

        private OntolinguaNamedSentence _defSentence;

        private OntolinguaNamedSentence _ifDefSentence;


        public IfDef_DefSentenceOwner(string name, Comment comment, KifSequence<KifIndividualVariable> argList)
            : base(name, comment, argList) {
        }

        public KifSentence IfDefSentence {
            get { return _ifDefSentence.Sentence; }
            set {
                if (value != null) {
                    _ifDefSentence = new OntolinguaNamedSentence(IF_DEF_SENTENCE, value);
                    _defSentence = null;
                } else
                    _ifDefSentence = null;
            }
        }

        public KifSentence DefSentence {
            get { return _defSentence.Sentence; }
            set {
                if (value != null) {
                    _defSentence = new OntolinguaNamedSentence(DEF_SENTENCE, value);
                    _ifDefSentence = null;
                } else
                    _defSentence = null;
            }
        }


        protected override ICollection<IModelItem> getInnerChildren() {
            List<IModelItem> list = new List<IModelItem>();
            if (_defSentence != null)
                list.Add(_defSentence);
            else if (_ifDefSentence != null)
                list.Add(_ifDefSentence);
            return list;
        }
    }
}