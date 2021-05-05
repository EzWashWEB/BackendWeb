using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzWash.API.Domain.Persistence.Contexts;
using EzWash.API.Domain.Persistence.Repositories;
using EzWash.API.Domain.Services;
using EzWash.API.Persistence.Repositories;
using EzWash.API.Services;
using Microsoft.EntityFrameworkCore;

namespace EzWash.API
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
            
            // Database Connection Configuration
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
                //options.UseMySQL(Configuration.GetConnectionString("AzureMySQLConnection"));
            });
            
            //TODO: Dependency Injection Configuration
            //HINT: services.AddScoped<ICategoryRepository, CategoryRepository>();
            //HINT: services.AddScoped<IUnitOfWork, UnitOfWork>();
            //HINT: services.AddScoped<ICategoryService, CategoryService>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IProvinceService, ProvinceService>();
            
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IDistrictService, DistrictService>();

            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletService, WalletService>();


            services.AddScoped<IPlanRepository, PlanRepository>();

            services.AddScoped<IPlanService, PlanService>();


            // Apply Endpoint Naming Convention
            services.AddRouting(options => options.LowercaseUrls = true);
            
            // AutoMapper Setup
            services.AddAutoMapper(typeof(Startup));
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EzWash.API", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EzWash.API v1"));
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
