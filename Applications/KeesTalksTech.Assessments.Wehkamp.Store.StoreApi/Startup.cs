using KeesTalksTech.Assessments.Wehkamp.Store.Core.Catalog;
using KeesTalksTech.Assessments.Wehkamp.Store.Core.Shopping;
using KeesTalksTech.Assessments.Wehkamp.Store.Shopping;
using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog;
using KeesTalksTech.Assessments.Wehkamp.Store.UnsplashCatalog.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;

namespace KeesTalksTech.Assessments.Wehkamp.Store.StoreApi
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

            //add domain services configuration

            var serviceProvider = services.BuildServiceProvider();
            var env = serviceProvider.GetService<IHostingEnvironment>();

            //1. Configure product service
            //services.AddSingleton<IUnsplashPhotoRepository, UnsplashPhotoMemoryRepository>();
            var productXmlDataFilePath = Path.Combine(env.ContentRootPath, "data", "products.xml");
            services.AddSingleton<IPhotoProductRepository>(new PhotoProductXmlRepository(productXmlDataFilePath));
            services.AddTransient<IProductService, PhotoProductService>();

            //2. Configure basket service
            services.AddSingleton<IBasketRepository, BasketMemoryRepository>();
            services.AddTransient<IBasketService, BasketService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //add some error handling middleware!
            app.UseMvc();

            //get some nice plaintext errors
            app.UseStatusCodePages();
        }
    }
}
