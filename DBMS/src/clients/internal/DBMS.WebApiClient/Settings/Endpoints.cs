using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.WebApiClient.Settings
{
    class Endpoints
    {
        public string AllTables { get; set; } = "all-tables";
        public string GetDb { get; set; } = "get-db";
        public string GetAllDb { get; set; } = "get-db-all";

        public string AddField { get; set; } = "add-field";
        public string DeleteField { get; set; } = "delete-field";
        public string Data { get; set; } = "data";
        public string DeleteConditions { get; set; } = "delete-conditions";
        public string DeleteIds { get; set; } = "delete-ids";
        public string UpdateSchema { get; set; } = "update-schema";
        public string Select { get; set; } = "select";
        public string Union { get; set; } = "union";
    }
}
