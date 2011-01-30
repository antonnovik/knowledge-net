using System;
using System.Collections.Generic;
using System.IO;

namespace Knowledge.KIF.Converter.writers {
    class FileWriter : IWriter {
        private StreamWriter _writer;
        private string _filePath;

        public FileWriter(string filePath) {
            _filePath = filePath;
        }

        public void open() {
            _writer = new StreamWriter(_filePath);
        }

        public void write(Object source) {
            _writer.Write(source.ToString());
        }

        public void write(ICollection<Object> source) {
            if (source != null) {
                foreach (Object src in source) {
                    write(src.ToString());
                    _writer.WriteLine();
                }
            }
        }

        public void close() {
            _writer.Close();
        }
    }
}