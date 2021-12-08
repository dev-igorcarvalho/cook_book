using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using dot_net_api.Context;
using dot_net_api.Handlers;
using Microsoft.EntityFrameworkCore;
using dot_net_api.Http_filters;
using dot_net_api.Repositories;

namespace dot_net_api
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
            services.AddScoped<EventoRepository>(); // registra a classe no injetor de dependencias
            services
                .AddDbContext<ApplicationDbContext>
                (
                    options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddControllers(options => { options.Filters.Add<SimpleFilter>(); });




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.ConfigureExceptionHandler();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
