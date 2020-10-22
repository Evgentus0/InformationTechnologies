using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBMS.WebApi.Infrastructure.Helpers;
using DBMS.WebApi.Infrastructure.Interfaces;
using DBMS.WebApi.Infrastructure.Services;
using DBMS.WebApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DBMS.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Settings = Configuration.Get<Settings.Settings>();
        }

        protected virtual Settings.Settings Settings { get; set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ConfigureIoC(services);
            ConfigureCors(services);
        }

        protected virtual void ConfigureIoC(IServiceCollection services)
        {
            services.AddSingleton(x => Settings);
            services.AddScoped<IDbDal, DbDal>();
            services.AddScoped<IDbMapper, DbMapper>();
            services.AddScoped<IFileHelper, FileHelper>();
            services.AddScoped<ITableDal, TableDal>();
        }
        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowMyOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
