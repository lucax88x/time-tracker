using System;
using TimeTracker.Core;
using TimeTracker.Core.Interfaces;

namespace TimeTracker.Infra.Write.Test.Sample
{
    internal class SampleAggregateRoot : AggregateRoot
    {
        public string Text { get; private set; }

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
        
        public static SampleAggregateRoot Create(Guid id, string text, int version)
        {
            var instance = new SampleAggregateRoot();

            instance.ApplyChange(new SampleAggregateRootCreated(id, text));
            instance.Version = version;

            return instance;
        }
    }
}