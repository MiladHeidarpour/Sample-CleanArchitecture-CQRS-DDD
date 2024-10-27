using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.ValueObjects;

namespace Shop.Domain.OrderAgg;

public class Order : AggregateRoot
{
    private Order()
    {

    }
    public Order(long userId)
    {
        UserId = userId;
        Status = OrderStatus.Pending;
        Items = new List<OrderItem>();
    }
    public long UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderDiscount? Discount { get; private set; }
    public OrderAddress? Address { get; private set; }
    public OrderShippingMethod? ShippingMethod { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public DateTime? LastUpdate { get; set; }
    public int TotalPrice { get
        {
            var totalPrice = Items.Sum(f => f.TotalPrice);

            if (ShippingMethod != null)
            {
                totalPrice += ShippingMethod.ShippingCost;
            }
            if (Discount!=null)
            {
                totalPrice -= Discount.DiscountAmount;
            }

            return 0;
        } }
    public int ItemCount => Items.Count;

    public void AddItem(OrderItem item)
    {
        ChangeOrderGaurd();

        var oldItem = Items.FirstOrDefault(f => f.InventoryId == item.InventoryId);
        if (oldItem != null)
        {
            oldItem.ChangeCount(item.Count + oldItem.Count);
            return;
        }
        Items.Add(item);
    }
    public void RemoveItem(long itemId)
    {
        ChangeOrderGaurd();
        var currentItem = Items.FirstOrDefault(s => s.Id == itemId);
        if (currentItem != null)
        {
            Items.Remove(currentItem);
        }
    }
    public void ChangeCountItem(long itemId, int newCount)
    {
        ChangeOrderGaurd();
        var currentItem = Items.FirstOrDefault(s => s.Id == itemId);
        if (currentItem == null)
        {
            throw new NullOrEmptyDomainDataException();
        }
        currentItem.ChangeCount(newCount);
    }

    public void IncreaseItmeCount(long itemId,int count)
    {
        var currentItem = Items.FirstOrDefault(s => s.Id == itemId);
        if (currentItem == null)
        {
            throw new NullOrEmptyDomainDataException();
        }
        currentItem.IncreaseCount(count);
    }
    public void DecreaseItmeCount(long itemId, int count)
    {
        var currentItem = Items.FirstOrDefault(s => s.Id == itemId);
        if (currentItem == null)
        {
            throw new NullOrEmptyDomainDataException();
        }
        currentItem.DecreaseCount(count);
    }
    public void ChangeStatus(OrderStatus status)
    {
        Status = status;
        LastUpdate = DateTime.Now;
    }
    public void CheckOut(OrderAddress orderAddress)
    {
        ChangeOrderGaurd();
        Address = orderAddress;
    }

    public void ChangeOrderGaurd()
    {
        if (Status != OrderStatus.Pending)
        {
            throw new InvalidDomainDataException("امکان ویرایش این سفارش وجود ندارد");
        }
    }
}
