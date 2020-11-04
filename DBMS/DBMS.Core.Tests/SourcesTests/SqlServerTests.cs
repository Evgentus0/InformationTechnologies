using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Core.Tests.SourcesTests
{
    class SqlServerTests: CoreTests
    {
        protected override IDataBaseService SetDbService()
        {
            string name = "EntityServiceTest";
            long fileSize = 1000000;
            string path = @"DESKTOP-2UQRN34\SQLEXPRESS";
            return DataBaseServiceFactory.GetDataBaseService(name, path, fileSize, DBMS_Core.Sources.SupportedSources.SqlServer);
        }
    }
}
