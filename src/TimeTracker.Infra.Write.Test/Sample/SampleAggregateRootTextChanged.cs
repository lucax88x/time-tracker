using System;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Infra.Write.Test.Sample
{
    public class SampleAggregateRootTextChanged : Event
    {
        public string Text { get; }

        public SampleAggregateRootTextChanged(Guid id, string text) : base(id)
        {
            Text = text;
        }
    }
}