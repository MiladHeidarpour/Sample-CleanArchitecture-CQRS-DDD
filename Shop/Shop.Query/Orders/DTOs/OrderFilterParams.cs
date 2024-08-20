using Shop.Domain.OrderAgg.Enums;
using Common.Query.Filter;

namespace Shop.Query.Orders.DTOs;

public class OrderFilterParams : BaseFilterParam
{
    public long? UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public OrderStatus? Status { get; set; }
    //public string Shire { get; set; }//استان
    //public string City { get; set; }
}
