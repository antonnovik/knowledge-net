/*
 * Created by: 
 */

using System.Collections.Generic;
using System.IO;
using Knowledge.KIF.Converter.model.kif;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.model {
    
    public class LexemFactory {
        
        private static LexemFactory _self_;

        private IDictionary<string, KifObject> _register;
        
        public static LexemFactory getInstance() {
            if (_self_ == null)
                _self_ = new LexemFactory();
            return _self_;
        }

        private LexemFactory() {
            _register = new Dictionary<string, KifObject>();
        }

        public KifObject getLexem(string name) {
            KifObject result = null;
            if (!(StringUtils.isEmpty(name) || _register.TryGetValue(name, out result))) {
                result = new Lexem(name);
                _register.Add(name, result);
            }
            return result;
        }
        
    }

    class Lexem : KifObject {

        private string _name;

        public Lexem(string name) {
            _name = name;
        }

        public override string ToString() {
            return _name;
        }
        
        
    }    
}