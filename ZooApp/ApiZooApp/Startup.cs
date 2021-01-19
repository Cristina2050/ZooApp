using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ZooServices;
using ZooServices.Automapper;
using NLog.Extensions.Logging;
using NLog.Web;
using ZooServices.BusinessLayer;
using ZooServices.Contracts.Zoo;
using ZooServices.Model;
using ZooServices.Repository.MySQL.Contract;
using ZooServices.Repository.MySQL.Service;

namespace ApiZooApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Swagger
            ConfigureSwagger(services);
            #endregion

            #region BD Connection
            //var connZoo = _env.IsDevelopment() ? Configuration.GetConnectionString("ZOO_CONNECTION") : Configuration.GetConnectionString();
            var connZoo = Configuration.GetConnectionString("ZOO_CONNECTION");
            services.AddDbContext<DbContextZoo>(options => options.UseMySql(connZoo));
            #endregion

            #region Automapper
            services.AddAutoMapper(typeof(ProfileMapZoo));
            #endregion

            #region Entities            
            services.AddScoped<IAnimalManager, AnimalManager>();
            #endregion

            #region Repository
            services.AddScoped<IZooRepository<Animal>, ZooRepository<Animal>>();
            #endregion

            services.AddLogging(logging =>
            {
                logging.AddNLog();
            });
            services.AddControllers();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(Swagger =>
            {
                Swagger.EnableAnnotations();
                Swagger.AddServer(new OpenApiServer()
                {
                    Url = "http://localhost:44399",  
                    Description = "Local development server"
                });
                Swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = "Zoo Administration Web Api",
                    Description = "It allows manage informarion of the Zoo.",
                    Contact = new OpenApiContact()
                    {
                        Name = "Ing. Cristina Morales O.",
                        Email = "cmorales2350@gmail.com",
                        Url = new System.Uri("https://www.google.com/"),
                    }
                });
                
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string url = string.Empty;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(SwaggerUI => {
                if (_env.IsDevelopment())                
                    url = "/swagger/v1/swagger.json";
                
                else                
                    url = "../swagger/v1/swagger.json";                

                SwaggerUI.SwaggerEndpoint(url, "Zoo Service v1.0.0");
            });
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
