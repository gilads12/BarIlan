using Microsoft.Extensions.Configuration;

namespace Calculator.Test.common
{
    public class TestHelper
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("test_config.json")
                .Build();
            return config;
        }
    }
}
