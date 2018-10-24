using System;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Infra.Write.Test.Sample
{
    public class SampleAggregateRootTextCleared : Event
    {
        public SampleAggregateRootTextCleared(Guid id) : base(id)
        {
        }
    }
}