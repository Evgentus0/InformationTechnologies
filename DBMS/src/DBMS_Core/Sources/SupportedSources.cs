using DBMS_Core.Attributes;
using DBMS_Core.Sources.DbWriter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DBMS_Core.Sources
{
    public enum SupportedSources
    {
        [AssemblyName(typeof(JsonSource), Constants.SourceType)]
        [AssemblyName(typeof(JsonDbWriter), Constants.DbWriterType)]
        Json,
        [AssemblyName(typeof(SqlServerSource), Constants.SourceType)]
        [AssemblyName(typeof(SqlServerDbWriter), Constants.DbWriterType)]
        SqlServer,
        [AssemblyName(typeof(MongoDbSource), Constants.SourceType)]
        [AssemblyName(typeof(MongoDbWriter), Constants.DbWriterType)]
        MongoDb
    }
}
