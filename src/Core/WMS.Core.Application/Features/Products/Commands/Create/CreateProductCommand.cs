using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Application.Contracts.Requests.Products;

namespace WMS.Core.Application.Features.Products.Commands.Create;

public record CreateProductCommand(CreateProductRequest CreateProductRequest) : ICommand<int>;