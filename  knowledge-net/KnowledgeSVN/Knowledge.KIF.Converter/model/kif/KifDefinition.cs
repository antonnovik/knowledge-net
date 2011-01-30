using System;
using System.Collections.Generic;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.model.kif {
    public abstract class KifDefinition : KifForm {
//TODO: unrestricted | complete | partial, but now - only unrestricted
        
        public enum TYPE {
            UNRESTRICTED,
            COMPLETE,
            PARTIAL
        } ;


        private string _name;
        private string _string;
        private KifSequence<KifSentence> _sentences;

        public KifDefinition(string name, string str, KifSequence<KifSentence> sentences) {
            if (StringUtils.isEmpty(name))
                throw new ArgumentException("name cann't be null or empty");
            if (sentences == null || sentences.isEmpty())
                throw new ArgumentException("list of sentences cann't be null or empty");
            _name = name;
            _string = str;
            _sentences = new KifSequence<KifSentence>(sentences);
        }

        protected KifDefinition(string name, KifSequence<KifSentence> sentences)
            : this(name, null, sentences) {
        }

        public string Name {
            get { return _name; }
        }

        public string @String {
            get { return _string; }
        }

        public TYPE getType() {
            return TYPE.UNRESTRICTED;
        }

        public KifSequence<KifSentence> Sentences {
            get { return new KifSequence<KifSentence>(_sentences); }
        }

        protected IList<IModelItem> getInnerChildren() {
            return _sentences.getChildren();
        }

        protected abstract string getConstructionName();

        public override IList<IModelItem> getChildren() {
            List<IModelItem> result = new List<IModelItem>();
            LexemFactory lexemFactory = LexemFactory.getInstance();
            result.Add(lexemFactory.getLexem(LEFT_BRACKET));
            result.Add(lexemFactory.getLexem(getConstructionName()));
            result.Add(DELIMITER);
            result.Add(new KifConstant(Name));
            result.Add(DELIMITER);
            if (_string != null) {
                result.Add(new KifString(_string));
                result.Add(DELIMITER);
            }
            result.AddRange(getInnerChildren());
            result.Add(lexemFactory.getLexem(RIGHT_BRACKET));
            return result;
        }
    }
}