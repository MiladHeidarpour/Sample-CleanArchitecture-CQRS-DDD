using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF;

namespace Shop.Infrastructure.Persistent.Ef.UserAgg
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ShopContext context) : base(context)
        {
        }
    }
}