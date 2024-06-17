using MediatR;
using WMS.Core.Domain.Shared.Results;

namespace WMS.Core.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;