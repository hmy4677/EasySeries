namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// 退款.
/// </summary>
public class RefundModel
{
    /// <summary>
    /// 订单总金额(单位:分).
    /// </summary>
    public int TotalAmount { get; set; }

    /// <summary>
    /// 退款金额(单位:分).
    /// </summary>
    public int RefundAmount { get; set; }

    /// <summary>
    /// 退款单号(自定一个唯一串).
    /// </summary>
    public string RefundNo { get; set; } = string.Empty;

    /// <summary>
    /// 商户单号.
    /// </summary>
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 退款原因.
    /// </summary>
    public string Reason { get; set; } = string.Empty;
}