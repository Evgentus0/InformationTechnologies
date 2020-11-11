using DBMS.SharedModels.Infrastructure.Helpers;
using DBMS.SharedModels.Infrastructure.Interfaces;
using DBMS.SharedModels.Infrastructure.Services;
using DBMS.SharedModels.Infrastructure.Settings;
using DBMS_Core.Extentions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.SharedModels.Extentions
{
    public static class IServiceCollectionExtention
    {
        public static IServiceCollection AddSharedFunctionality(this IServiceCollection services)
        {
            services.AddDBMSCore();

            services.AddScoped<IDbDal, DbDal>();
            services.AddScoped<IDbMapper, DbMapper>();
            services.AddScoped<IFileHelper, FileHelper>();
            services.AddScoped<ITableDal, TableDal>();

            return services;
        }
    }
}
