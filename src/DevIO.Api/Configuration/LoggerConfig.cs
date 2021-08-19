using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
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
        public static IServiceCollection AddLoggingConfiguration(this IServiceCollection services)
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
            return services;
        }
        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();
            return app;
        }

    }
}
