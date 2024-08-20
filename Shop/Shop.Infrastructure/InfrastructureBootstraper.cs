using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.CategoryAgg.Repositories;
using Shop.Domain.CommentAgg.Repositories;
using Shop.Domain.OrderAgg.Repositories;
using Shop.Domain.ProuductAgg.Repositories;
using Shop.Domain.RoleAgg.Repositories;
using Shop.Domain.SellerAgg.Repositories;
using Shop.Domain.SiteEntities.Repositories;
using Shop.Domain.UserAgg.Repositories;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.Ef.CategoryAgg;
using Shop.Infrastructure.Persistent.Ef.RoleAgg;
using Shop.Infrastructure.Persistent.Ef.SellerAgg;
using Shop.Infrastructure.Persistent.Ef.SiteEntities.Repositories;
using Shop.Infrastructure.Persistent.Ef.UserAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Infrastructure.Persistent.EF.CommnetAgg;
using Shop.Infrastructure.Persistent.EF.OrderAgg;
using Shop.Infrastructure.Persistent.EF.ProductAgg;

namespace Shop.Infrastructure;

public static class InfrastructureBootstraper
{
    public static void Init(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<ISellerRepository, SellerRepository>();
        services.AddTransient<IBannerRepository, BannerRepository>();
        services.AddTransient<ISliderRepository, SliderRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ICommentRepository, CommentRepository>();

        services.AddTransient<DapperContext>(_ =>
        {
            return new DapperContext(connectionString);
        });

        services.AddDbContext<ShopContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}
