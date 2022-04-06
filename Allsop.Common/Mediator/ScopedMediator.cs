using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Allsop.Common.Mediator
{
    public class ScopedMediator : IScopedMediator
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ScopedMediator(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return await mediator.Send(request, cancellationToken); // await necessary because of the using!
            }
        }

        public async Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                return await mediator.Send(request, cancellationToken); // await necessary because of the using!
            }
        }
    }
}
