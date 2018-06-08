using System;

namespace Calculator.Test.e2e
{
    public class DockerSettings: TestConfiguration
    {
        public string Ip { get; } = ContainerManager.GetDockerMachineIp();
        public string Url =>"http://" + Ip + ":" + ExternalPort;
        public string MachineName { get; } = Environment.MachineName;

        public DockerSettings(TestConfiguration config)
        {
            this.DockerComposePath = config.DockerComposePath;
            this.ExternalPort = config.ExternalPort;
            this.Tag = config.Tag;
            this.OpenChrome = config.OpenChrome;
            this.RetryAttemps = config.RetryAttemps;
        }
    }

}
