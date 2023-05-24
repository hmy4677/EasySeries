using EasySeries.Pay.Models.Ali;
using EasySerise.Pay.Implement;
using EasySerise.Pay.Models.Wechat;
using Microsoft.Extensions.DependencyInjection;

namespace EasySerise.Pay.AspNetCore;

public static class ServiceRegister
{
    public static IServiceCollection AddEasyPayService(this IServiceCollection services, AliPaySecurityInfo? ali = null, WechatPaySecurityInfo? wechat = null)
    {
        ali ??= new AliPaySecurityInfo();
        wechat ??= new WechatPaySecurityInfo();

        services.AddScoped<IEasyPayAli, EasyPayAli>(p => new EasyPayAli(ali));
        services.AddScoped<IEasyPayWechat, EasyPayWechat>(p => new EasyPayWechat(wechat));

        return services;
    }
}