namespace EasySeries.Post.Options;

/// <summary>
/// 顺丰医寄通.
/// </summary>
public class SFYJTSecurityOptions
{
    public const string SettingKey = "SFYJTSecurityOptions";

    /// <summary>
    /// 密钥.
    /// </summary>
    public string SecretKey { get; set; }

    /// <summary>
    /// 医院编号.
    /// </summary>
    public string HospitalCode { get; set; }

    /// <summary>
    /// 月结账号.
    /// </summary>
    public string MonthlyCard { get; set; }
}