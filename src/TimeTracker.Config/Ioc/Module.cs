using System;
using Autofac;
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
        }
    }
}