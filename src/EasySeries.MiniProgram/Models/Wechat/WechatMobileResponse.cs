namespace EasySeries.MiniProgram.Models.Wechat;

/// <summary>
/// 微信手机号响应.
/// </summary>
public class WechatMobileResponse : WechatResponse
{
    /// <summary>
    /// 手机号信息.
    /// </summary>
    [JsonProperty("phone_info")]
    public PhoneInfo? PhoneInfo { get; set; }
}

/// <summary>
/// 手机号信息.
/// </summary>
public class PhoneInfo
{
    /// <summary>
    /// 用户绑定的手机号（国外手机号会有区号）.
    /// </summary>
    [JsonProperty("phoneNumber")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 没有区号的手机号.
    /// </summary>
    [JsonProperty("purePhoneNumber")]
    public string PurePhoneNumber { get; set; }

    /// <summary>
    /// 区号.
    /// </summary>
    [JsonProperty("countryCode")]
    public string CountryCode { get; set; }
}