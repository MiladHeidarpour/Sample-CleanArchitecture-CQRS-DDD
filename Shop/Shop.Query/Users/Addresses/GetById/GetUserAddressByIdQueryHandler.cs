
using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Users.DTOs.Addresses;

namespace Shop.Query.Users.Addresses.GetById;

internal class GetUserAddressByIdQueryHandler : IQueryHandler<GetUserAddressByIdQuery, AddressDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserAddressByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<AddressDto?> Handle(GetUserAddressByIdQuery request, CancellationToken cancellationToken)
    {
        using var context = _dapperContext.CreateConnection();
        var sql = $"SELECT top 1 * from {_dapperContext.UserAddresses} where id=@id";
        var result = await context.QueryFirstOrDefaultAsync<AddressDto>(sql, new { id = request.AddressId });
        return result;
    }
}