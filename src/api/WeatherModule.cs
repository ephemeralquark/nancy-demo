using Nancy;

namespace Api
{
    public sealed class WeatherModule : NancyModule
    {
        public WeatherModule()
        {
            Get("/", args => "current/{zip}");
        }
    }
}