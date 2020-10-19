using DBMS_Core.Infrastructure.Services;
using DBMS_Core.Interfaces;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories
{
    public static class DataBaseServiceFactory
    {
        public static IDataBaseService GetDataBaseService(string path)
        {
            return new DataBaseService(path);
        }
        public static IDataBaseService GetDataBaseService(string name, string rootPath, long fileSize, SupportedSources source)
        {
            return new DataBaseService(name, rootPath, fileSize, source);
        }
    }
}
