/*
 * Created by: M. Sigalin
 * Created: Monday, October 30, 2006
 */

using System;
using System.Collections.Generic;
using Knowledge.KIF.Converter.model.kif;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.model.ontolingua {
    public class OntolinguaNamedSequenceOfSentences: OntolinguaObject {
        private string _name;
        private KifSequence<KifSentence> _sentences;

        public override IList<IModelItem> getChildren() {//TODO:
            List<IModelItem> result = new List<IModelItem>(_sentences.getSize() + 4);
            result.Add(new KifConstant(_name));
            result.Add(DELIMITER);
            result.Add(LexemFactory.getInstance().getLexem(LEFT_BRACKET));
            result.AddRange(_sentences.getChildren());
            result.Add(LexemFactory.getInstance().getLexem(RIGHT_BRACKET));
//            KifSequence<KifObject> sequence = new KifSequence<KifObject>(new KifConstant(Name));
//            sequence.addItem(Sentence);
//            return sequence.getChildren();
            return result;
        }

        public OntolinguaNamedSequenceOfSentences(string name, KifSequence<KifSentence> sentences) {
            if (StringUtils.isEmpty(name))
                throw new ArgumentException("name cann't be null or empty");
            if (sentences == null || sentences.getSize() == 0)
                throw new ArgumentNullException("sentences cann't be null or empty");
            _name = name;
            _sentences = new KifSequence<KifSentence>(sentences);
        }

        public KifSequence<KifSentence> Sentences {
            get { return new KifSequence<KifSentence>(_sentences); }
        }

        public string Name {
            get { return _name; }
        }        
    }
}