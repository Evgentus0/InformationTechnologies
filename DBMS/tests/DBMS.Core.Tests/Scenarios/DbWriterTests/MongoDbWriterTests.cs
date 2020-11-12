using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Interfaces;
using DBMS_Core.Sources.DbWriter;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static DBMS.Core.Tests.Infrastructure.Data.DbData;
using Models = DBMS_Core.Models;

namespace DBMS.Core.Tests.Scenarios.DbWriterTests
{
    [TestFixture]
    class MongoDbWriterTests
    {
        private IDbWriter _dbWriter;
        private Mock<IDbClient> _dbClientMock;

        [SetUp]
        public void Initialize()
        {
            var dbClientFactoryMock = new Mock<IDbClientFactory>();
            _dbClientMock = new Mock<IDbClient>();

            dbClientFactoryMock.Setup(x => x.GetMongoClient(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_dbClientMock.Object);

            _dbWriter = new MongoDbWriter(dbClientFactoryMock.Object);
        }

        [Test]
        public void DeleteDbTest()
        {
            //Arrange
            var dataBase = new Models.DataBase();

            //Act
            _dbWriter.DeleteDb(dataBase);

            //Assert
            _dbClientMock.Verify(m => m.DeleteDatabase(), Times.Once());
        }

        [Test]
        public void GetDbTest()
        {
            var dataBase = new Models.DataBase
            {
                Name = DataBases.DataBase1.Name,
                Settings = new Models.Settings
                {
                    RootPath = DataBases.DataBase1.RootPath
                },
                Tables = new List<Models.Table>
                {
                    new Models.Table
                    {
                        Name = Tables.TableData1.Name,
                        Schema = new Models.TableSchema
                        {
                            Fields = Tables.TableData1.Fields
                        }
                    }
                }
            };

            var dbString = JsonSerializer.Serialize(dataBase);
            _dbClientMock.Setup(x => x.GetDb()).Returns(dbString);

            //Act
            var actual = _dbWriter.GetDb(DataBases.DataBase1.RootPath);

            //Assert
            Assert.AreEqual(dataBase, actual);
        }

        [Test]
        public void GetDbsNamesTest()
        {
            //Arrange
            var excpected = new List<string> { DataBases.DataBase1.Name, DataBases.DataBase2.Name };
            _dbClientMock.Setup(x => x.GetDbsNames()).Returns(excpected);

            //Act
            var actual = _dbWriter.GetDbsNames(CommonData.Server);

            //Assert
            Assert.AreEqual(excpected.Count, actual.Count);
            Assert.Multiple(() =>
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    Assert.AreEqual(excpected[i], actual[i]);
                }
            });
        }

        [Test]
        public void UpdateDbTest()
        {
            //Arrange
            var dataBase = new Models.DataBase
            {
                Name = DataBases.DataBase1.Name,
                Settings = new Models.Settings
                {
                    RootPath = DataBases.DataBase1.RootPath
                },
                Tables = new List<Models.Table>
                {
                    new Models.Table
                    {
                        Name = Tables.TableData1.Name,
                        Schema = new Models.TableSchema
                        {
                            Fields = Tables.TableData1.Fields
                        }
                    }
                }
            };
            var stringDb = JsonSerializer.Serialize(dataBase);
            string actual = string.Empty;
            _dbClientMock.Setup(x => x.UpdateOrCreateDb(stringDb)).Callback<string>(res => actual = res);

            //Act
            _dbWriter.UpdateDb(dataBase);

            //Assert
            Assert.AreEqual(stringDb, actual);
        }
    }
}
