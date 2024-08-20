using Common.Application;
using Shop.Application.Roles.Create;
using Shop.Application.Roles.Edit;
using Shop.Query.Roles.DTOs;
using Shop.Query.Roles.GetByList;

namespace Shop.Presentation.Facade.Roles;

public interface IRoleFacade
{
    Task<OperationResult> CreateRole(CreateRoleCommand command);
    Task<OperationResult> EditRole(EditRoleCommand command);

    Task<RoleDto?> GetRoleById(long roleId);
    Task<List<RoleDto>> GetRoles();
}
