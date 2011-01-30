/*
 * Created by: M. Sigalin
 */

using System;
using System.Collections.Generic;

namespace Knowledge.KIF.Converter.model.kif {
    
//TODO: neew refactoring
    public class KifConcreteTerm<T> : KifTerm where T : /*KifConstant,*/ KifObject {//TODO::
        private T _source;

        public override string ToString() {
            return _source.ToString();
        }

        public override IList<IModelItem> getChildren() {
            return _source.getChildren();
        }

        public override bool Equals(object obj) {//TODO:
            return _source.Equals(obj);
        }

        public T Source {
            get { return _source; }
        }

        public KifConcreteTerm(T source) {
            if (source == null)
                throw new ArgumentNullException("Source kif object cann't be null");
            _source = source;
        }

        public override bool isLeaf() {
            return _source.isLeaf();
        }
    }
}