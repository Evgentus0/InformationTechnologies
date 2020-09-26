using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Attributes
{
    internal class AssemblyNameAttribute: Attribute
    {
        public Type Type { get; set; }

        public AssemblyNameAttribute(Type type)
        {
            Type = type;
        }
    }
}
