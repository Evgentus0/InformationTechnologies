using DBMS_Core.Infrastructure.Factories.Interfaces;
using DBMS_Core.Infrastructure.FileStore;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Implementations
{
    class FileWorkerFactory : IFileWorkerFactory
    {
        IDbWriterFactory _dbWriterFactory;
        ISourceFactory _sourceFactory;

        public FileWorkerFactory(IDbWriterFactory dbWriterFactory,
            ISourceFactory sourceFactory)
        {
            _dbWriterFactory = dbWriterFactory;
            _sourceFactory = sourceFactory;
        }

        public IFileWorker GetFileWorker(DataBase dataBase)
        {
            return new FileWorker(dataBase, _dbWriterFactory, _sourceFactory);
        }
    }
}
