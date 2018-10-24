using System;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Infra.Write.Test.Sample
{
    public class SampleAggregateRootCreated : Event
    {
        public string Text { get; }

        public SampleAggregateRootCreated(Guid id, string text) : base(id)
        {
            Text = text;
        }
    }
}