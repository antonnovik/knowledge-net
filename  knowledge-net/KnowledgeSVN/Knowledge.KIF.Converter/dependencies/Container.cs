/*
 * Created by: M. Sigalin
 */

using System.Collections.Generic;
using Knowledge.KIF.Converter.Collections.Generic;
using Knowledge.KIF.Converter.dependencies.exception;
using Knowledge.KIF.Converter.util;

namespace Knowledge.KIF.Converter.dependencies {
    public class Container {
        private string _comment;
        private string _name;

        private Dictionary<string, ISet<Item>> _content;//TODO: refact. - monster-struct

        public Container(string name): this(name, null) {
        }

        public string Comment {
            get { return _comment; }
            set { _comment = value; }
        }

        public Container(string name, string comment) {
            _name = name;
            _comment = comment;
            _content = new Dictionary<string, ISet<Item>>();            
        }

        public string Name {
            get { return _name; }
        }

        public void addDependency(string itemName, Item dependency) {//TODO: bool
            if (StringUtils.isEmpty(itemName))
                throw new DependencyException("item name cann't be null or empty");
            if (dependency == null)
                throw new DependencyException("dependency cann't be null");
            ISet<Item> dependencies; 
            if (!_content.TryGetValue(itemName, out dependencies)) {
                dependencies = new HashSet<Item>();
                _content.Add(itemName, dependencies);
            }
            dependencies.Add(dependency);
        }
        
        public ISet<Item> getDependencies(string itemName) {
            ISet<Item> dependencies;
            return _content.TryGetValue(itemName, out dependencies) ? null : new HashSet<Item>(dependencies);
        }
    }
}