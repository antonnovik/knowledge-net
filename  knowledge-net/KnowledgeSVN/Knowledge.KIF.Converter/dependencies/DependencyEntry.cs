using System;
using System.Collections.Generic;
using System.Text;
using Knowledge.KIF.Converter.util;
using Knowledge.KIF.Converter.dependencies.exception;

namespace Knowledge.KIF.Converter.dependencies
{
    class DependencyEntry
    {
        private string _type;
        private IList<string> _dependencies;

        public DependencyEntry(string type)
        {
            _type = type;
            _dependencies = new List<string>();
        }

        public DependencyEntry(): this(null) {}

        public void setType(string type)
        {
            _type = type;
        }
        public string getType()
        {
            return _type;
        }
        public void addDependency(string name) {
            if (StringUtils.isEmpty(name))
                throw new DependencyException("Dependency name cannot be empty!");
            if (_dependencies.Contains(name))
                throw new DependencyException("Dublicate dependency name: " + name);
            _dependencies.Add(name);
        }

        public IList<string> getDependencies() {
            return _dependencies;
        }

        public bool isFinal() {
            return _dependencies.Count == 0;
        }
    }
}
