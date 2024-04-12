namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// 微信APP支付model.
/// </summary>
public class AppPayModel
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
    /// 商品描述.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}