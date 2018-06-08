namespace Calculator.Test.e2e
{

    public class TestConfiguration
    {
        public bool OpenChrome { get; set; } = true;
        public int RetryAttemps { get; set; } = 3;
        public string Tag { get; set; } = "latest";
        public string ExternalPort { get; set; } = "5002";
        public string DockerComposePath { get; set; } = @"../../../docker-compose-e2e.yml";
    }

}
