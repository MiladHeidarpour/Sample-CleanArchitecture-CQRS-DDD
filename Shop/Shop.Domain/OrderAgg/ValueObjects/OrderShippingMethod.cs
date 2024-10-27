using Common.Domain;

namespace Shop.Domain.OrderAgg.ValueObjects;

public class OrderShippingMethod : ValueObject
{
    public string ShippingType { get;private set; }
    public int ShippingCost { get; private set; }
    public OrderShippingMethod(string shippingType, int shippingCost)
    {
        ShippingType = shippingType;
        ShippingCost = shippingCost;
    }
}
