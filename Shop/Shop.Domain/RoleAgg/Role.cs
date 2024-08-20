using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.RoleAgg;

public class Role:AggregateRoot
{
    public string Title { get;private set; }
    public List<RolePermission> Permissions { get;private set; }
    public Role(string title, List<RolePermission> permissions)
    {
        Title = title;
        Permissions = permissions;
    }
    public Role(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        Title = title;
        Permissions = new List<RolePermission>();
    }
    private Role()
    {
        
    }

    public void SetPermissions(List<RolePermission> permissions)
    {
        Permissions= permissions;
    }
    public void Edit(string title)
    {
        NullOrEmptyDomainDataException.CheckString(title,nameof(title));
        Title= title;
    }
}
