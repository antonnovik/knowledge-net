using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.converters.impl.kif {

    class KifConverter: IConverter {
        public ICollection<Object> convert(ICollection source) {
            List<Object> result = new List<object>();
            result.Add("KIF CONVERTOR");
            result.Add("=================================");

            foreach (Object obj in source) {
                result.Add(obj);
            }

            return result; 
        } 
    }

}
