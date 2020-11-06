using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.WebApiClient.Settings
{
    class Settings
    {
        public Endpoints Endpoints { get; set; }
        public Constants Constants { get; set; }
        public string Host { get; internal set; } = @"https://localhost:5001/api";

        public Settings()
        {
            Endpoints = new Endpoints();
            Constants = new Constants();
        }
    }
}
