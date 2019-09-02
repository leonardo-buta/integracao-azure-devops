using AutoMapper;
using Integracao.AzureDevops.Application.DTO;
using Integracao.AzureDevops.Domain.Models;
using Integracao.AzureDevops.Infra.Context;
using Integracao.AzureDevops.Infra.CrossCutting.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace Integracao.AzureDevops.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IntegracaoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Integração Azure DevOps"
                });
            });

            RegisterServices(services, Configuration);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AzureWorkItem, WorkItemDTO>();
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Integração API V1");
            });
        }

        private static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            NativeInjector.RegisterServices(services, configuration);
        }
    }
}
