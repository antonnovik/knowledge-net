using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.util.wrappers
{
    class Integer
    {
        private int _value;

        public Integer(int value) {
            _value = value;
        }

        public Integer(): this(0) {
        }

        public int intValue() {
            return _value;
        }

    }
}
