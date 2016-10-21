using Nancy;
using Nancy.TinyIoc;
using WeatherWS;

namespace api
{
    public class NancyBootstrapper : DefaultNancyBootstrapper
    {
        public NancyBootstrapper() {}

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<WeatherSoap>(new WeatherSoapClient(WeatherSoapClient.EndpointConfiguration.WeatherSoap));
        }
    }
}
