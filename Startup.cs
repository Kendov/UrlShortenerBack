using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using urlShortener.Data;
using urlShortener.Extensions;
using urlShortener.Services;
using urlShortener.Services.DomainNotification;

namespace urlShortener
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


            services.AddScoped<INotificationContext, NotificationContext>();


            services.Configure<DbConfiguration>(
            Configuration.GetSection("ConnectionStrings"));

            services.AddSingleton<IDbConfiguration>(sp =>
            sp.GetRequiredService<IOptions<DbConfiguration>>().Value);

            services.AddSingleton<MongoDbContext>();
            services.AddScoped<IUrlDataService, UrlDataService>();

            // Auto Mapper Configurations
            var mapconfig = new MapperConfiguration( x =>
           {
               x.AddProfile(new AutoMapperConfig());
           });
            IMapper mapper = mapconfig.CreateMapper();
            services.AddSingleton(mapper);

            //Enable Cross-Origin Requests
            services.AddCors(option => option.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddControllers(options =>
            {
                options.Filters.Add<NotificationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.ConfigureExceptionHandler();
            //app.ConfigureCustomExceptionMiddleware();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
