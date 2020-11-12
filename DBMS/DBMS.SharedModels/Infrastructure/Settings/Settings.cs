using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMS.SharedModels.Infrastructure.Settings
{
    public class Settings
    {
        public string SuccessMessage { get; set; }
        public Dictionary<SupportedSources, string> RootPath { get; set; }
    }
}
