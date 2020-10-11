using DBMS_Core.Extentions;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace DBMS_Core.Infrastructure.Factories
{
    public static class SupportedTypesFactory
    {
        public static object GetTypeInstance(SupportedTypes type, string value)
        {
            try
            {
                return _parseStrigns[type](value);
            }
            catch(Exception ex)
            {
                throw new InvalidCastException("Unsupported type", ex);
            } 
        }

        private static Dictionary<SupportedTypes, Func<string, object>> _parseStrigns =>
            new Dictionary<SupportedTypes, Func<string, object>>
            {
                [SupportedTypes.Char] = new Func<string, object>(x => x.First()),
                [SupportedTypes.Integer] = new Func<string, object>(x => int.Parse(x)),
                [SupportedTypes.Real] = new Func<string, object>(x => double.Parse(x)),
                [SupportedTypes.String] = new Func<string, object>(x => x),
                [SupportedTypes.RealInterval] = new Func<string, object>(x =>
                {
                    try
                    {
                        return JsonSerializer.Deserialize<RealInterval>(x);
                    }
                    catch
                    {
                        var intervals = x.Split(';');
                        return new RealInterval
                        {
                            From = double.Parse(intervals[0]),
                            To = double.Parse(intervals[1])
                        };
                    }
                }),
                [SupportedTypes.Picture] = new Func<string, object>(x => string.IsNullOrEmpty(x) ? null : JsonSerializer.Deserialize<Picture>(x))
            };
    }
}
