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
            services.AddScoped<NotFoundFilter>();  // ----> filter database nesnesi al�yorsa eklememez gerekmektedir.

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(ServiceClass<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                //UseSqlServer --> Sqlserver kullan�laca��n� haber veriyoruz.
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(),
                    
                    o=>o.MigrationsAssembly("NLayerBestPractices.Data"));

            });

            // Addscoped --> bir request esnas�nda bir s�n�f�n constructor'�nda IUnitOfWork ile kar��la��rsa UnitOfWork'ten bir nesne �rne�i al�cak.
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<>();

            //AddTransient<> --> request esnas�nda unitofwork nesnesine birden fazla kez ihtiya� duyulursa Addscoped ayn� nesne �rne�i �zerinden devam ederken, addtransient her seferinde yeni bir unitofwork nesnesi �retir.

            //Performnans a��s�ndan AddScoped kullan�yoruz.
            services.AddControllers();




            //Sen Validation i�ine kar��ma ben hallediyorum...
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //default de�eri false
                options.SuppressModelStateInvalidFilter = true;
            }
            );


            //Global d�zeyde t�m controller'larda ValidationFilter kullanmka istersen eklemeliyim.

            services.AddControllers(o =>
            {
                o.Filters.Add(new ValidationFilter());

            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Katmanlar� yazd���m�z metot --> Configure
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
