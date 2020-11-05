using DBMS_Core;
using DBMS_Core.Infrastructure.Factories;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace DBMS.Core.Tests.SourcesTests.ExistingDbTests
{
    class SqlServerExistingDbTests : CoreTests
    {
        protected override void SetDbService()
        {
            string name = _settings.DbName;
            long fileSize = 1000000;
            string path = _settings.SqlServer;
            dataBaseService = DataBaseServiceFactory.GetDataBaseService(name, path, fileSize,
                DBMS_Core.Sources.SupportedSources.SqlServer);
            SetData();

            dataBaseService = DataBaseServiceFactory.GetDataBaseService($"{path}{Constants.Separator}{name}");
        }
    }
}
