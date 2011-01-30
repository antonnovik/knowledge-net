using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Knowledge.KIF.Converter.util;
using Knowledge.KIF.Converter.dependencies;

namespace Knowledge.KIF.Converter.dependencies.impl {

    class DependenciesManager: IDependenciesManager {

        public static readonly string TAG_CONTAINER = "container";
        
        public static readonly string TAG_DEPENDENCIES = "dependencies";
        public static readonly string TAG_DEPENDENCY = "dependency";
        public static readonly string TAG_DEFAULT = "default";
        public static readonly string TAG_ITEM = "item";
        public static readonly string ATTR_NAME = "name";
        public static readonly string ATTR_TYPE = "type";

        public static readonly string DEPENDENCY_TYPE_CSHARP = "csharp";
        public static readonly string DEPENDENCY_TYPE_ONTOLINGUA = "ontolingua";

        private string _fileName;
        private IDictionary<string, DependencyEntry> _dependencies;
        private IDictionary<string, DependencyEntry> _defaultDependencies;

        private bool accept(String parameter) {
            bool result = false;
            if (!StringUtils.isEmpty(parameter)) {
                result = DEPENDENCY_TYPE_CSHARP.Equals(parameter) || DEPENDENCY_TYPE_ONTOLINGUA.Equals(parameter);
            }
            return result;
        }

        private void initDefaultDependencies(XmlNodeList itemList) {
            _defaultDependencies = new Dictionary<string, DependencyEntry>();
            if (itemList != null) { 
                if (itemList.Count > 1)
                    throw new XmlException("Xml file " + _fileName + " has incorrect format: element " + TAG_DEFAULT + " has more than one occurrence");
                XmlNode defaultItems = itemList[0];
                if (defaultItems != null) {
                    XmlNodeList dependencies = defaultItems.ChildNodes;
                    if (dependencies != null)
                    {
                        for (int k = 0; k < dependencies.Count; k++)
                        {
                            XmlNode elem = dependencies[k];
                            if (TAG_ITEM.Equals(elem.LocalName))
                                resolveItem(elem, _defaultDependencies);
                        }
                    }
                }   
            }
        }

        private void resolveItem(XmlNode node, IDictionary<string, DependencyEntry> collector) {
            XmlAttribute attrName = node.Attributes[ATTR_NAME];
            XmlAttribute attrType = node.Attributes[ATTR_TYPE];
            if (attrName == null)
                throw new XmlException("Xml file " + _fileName + " has incorrect format: missing attribute " + ATTR_NAME + " in element " + TAG_ITEM);
            string name = attrName.Value;
            string type = null;
            if (attrType == null || StringUtils.isEmpty(attrType.Value))
                type = DEPENDENCY_TYPE_ONTOLINGUA;
            else
                type = attrType.Value;
            if (!accept(type))
                throw new XmlException("Xml file " + _fileName + " has incorrect format: unknown value " + type + " of attribute " + ATTR_TYPE);
            DependencyEntry entry = new DependencyEntry(type);
            XmlNodeList dependencies = node.ChildNodes;
            if (dependencies != null) {
                for (int k = 0; k < dependencies.Count; k++) {
                    XmlNode dependency = dependencies[k];
                    if (TAG_DEPENDENCY.Equals(dependency.LocalName))
                        entry.addDependency(dependency.InnerText);
                }
            }
            collector.Add(name, entry);
        }

        private void init() {
            XmlDocument doc = IOUtils.readXml(_fileName);
            XmlElement root = doc.DocumentElement;

            if (!TAG_DEPENDENCIES.Equals(root.LocalName))
                throw new XmlException("Xml file " + _fileName + " has incorrect format: missing root element " + TAG_DEPENDENCIES);
            _dependencies = new Dictionary<string, DependencyEntry>();
            initDefaultDependencies(root.GetElementsByTagName(TAG_DEFAULT));
            XmlNodeList itemList = root.ChildNodes;
            if (itemList != null)
                for (int i = 0; i < itemList.Count; i++)
                {
                    XmlNode elem = itemList[i];
                    if (TAG_ITEM.Equals(elem.LocalName))
                        resolveItem(elem, _dependencies);
                }
        }

        public DependenciesManager(String fileName) {
            _fileName = fileName;
            init();
        }

        public IDictionary<string, DependencyEntry> getDependencies() {
            return _dependencies;
        }
        
        public IDictionary<string, DependencyEntry> getDefaultDependencies() {
            return _defaultDependencies;
        }

        //TODO: for testing!
        public void print() {
            Console.WriteLine("Default:");
            IEnumerator<string> enumerator = _defaultDependencies.Keys.GetEnumerator();
            while (enumerator.MoveNext()) {
                string key = enumerator.Current;
                Console.WriteLine(key);
                IList<string> list = _defaultDependencies[key].getDependencies();
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine("   " + list[i]);
            }

            Console.WriteLine("Other:");
            enumerator = _dependencies.Keys.GetEnumerator();
            while (enumerator.MoveNext()) {
                string key = enumerator.Current;
                Console.WriteLine(key);
                IList<string> list = _dependencies[key].getDependencies();
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine("   " + list[i]);
            }
        }
    }
}
