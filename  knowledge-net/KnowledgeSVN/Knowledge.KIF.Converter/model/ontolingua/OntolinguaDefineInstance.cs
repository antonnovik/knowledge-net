using System;
using System.Collections.Generic;
using Knowledge.KIF.Converter.model.kif;

namespace Knowledge.KIF.Converter.model.ontolingua {
    public class OntolinguaDefineInstance : OntolinguaDefinition {
        public static readonly string CONSTRUCTION_NAME = "DEFINE-INSTANCE";

        public OntolinguaDefineInstance(string name, Comment comment, KifSequence<KifIndividualVariable> argList)
            : base(name, comment, argList) {
        }

        protected override string getConstructionName() {
            return CONSTRUCTION_NAME;
        }

        protected override string getBody() {
            return "";
        }

        protected override ICollection<IModelItem> getInnerChildren() {
            throw new NotImplementedException();
        }
    }
}