using Knowledge.KIF.Converter.model.kif;

namespace Knowledge.KIF.Converter.model.ontolingua {
    public class OntolinguaDefineRelation : ClassAndRelationBase {
//TODO: add when and documentation
        public static readonly string CONSTRUCTION_NAME = "DEFINE-RELATION";

        public OntolinguaDefineRelation(string name, Comment comment, KifSequence<KifIndividualVariable> argList)
            : base(name, comment, argList) {
        }

        protected override string getConstructionName() {
            return CONSTRUCTION_NAME;
        }

        protected override string getBody() {
            return "";
        }
    }
}