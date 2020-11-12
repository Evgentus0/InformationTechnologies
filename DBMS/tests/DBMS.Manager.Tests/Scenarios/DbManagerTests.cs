using DBMS.Manager.RestApi;
using DBMS.Manager.Tests.Infrasctructure.Data;
using DBMS.WebApiClient;
using DBSM.Manager.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Manager.Tests.Scenarios
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
            _dbManager = new DbManagerRest(DbData.DbName, _clientMock.Object);
        }

        [Test]
        public void AddTableTest()
        {
            //Arrange
            string excpectedDbName = DbData.DbName;
            string excpectedTableName = DbData.TableName;
            string actualDbName = string.Empty, actualTableName = string.Empty;
            _clientMock.Setup(x => x.AddTable(excpectedDbName, excpectedTableName)).Callback<string, string>((db, table) =>
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
            string excpectedDbName = DbData.DbName;
            string excpectedTableName = DbData.TableName;
            string actualDbName = string.Empty, actualTableName = string.Empty;
            _clientMock.Setup(x => x.DeleteTable(excpectedDbName, excpectedTableName)).Callback<string, string>((db, table) =>
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
    }
}
