using EasySerise.Post.Implement;
using EasySerise.Post.Options;
using Microsoft.Extensions.DependencyInjection;

namespace EasySerise.Post.AspNetCore;

public static class ServiceRegister
{
    public static IServiceCollection AddEasyPostService(this IServiceCollection services)
    {
        services.AddOptions<KuaiDi100SecurityOptions>().BindConfiguration(KuaiDi100SecurityOptions.SettingKey);
        services.AddOptions<JDLSecurityOptions>().BindConfiguration(JDLSecurityOptions.SettingKey);

        services.AddScoped<IEasyPostKuaiDi100, EasyPostKuaiDi100>();
        services.AddScoped<IEasyPostJDL, EasyPostJDL>();

        return services;
    }
}