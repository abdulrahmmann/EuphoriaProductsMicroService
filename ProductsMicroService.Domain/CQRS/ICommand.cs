using MediatR;

namespace ProductsMicroService.Domain.CQRS;

public interface ICommand<out TResponse>: IRequest<TResponse>
{
    
}

public interface ICommand : ICommand<Unit>
{
    
}