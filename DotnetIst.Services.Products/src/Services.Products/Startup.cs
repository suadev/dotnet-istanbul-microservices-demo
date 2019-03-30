using System;
using Shared;
using Shared.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Customers.Data;
using Services.Products.Data;
using Services.Products.Events;

namespace Services.Products
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
            services.AddDbContext<ProductDBContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );
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
               .SubscribeEvent<OrderCreated>();

            SeedData.Initialize(app.ApplicationServices);
        }
    }
}
