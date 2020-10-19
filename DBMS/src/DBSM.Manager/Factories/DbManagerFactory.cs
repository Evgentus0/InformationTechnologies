using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Sources;
using DBSM.Manager.Interfaces;
using DBSM.Manager.Local;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBSM.Manager.Factories
{
    public static class DbManagerFactory
    {
        public static IDbManager GetDbManagerLocal(string path)
        {
            return new DbManagerLocal(DataBaseServiceFactory.GetDataBaseService(path));
        }

        public static IDbManager GetDbManagerLocal(string name, string rootPath, long fileSize, SupportedSources source)
        {
            return new DbManagerLocal(DataBaseServiceFactory.GetDataBaseService(name, rootPath, fileSize, source));
        }
    }
}
