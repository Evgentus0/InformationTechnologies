using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.WebApiClient.Settings
{
    class Constants
    {
        public string TableName { get; internal set; } = "tableName";
        public string DbController { get; internal set; } = "DataBase";
        public string TableController { get; internal set; } = "Table";
        public string FieldName { get; set; } = "fieldName";
    }
}
