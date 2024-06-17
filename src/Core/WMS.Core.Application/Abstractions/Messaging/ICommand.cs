using MediatR;
using WMS.Core.Domain.Shared.Results;

namespace WMS.Core.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;