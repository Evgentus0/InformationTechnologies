using DBMS_Core.Extentions;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories
{
    class DbWriterFactory
    {
        static public IDbWriter GetDbWriter(Settings settings)
        {
            if (Cache.ContainsKey(settings.DefaultSource.GetAssemblyDescription(Constants.DbWriterType)))
                return Cache[settings.DefaultSource.GetAssemblyDescription(Constants.DbWriterType)];

            var dbWriterType = Type.GetType(settings.DefaultSource.GetAssemblyDescription(Constants.DbWriterType));
            var dbWriterObject = Activator.CreateInstance(dbWriterType);
            var dbWriter = (IDbWriter)dbWriterObject;
            Cache.Add(settings.DefaultSource.GetAssemblyDescription(Constants.DbWriterType), dbWriter);

            return dbWriter;
        }

        private static Dictionary<string, IDbWriter> _cache;
        private static Dictionary<string, IDbWriter> Cache
        {
            get
            {
                if (_cache == null)
                    _cache = new Dictionary<string, IDbWriter>();
                return _cache;
            }
            set
            {
                _cache = value;
            }
        }
    }
}
