using System;
using System.ServiceModel;
using System.Text;
using Nancy;
using Newtonsoft.Json;

namespace api
{
    public sealed class WeatherModule : NancyModule
    {
        public WeatherModule(IWeatherProvider weatherProvider)
        {
            Get("/", _ => {
                return "/current/{zip}";
            });

            Get("/current/{zip}", args =>
            {
                var zip = args["zip"];
                try
                {
                    var serializedWeather = JsonConvert.SerializeObject(weatherProvider.GetCurrent(zip));
                    var weatherBytes = Encoding.UTF8.GetBytes(serializedWeather);

                    return new Response
                    {
                        StatusCode = HttpStatusCode.OK,
                        ContentType = "application/json",
                        Contents = s => s.Write(weatherBytes, 0, weatherBytes.Length)
                    };
                }
                catch (CommunicationException e)
                {
                    var exceptionMessageBytes =  Encoding.UTF8.GetBytes(e.Message);

                    return new Response
                    {
                        StatusCode = HttpStatusCode.ServiceUnavailable,
                        ContentType = "application/json",
                        Contents = s => s.Write(exceptionMessageBytes, 0, exceptionMessageBytes.Length)
                    };
                }
            });
        }
    }
}