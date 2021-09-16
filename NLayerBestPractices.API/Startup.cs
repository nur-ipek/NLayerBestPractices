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

            //dependency injection
            //typeOf --> Generic class'larda
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));
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
