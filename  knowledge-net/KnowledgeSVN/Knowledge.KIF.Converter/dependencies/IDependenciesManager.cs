using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.dependencies
{
    interface IDependenciesManager
    {
        IDictionary<string, DependencyEntry> getDependencies();
        IDictionary<string, DependencyEntry> getDefaultDependencies();
    }
}
