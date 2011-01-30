using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.converters
{
    abstract class AbstractConverterFactory<T>
    {
        private IDictionary<Type, IConverterOld<T>> _converters;
        protected AbstractConverterFactory() { 
            _converters = new Dictionary<Type, IConverterOld<T>>();
        }

        protected virtual IConverterOld<T> initConverter(Type type) {
            throw new InvalidOperationException("Not implemented"); 
        }

        public IConverterOld<T> getConverter(Type type) {
            IConverterOld<T> converter = _converters[type];
            if (converter == null)
                converter = initConverter(type);
            if (converter != null)
                _converters.Add(type, converter);
            return converter;
        }
    }
}
