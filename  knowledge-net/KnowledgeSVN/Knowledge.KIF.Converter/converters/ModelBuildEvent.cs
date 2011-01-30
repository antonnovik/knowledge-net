/*
 * Created by: M. Sigalin
 */

namespace Knowledge.KIF.Converter.converters {
    public class ModelBuildEvent {
        public enum ITEM_TYPE { FRAME, SLOT, RULESET, CONCEPT, PROPERTY };
        
        private string _itemName;
        private ITEM_TYPE _type;

        public ModelBuildEvent(string itemName, ITEM_TYPE type) {
            _itemName = itemName;
            _type = type;
        }

        public string ItemName {
            get { return _itemName; }
        }

        public ITEM_TYPE Type {
            get { return _type; }
        }
    }
}