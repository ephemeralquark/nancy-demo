using System;
using Nancy;

namespace NancyDemo.Web
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        public NancyBootstrapper()
        {
            
        }
        
        public NancyBootstrapper(AppConfiguration appConfig)
        {
            /*
            We could register appConfig as an instance in the container which can
            be injected into areas that need it or we could create our own INancyEnvironment
            extension and use that.
            */
            Console.WriteLine(appConfig.Smtp.Server);
            Console.WriteLine(appConfig.Smtp.User);
            Console.WriteLine(appConfig.Logging.IncludeScopes);
        }
    }   
}