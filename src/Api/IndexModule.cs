using Nancy;

namespace Api
{
    public sealed class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get("/", args => "Hello World");
        }
    }
}