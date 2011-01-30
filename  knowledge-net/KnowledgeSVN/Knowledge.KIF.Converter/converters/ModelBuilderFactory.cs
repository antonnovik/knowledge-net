/*
 * Created by: M. Sigalin
 * Created: Thursday, October 26, 2006
 */

using Knowledge.KIF.Converter.converters.impl.kif;
using Knowledge.KIF.Converter.converters.impl.ontolingua;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.converters {
    public class ModelBuilderFactory {
        
        public static readonly string ONTOLINGUA = "ontolingua";
        public static readonly string KIF = "kif";

        private static ModelBuilderFactory _self_;

        private ModelBuilderFactory() {
        }

        public static ModelBuilderFactory getInstance() {
            if (_self_ == null)
                _self_ = new ModelBuilderFactory();
            return _self_;
        }

        public IModelBuilder createBuilder(string type) {
            IModelBuilder result = null;
            if (StringUtils.equalsIgnoreCase(ONTOLINGUA, type))
                result = new OntolinguaModelBuilder();
            else if (StringUtils.equalsIgnoreCase(KIF, type))
                result = new KifModelBuilder();

            return result;
        }
    }
}