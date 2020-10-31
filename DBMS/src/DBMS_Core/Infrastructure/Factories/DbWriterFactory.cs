using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using DBMS_Core.Sources;
using DBMS_Core.Sources.DbWriter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories
{
    class DbWriterFactory
    {
        static public IDbWriter GetDbWriter(Settings settings)
        {
            return _writerDic[settings.DefaultSource];
        }

        static private Dictionary<SupportedSources, IDbWriter> _writerDic =>
            new Dictionary<SupportedSources, IDbWriter>()
            {
                [SupportedSources.Json] = new JsonDbWriter(),
                [SupportedSources.SqlServer] = new SqlServerDbWriter()
            };
    }
}
