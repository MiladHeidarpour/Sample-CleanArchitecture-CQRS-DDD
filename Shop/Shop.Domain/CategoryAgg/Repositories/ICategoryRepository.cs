using Common.Domain.Repository;

namespace Shop.Domain.CategoryAgg.Repositories;

public interface ICategoryRepository:IBaseRepository<Category>
{
    Task<bool> DeleteCategory(long CategoryId);
}
