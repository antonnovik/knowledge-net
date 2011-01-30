/*
 * Created by: M. Sigalin
 */

using System;
using System.Collections.Generic;

namespace Knowledge.KIF.Converter.model.kif {//TODO: toString - empty or not?
    public class KifModel : AbstractModelItem, IModel {
        private IList<IModelItem> _content;

 
        public KifModel() {
            _content = new List<IModelItem>();
        }

        public override bool isLeaf() {
            return false;
        }

        public void addObject(KifObject kifObject) {
            if (kifObject == null)
                throw new ArgumentNullException(); //TODO: message
            _content.Add(kifObject);
        }

        public void addRange(IEnumerable<KifObject> kifObjects) {
            if (kifObjects == null)
                throw new ArgumentNullException(); //TODO: message
            foreach (KifObject obj in kifObjects) 
                addObject(obj);
        }        
        
        public override IEnumerator<IModelItem> getChildrenIterator() {
            return _content.GetEnumerator();
        }
    }
}