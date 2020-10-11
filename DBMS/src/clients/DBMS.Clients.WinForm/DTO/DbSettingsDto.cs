using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Clients.WinForm.DTO
{
    public class DbSettingsDto
    {
        //string name, string rootPath, long fileSize, SupportedSources source
        public string Name { get; set; }
        public string RootPath { get; set; }
        public long FileSize { get; set; }
        public SupportedSources DefaultSource { get; set; }
    }
}
