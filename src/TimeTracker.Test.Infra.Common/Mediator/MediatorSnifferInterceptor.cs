using System;
using System.Linq;
using Castle.DynamicProxy;

namespace TimeTracker.Test.Infra.Common.Mediator
{
    public class MediatorSnifferInterceptor : IInterceptor
    {
        private readonly MediatorSniffer _mediatorSniffer;

        public MediatorSnifferInterceptor(MediatorSniffer mediatorSniffer)
        {
            _mediatorSniffer = mediatorSniffer;
        }

        public void Intercept(IInvocation invocation)
        {           
            var argument = invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray().FirstOrDefault();

            if (argument != null)
            {
                var sub = argument.Substring(argument.IndexOf("TimeTracker", StringComparison.Ordinal));
                sub = sub.Substring(0, sub.IndexOf("]", StringComparison.Ordinal));
                var resp = sub.Split('.').Last();
                
                _mediatorSniffer.Add(resp);
            }
            
            invocation.Proceed();
        }
    }
}