using System.ComponentModel.DataAnnotations;

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
    /// 返回Url.
    /// </summary>
    [Required]
    public string ReturnUrl { get; set; } = string.Empty;

    /// <summary>
    /// 支付方式(默认手机wap).
    /// </summary>
    public string ProductCode { get; set; } = "QUICK_WAP_WAY";
}