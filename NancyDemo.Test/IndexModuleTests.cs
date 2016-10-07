using System;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using NancyDemo.Web;
using Xunit;

namespace NancyDemo.Test
{
    public class IndexModuleTests
    {
        private Task<BrowserResponse> browserResponse;
        private Action<BrowserContext> browserContext;

        private readonly Browser browser;

        
        public IndexModuleTests()
        {
            browser = new Browser(config =>
            {
                config.Module<IndexModule>();
            });
        }

        [Fact]
        public void when_we_request_claims_it_checks_if_we_have_a_valid_session()
        {
            browserContext = with =>
            {
                with.HttpRequest();
                with.Header("Content-Type", "application/json");
                with.Header("Accept", "application/json");
            };

            browserResponse = browser.Get("/", browserContext);

            Assert.Equal(HttpStatusCode.OK, browserResponse.Result.StatusCode);
        }
    }
}
