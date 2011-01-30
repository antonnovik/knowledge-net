namespace Knowledge.KIF.Converter.model.kif {
    public class KifIndividualVariable : KifVariable {
        public static readonly string PREFIX = "?";

        public KifIndividualVariable(string name) : base(name) {
        }

        protected override string getPrefix() {
            return PREFIX;
        }
    }
}