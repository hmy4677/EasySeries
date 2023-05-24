namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 微信支付model.
/// </summary>
public class PayModel
{
    /// <summary>
    /// 金额(单位:分).
    /// </summary>
    [Required]
    public int Amount { get; set; }

    /// <summary>
    /// 商户单号.
    /// </summary>
    [Required]
    public string OutTradeNO { get; set; } = string.Empty;

    /// <summary>
    /// 支付人openid.
    /// </summary>
    [Required]
    public string OpenId { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}