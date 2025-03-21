﻿using Domain.Entities.Abstractions;
using MediatR;
namespace Application.Abstraction.Messaging
{
    public interface  IQueryHandler<TQuery,TResponse>:IRequestHandler<TQuery,Result<TResponse>>
         where TQuery : IQuery<TResponse>
    {
        
    }
}
