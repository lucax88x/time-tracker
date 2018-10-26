using System;
using Autofac;
using eInvoice.Config;
using Microsoft.Extensions.Configuration;

namespace TimeTracker.Config.Ioc
{
    public class Module : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public Module(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
                    new Cors
                    {
                        Enabled = Convert.ToBoolean(_configuration.GetSection("Cors:Enabled").Value)
                    })
                .SingleInstance();
            
            builder.Register(c =>
                    new Cassandra
                    {
                        ContactPoint = _configuration.GetSection("Cassandra:ContactPoint").Value,
                        Keyspace = _configuration.GetSection("Cassandra:Keyspace").Value
                    })
                .SingleInstance();

            builder.Register(c =>
                {
                    var redis = new Redis();

                    var endpoints = _configuration.GetSection("Redis:Endpoints");
                    foreach (var endpoint in endpoints.GetChildren())
                    {
                        redis.Endpoints.Add(endpoint.Value);
                    }

                    return redis;
                })
                .SingleInstance();
        }
    }
}