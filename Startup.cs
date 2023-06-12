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

            // Inyección de dependencias

            // Registra la clase RedisConnection como un servicio singleton en la aplicación.
            // Un servicio singleton se crea una vez y se comparte en toda la aplicación.
            services.AddSingleton<RedisConnection>();

            /*
             * Registra la implementación de ITareasRepository con RedisTareasRepository como un servicio 
             * de ámbito.
             * Un servicio de ámbito se crea una vez por solicitud y se comparte entre los componentes dentro de esa solicitud.
             * RedisTareasRepository accede a los datos en Redis y proporciona métodos para interactuar con la base de datos de Redis.
             */
            services.AddScoped<ITareasRepository, RedisTareasRepository>();

            /*
             * Registra la implementación de ITareasServices con TareasServices como un servicio de ámbito.
             * TareasServices encapsula la lógica de negocio relacionada con las tareas
             * y se utiliza para realizar operaciones relacionadas con las tareas.
             */
            services.AddScoped<ITareasServices, TareasServices>();

            /*
             * Registra la implementación de ITareasAppServices con TareasAppServices 
             * como un servicio de ámbito.
             * TareasAppServices actúa como una capa de aplicación que se comunica con 
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
