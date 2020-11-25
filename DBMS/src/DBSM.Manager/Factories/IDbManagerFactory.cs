using DBMS_Core.Sources;
using DBSM.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Manager.Factories
{
    public interface IDbManagerFactory
    {
        IDbManager GetDbManagerLocal(string path);
        IDbManager GetDbManagerLocal(string name, string rootPath, long fileSize, SupportedSources source);

        IDbManager GetDbManagerRest(string name);
        IDbManager GetDbManagerRest(string name, long fileSize, SupportedSources source);

        List<string> GetRemoteDbsList();
    }
}
