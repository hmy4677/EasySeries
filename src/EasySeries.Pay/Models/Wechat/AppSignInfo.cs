namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// APP支付签名.
/// </summary>
public class AppSignInfo
{
    /// <summary>
    /// 应用ID
    /// </summary>
    public string Appid { get; set; } = string.Empty;

    /// <summary>
    /// 商户号.
    /// </summary>
    public string Partnerid { get; set; } = string.Empty;

    /// <summary>
    /// 预支付交易会话ID.
    /// </summary>
    public string Prepayid { get; set; } = string.Empty;

    /// <summary>
    /// 订单详情扩展字符串.
    /// </summary>
    public string Package { get; set; } = string.Empty;

    /// <summary>
    /// 随机字符串.
    /// </summary>
    public string Noncestr { get; set; } = string.Empty;

    /// <summary>
    /// 时间戳.
    /// </summary>
    public string Timestamp { get; set; } = string.Empty;

    /// <summary>
    /// 签名.
    /// </summary>
    public string Sign { get; set; } = string.Empty;
}