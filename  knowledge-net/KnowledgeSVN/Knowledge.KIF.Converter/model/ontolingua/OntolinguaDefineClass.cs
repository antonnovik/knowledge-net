using System;
using Knowledge.KIF.Converter.model.kif;

namespace Knowledge.KIF.Converter.model.ontolingua {
    public class OntolinguaDefineClass : ClassAndRelationBase {
        public static readonly string CONSTRUCTION_NAME = "DEFINE-CLASS";

        private static KifSequence<KifIndividualVariable> checkArgs(KifIndividualVariable arg) {
            if (arg == null)
                throw new ArgumentNullException("individual variable cann't be null");
            return new KifSequence<KifIndividualVariable>(arg);
        }
        
        public OntolinguaDefineClass(string name, Comment comment, KifIndividualVariable arg)
            : base(name, comment, checkArgs(arg)) {
        }

        protected override string getConstructionName() {
            return CONSTRUCTION_NAME;
        }

    }
}