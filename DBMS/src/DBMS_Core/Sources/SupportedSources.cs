using DBMS_Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DBMS_Core.Sources
{
    public enum SupportedSources
    {
        [AssemblyName(typeof(JsonSource))]
        Json,
        [AssemblyName(typeof(SqlServerSource))]
        SqlServer
    }
}
