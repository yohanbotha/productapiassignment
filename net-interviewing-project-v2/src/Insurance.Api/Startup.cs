using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ProductApiAdapter;
using Library.ProductApiAdapter.Configuration;
using Insurance.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Insurance.Api.ExceptionFilters;

namespace Insurance.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<ProductDataApiConfiguration>(configuration =>
            {
                configuration.BaseUrl = Configuration["ProductApi"];
            });

            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductDataApiClient, ProductDataApiClient>();

            services.AddScoped<CustomExeptionAttribute>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
