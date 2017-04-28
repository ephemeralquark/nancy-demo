using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Moq;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using Xunit;

namespace api.test
{
    public class WeatherModuleTests
    {
        private readonly Mock<IWeatherProvider> mockWeatherProvider;
        private Task<BrowserResponse> browserResponse;
        private Action<BrowserContext> browserContext;

        private readonly Browser browser;


        public WeatherModuleTests()
        {
            mockWeatherProvider = new Mock<IWeatherProvider>();

            browser = new Browser(config =>
            {
                config.Dependency<IWeatherProvider>(mockWeatherProvider.Object);
                config.Module<WeatherModule>();
            });
        }

        [Fact]
        public void when_we_request_index_it_returns_api_description()
        {
            browserResponse = browser.Get("/", with =>
            {
                with.HttpRequest();
                with.Header("Content-Type", "application/json");
                with.Header("Accept", "application/json");
            });

            Assert.Equal(HttpStatusCode.OK, browserResponse.Result.StatusCode);
            Assert.Equal("/current/{zip}", browserResponse.Result.Body.AsString());
        }

        [Fact]
        public void returns_current_weather_for_zip_from_weather_provider()
        {
            var weatherResponse = new Weather
            {
                Temperature = (decimal)98.6,
                RelativeHumidity = 50
            };
            mockWeatherProvider.Setup(mwp => mwp.GetCurrent(12345)).Returns(weatherResponse);

            browserResponse = browser.Get("/current/12345", with =>
            {
                with.HttpRequest();
                with.Header("Content-Type", "application/json");
                with.Header("Accept", "application/json");
            });

            mockWeatherProvider.Verify(mwp => mwp.GetCurrent(12345));
            Assert.Equal(JsonConvert.SerializeObject(weatherResponse), browserResponse.Result.Body.AsString());
            Assert.Equal(HttpStatusCode.OK, browserResponse.Result.StatusCode);
            Assert.Equal("application/json", browserResponse.Result.ContentType);
        }

        [Fact]
        public void handles_communication_exception_from_weather_provider()
        {
            mockWeatherProvider
                .Setup(mwp => mwp.GetCurrent(12345))
                .Throws(new CommunicationException());
            
            browserResponse = browser.Get("/current/12345", with =>
            {
                with.HttpRequest();
                with.Header("Content-Type", "application/json");
                with.Header("Accept", "application/json");
            });

            Assert.Equal(HttpStatusCode.ServiceUnavailable, browserResponse.Result.StatusCode);
        }
    }
}
