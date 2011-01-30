using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.dependencies.exception
{
    class DependencyException : ArgumentException
    {
        public DependencyException(String message) : base(message) { }
    }
}
