using DBMS.Manager.Factories;
using DBMS.SharedModels.Extentions;
using DBMS.WebApiClient;
using DBSM.Manager.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DBMS.Manager.Exntentions
{
    public static class IServiceCollectionExtention
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddSharedFunctionality();

            services.AddScoped<IDbManagerFactory, DbManagerFactory>();
            services.AddScoped<IClient, Client>();
            services.AddScoped(x => new HttpClient());

            return services;
        }
    }
}
