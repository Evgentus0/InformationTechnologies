using DBMS.Manager.Factories;
using DBMS.Manager.RestApi;
using DBMS.WebApiClient;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Infrastructure.Factories.Interfaces;
using DBMS_Core.Sources;
using DBSM.Manager.Interfaces;
using DBSM.Manager.Local;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBSM.Manager.Factories
{
    public class DbManagerFactory: IDbManagerFactory
    {
        private IClient _client;
        private IDataBaseServiceFactory _dataBaseServiceFactory;

        public DbManagerFactory(IClient client, IDataBaseServiceFactory dataBaseServiceFactory)
        {
            _client = client;
            _dataBaseServiceFactory = dataBaseServiceFactory;
        }


        #region local
        public IDbManager GetDbManagerLocal(string path)
        {
            return new DbManagerLocal(_dataBaseServiceFactory.GetDataBaseService(path));
        }

        public IDbManager GetDbManagerLocal(string name, string rootPath, long fileSize, SupportedSources source)
        {
            return new DbManagerLocal(_dataBaseServiceFactory.GetDataBaseService(name, rootPath, fileSize, source));
        }
        #endregion


        #region rest
        public IDbManager GetDbManagerRest(string name)
        {
            if (GetRemoteDbsList().Contains(name))
            {
                return new DbManagerRest(name, _client);
            }
            throw new ArgumentException($"Talbe with name: {name} does not exist!");
        }
        public IDbManager GetDbManagerRest(string name, long fileSize, SupportedSources source)
        {
            try
            {
                _client.CreateDb(name, fileSize, source);

                return new DbManagerRest(name, _client);
            }
            catch(Exception ex)
            {
                throw new Exception("Can not create db", ex);
            }
        }
        public List<string> GetRemoteDbsList()
        {
            return _client.GetDbsList();
        }
        #endregion
    }
}
