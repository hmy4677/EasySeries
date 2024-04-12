namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// 微信支付model.
/// </summary>
public class PayModel : AppPayModel
{
    /// <summary>
    /// 支付人openid.
    /// </summary>
    [Required]
    public string OpenId { get; set; } = string.Empty;
}