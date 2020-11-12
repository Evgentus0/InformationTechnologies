using DBMS_Core.Interfaces;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Interfaces
{
    public interface IDataBaseServiceFactory
    {
        IDataBaseService GetDataBaseService(string path);
        IDataBaseService GetDataBaseService(string name, string rootPath, long fileSize, SupportedSources source);
    }
}
