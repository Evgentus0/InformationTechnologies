using DBMS.SqlServerSource;
using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Infrastructure.Factories.Implementations;
using DBMS_Core.Infrastructure.Factories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Extentions
{
    public static class IServiceCollectionExtention
    {
        public static IServiceCollection AddDBMSCore(this IServiceCollection services)
        {
            services.AddScoped<IDataBaseServiceFactory, DataBaseServiceFactory>();
            services.AddScoped<IDbWriterFactory, DbWriterFactory>();
            services.AddScoped<IFileWorkerFactory, FileWorkerFactory>();
            services.AddScoped<ISourceFactory, SourceFactory>();
            services.AddScoped<ITableServiceFactory, TableServiceFactory>();
            services.AddScoped<IValidatorsFactory, ValidatorsFactory>();
            services.AddScoped<IDbClientFactory, DbClientFactory>();

            return services;
        }
    }
}
