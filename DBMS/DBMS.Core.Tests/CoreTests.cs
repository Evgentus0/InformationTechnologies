using DBMS_Core.Infrastructure.Services;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DBMS.Core.Tests
{
    [TestFixture]
    public class CoreTests
    {
        //private IStorage _storage;
        private IDataBaseService dataBaseService;
        [SetUp]
        public void Initialize()
        {
            string name = "EntityServiceTest";
            long fileSize = 1000000;
            string path = @"D:\Education\4 course\InformationTechnologies\Labs\DBMS\tests";
            dataBaseService = new DataBaseService(name, path, fileSize, DBMS_Core.Sources.SupportedSources.Json);

            dataBaseService.AddTable("Table1");
            dataBaseService.AddTable("Table2");
            dataBaseService.AddTable("Table3");
            var table3 = dataBaseService["Table3"];
            var table1 = dataBaseService["Table1"];
            var table2 = dataBaseService["Table2"];
            table2.AddNewField("Name2", DBMS_Core.Models.Types.SupportedTypes.String);
            table2.AddNewField("Age2", DBMS_Core.Models.Types.SupportedTypes.Integer);
            table2.AddNewField("Income2", DBMS_Core.Models.Types.SupportedTypes.Integer);
            var data2 = new List<List<object>>()
            {
                 new List<object>{"name1", 10, 3 },
                 new List<object>{"name3", -12, 2},
                 new List<object>{"name2", 124, -10}
            };
            table2.InsertDataRange(data2);

            table1.AddNewField("Name", DBMS_Core.Models.Types.SupportedTypes.String);
            table1.AddNewField("Age", DBMS_Core.Models.Types.SupportedTypes.Integer);
            table1.AddNewField("Income", DBMS_Core.Models.Types.SupportedTypes.Integer);
            var data = new List<List<object>>()
            {
                 new List<object>{"name1", 10, 1 },
                 new List<object>{"name3", -12, 2},
                 new List<object>{"name2", 124, -10}
            };
            table1.InsertDataRange(data);

            table3.AddNewField("Name", DBMS_Core.Models.Types.SupportedTypes.String);
            table3.AddNewField("Real", DBMS_Core.Models.Types.SupportedTypes.Integer);
            table3.AddNewField("RealInterval", DBMS_Core.Models.Types.SupportedTypes.RealInterval);
            var data3 = new List<List<object>>()
            {
                 new List<object>{"Test1", 12, new RealInterval { From=12, To=15} },
                 new List<object>{"Test2", 13, new RealInterval { From=22, To=1223} },
                 new List<object>{"Test3",  15, new RealInterval { From = 123, To=1234414} }
            };
            table3.InsertDataRange(data3);
        }

        [Test]
        public void UnionTest()
        {
            var expectedList = new List<List<object>>
            {
                new List<object>{"name1", 10, 3 },
                new List<object>{"name3", -12, 2},
                new List<object>{"name2", 124, -10},
                new List<object>{"name1", 10, 1 },
                new List<object>{"name3", -12, 2},
                new List<object>{"name2", 124, -10}
            };

            var json = JsonSerializer.Serialize(expectedList);
            var expected = JsonSerializer.Deserialize<List<List<object>>>(json);

            var table2 = dataBaseService["Table2"];
            var union = table2.Union(dataBaseService["Table1"].Table);

            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    for (int j = 0; j < expected[i].Count; j++)
                    {
                        Assert.AreEqual(expected[i][j].ToString(), union[i][j + 1].ToString());
                    }
                }
            });
        }

        [Test]
        public void Select()
        {
            var table2 = dataBaseService["Table2"];

            var expectedList = new List<List<object>>()
            {
                 new List<object>{"name1", 10, 3 },
                 new List<object>{"name3", -12, 2},
                 new List<object>{"name2", 124, -10}
            };

            var json = JsonSerializer.Serialize(expectedList);
            var expected = JsonSerializer.Deserialize<List<List<object>>>(json);

            var actual = table2.Select();

            Assert.Multiple(() =>
            {
                for (int i = 0; i < expected.Count; i++)
                {
                    for (int j = 0; j < expected[i].Count; j++)
                    {
                        Assert.AreEqual(expected[i][j].ToString(), actual[i][j + 1].ToString());
                    }
                }
            });
        }

        [Test]
        public void Update()
        {
            var table1 = dataBaseService["Table1"];

            var guid = Guid.Parse(table1.Select().First().First().ToString());

            var listUpdate = new List<List<object>>
            {
                new List<object>{ guid, "newValue", 123, 412 }
            };

            var json = JsonSerializer.Serialize(listUpdate);
            var expected = JsonSerializer.Deserialize<List<List<object>>>(json).First();

            table1.UpdateRows(listUpdate);

            var actualRow = table1.Select().First(x => x.First().ToString() == guid.ToString());

            Assert.Multiple(() =>
            {
                for(int i=0;i< actualRow.Count; i++)
                {
                    Assert.AreEqual(expected[i].ToString(), actualRow[i].ToString());
                }
            });
        }
    }
}
