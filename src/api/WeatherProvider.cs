using System;
using System.ServiceModel;
using WeatherWS;

namespace api
{
    public class WeatherProvider : IWeatherProvider
    {
        private readonly WeatherSoap weatherSoap;

        public WeatherProvider(WeatherSoap weatherSoap)
        {
            this.weatherSoap = weatherSoap;
        }

        public Weather GetCurrent(int zip)
        {
            var getWeatherRequest = new GetCityWeatherByZIPRequest
            {
                Body = new GetCityWeatherByZIPRequestBody
                {
                    ZIP = zip.ToString()
                }
            };

            try
            {
                var weatherServiceReturn = weatherSoap.GetCityWeatherByZIPAsync(getWeatherRequest).Result;
                return new Weather
                {
                    Temperature = Convert.ToDecimal(weatherServiceReturn.Body.GetCityWeatherByZIPResult.Temperature)
                };
            }
            catch (Exception e)
            {
                throw new CommunicationException("CDYNE failed to retrieve resource", e);
            }
        }
    }
}