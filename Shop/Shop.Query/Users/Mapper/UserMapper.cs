using Microsoft.EntityFrameworkCore;
using Shop.Domain.UserAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.Mapper;

public static class UserMapper
{
    public static UserDto MapToDto(this User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            CreationDate = user.CreationDate,
            Name = user.Name,
            Family = user.Family,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            AvatarName = user.AvatarName,
            Gender = user.Gender,
            Password = user.Password,
            IsActive = user.IsActive,
            Roles = user.Roles.Select(s => new UserRoleDto()
            {
                RoleId = s.Id,
                RoleTitle = "",
            }).ToList()
        };
    }

    public static async Task<UserDto> SetUserRoleTitle(this UserDto user, ShopContext _context)
    {
        var roleIds = user.Roles.Select(s => s.RoleId);
        var result = await _context.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync();
        var roles = new List<UserRoleDto>();
        foreach (var role in result)
        {
            roles.Add(new UserRoleDto()
            {
                RoleId = role.Id,
                RoleTitle = role.Title,
            });
        }
        user.Roles = roles;
        return user;
    }
    public static UserFilterData MapFilterData(this User user)
    {
        return new UserFilterData()
        {
            Id = user.Id,
            CreationDate = user.CreationDate,
            Name = user.Name,
            Family = user.Family,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            AvatarName = user.AvatarName,
            Gender = user.Gender,
        };
    }
}
