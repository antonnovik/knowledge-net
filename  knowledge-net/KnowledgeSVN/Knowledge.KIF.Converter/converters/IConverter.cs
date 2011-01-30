using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.converters {
    public interface IConverter {
        ICollection<Object> convert(ICollection source); 
    }
}
