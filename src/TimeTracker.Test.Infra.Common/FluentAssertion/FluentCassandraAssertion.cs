using System;
using System.Threading.Tasks;
using FluentAssertions;
using TimeTracker.Infra.Write;

namespace TimeTracker.Test.Infra.Common.FluentAssertion
{
    public class FluentCassandraAssertion
    {
        private readonly IWriteRepository _writeRepository;

        public FluentCassandraAssertion(IWriteRepository writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task Exists(Guid id)
        {
            var result = await _writeRepository.Exists(id);

            if (!result) true.Should().BeFalse($"does not exist with {id}!");

            true.Should().BeTrue();
        }
    }
}