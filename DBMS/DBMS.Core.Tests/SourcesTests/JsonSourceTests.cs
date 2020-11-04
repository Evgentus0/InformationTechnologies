using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Core.Tests.SourcesTests
{
    class JsonSourceTests : CoreTests
    {
        protected override IDataBaseService SetDbService()
        {
            string name = "EntityServiceTest";
            long fileSize = 1000000;
            string path = @"D:\Education\4 course\InformationTechnologies\DataBases\testsStore";
            return DataBaseServiceFactory.GetDataBaseService(name, path, fileSize, DBMS_Core.Sources.SupportedSources.Json);
        }
    }
}
