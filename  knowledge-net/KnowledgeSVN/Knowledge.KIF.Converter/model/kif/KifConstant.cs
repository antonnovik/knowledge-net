namespace Knowledge.KIF.Converter.model.kif {
    
    public class KifConstant: KifTerm {

        public KifConstant(string name): base(name) {
            //TODO: additional checking
        }

        public string Name {
            get { return termString(); }
        }

        public override string ToString() {
            return Name;
        }
    }
}
