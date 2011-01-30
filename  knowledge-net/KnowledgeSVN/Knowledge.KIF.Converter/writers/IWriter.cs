using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.writers {
    public interface IWriter {
        void open();
        void write(Object source);
        void write(ICollection<Object> source);
        void close();
    }
}
