using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using KeesTalksTech.Assessments.Wehkamp.Store.StoreApiClient;
using KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Configuration;
using KeesTalksTech.Assessments.Wehkamp.Store.WebApp.Helpers;

namespace KeesTalksTech.Assessments.Wehkamp.Store.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddTransient<IStoreSdkClient>((provider) =>
            {
                var sdkClientSettings = new StoreSdkClientSettingsByConfig(Configuration);
                var sdkClient = new StoreSdkClient(sdkClientSettings);
                return sdkClient;
            });

            //add URL builder
            services.AddSingleton<IComposer, ComposerImplementationUpperCase>();
            services.AddSingleton<MyComponent, MyComponent>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Basket}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Products}/{action=Index}/{id?}");
            });


        }
    }
}
