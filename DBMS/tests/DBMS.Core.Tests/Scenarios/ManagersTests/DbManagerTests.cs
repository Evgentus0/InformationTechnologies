using DBMS.Core.Tests.Infrastructure.Data;
using DBMS.Manager.RestApi;
using DBMS.WebApiClient;
using DBSM.Manager.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Models = DBMS_Core.Models;
using static DBMS.Core.Tests.Infrastructure.Data.DbData;
using System.Linq;

namespace DBMS.Core.Tests.Scenarios.ManagersTests
{
    [TestFixture]
    class DbManagerTests
    {
        private IDbManager _dbManager;
        private Mock<IClient> _clientMock;

        [SetUp]
        public void Setup()
        {
            _clientMock = new Mock<IClient>();
            _dbManager = new DbManagerRest(DataBases.DataBase1.Name, _clientMock.Object);
        }

        [Test]
        public void GetTableTest()
        {
            //Arrange
            var table = new Models.Table
            {
                Name = Tables.TableData1.Name,
                Schema = new Models.TableSchema
                {
                    Fields = Tables.TableData1.Fields
                }
            };
            _clientMock.Setup(x => x.GetTable(It.IsAny<string>(), It.IsAny<string>())).Returns(table);

            //Act
            var tableService = _dbManager[Tables.TableData1.Name];
            var actualTable = tableService.Table;

            //Assert
            Assert.AreEqual(table, actualTable);

        }

        [Test]
        public void AddTableTest()
        {
            //Arrange
            string excpectedDbName = DataBases.DataBase1.Name;
            string excpectedTableName = Tables.TableData1.Name;
            string actualDbName = string.Empty, actualTableName = string.Empty;
            _clientMock.Setup(x => x.AddTable(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((db, table) =>
            {
                actualDbName = db;
                actualTableName = table;
            });

            //Act
            _dbManager.AddTable(excpectedTableName);

            //Assert
            Assert.AreEqual(excpectedDbName, actualDbName);
            Assert.AreEqual(excpectedTableName, actualTableName);
        }

        [Test]
        public void DeleteTableTest()
        {
            //Arrange
            string excpectedDbName = DataBases.DataBase1.Name;
            string excpectedTableName = Tables.TableData1.Name;
            string actualDbName = string.Empty, actualTableName = string.Empty;
            _clientMock.Setup(x => x.DeleteTable(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((db, table) =>
            {
                actualDbName = db;
                actualTableName = table;
            });

            //Act
            _dbManager.DeleteTable(excpectedTableName);

            //Assert
            Assert.AreEqual(excpectedDbName, actualDbName);
            Assert.AreEqual(excpectedTableName, actualTableName);
        }

        [Test]
        public void GetTablesTest()
        {
            //Arrange
            var tablesList = new List<Models.Table>
            {
                new Models.Table
                {
                    Name = Tables.TableData1.Name,
                    Schema = new Models.TableSchema
                    {
                        Fields = Tables.TableData1.Fields
                    }
                },
                new Models.Table
                {
                    Name = Tables.TableData2.Name,
                    Schema = new Models.TableSchema
                    {
                        Fields = Tables.TableData2.Fields
                    }
                }
            };

            _clientMock.Setup(x => x.GetTables(DataBases.DataBase1.Name)).Returns(tablesList);

            //Act
            var tableManagers = _dbManager.GetTables();
            var actualTables = tableManagers.Select(x => x.Table).ToList();

            //Assert
            Assert.AreEqual(tablesList.Count, actualTables.Count);
            Assert.Multiple(() =>
            {
                for(int i = 0; i < actualTables.Count; i++)
                {
                    Assert.AreEqual(tablesList[i], actualTables[i]);
                }
            });
        }
    }
}