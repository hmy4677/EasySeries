namespace EasySeries.Pay.Models.Ali;

/// <summary>
/// 阿里退款model.
/// </summary>
public class AliPayRefundModel
{
    /// <summary>
    /// 商户单号.
    /// </summary>
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 退款id(可guid).
    /// </summary>
    public string RefundId { get; set; } = string.Empty;

    /// <summary>
    /// 退款金额.
    /// </summary>
    public decimal RefundAmount { get; set; }

    /// <summary>
    /// 退款原因.
    /// </summary>
    public string Reason { get; set; } = string.Empty;
}