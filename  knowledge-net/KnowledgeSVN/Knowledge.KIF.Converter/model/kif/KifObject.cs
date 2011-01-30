using System.Collections.Generic;

namespace Knowledge.KIF.Converter.model.kif {
    public abstract class KifObject : AbstractModelItem {
//        private int _typeID;
     //   private String _name;


        /**
	       Get the type ID of this KIF object. Type IDs of various of Kif objects
           are defined in this class. you are encouraged to use InstanceOf 
           operator instead of this method.
           @return the type ID of this KIF object.
        */
        protected static KifDelimiter DELIMITER = new KifDelimiter();//TODO:

        public override string ToString() {
            return "";
        }

        public virtual IList<IModelItem> getChildren() {//TODO:
            return EMPTY_LIST;
        }

        public override IEnumerator<IModelItem> getChildrenIterator() {
            return getChildren().GetEnumerator();
        }

        public override bool isLeaf() {
            return getChildren().Count == 0;
        }        
        public static readonly string LEFT_BRACKET = "(";
        public static readonly string RIGHT_BRACKET = ")";
        public static readonly string SPACE = " ";
        public static readonly string IDENT = "\t";
        public static readonly string NEW_LINE = "\n";
        
    }
}