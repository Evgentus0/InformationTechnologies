using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.SharedModels.DTO
{
    public class Table
    {
        public string Name { get; set; }
        public TableSchema TableSchema { get; set; }
        public List<Source> Sources { get; set; }
    }
}
