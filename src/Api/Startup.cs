using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Nancy.Owin;

namespace api
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            
            app.UseOwin(x => x.UseNancy(
                opt => opt.Bootstrapper = new NancyBootstrapper()));
        }
    }
}
