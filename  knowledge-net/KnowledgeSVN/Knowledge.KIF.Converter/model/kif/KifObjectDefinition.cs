namespace Knowledge.KIF.Converter.model.kif {
    public class KifObjectDefinition : KifDefinition {
        
        public static readonly string NAME = "defobject";

        protected override string getConstructionName() {
            return NAME;
        }

        public KifObjectDefinition(string name, KifSequence<KifSentence> sentences)
            : base(name, sentences) {
        }

        public KifObjectDefinition(string name, string str, KifSequence<KifSentence> sentences)
            : base(name, str, sentences) {
        }

    }
}