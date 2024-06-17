using MediatR;
using WMS.Core.Domain.Shared.Results;

namespace WMS.Core.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;