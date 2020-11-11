using DBMS_Core.Infrastructure.Factories.Interfaces;
using DBMS_Core.Infrastructure.Services;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Implementations
{
    class TableServiceFactory : ITableServiceFactory
    {
        public ITableService GetTableService(Table table, IFileWorker fileWorker)
        {
            return new TableService(table, fileWorker);
        }
    }
}
