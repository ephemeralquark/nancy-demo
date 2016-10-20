using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Nancy.Owin;

namespace api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void Configure(IApplicationBuilder app)
        {
            var config = this.Configuration;
            var appConfig = new AppConfiguration();
            ConfigurationBinder.Bind(config, appConfig);
            
            app.UseOwin(x => x.UseNancy(
                opt => opt.Bootstrapper = new NancyBootstrapper(appConfig)));
        }
    }
}
