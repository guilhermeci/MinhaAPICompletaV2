using DevIO.Api.Extensions;
using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "9d01c5ad97454775bd1b7241248ace3a";
                o.LogId = new Guid("638f8d4a-3341-425e-99fd-a709847f0f68");
            });

            //services.AddLogging(builder => {
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "9d01c5ad97454775bd1b7241248ace3a";
            //        o.LogId = new Guid("638f8d4a-3341-425e-99fd-a709847f0f68");
            //    });
            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});
            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "9d01c5ad97454775bd1b7241248ace3a";
                    options.LogId = new Guid("638f8d4a-3341-425e-99fd-a709847f0f68");
                    options.HeartbeatId = "ba27e23e95e24329b770cfa6177b073c";
                })
                .AddCheck("Produtos", new SqlServerHealthCheck(Configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(Configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");

            services.AddHealthChecksUI()
               .AddInMemoryStorage();
            return services;
        }
        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();
            app.UseHealthChecks("/api/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }

    }
}
