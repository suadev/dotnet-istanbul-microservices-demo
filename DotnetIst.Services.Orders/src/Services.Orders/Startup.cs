using System;
using System.Net.Http.Headers;
using Shared.Models;
using Shared.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Orders.Commands;
using Services.Orders.Data;
using Shared;
using Services.Orders.HttpServices;
using Services.Customers.Data;
using Services.Orders.Events;

namespace Services.Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OrderDBContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );

            var httpServices = Configuration.GetOptions<HttpServiceOptions>("HttpServices");
            services.AddHttpClient<ICustomerHttpService, CustomerHttpService>(client =>
            {
                client.BaseAddress = new Uri(httpServices.CustomerHttpServiceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services.BuildContainer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseRabbitMq()
               .SubscribeCommand<CreateOrder>()
               .SubscribeEvent<ProductsReserved>()
               .SubscribeEvent<ProductsReserveFailed>();

            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
