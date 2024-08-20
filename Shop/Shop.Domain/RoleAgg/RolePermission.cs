using Common.Domain;
using Shop.Domain.RoleAgg.Enums;

namespace Shop.Domain.RoleAgg;

public class RolePermission:BaseEntity
{
    public long RoleId { get;internal set; }
    public Permission Permission { get;private set; }
    public RolePermission( Permission permission)
    {
        Permission = permission;
    }
}
