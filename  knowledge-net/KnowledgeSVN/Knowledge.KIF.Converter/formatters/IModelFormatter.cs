using System.IO;
using Knowledge.KIF.Converter.model;

namespace Knowledge.KIF.Converter.formatters {
    public interface IModelFormatter {
        void write(IModel model, TextWriter writer);
    }
}