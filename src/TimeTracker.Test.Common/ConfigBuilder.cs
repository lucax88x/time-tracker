using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace TimeTracker.Test.Common
{
    public class ConfigBuilder
    {
        private readonly ConfigurationBuilder _configurationBuilder;

        public ConfigBuilder()
        {
            _configurationBuilder = new ConfigurationBuilder();

            var defaultConfig = new Dictionary<string, string>
            {
                {"Cors:Enabled", "true"},
                {"Cassandra:ContactPoint", "127.0.0.1"},
                {"Cassandra:Keyspace", "timetracker"},
                {"Redis:Endpoints:0", "localhost"}
            };

            Add(defaultConfig);

            _configurationBuilder.AddEnvironmentVariables("TimeTracker_");
        }

        public ConfigBuilder Add(Dictionary<string, string> settings)
        {
            _configurationBuilder.AddInMemoryCollection(settings);
            return this;
        }

        public IConfiguration Build()
        {
            return _configurationBuilder.Build();
        }

        public Autofac.Module BuildModule()
        {
            return new Config.Ioc.Module(Build());
        }
    }
}