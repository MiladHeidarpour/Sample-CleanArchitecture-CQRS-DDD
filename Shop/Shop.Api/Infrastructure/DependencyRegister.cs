﻿using Shop.Api.Infrastructure.Gateways.Zibal;
using Shop.Api.Infrastructure.JwtUtil;

namespace Shop.Api.Infrastructure;

public static class DependencyRegister
{
    public static void RegisterApiDependency(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
        services.AddTransient<CustomJwtValidation>();
        services.AddHttpClient<IZibalService, ZibalService>();

        services.AddCors(option =>
        {
            option.AddPolicy(name: "ShopApi",
               builder =>
               {
                   builder.WithOrigins().AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
               });
        });
    }
}
