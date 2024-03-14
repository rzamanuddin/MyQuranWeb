using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyQuranWeb.Domain.Models;
using MyQuranWebRepository.Hadiths;
using MyQuranWebRepository.Interfaces;
using MyQuranWebRepository.Interfaces.Hadiths;
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
            services.AddScoped<ITafsirNewRepository, TafsirNewRepository>();
            services.AddScoped<IJuzHeaderRepository, JuzHeaderRepository>();
            services.AddScoped<IJuzDetailRepository, JuzDetailRepository>();
            services.AddScoped<IHadithRepository, HadithRepository>();
            services.AddScoped<IAsmaulHusnaRepository, AsmaulHusnaRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddOptions<AppSettingModel>();

            services.AddDbContext<MyQuranContext>(opt =>
                //opt.UseLazyLoadingProxies()
                opt.UseSqlServer(configuration.GetConnectionString("MyQuranContext"),
                sqlServerOptions => sqlServerOptions.CommandTimeout(Convert.ToInt32(configuration.GetSection("AppSetting:CommandTimeout").Value)))                
                );

            //services.Configure<AppSettingModel>(configuration.GetSection("AppSettingModel"));

            return services;
        }
    }
}
