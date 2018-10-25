using TimeTracker.Core;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Infra.Write.Test.Sample
{
    internal class EmptyAggregateRoot : AggregateRoot
    {
        protected override void Apply(Event @event)
        {
        }

        public static EmptyAggregateRoot Create()
        {
            return new EmptyAggregateRoot();
        }
    }
}