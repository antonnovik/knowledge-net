using System;

namespace Knowledge.KIF.Converter.model.kif {//TODO: remove or refact.
    public class KifOperator : KifObject {
        private String _opString;


        public KifOperator(int opID) {
            _opString = opToString(opID);
        }


        /**
	   Convert this KIF operator into String format.
           @return this KIF operator in String format.
        */

        public override String ToString() {
            return _opString;
        }


        public static String opToString(int opID) /*throws KifRTException*/ {
            /*          for (int i = 0; i < _kifOpString.Length; i++)
                if (_kifOpString[i]._opID == opID)
                    return new String(_kifOpString[i]._opString.ToCharArray());*/

            throw (new KifRTException("No such Operator ID !"));
        }


        /*        private static KifOpString[] _kifOpString = {

                new KifOpString(KifObject.KIFOP_LISTOF,	"LISTOF"),
                new KifOpString(KifObject.KIFOP_DOUBLE_QUOTE,		"DOUBLE_QUOTE"),
                new KifOpString(KifObject.KIFOP_IF,		"IF"),
                new KifOpString(KifObject.KIFOP_COND,		"COND"),

                new KifOpString(KifObject.KIFOP_SENTEQ,	"="),
                new KifOpString(KifObject.KIFOP_SENTNOTEQ,	"\\="),
                new KifOpString(KifObject.KIFOP_NOT,		"NOT"),
                new KifOpString(KifObject.KIFOP_AND,		"AND"),
                new KifOpString(KifObject.KIFOP_OR,		"OR"),
                new KifOpString(KifObject.KIFOP_IMPLIES,	"=>"),
                new KifOpString(KifObject.KIFOP_IMPLIED,	"<="),
                new KifOpString(KifObject.KIFOP_EQUIV,		"<=>"),
                new KifOpString(KifObject.KIFOP_FORALL,	"FORALL"),
                new KifOpString(KifObject.KIFOP_EXISTS,	"EXISTS"),

                new KifOpString(KifObject.KIFOP_DEFOBJECT,	"DEFOBJECT"),
                new KifOpString(KifObject.KIFOP_DEFUNCTION,"DEFUNCTION"),
                new KifOpString(KifObject.KIFOP_DEFRELATION,"DEFRELATION"),

                new KifOpString(KifObject.KIFOP_NUMEQ,		"="),
                new KifOpString(KifObject.KIFOP_NUMNEQ,	"\\="),
                new KifOpString(KifObject.KIFOP_LESS,		"<"),
                new KifOpString(KifObject.KIFOP_GREATER,	">"),
                new KifOpString(KifObject.KIFOP_EQLESS,	"=<"),
                new KifOpString(KifObject.KIFOP_EQGREATER,	">=")
            };*/
    }


    /**
   The typeID-string pairs of the KIF operators.
    */

    class KifOpString {
        public int _opID;
        public String _opString;

        /**
	   Create a typeID-string pair of a KIF operator.
	   @param opID type ID of the operator
           @param opString the string representation of the operator.
        */

        public KifOpString(int opID, String opString) {
            _opID = opID;
            _opString = opString;
        }
    }
}