using DBMS_Core;
using DBMS_Core.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Core.Tests.SourcesTests.ExistingDbTests
{
    class MongoDbExistingDbTests:CoreTests
    {
        protected override void SetDbService()
        {
            string name = _settings.DbName;
            long fileSize = 1000000;
            string path = _settings.MongoDbServer;
            dataBaseService = DataBaseServiceFactory.GetDataBaseService(name, path, fileSize,
                DBMS_Core.Sources.SupportedSources.MongoDb);
            SetData();

            dataBaseService = DataBaseServiceFactory.GetDataBaseService($"{path}{Constants.Separator}{name}");
        }
    }
}
