using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RedisApi.CrossCutting;
using RedisApi.Domain.Interfaces;
using RedisApi.Domain.Services;
using RedisApi.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisApi
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

            // Inyecci�n de dependencias

            // Registra la clase RedisConnection como un servicio singleton en la aplicaci�n.
            // Un servicio singleton se crea una vez y se comparte en toda la aplicaci�n.
            services.AddSingleton<RedisConnection>();

            /*
             * Registra la implementaci�n de ITareasRepository con RedisTareasRepository como un servicio 
             * de �mbito.
             * Un servicio de �mbito se crea una vez por solicitud y se comparte entre los componentes dentro de esa solicitud.
             * RedisTareasRepository accede a los datos en Redis y proporciona m�todos para interactuar con la base de datos de Redis.
             */
            services.AddScoped<ITareasRepository, RedisTareasRepository>();

            /*
             * Registra la implementaci�n de ITareasServices con TareasServices como un servicio de �mbito.
             * TareasServices encapsula la l�gica de negocio relacionada con las tareas
             * y se utiliza para realizar operaciones relacionadas con las tareas.
             */
            services.AddScoped<ITareasServices, TareasServices>();

            /*
             * Registra la implementaci�n de ITareasAppServices con TareasAppServices 
             * como un servicio de �mbito.
             * TareasAppServices act�a como una capa de aplicaci�n que se comunica con 
             * los controladores y los servicios de dominio para manejar las solicitudes
             * relacionadas con las tareas.
             */
            services.AddScoped<ITareasAppServices, TareasAppServices>();
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

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
