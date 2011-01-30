using System;
using System.Collections.Generic;

namespace Knowledge.KIF.Converter.model.ontolingua {
    public class DefFrame : OntolinguaDefinition {
        public static readonly string CONSTRUCTION_NAME = "DEFINE-FRAME";

        public DefFrame(string name, Comment comment)
            : base(name, comment, null) {
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