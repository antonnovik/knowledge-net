using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Knowledge.KIF.Converter.writers
{
    public abstract class AbstractWriterOld
    {
        public abstract String getTextRepresentation();

        public virtual void write(StreamWriter stream) {
            stream.Write(getTextRepresentation());
        }
    }
}
