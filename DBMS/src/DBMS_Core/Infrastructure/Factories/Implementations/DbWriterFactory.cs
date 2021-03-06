﻿using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Extentions;
using DBMS_Core.Infrastructure.Factories.Interfaces;
using DBMS_Core.Interfaces;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Implementations
{
    class DbWriterFactory : IDbWriterFactory
    {
        private IDbClientFactory _dbClientFactory;

        public DbWriterFactory(IDbClientFactory dbClientFactory)
        {
            _dbClientFactory = dbClientFactory;
        }

        public IDbWriter GetDbWriter(SupportedSources source)
        {
            if (Cache.ContainsKey(source.GetAssemblyDescription(Constants.DbWriterType)))
                return Cache[source.GetAssemblyDescription(Constants.DbWriterType)];

            var dbWriterType = Type.GetType(source.GetAssemblyDescription(Constants.DbWriterType));
            var dbWriterObject = Activator.CreateInstance(dbWriterType, _dbClientFactory);
            var dbWriter = (IDbWriter)dbWriterObject;
            Cache.Add(source.GetAssemblyDescription(Constants.DbWriterType), dbWriter);

            return dbWriter;
        }

        private  Dictionary<string, IDbWriter> _cache;
        private  Dictionary<string, IDbWriter> Cache
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
