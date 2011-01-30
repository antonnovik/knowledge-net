using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.model.kif {
    public class KifSequence<T> : KifObject where T : KifObject {
        private List<T> _content;

        public KifSequence() {
            _content = new List<T>();
        }


        public KifSequence(T obj) : this() {
            _content.Add(obj);
        }


        public KifSequence(T obj, KifSequence<T> seq) : this(obj) {
            addRange(seq);
        }

        public KifSequence(KifSequence<T> seq, T obj) : this(seq) {
            addItem(obj);
        }

        public KifSequence(KifSequence<T> seq) {
            _content = new List<T>(seq._content);
        }

        public virtual void addItem(T obj) {
            _content.Add(obj);
        }

        public virtual IEnumerator<T> getIterator() {
            return _content.GetEnumerator();
        }

        public virtual void addRange(KifSequence<T> seq) {//TODO:
            _content.AddRange(seq._content);
        }

        public virtual bool isEmpty() {
            return _content.Count == 0;
        }

        public virtual int getSize() {
            return _content.Count;
        }

        public override String ToString() {
            StringBuilder buf = new StringBuilder();
            if (getSize() > 1)
                buf.Append(_content[0].ToString());

            for (int i = 1; i < getSize(); i++)
                buf.Append(SPACE + _content[i].ToString());

            return buf.ToString();
        }

        public override IList<IModelItem> getChildren() {
            List<IModelItem> result = new List<IModelItem>();
            int count = _content.Count;
            if (count > 0) {
                for (int i = 0; i < count - 1; i++) {
                    result.Add(_content[i]);
                    result.Add(DELIMITER);
                }
                result.Add(_content[count - 1]);
            }
            return result;
        }

        public override bool isLeaf() {
            return isEmpty(); //TODO:
        }
    }
}