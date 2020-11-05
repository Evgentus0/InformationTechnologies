using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Interfaces
{
    interface IDbWriter
    {
        public void UpdateDb(DataBase dataBase);
        public void DeleteDb(DataBase dataBase);
        DataBase GetDb(string filePath);
    }
}
