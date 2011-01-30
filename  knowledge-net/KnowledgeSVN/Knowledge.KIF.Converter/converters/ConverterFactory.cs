using System;
using System.Collections.Generic;
using System.Text;
using Knowledge.KIF.Converter.converters.impl.kif;
using Knowledge.KIF.Converter.converters.impl.ontolingua;

namespace Knowledge.KIF.Converter.converters {

    public class ConverterFactory {

        public static readonly string ONTOLINGUA = "ontolingua";
        public static readonly string KIF = "kif";

        private static ConverterFactory _self_;

        private IDictionary<string, IConverter> _converters;

        private ConverterFactory() {
            _converters = new Dictionary<string, IConverter>();
        }

        public static ConverterFactory getInstance() {
            if (_self_ == null)
                _self_ = new ConverterFactory();
            return _self_;
        }

        public IConverter getConverter(string type) {
            IConverter converter = null;
            if (!_converters.TryGetValue(type, out converter)) {
                if (ONTOLINGUA.Equals(type, StringComparison.CurrentCultureIgnoreCase))
                    converter = new OntolinguaConverter();
                else if (KIF.Equals(type, StringComparison.CurrentCultureIgnoreCase))
                    converter = new KifConverter();

                if (converter != null)
                    _converters.Add(type, converter);
            }
            return converter;
        }
    }
}
