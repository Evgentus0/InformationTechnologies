using DBMS.SqlServerSource.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Source = DBMS_Core.Sources;

namespace DBMS.Core.Tests.Scenarios.SourceTests.SpecificSourcesTests
{
    class MongoDbSourceTests : BaseSourceTests
    {
        protected override void Setup()
        {
            _dbClientMock = new Mock<IDbClient>();
            var dbFactoryMock = new Mock<IDbClientFactory>();

            dbFactoryMock.Setup(x => x.GetMongoClient(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_dbClientMock.Object);

            _source = new Source.MongoDbSource(dbFactoryMock.Object);
            _source.Url = "server|db|table";
        }
    }
}
