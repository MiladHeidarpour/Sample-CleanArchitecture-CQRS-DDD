using Shop.Domain.RoleAgg;
using Shop.Domain.RoleAgg.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF;

namespace Shop.Infrastructure.Persistent.Ef.RoleAgg;

internal class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(ShopContext context) : base(context)
    {
    }
}