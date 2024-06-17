using WMS.Core.Application.Abstractions.Messaging;
using WMS.Core.Domain.Entities;
using WMS.Core.Domain.Shared.Results;
using WMS.Core.Infrastructure.Data.Uow;

namespace WMS.Core.Application.Features.Products.Commands.Create;

internal sealed class CreateProductCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, int>
{
    public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productRepository = unitOfWork.GetRepository<Product>();

        var product = Product.Create(
            request.CreateProductRequest.Code,
            request.CreateProductRequest.Name,
            request.CreateProductRequest.ImagePath,
            request.CreateProductRequest.ExpirationPeriodType,
            request.CreateProductRequest.ExpirationPeriodValue,
            request.CreateProductRequest.CategoryId);

        productRepository.Insert(product);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return product.RowId;
    }
}