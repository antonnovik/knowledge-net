using System;
using System.Collections.Generic;
using System.IO;

namespace Knowledge.KIF.Converter.model {

    public abstract class AbstractModelItem: IModelItem {
        
        public virtual void store(TextWriter writer) {
            if (isLeaf())
                writer.Write(ToString());
            else {
                IEnumerator<IModelItem> iterator = getChildrenIterator();
                while (iterator.MoveNext())
                    iterator.Current.store(writer);
            }
        }

      //  public abstract string getTextRepresentation();
        protected static IList<IModelItem> EMPTY_LIST = new List<IModelItem>();

        public virtual IEnumerator<IModelItem> getChildrenIterator() {
            return EMPTY_LIST.GetEnumerator();
        }

        public virtual bool isLeaf() {
            return true;
        }

        public virtual bool isDelimiter() {
            return false;
        }

//        public override string ToString() {
//            IEnumerator<IModelItem> iter = getChildrenIterator();
//        }
    }

}
