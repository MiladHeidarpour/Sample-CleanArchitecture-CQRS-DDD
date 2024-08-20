
using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs.Addresses;

namespace Shop.Query.Users.Addresses.GetList;

internal class GetUserAddressesListQueryHandler : IQueryHandler<GetUserAddressesListQuery, List<AddressDto>>
{
    private readonly DapperContext _dapperContext;

    public GetUserAddressesListQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<AddressDto>> Handle(GetUserAddressesListQuery request, CancellationToken cancellationToken)
    {
        using var context = _dapperContext.CreateConnection();
        var sql = $"SELECT * from {_dapperContext.UserAddresses} where UserId=@userId";
        var result = await context.QueryAsync<AddressDto>(sql, new { userId = request.UserId });
        return result.ToList();
    }
}