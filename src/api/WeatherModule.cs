using System.Text;
using Nancy;
using Newtonsoft.Json;

namespace api
{
    public sealed class WeatherModule : NancyModule
    {
        public WeatherModule(IWeatherProvider weatherProvider)
        {
            Get("/", args => "/{zip}");

            Get("/{zip}", args =>
            {
                var zip = args["zip"];
                var serializedWeather = JsonConvert.SerializeObject(weatherProvider.GetCurrent(zip));
                var weatherBytes = Encoding.UTF8.GetBytes(serializedWeather);

                return new Response
                {
                    StatusCode = HttpStatusCode.Found,
                    ContentType = "application/json",
                    Contents = s => s.Write(weatherBytes, 0, weatherBytes.Length)
                };
            });
        }
    }
}