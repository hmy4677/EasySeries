namespace EasySeries.Pay.Static.Models.Ali;

/// <summary>
/// 支付宝订单信息.
/// </summary>
public class AliPayInfo
{
    /// <summary>
    /// 商户单号.
    /// </summary>
    [Required]
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 金额(单位:元).
    /// </summary>
    [Required]
    public decimal Amount { get; set; }

    /// <summary>
    /// 商品描述.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// 返回Url(用于wap支付时).
    /// </summary>
    public string? ReturnUrl { get; set; } = string.Empty;
}
