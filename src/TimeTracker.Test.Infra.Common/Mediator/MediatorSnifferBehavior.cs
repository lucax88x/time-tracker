using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TimeTracker.Test.Infra.Common.Mediator
{
    public class MediatorSnifferBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly MediatorSniffer _mediatorSniffer;

        public MediatorSnifferBehavior(MediatorSniffer mediatorSniffer)
        {
            _mediatorSniffer = mediatorSniffer;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _mediatorSniffer.Add(typeof(TRequest).Name);            

            return await next();           
        }
    }
}