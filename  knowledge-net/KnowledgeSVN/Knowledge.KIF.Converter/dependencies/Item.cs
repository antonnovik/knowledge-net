/*
 * Created by: M. Sigalin
 */

using Knowledge.KIF.Converter.dependencies.exception;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.dependencies {
    
    public class Item {
        private string _name;
        private string _container;

        public string Name {
            get { return _name; }
        }

        public string Container {
            get { return _container; }
        }

        public Item(string name, string container) {
            if (StringUtils.isEmpty(name))
                throw new DependencyException("name of item cann't be null");
            if (StringUtils.isEmpty(container))
                throw new DependencyException("container of item cann't be null");
            _name = name;
            _container = container;
        }

        private static string DELIMITER = "$";
        public override int GetHashCode() {
            return (_container + DELIMITER + _name).GetHashCode();
        }

        public override bool Equals(object obj) {
            Item item = obj as Item;
            return item != null && _name.Equals(item.Name) && _container.Equals(item.Container);
        }
    }
}