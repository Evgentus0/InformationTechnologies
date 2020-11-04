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
        [AssemblyName(typeof(int), Constants.TypeDescription)]
        [ValidatorType(typeof(NumericValidator<int>))]
        Integer,
        [AssemblyName(typeof(double), Constants.TypeDescription)]
        [ValidatorType(typeof(NumericValidator<double>))]
        Real,
        [AssemblyName(typeof(char), Constants.TypeDescription)]
        Char,
        [AssemblyName(typeof(string), Constants.TypeDescription)]
        [ValidatorType(typeof(StringValidator))]
        String,
        [AssemblyName(typeof(RealInterval), Constants.TypeDescription)]
        [ValidatorType(typeof(RealIntervalValidator))]
        RealInterval,
        [AssemblyName(typeof(Picture), Constants.TypeDescription)]
        Picture
    }
}
