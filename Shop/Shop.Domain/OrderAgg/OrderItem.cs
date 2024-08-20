using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.OrderAgg;

public class OrderItem : BaseEntity
{
    public long OrderId { get; internal set; }
    public long InventoryId { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public int TotalPrice => Price * Count;
    public OrderItem(long inventoryId, int count, int price)
    {
        PriceGaurd(price);
        CountGaurd(count);
        InventoryId = inventoryId;
        Count = count;
        Price = price;
    }

    public void ChangeCount(int newCount)
    {
        CountGaurd(newCount);
        Count = newCount;
    }
    public void IncreaseCount(int count)
    {
        Count += count;
    }
    public void DecreaseCount(int count)
    {
        if (Count == 1)
        {
            return;
        }
        if (Count - count <= 0)
        {
            return;
        }
        Count -= count;
    }
    public void SetPrice(int newPrice)
    {
        PriceGaurd(newPrice);
        Price = newPrice;
    }

    public void PriceGaurd(int newPrice)
    {
        if (newPrice < 1)
        {
            throw new InvalidDomainDataException("مبلغ کالا نامعتبر است");
        }
    }
    public void CountGaurd(int newCount)
    {
        if (Count < 1)
        {
            throw new InvalidDomainDataException();
        }
    }
}
