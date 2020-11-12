using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Extentions;
using DBMS_Core.Infrastructure.Factories.Interfaces;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Implementations
{
    class SourceFactory : ISourceFactory
    {
        private IDbClientFactory _dbClientFactory;

        public SourceFactory(IDbClientFactory dbClientFactory)
        {
            _dbClientFactory = dbClientFactory;
        }

        public ISource GetSourceObject(SupportedSources type, DataBase dataBase, Table table)
        {
            var sourceType = Type.GetType(type.GetAssemblyDescription(Constants.SourceType));

            var sourceObject = Activator.CreateInstance(sourceType, _dbClientFactory);

            var source = (ISource)sourceObject;
            source.SetUrl(dataBase, table);

            return source;
        }
    }
}
