/*
 * Created by: M. Sigalin
 */

using System.Collections.Generic;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.converters {
    public class TypeResolver {
        private static TypeResolver _self_;

        private IDictionary<string, string> _types;

        public static TypeResolver getInstance() {
            if (_self_ == null)
                _self_ = new TypeResolver();
            return _self_;
        }

        private TypeResolver() {
            _types = new Dictionary<string, string>();
            _types.Add("byte", "Non-Negative-Integer");

            _types.Add("sbyte", "Integer");

            _types.Add("char", "Non-Negative-Integer");

            _types.Add("decimal", "Real-Number");
            _types.Add("double", "Real-Number");
            _types.Add("float", "Real-Number");

            _types.Add("int", "Integer");
            _types.Add("long", "Integer");

            _types.Add("uint", "Non-Negative-Integer");
            _types.Add("ulong", "Non-Negative-Integer");

            _types.Add("short", "Integer");

            _types.Add("ushort", "Non-Negative-Integer");

            _types.Add("string", "String");
            _types.Add("String", "String");
        }

        public string resolveType(string type) {
            string result = null;
            if (!StringUtils.isEmpty(type)) {
                _types.TryGetValue(type, out result);
            }
            return result == null ? type : result;
        }
    }
}