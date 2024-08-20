
using Common.Application;
using Shop.Domain.OrderAgg.Repositories;

namespace Shop.Application.Orders.IncreaseItemCount;

public class IncreaseOrderItemCountCommandHandler : IBaseCommandHandler<IncreaseOrderItemCountCommand>
{
    private readonly IOrderRepository _repository;

    public IncreaseOrderItemCountCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(IncreaseOrderItemCountCommand request, CancellationToken cancellationToken)
    {
        var currentOrder = await _repository.GetCurrentUserOrder(request.UserId);
        if(currentOrder == null) 
        {
            return OperationResult.NotFound();
        }
        currentOrder.IncreaseItmeCount(request.ItemId, request.Count);
        await _repository.Save();
        return OperationResult.Success();
    }
}
