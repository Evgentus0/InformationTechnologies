using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Core.Tests.SourcesTests
{
    class MongoDbTests: CoreTests   
    {
        protected override IDataBaseService SetDbService()
        {
            string name = "EntityServiceTest";
            long fileSize = 1000000;
            string path = @"mongodb://localhost:27017";
            return DataBaseServiceFactory.GetDataBaseService(name, path, fileSize, DBMS_Core.Sources.SupportedSources.MongoDb);
        }
    }
}
