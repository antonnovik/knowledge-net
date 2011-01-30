using System.Collections.Generic;
using System.IO;

namespace Knowledge.KIF.Converter.model {
    public interface IModelItem {
        //     string getTextRepresentation();TODO: toString??
        void store(TextWriter writer);
        IEnumerator<IModelItem> getChildrenIterator();
        bool isLeaf();
        bool isDelimiter();
        //  bool isReservedKeyword();
    }
}