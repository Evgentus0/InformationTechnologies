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

namespace DBMS_ConsoleClients
{
    internal class Program
    {
        private static void Main(string[] args)
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
            //// string name = "FirstDB";
            //string path = @"D:\Education\4 course\InformationTechnologies\DataBases";
            //// long fileSize = 1000000;
            //// //IDataBaseService dataBaseService = new DataBaseService(name, path, fileSize, DBMS_Core.Sources.SupportedSources.Json);
            //IDataBaseService dataBaseService;
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
    }
}
