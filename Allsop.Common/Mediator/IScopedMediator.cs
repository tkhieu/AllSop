using MediatR;

namespace Allsop.Common.Mediator
{
    public interface IScopedMediator
    {
        Task<object> Send(object request, CancellationToken cancellationToken = default);
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
}
