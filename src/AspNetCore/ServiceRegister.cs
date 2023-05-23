using EasySerise.Pay.Implement;
using Microsoft.Extensions.DependencyInjection;

namespace EasySerise.Pay.AspNetCore;

public static class ServiceRegister
{
    public static IServiceCollection AddEasyPayService(this IServiceCollection services)
    {
        services.AddScoped<IEasyPayAli, EasyPayAli>();
        services.AddScoped<IEasyPayWechat, EasyPayWechat>();

        return services;
    }
}