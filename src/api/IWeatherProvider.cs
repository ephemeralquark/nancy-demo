namespace api
{
    public interface IWeatherProvider
    {
        Weather GetCurrent(int zip);
    }
}