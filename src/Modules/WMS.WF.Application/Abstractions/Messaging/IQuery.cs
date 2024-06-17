using MediatR;

namespace WMS.WF.Application.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;