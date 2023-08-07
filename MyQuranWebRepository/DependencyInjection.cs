using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ICompanyRegistrationRepository, CompanyRegistrationRepository>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<ICompanyRegistrationRepository, CompanyRegistrationRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddDbContext<Models.Altus.AltusContext>(opt => opt
            //    .UseSqlServer("Server=ID1VMK8-05; Database=Altus; User ID=logweb; Password=tomcat;"));

            services.AddScoped<IAyahRepository, AyahRepository>();
            services.AddScoped<ISurahRepository, SurahRepository>();
            services.AddScoped<ITafsirRepository, TafsirRepository>();
            services.AddScoped<IJuzHeaderRepository, JuzHeaderRepository>();
            services.AddScoped<IJuzDetailRepository, JuzDetailRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddOptions<AppSettingModel>();

            services.AddDbContext<MyQuranContext>(opt =>
                //opt.UseLazyLoadingProxies()
                opt.UseSqlServer(configuration.GetConnectionString("MyQuranContext"))                
                );

            //services.Configure<AppSettingModel>(configuration.GetSection("AppSettingModel"));

            return services;
        }
    }
}
