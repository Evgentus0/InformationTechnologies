using DBMS_Core.Infrastructure.Factories.Interfaces;
using DBMS_Core.Infrastructure.Services;
using DBMS_Core.Interfaces;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Implementations
{
    class DataBaseServiceFactory : IDataBaseServiceFactory
    {
        private IFileWorkerFactory _fileWorkerFactory;
        private ITableServiceFactory _tableServiceFactory;
        private IDbWriterFactory _dbWriterFactory;

        public DataBaseServiceFactory(IFileWorkerFactory fileWorkerFactory,
            ITableServiceFactory tableServiceFactory,
            IDbWriterFactory dbWriterFactory)
        {
            _fileWorkerFactory = fileWorkerFactory;
            _tableServiceFactory = tableServiceFactory;
            _dbWriterFactory = dbWriterFactory;
        }

        public IDataBaseService GetDataBaseService(string path)
        {
            return new DataBaseService(path, _fileWorkerFactory, _tableServiceFactory, _dbWriterFactory);
        }

        public IDataBaseService GetDataBaseService(string name, string rootPath, long fileSize, SupportedSources source)
        {
            return new DataBaseService(name, rootPath, fileSize, source, _fileWorkerFactory, _tableServiceFactory, _dbWriterFactory);
        }
    }
}
