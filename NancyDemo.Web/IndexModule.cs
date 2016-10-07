using Nancy;

namespace NancyDemo.Web
{
    public sealed class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get("/", args => "Hello World");
        }
    }
}