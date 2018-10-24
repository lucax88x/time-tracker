using System;
using TimeTracker.Core;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Infra.Write.Test.Sample
{
    internal class SampleAggregateRoot : AggregateRoot
    {
        public string Text { get; private set; }

        public SampleAggregateRoot()
        {
        }

        public SampleAggregateRoot(Guid id, string text, int version)
        {
            ApplyChange(new SampleAggregateRootCreated(id, text));
            Version = version;
        }

        public void ChangeText(string text)
        {
            ApplyChange(new SampleAggregateRootTextChanged(Id, text));
        }        
        
        public void ClearText()
        {
            ApplyChange(new SampleAggregateRootTextCleared(Id));
        }

        protected override void Apply(Event @event)
        {
            switch (@event)
            {
                case SampleAggregateRootCreated created:
                {
                    Id = created.Id;
                    Text = created.Text;
                    return;
                }
                case SampleAggregateRootTextChanged textChanged:
                {
                    Text = textChanged.Text;
                    return;
                }                
                case SampleAggregateRootTextCleared _:
                {
                    Text = string.Empty;
                    return;
                }
            }
        }
    }
}