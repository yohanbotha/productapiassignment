using Library.ProductApiAdapter;
using Library.ProductApiAdapter.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Insurance.Api.ExceptionFilters;
using Insurance.Domain.Services;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Data;
using Microsoft.EntityFrameworkCore;

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
            services.AddSwaggerGen();

            services.AddDbContext<InsuranceDBContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("InsuranceDB")));

            services.Configure<ProductDataApiConfiguration>(configuration =>
            {
                configuration.BaseUrl = Configuration["ProductApi"];
            });

            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductDataApiClient, ProductDataApiClient>();
            services.AddScoped<IInsuranceSettingsService, InsuranceSettingsService>();
            services.AddScoped<IRateService, RateService>();

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

            app.UseSwagger();
            app.UseSwaggerUI();

            UpdateDatabase(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<InsuranceDBContext>();
            context.Database.Migrate();
        }
    }
}
