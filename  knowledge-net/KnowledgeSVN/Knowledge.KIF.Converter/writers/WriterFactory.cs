using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.writers {

    public class WriterFactory {
        public static IWriter createFileWriter(string filePath) {
            return new FileWriter(filePath);
        }
    }
}
