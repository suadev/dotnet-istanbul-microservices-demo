using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Shared.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared
{
    public static class Extensions
    {
        public static string Underscore(this string value)
            => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            return model;
        }

        public static IServiceProvider BuildContainer(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetCallingAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq();
            return new AutofacServiceProvider(builder.Build());
        }
    }
}