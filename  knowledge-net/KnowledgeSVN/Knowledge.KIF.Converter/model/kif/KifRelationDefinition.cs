namespace Knowledge.KIF.Converter.model.kif {
    public class KifRelationDefinition : KifDefinition {
//TODO:

        public static readonly string NAME = "defrelation";

        protected override string getConstructionName() {
            return NAME;
        }
        
        public KifRelationDefinition(string name, string str, KifSequence<KifSentence> sentences)
            : base(name, str, sentences) {
        }

        public KifRelationDefinition(string name, KifSequence<KifSentence> sentences) : base(name, sentences) {
        }
    }
}