using DBMS_Core.Extentions;
using DBMS_Core.Interfaces;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories
{
    public static class SourceFactory
    {
        public static ISource GetSourceObject(SupportedSources type, string rootPath, string tableName)
        {
            var sourceType = Type.GetType(type.GetAssemblyDescription());

            var sourceObject = Activator.CreateInstance(sourceType);

            var source = (ISource)sourceObject;
            source.Url = $"{rootPath}\\{tableName}";

            return source;
        }
    }
}
