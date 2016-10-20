using System;
using Nancy;
using Nancy.TinyIoc;
using WeatherWS;

namespace api
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        public NancyBootstrapper(AppConfiguration appConfig)
        {
            /*
            We could register appConfig as an instance in the container which can
            be injected into areas that need it or we could create our own INancyEnvironment
            extension and use that.
            */
            Console.WriteLine("SMTP Server: " + appConfig.Smtp.Server);
            Console.WriteLine("Log Levels (includes scopes? " + appConfig.Logging.IncludeScopes + ")");
            Console.WriteLine("System: " + appConfig.Logging.LogLevel.System);
            Console.WriteLine("Default: " + appConfig.Logging.LogLevel.Default);
            Console.WriteLine("Microsoft: " + appConfig.Logging.LogLevel.Microsoft);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<WeatherSoap>(new WeatherSoapClient(WeatherSoapClient.EndpointConfiguration.WeatherSoap));
        }
    }
}
