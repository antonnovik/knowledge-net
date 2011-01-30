/*
 * Created by: M. Sigalin
 */

namespace Knowledge.KIF.Converter.model.kif {
    public class KifComment: KifObject {

        public static readonly string PREFIX = ";";

        private string _text;

        public KifComment(string text) {
            _text = text == null ? "" : text;
        }

        public override string ToString() {
            return PREFIX + _text;
        }
    }
}