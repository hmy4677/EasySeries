namespace EasySeries.Pay.Models.Ali;

/// <summary>
/// 阿里支付model
/// </summary>
public class AliPayModel
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
    public string ReturnUrl { get; set; } = string.Empty;
}