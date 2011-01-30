using System;
using System.Collections.Generic;
using Knowledge.KIF.Converter.model.kif;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.model.ontolingua {
    abstract public class OntolinguaDefinition : OntolinguaObject {
        private string _name;
        private Comment _docString;
        private KifSequence<KifIndividualVariable> _argList;
        //TODO: argument string

        public OntolinguaDefinition(string name, Comment comment, KifSequence<KifIndividualVariable> argList) {
            if (StringUtils.isEmpty(name))
                throw new ArgumentException("name cann't be null or empty");
            _name = name;
            _docString = comment;
            _argList = argList == null ? argList : new KifSequence<KifIndividualVariable>(argList);
        }

        protected abstract string getConstructionName();

        public virtual string getName() {
            return _name;
        }

        public virtual Comment getComment() {
            return _docString;
        }

        protected abstract string getBody();

        public override string ToString() {
            return LEFT_BRACKET + getConstructionName() + SPACE + getName() + getBody() + RIGHT_BRACKET; //TODO:
        }

        public virtual KifSequence<KifIndividualVariable> getArumentList() {
            return new KifSequence<KifIndividualVariable>(_argList);
        }

        protected abstract ICollection<IModelItem> getInnerChildren(); //TODO:

        public override IList<IModelItem> getChildren() {
            List<IModelItem> list = new List<IModelItem>();
            list.Add(LexemFactory.getInstance().getLexem(LEFT_BRACKET));
            list.Add(LexemFactory.getInstance().getLexem(getConstructionName()));
            list.Add(new KifConstant(getName()));
            if (_argList != null && !_argList.isEmpty()) {
                list.Add(LexemFactory.getInstance().getLexem(LEFT_BRACKET)); //TODO:
                list.AddRange(_argList.getChildren());
                list.Add(LexemFactory.getInstance().getLexem(RIGHT_BRACKET));
            }
            if (_docString != null)
                list.Add(new KifSequence<KifObject>(_docString));
            list.AddRange(getInnerChildren());
            list.Add(LexemFactory.getInstance().getLexem(RIGHT_BRACKET));
            return list;
        }

        public override bool isLeaf() {
            return false;
        }
    }
}