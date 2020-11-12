using DBMS.SqlServerSource.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Core = DBMS_Core.Sources;
using Source = DBMS_Core.Sources;

namespace DBMS.Core.Tests.Scenarios.SourceTests.SpecificSourcesTests
{
    class SqlSourceTests : BaseSourceTests
    {
        protected override void Setup()
        {
            _dbClientMock = new Mock<IDbClient>();
            var dbFactoryMock = new Mock<IDbClientFactory>();

            dbFactoryMock.Setup(x => x.GetSqlClient(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(_dbClientMock.Object);

            _source = new Source.SqlServerSource(dbFactoryMock.Object);
            _source.Url = "server|db|table";
        }
    }
}
