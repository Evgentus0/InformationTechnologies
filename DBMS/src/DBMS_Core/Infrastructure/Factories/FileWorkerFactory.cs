using DBMS_Core.Infrastructure.FileStore;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories
{
    public static class FileWorkerFactory
    {
        public static IFileWorker GetFileWorker(DataBase dataBase)
        {
            return new FileWorker(dataBase);
        }
    }
}
