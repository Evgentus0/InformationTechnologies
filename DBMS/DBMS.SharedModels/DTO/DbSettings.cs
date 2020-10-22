using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.SharedModels.DTO
{
    public class DbSettings
    {
        public string RootPath { get; set; }
        public long FileSize { get; set; }
        public SupportedSources DefaultSource{ get; set; }
    }
}
