using EasySeries.Pay.Options;
using EasySeries.Pay.Implement;
using Microsoft.Extensions.DependencyInjection;

namespace EasySeries.Pay.AspNetCore;

public static class ServiceRegister
{
    public static IServiceCollection AddEasyPayService(this IServiceCollection services)
    {
        services.AddOptions<AliPaySecurityOptions>().BindConfiguration(AliPaySecurityOptions.SettingKey);
        services.AddOptions<WechatPaySecurityOptions>().BindConfiguration(WechatPaySecurityOptions.SettingKey);
        services.AddOptions<UnifyTradeSecurityOptions>().BindConfiguration(UnifyTradeSecurityOptions.SettingKey);

        services.AddScoped<IEasyPayAli, EasyPayAli>();
        services.AddScoped<IEasyPayWechat, EasyPayWechat>();
        services.AddScoped<IEasyPayUnifyTrade, EasyPayUnifyTrade>();

        return services;
    }
}