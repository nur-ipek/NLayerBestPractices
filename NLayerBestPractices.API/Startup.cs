 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLayerBestPractices.Core.Repositories;
using NLayerBestPractices.Core.Services;
using NLayerBestPractices.Core.UnitOfWorks;
using NLayerBestPractices.Data;
using NLayerBestPractices.Data.Repositories;
using NLayerBestPractices.Data.UnitOfWorks;
using NLayerBestPractices.Service.Services;
using AutoMapper;
using NLayerBestPractices.API.Filters;

namespace NLayerBestPractices.API
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
            //TODO: Startup ??
            services.AddAutoMapper(typeof(Startup));
            //dependency injection

            //typeOf --> Generic class'larda
            services.AddScoped<NotFoundFilter>();  // ----> filter database nesnesi alıyorsa eklememez gerekmektedir.

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(ServiceClass<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                //UseSqlServer --> Sqlserver kullanılacağını haber veriyoruz.
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(),
                    
                    o=>o.MigrationsAssembly("NLayerBestPractices.Data"));

            });

            // Addscoped --> bir request esnasında bir sınıfın constructor'ında IUnitOfWork ile karşılaşırsa UnitOfWork'ten bir nesne örneği alıcak.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<>();

            //AddTransient<> --> request esnasında unitofwork nesnesine birden fazla kez ihtiyaç duyulursa Addscoped aynı nesne örneği üzerinden devam ederken, addtransient her seferinde yeni bir unitofwork nesnesi üretir.

            //Performnans açısından AddScoped kullanıyoruz.
            services.AddControllers();




            //Sen Validation işine karışma ben hallediyorum...
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //default değeri false
                options.SuppressModelStateInvalidFilter = true;
            }
            );


            //Global düzeyde tüm controller'larda ValidationFilter kullanmka istersen eklemeliyim.

            services.AddControllers(o =>
            {
                o.Filters.Add(new ValidationFilter());

            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Katmanları yazdığımız metot --> Configure
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
