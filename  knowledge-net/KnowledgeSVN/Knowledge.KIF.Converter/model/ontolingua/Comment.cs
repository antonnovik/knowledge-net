using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.model.ontolingua
{
    public class Comment: OntolinguaObject {

        public static readonly string QUOTE = "\"";
        private String _text;

        public Comment(String text) {
            _text = text;
        }

        public virtual String getText() {
            return _text;
        }

        public override string ToString() {
            return QUOTE + (_text == null ? "" : _text) + QUOTE;
        }
    }
}
