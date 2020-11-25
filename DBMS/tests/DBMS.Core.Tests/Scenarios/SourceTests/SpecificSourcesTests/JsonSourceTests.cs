using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Sources;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Core.Tests.Scenarios.SourceTests.SpecificSourcesTests
{
    class JsonSourceTests : BaseSourceTests
    {
        protected override void Setup()
        {
            _dbClientMock = new Mock<IDbClient>();
            var dbFactoryMock = new Mock<IDbClientFactory>();

            dbFactoryMock.Setup(x => x.GetJsonClient(It.IsAny<string>())).Returns(_dbClientMock.Object);

            _source = new JsonSource(dbFactoryMock.Object);
        }
    }
}
