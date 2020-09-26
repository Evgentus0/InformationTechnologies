using DBMS_Core.Interfaces;
using DBMS_Core.Infrastructure.Services;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DBMS_Core.Models.Types;
using DBMS_Core.Infrastructure.Validators;

namespace DBMS_ConsoleClients
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "FirstDB";
            string path = @"D:\Education\4 course\InformationTechnologies\DataBases";
            long fileSize = 1000000;
            IDataBaseService dataBaseService = new DataBaseService(name, path, fileSize, DBMS_Core.Sources.SupportedSources.Json);
            //IDataBaseService dataBaseService = new DataBaseService(path + "\\FirstDB.edbf");
            dataBaseService.AddTable("FirstTable");

            var table = dataBaseService["FirstTable"];

            table.AddNewField("Name", DBMS_Core.Models.Types.SupportedTypes.String);
            table.AddNewField("Age", DBMS_Core.Models.Types.SupportedTypes.Integer, 
                new List<IValidator> { new NumericValidator<int>(NumericValidatorOperation.Greater, 0)});
            table.AddNewField("Income", DBMS_Core.Models.Types.SupportedTypes.RealInterval);
            var data = new List<List<object>>()
            {
                new List<object>{"name1", 12, new RealInterval { From=12, To=54353} },
                new List<object>{"name3", -12, new RealInterval { From=12, To=564353} },
                new List<object>{"name2", 124, new RealInterval { From=124, To=543} },
            };
            table.InsertDataRange(data);
        }
    }
}
