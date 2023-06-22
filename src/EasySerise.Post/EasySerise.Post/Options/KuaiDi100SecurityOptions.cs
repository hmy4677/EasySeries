namespace EasySerise.Post.Options;

/// <summary>
/// 快递100options.
/// </summary>
public class KuaiDi100SecurityOptions
{
    public static readonly string SettingKey = "KuaiDi100SecurityOptions";

    public string Customer { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public string Secret { get; set; } = string.Empty;
}