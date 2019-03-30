using System;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Shared.Logging
{
    public static class Extensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, string applicationName)
        {
            return webHostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.Enrich.FromLogContext()
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ApplicationName", applicationName);

                loggerConfiguration
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200/"))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = "logstash-{0:yyyy.MM.dd}"
                });
            });
        }
    }
}