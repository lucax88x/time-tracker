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
                var resp = argument.Split('.').Last();
                
                _mediatorSniffer.Add(resp);
            }
            
            invocation.Proceed();
        }
    }
}