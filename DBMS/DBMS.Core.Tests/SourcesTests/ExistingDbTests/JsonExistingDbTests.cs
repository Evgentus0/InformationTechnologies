using DBMS_Core;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace DBMS.Core.Tests.SourcesTests.ExistingDbTests
{
    class JsonExistingDbTests : CoreTests
    {
        protected override void SetDbService()
        {
            string name = _settings.DbName;
            long fileSize = 1000000;
            string path = _settings.JsonRoot;
            dataBaseService =  DataBaseServiceFactory.GetDataBaseService(name, path, fileSize, 
                DBMS_Core.Sources.SupportedSources.Json);
            SetData();

            dataBaseService = DataBaseServiceFactory.GetDataBaseService($"{path}\\{name}{Constants.DataBaseFileExtention}");
        }
    }
}
