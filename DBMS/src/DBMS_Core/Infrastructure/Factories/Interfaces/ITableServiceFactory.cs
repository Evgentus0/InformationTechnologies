using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Interfaces
{
    public interface ITableServiceFactory
    {
        ITableService GetTableService(Table table, IFileWorker fileWorker);
    }
}
