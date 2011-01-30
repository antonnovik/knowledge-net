namespace Knowledge.KIF.Converter.model.kif {
    public class KifFunction : KifDefinition {
        
        public static readonly string NAME = "deffunction";

        public KifFunction(string name, KifSequence<KifSentence> sentences) : base(name, sentences) {
        }

        public KifFunction(string name, string str, KifSequence<KifSentence> sentences) : base(name, str, sentences) {
        }

        protected override string getConstructionName() {
            return NAME;
        }
    }
}