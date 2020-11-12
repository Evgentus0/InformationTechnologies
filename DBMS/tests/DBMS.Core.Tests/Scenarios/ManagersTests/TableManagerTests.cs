using DBMS.Manager.RestApi;
using DBMS.WebApiClient;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using DBSM.Manager.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DBMS.Core.Tests.Infrastructure.Data.DbData;
using Models = DBMS_Core.Models;

namespace DBMS.Core.Tests.Scenarios.ManagersTests
{
    [TestFixture]
    class TableManagerTests
    {
        private ITableManager _tableManager;
        private Mock<IClient> _clientMock;
        private Models.Table _table;

        [SetUp]
        public void Setup()
        {
            _clientMock = new Mock<IClient>();
            _table = new Models.Table
            {
                Name = Tables.TableData1.Name,
                Schema = new Models.TableSchema
                {
                    Fields = Tables.TableData1.Fields
                }
            };
            _tableManager = new TableManagerRest(_table, DataBases.DataBase1.Name, _clientMock.Object);
        }

        [Test]
        public void AddNewFieldTest()
        {
            //Arrange
            var field = Tables.TableData1.Fields.First();
            Models.Field actualField = new Models.Field();
            string actualDbName = string.Empty, actualTableName = string.Empty;

            _clientMock.Setup(x => x.AddField(It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<SupportedTypes>(), It.IsAny<List<IValidator>>()))
                .Callback<string, string, 
                string, SupportedTypes, List<IValidator>>((db, table, field, type, validators) => 
                {
                    actualField.Name = field;
                    actualField.Type = type;
                    actualField.Validators = validators;

                    actualDbName = db;
                    actualTableName = table;
                });

            //Act
            _tableManager.AddNewField(field);

            //Assert
            Assert.AreEqual(field, actualField);

            Assert.AreEqual(_table.Name, actualTableName);
            Assert.AreEqual(DataBases.DataBase1.Name, actualDbName);
        }

        [Test]
        public void DeleteFieldTest()
        {
            //Arrange
            var fieldName = Tables.TableData1.Fields.First().Name;
            string actualField = string.Empty;
            string actualDbName = string.Empty, actualTableName = string.Empty;
            _clientMock.Setup(x => x.DeleteField(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string, string>((db, table, field) =>
                {
                    actualDbName = db;
                    actualTableName = table;
                    actualField = field;
                });

            //Act
            _tableManager.DeleteField(fieldName);

            //Assert
            Assert.AreEqual(fieldName, actualField);

            Assert.AreEqual(_table.Name, actualTableName);
            Assert.AreEqual(DataBases.DataBase1.Name, actualDbName);
        }

        [Test]
        public void DeleteRows()
        {
            //Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guidList = new List<Guid> { guid1, guid2 };

            var actualIds = new List<Guid>();
            string actualDbName = string.Empty, actualTableName = string.Empty;
            _clientMock.Setup(x => x.DeleteRows(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<Guid>>()))
                .Callback<string, string, List<Guid>>((db, table, ids) =>
                {
                    actualDbName = db;
                    actualTableName = table;
                    actualIds = ids;
                });

            //Act
            _tableManager.DeleteRows(guidList);

            //Assert
            Assert.AreEqual(_table.Name, actualTableName);
            Assert.AreEqual(DataBases.DataBase1.Name, actualDbName);

            Assert.AreEqual(guidList.Count, actualIds.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < actualIds.Count; i++)
                {
                    Assert.AreEqual(guidList[i], actualIds[i]);
                }
            });
        }

        [Test]
        public void InsertDataTest()
        {
            //Arrange
            var row = Tables.TableData1.Data.First();
            List<object> actualRow = new List<object>();
            string actualDbName = string.Empty, actualTableName = string.Empty;

            _clientMock.Setup(x => x.InsertData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<List<object>>>()))
                .Callback<string, string, List<List<object>>>((db, table, rows) =>
                {
                    actualDbName = db;
                    actualTableName = table;
                    actualRow = rows.First();
                });

            //Act
            _tableManager.InsertData(row);

            //Assert
            Assert.AreEqual(_table.Name, actualTableName);
            Assert.AreEqual(DataBases.DataBase1.Name, actualDbName);

            Assert.AreEqual(row.Count, actualRow.Count);

            Assert.Multiple(() =>
            {
                for(int i = 0; i < actualRow.Count; i++)
                {
                    Assert.AreEqual(row[i], actualRow[i]);
                }
            });
        }

        [Test]
        public void SelectTest()
        {
            //Arrange
            var excpectedRows = Tables.TableData1.Data;

            _clientMock.Setup(x => x.Select(It.IsAny<string>(), It.IsAny<string>())).Returns(excpectedRows);

            //Act
            var actualRows = _tableManager.Select();

            //Assert
            Assert.AreEqual(excpectedRows.Count, actualRows.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < actualRows.Count; i++)
                {
                    Assert.AreEqual(excpectedRows[i].Count, actualRows[i].Count);
                    for (int j = 0; j < excpectedRows[i].Count; j++)
                    {
                        Assert.AreEqual(excpectedRows[i][j], actualRows[i][j]);
                    }
                }
            });
        }

        [Test]
        public void UnionTest()
        {
            //Arrange
            var excpectedRows = Tables.TableData1.Data;

            _clientMock.Setup(x => x.Union(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Models.Table[]>())).Returns(excpectedRows);

            //Act
            var actualRows = _tableManager.Union();

            //Assert
            Assert.AreEqual(excpectedRows.Count, actualRows.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < actualRows.Count; i++)
                {
                    Assert.AreEqual(excpectedRows[i].Count, actualRows[i].Count);
                    for (int j = 0; j < excpectedRows[i].Count; j++)
                    {
                        Assert.AreEqual(excpectedRows[i][j], actualRows[i][j]);
                    }
                }
            });
        }

        [Test]
        public void UpdateSchemaTest()
        {
            string actualField = string.Empty;
            string actualDbName = string.Empty;
            Models.Table actualTable = new Models.Table();
            _clientMock.Setup(x => x.UpdateSchema(It.IsAny<string>(), It.IsAny<Models.Table>()))
                .Callback<string, Models.Table>((db, table) =>
                {
                    actualDbName = db;
                    actualTable = table;
                });
            _clientMock.Setup(x => x.GetTable(It.IsAny<string>(), It.IsAny<string>())).Returns(_table);

            //Act
            _tableManager.UpdateSchema();

            //Assert
            Assert.AreEqual(_table, actualTable);
            Assert.AreEqual(DataBases.DataBase1.Name, actualDbName);
        }
    }
}
