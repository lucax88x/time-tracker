using System;
using Autofac;
using TimeTracker.Infra.Write.Migrations;

namespace TimeTracker.Test.Infra.Common.Cassandra
{
    public class DropCreateDatabase : IDisposable
    {
        private readonly IContainer _container;

        public DropCreateDatabase()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TimeTracker.Infra.Write.Migrations.Ioc.Module());
            _container = builder.Build();
            
            // how to kill db?
            RunMigrations();
        }

        private void RunMigrations()
        {
            var migrator = _container.Resolve<Migrator>();
            migrator.Up();            
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}