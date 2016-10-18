using System.Threading.Tasks;
using api;
using Moq;
using WeatherWS;
using Xunit;

namespace Api.Test
{
    public class WeatherProviderTest
    {
        private readonly Mock<WeatherSoap> mockWeatherSoapService;
        private readonly IWeatherProvider subject;

        public WeatherProviderTest()
        {
            mockWeatherSoapService = new Mock<WeatherSoap>();
            subject = new WeatherProvider(mockWeatherSoapService.Object);
        }

        [Fact]
        public void calls_CDYNE_webservice_to_get_current_weather_info_for_given_zip()
        {
            var getCityWeatherByZipResponse = new GetCityWeatherByZIPResponse
            {
                Body = new GetCityWeatherByZIPResponseBody
                {
                    GetCityWeatherByZIPResult = new WeatherReturn
                    {
                        Temperature = "99.1"
                    }
                }
            };

            var task = new Task<GetCityWeatherByZIPResponse>(() => getCityWeatherByZipResponse);
            task.RunSynchronously();
            mockWeatherSoapService
                .Setup(ws => ws.GetCityWeatherByZIPAsync(It.Is<GetCityWeatherByZIPRequest>(r => r.Body.ZIP.Equals("12345"))))
                .Returns(task);

            var weather = subject.GetCurrent(12345);

            Assert.Equal((decimal) 99.1, weather.Temperature);
        }
    }
}