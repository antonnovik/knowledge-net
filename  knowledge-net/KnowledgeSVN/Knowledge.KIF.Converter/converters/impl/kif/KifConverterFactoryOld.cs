using System;
using System.Collections.Generic;
using System.Text;
using Knowledge.KIF.Converter.model.kif;

namespace Knowledge.KIF.Converter.converters.impl.kif
{
    class KifConverterFactoryOld: AbstractConverterFactory<KifObject>
    {
        private static KifConverterFactoryOld _self_;

        private KifConverterFactoryOld(): base() { 
        }

        public static KifConverterFactoryOld getInstance() {
            if (_self_ == null)
                _self_ = new KifConverterFactoryOld();
            return _self_;
        }

        protected override IConverterOld<KifObject> initConverter(Type type){
            return null;
        }
    }
}
