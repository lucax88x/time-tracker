using FluentAssertions;
using TimeTracker.Test.Infra.Common.Mediator;

namespace TimeTracker.Test.Infra.Common.FluentAssertion
{
    public class FluentMediatorAssertion
    {
        private readonly MediatorSniffer _mediatorSniffer;

        public FluentMediatorAssertion(MediatorSniffer mediatorSniffer)
        {
            _mediatorSniffer = mediatorSniffer;
        }

        public void Be(string expected)
        {
            var mediator = _mediatorSniffer.ToString();
            mediator.Should().Be(expected);
        }
    }
}