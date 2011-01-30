using System.Collections.Generic;
using System.IO;
using Knowledge.KIF.Converter.model;
using Knowledge.KIF.Converter.model.kif;

namespace Knowledge.KIF.Converter.formatters {
    public class SimpleKifFormatter : IModelFormatter {
        public virtual void write(IModel model, TextWriter writer) {
            IEnumerator<IModelItem> iterator = model.getChildrenIterator();
            while (iterator.MoveNext()) {
                if (iterator.Current.isLeaf())
                    writer.Write(iterator.Current.ToString() + "\n\n\n");
                else
                    WriteConstruction("\n\n\n", iterator.Current, writer, 0);
            }
        }

        private KifObject LB = LexemFactory.getInstance().getLexem("(");
        private KifObject RB = LexemFactory.getInstance().getLexem(")");

        protected virtual void WriteConstruction(string ident, IModelItem modelItem, TextWriter writer, int pos) {
            KifObject kifObject = modelItem as KifObject;

            IList<IModelItem> items = kifObject.getChildren();
            writer.Write(ident);
            for (int i = 0; i < items.Count; i++) {
                IModelItem item = items[i];
                if (item.isLeaf()) {
                    writer.Write(item.ToString());
                    if (i < items.Count - 1 && item != LB && items[i + 1] != RB)
                        writer.Write(" ");
                } else {
                    WriteConstruction(calcIdent(ident, pos + 1), item, writer, pos + 1);
                }
            }
        }

        protected string calcIdent(string ident, int pos) {
            string result = null;
            if (pos == 1)
                result = "\n\t";
            else if (pos < 2)
                result = "\n\t  ";
            else 
                result = "\n\t" + new string(' ', pos * 2);
            return result;
        }

        protected string ci(string ident, int pos) {
            string result = null;
            if (pos == 1)
                result = "\n\t";
            else if (pos < 2)
                result = "\n\t  ";
            else
                result = "\n\t" + new string(' ', pos * 2);
            return result;
        }        
        protected void formatConstruction(string del, KifObject kifObject, TextWriter writer) {
            int count = 0;
            IList<IModelItem> list = kifObject.getChildren();
            for (int i = 0; i < list.Count - 1; i++) {
                IModelItem item = list[i];
                if (item.isDelimiter()) {
                    if (list[i + 1].isLeaf())
                        writer.Write(" ");
                    
                    if (count == 1)
                        writer.Write("\n");
                    else {
                        count++;
                        
                    }
                }
            }
        }

        protected void fc(string prefix, IModelItem item) {
        }
    
    }
}