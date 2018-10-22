using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using TimeTracker.Config;
using TimeTracker.Web.Api.Filters;
using Module = TimeTracker.Web.Api.Ioc.Module;

namespace TimeTracker
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            services
                .AddMvc(options =>
                    options.Filters.AddService(typeof(ApiExceptionFilter)))
                .AddJsonOptions(options =>
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            
            services.AddScoped<ApiExceptionFilter>();
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new Module(Configuration));
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Cors cors)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureCors(app, cors);
            ConfigureLogger();
            app.UseMvc();
        }
        
        private static void ConfigureCors(IApplicationBuilder app, Cors cors)
        {
            if (cors.Enabled)
            {
                app.UseCors(builder => builder
                    .WithOrigins("http://localhost:3000")
                    .WithHeaders("authorization", "content-type", "cache-control", "pragma", "expires", "if-modified-since")
                    .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE"));
            }
        }
        
        private void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.RollingFile("logs/api-{Hour}.txt")
                .WriteTo.Console()
                .CreateLogger();
        }

    }
}