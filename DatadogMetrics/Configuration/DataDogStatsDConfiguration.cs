using StatsdClient;

namespace DatadogMetrics.Configuration;

public static class DataDogStatsDConfiguration
{
    public static readonly StatsdConfig DogstatsdConfig = new()
    {
        StatsdServerName = "127.0.0.1",
        //StatsdPort = 8125,
    };
}
