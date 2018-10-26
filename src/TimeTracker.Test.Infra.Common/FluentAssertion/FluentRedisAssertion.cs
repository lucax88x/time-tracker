using System;
using System.Threading.Tasks;
using FluentAssertions;
using TimeTracker.Infra.Read.Core;

namespace TimeTracker.Test.Infra.Common.FluentAssertion
{
    public class FluentRedisAssertion
    {
        private readonly ReadRepositoryFactory _readRepositoryFactory;

        public FluentRedisAssertion(ReadRepositoryFactory readRepositoryFactory)
        {
            _readRepositoryFactory = readRepositoryFactory;
        }

        public async Task Exists(string prefix, Guid id)
        {
            var readRepository = _readRepositoryFactory.Build(prefix);

            var result = await readRepository.Exists(id);

            if (!result) true.Should().BeFalse($"does not exist with {id}!");

            true.Should().BeTrue();
        }
    }
}