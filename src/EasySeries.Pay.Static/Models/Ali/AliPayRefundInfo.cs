namespace EasySeries.Pay.Static.Models.Ali;

/// <summary>
/// 支付宝退款信息.
/// </summary>
public class AliPayRefundInfo
{
    /// <summary>
    /// 商户订单号.
    /// </summary>
    public string OutTradeNo { get; set; }

    /// <summary>
    /// 退款金额.
    /// </summary>
    public decimal RefundAmount { get; set; }

    /// <summary>
    /// 退款id.
    /// </summary>
    public string RefundId { get; set; }

    /// <summary>
    /// 退款原因.
    /// </summary>
    public string Reason { get; set; }
}