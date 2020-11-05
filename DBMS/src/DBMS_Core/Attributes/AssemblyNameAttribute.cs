using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    internal class AssemblyNameAttribute: Attribute
    {
        public Type Type { get; set; }
        public string  Key { get; set; }

        public AssemblyNameAttribute(Type type, string key)
        {
            Type = type;
            Key = key;
        }
    }
}
