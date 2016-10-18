using System;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace Api.Test
{
    public class WeatherModuleTests
    {
        private Task<BrowserResponse> browserResponse;
        private Action<BrowserContext> browserContext;

        private readonly Browser browser;


        public WeatherModuleTests()
        {
            browser = new Browser(config =>
            {
                config.Module<WeatherModule>();
            });
        }

        [Fact]
        public void when_we_request_index_it_returns_api_description()
        {
            browserContext = with =>
            {
                with.HttpRequest();
                with.Header("Content-Type", "application/json");
                with.Header("Accept", "application/json");
            };

            browserResponse = browser.Get("/", browserContext);

            Assert.Equal(HttpStatusCode.OK, browserResponse.Result.StatusCode);
            Assert.Equal("current/{zip}", browserResponse.Result.Body.AsString());
        }
    }
}
