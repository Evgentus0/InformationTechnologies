using DBMS.WebApiClient;
using DBMS_Core.Infrastructure.Services;
using DBMS_Core.Infrastructure.Validators;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System.Collections.Generic;
using System.IO;
using DBMS.Manager.RestApi;
using DBSM.Manager.Factories;
using DBMS_Core.Sources;
using DBMS_Core.Infrastructure.Factories;
using DBMS.SqlServerSource.Interfaces;
using DBMS.SqlServerSource.Clients;

namespace DBMS_ConsoleClients
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MongoDb();
        }



        private static void Rest()
        {
            string name = "FirstDB";
            //string path = @"D:\Education\4 course\InformationTechnologies\DataBases";
            SupportedSources source = SupportedSources.Json;
            long fileSize = 1000000;

            //var manager = DbManagerFactory.GetDbManagerRest(name, fileSize, source);
            var manager = DbManagerFactory.GetDbManagerRest(name);
            //manager.AddTable("Table1");
            //manager.AddTable("Table2");
            var table = manager["Table1"];

            //table.AddNewField("Name", SupportedTypes.String);
            //table.AddNewField("Income", SupportedTypes.RealInterval);

            //var data = new List<List<object>>()
            // {
            //     new List<object>{"name1", new RealInterval { From=12, To=54353} },
            //     new List<object>{"name3", new RealInterval { From=12, To=564353} },
            //     new List<object>{"name2", new RealInterval { From=124, To=543} },
            // };

            //table.InsertDataRange(data);

            var select = table.Select();
            //dataBaseService.DeleteTable("SecondTable");
            ////dataBaseService.AddTable("FirstTable");
            //var table = dataBaseService["FirstTable"];

            ////table.AddNewField("Name", SupportedTypes.String);
            ////table.AddNewField("Age", DBMS_Core.Models.Types.SupportedTypes.Integer,
            ////    new List<IValidator> { new NumericValidator<int>(NumericValidatorOperation.Greater, 0) });
            ////table.AddNewField("Income", DBMS_Core.Models.Types.SupportedTypes.RealInterval);
            //var data = new List<List<object>>()
            // {
            //     new List<object>{"name1", 12, new RealInterval { From=12, To=54353} },
            //     new List<object>{"name3", -12, new RealInterval { From=12, To=564353} },
            //     new List<object>{"name2", 124, new RealInterval { From=124, To=543} },
            // };
            //table.InsertDataRange(data);


            //var selectData = table.Select(3, 1,
            //    new Dictionary<string, List<IValidator>>
            //    {
            //        ["Name"] = new List<IValidator> { new StringValidator(StringValidatorOperation.EndWith, "2") }
            //    });
        }

        private static void Local()
        {
            string name = "FirstDB";
            //string path = @"DESKTOP-2UQRN34\SQLEXPRESS";
            string path = @"mongodb://localhost:27017";
            long fileSize = 1000000;
            //IDataBaseService dataBaseService = DataBaseServiceFactory.GetDataBaseService(name, path, fileSize, SupportedSources.MongoDb);
            IDataBaseService dataBaseService = DataBaseServiceFactory.GetDataBaseService($"{path}|{name}");
            //dataBaseService.AddTable("SecondTable");
            //dataBaseService.AddTable("FirstTable");
            var table = dataBaseService["FirstTable"];

            //table.AddNewField("Name", SupportedTypes.String);
            //table.AddNewField("Age", DBMS_Core.Models.Types.SupportedTypes.Integer,
            //    new List<IValidator> { new NumericValidator<int>(NumericValidatorOperation.Greater, 0) });
            //table.AddNewField("Income", DBMS_Core.Models.Types.SupportedTypes.RealInterval);
            //var data = new List<List<object>>()
            // {
            //     new List<object>{"name1", 12, new RealInterval { From=12, To=54353} },
            //     new List<object>{"name3", -12, new RealInterval { From=12, To=564353} },
            //     new List<object>{"name2", 124, new RealInterval { From=124, To=543} },
            // };
            //table.InsertDataRange(data);
            var select = table.Select();


            var selectData = table.Select(3, 1,
                new Dictionary<string, List<IValidator>>
                {
                    ["Name"] = new List<IValidator> { new StringValidator(StringValidatorOperation.EndWith, "2") }
                });
        }

        private static void MongoDb()
        {
            string connectionString = @"mongodb://localhost:27017";
            string dbName = "FirstDB";
            string tName = "tName";

            IDbClient client = new MongoDbClient(connectionString, dbName, tName);
            //client.CreateTable();
            client.DeleteDatabase();

            //var data = new List<string>
            //{
            //    "str1",
            //    "str2",
            //    "str3"
            //};
            //client.InsertData(data);

            //var res = client.GetData();
        }
    }
}
