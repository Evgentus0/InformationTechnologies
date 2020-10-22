using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.SharedModels.DTO
{
    public class DataBase
    {
        public string Name { get; set; }
        public DbSettings Settings { get; set; }
        public List<Table> Tables { get; set; }
    }
}
