using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Interfaces
{
    interface ISourceFactory
    {
        ISource GetSourceObject(SupportedSources type, DataBase dataBase, Table table);
    }
}
