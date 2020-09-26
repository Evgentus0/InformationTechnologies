using DBMS_Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DBMS_Core.Models.Types
{
    public enum SupportedTypes
    {
        [AssemblyName(typeof(int))]
        Integer,
        [AssemblyName(typeof(double))]
        Real,
        [AssemblyName(typeof(char))]
        Char,
        [AssemblyName(typeof(string))]
        String,
        [AssemblyName(typeof(RealInterval))]
        RealInterval,
        [AssemblyName(typeof(Picture))]
        Picture
    }
}
