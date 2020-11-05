using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Core.Tests.SourcesTests
{
    class JsonSourceTests : CoreTests
    {
        protected override void SetDbService()
        {
            string name = _settings.DbName;
            long fileSize = 1000000;
            string path = _settings.JsonRoot;
            dataBaseService = DataBaseServiceFactory.GetDataBaseService(name, path, fileSize, 
                DBMS_Core.Sources.SupportedSources.Json);
            SetData();
        }
    }
}
