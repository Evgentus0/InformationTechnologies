using DBMS_Core.Extentions;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DBMS_Core.Infrastructure.Factories
{
    public static class SupportedTypesFactory
    {
        public static object GetTypeInstance(SupportedTypes type, string value)
        {
            return JsonSerializer.Deserialize(value, Type.GetType(type.GetAssemblyDescription()));
        }

    }
}
