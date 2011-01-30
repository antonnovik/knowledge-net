/*
 * Created by: M. Sigalin
 * Created: Thursday, October 26, 2006
 */

using System;
using System.Collections.Generic;
using Knowledge.KIF.Converter.model.kif;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.model.ontolingua {
    
    public class OntolinguaNamedSentence: OntolinguaObject {
        private string _name;
        private KifSentence _sentence;

        public override IList<IModelItem> getChildren() {
            KifSequence<KifObject> sequence = new KifSequence<KifObject>(new KifConstant(Name));
            sequence.addItem(DELIMITER);
            sequence.addItem(Sentence);
            return sequence.getChildren();
        }

        public OntolinguaNamedSentence(string name, KifSentence sentence) {
            if (StringUtils.isEmpty(name))
                throw new ArgumentException("name cann't be null or empty");
            if (sentence == null)
                throw new ArgumentNullException("sentence cann't be null");
            _name = name;
            _sentence = sentence;
        }

        public KifSentence Sentence {
            get { return _sentence; }
        }

        public string Name {
            get { return _name; }
        }
    }
    
}