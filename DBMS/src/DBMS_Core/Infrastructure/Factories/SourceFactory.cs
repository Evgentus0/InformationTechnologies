using DBMS_Core.Extentions;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories
{
    public static class SourceFactory
    {
        public static ISource GetSourceObject(SupportedSources type, DataBase dataBase, Table table)
        {
            var sourceType = Type.GetType(type.GetAssemblyDescription(Constants.SourceType));

            var sourceObject = Activator.CreateInstance(sourceType);

            var source = (ISource)sourceObject;
            source.SetUrl(dataBase, table) ;

            return source;
        }
    }
}
