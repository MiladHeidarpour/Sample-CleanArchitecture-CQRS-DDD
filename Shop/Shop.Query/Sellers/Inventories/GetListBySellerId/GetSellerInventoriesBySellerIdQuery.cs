using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Query.Sellers.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Sellers.Inventories.GetListBySellerId;

public record GetSellerInventoriesBySellerIdQuery(long SellerId) : IQuery<List<InventoryDto>>;

