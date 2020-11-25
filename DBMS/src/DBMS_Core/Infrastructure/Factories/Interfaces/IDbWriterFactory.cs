using DBMS_Core.Interfaces;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Interfaces
{
    public interface IDbWriterFactory
    {
        IDbWriter GetDbWriter(SupportedSources source);
    }
}
