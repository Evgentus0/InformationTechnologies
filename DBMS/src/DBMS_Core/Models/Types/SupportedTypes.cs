using DBMS_Core.Attributes;
using DBMS_Core.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DBMS_Core.Models.Types
{
    public enum SupportedTypes
    {
        [AssemblyName(typeof(int))]
        [ValidatorType(typeof(NumericValidator<int>))]
        Integer,
        [AssemblyName(typeof(double))]
        [ValidatorType(typeof(NumericValidator<double>))]
        Real,
        [AssemblyName(typeof(char))]
        Char,
        [AssemblyName(typeof(string))]
        [ValidatorType(typeof(StringValidator))]
        String,
        [AssemblyName(typeof(RealInterval))]
        [ValidatorType(typeof(RealIntervalValidator))]
        RealInterval,
        [AssemblyName(typeof(Picture))]
        Picture
    }
}
