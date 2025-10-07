using MediatR;

namespace ProductsMicroService.Domain.CQRS;

public interface IQuery<out T>: IRequest<T> where T : notnull
{
    
}