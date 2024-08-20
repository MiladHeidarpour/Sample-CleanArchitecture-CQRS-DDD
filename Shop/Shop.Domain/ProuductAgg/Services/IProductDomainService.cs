using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProuductAgg.Services
{
    public interface IProductDomainService
    {
        bool IsSlugExsit(string slug);
    }
}
