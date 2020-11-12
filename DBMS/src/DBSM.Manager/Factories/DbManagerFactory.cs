using DBMS.Manager.RestApi;
using DBMS.WebApiClient;
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
        #region local
        public static IDbManager GetDbManagerLocal(string path)
        {
            return new DbManagerLocal(DataBaseServiceFactory.GetDataBaseService(path));
        }

        public static IDbManager GetDbManagerLocal(string name, string rootPath, long fileSize, SupportedSources source)
        {
            return new DbManagerLocal(DataBaseServiceFactory.GetDataBaseService(name, rootPath, fileSize, source));
        }
        #endregion


        #region rest
        public static IDbManager GetDbManagerRest(string name)
        {
            if (GetRemoteDbsList().Contains(name))
            {
                return new DbManagerRest(name);
            }
            throw new ArgumentException($"Talbe with name: {name} does not exist!");
        }
        public static IDbManager GetDbManagerRest(string name, long fileSize, SupportedSources source)
        {
            try
            {
                new Client().CreateDb(name, fileSize, source);

                return new DbManagerRest(name);
            }
            catch(Exception ex)
            {
                throw new Exception("Can not create db", ex);
            }
        }
        public static List<string> GetRemoteDbsList()
        {
            return new Client().GetDbsList();
        }
        #endregion
    }
}
