using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.converters {

    interface IConverterOld<T> {
        Object convert(T source);
    }
}
