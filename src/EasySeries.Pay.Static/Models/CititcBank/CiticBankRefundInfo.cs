namespace EasySeries.Pay.Static.Models.CititcBank;

/// <summary>
/// 中信银行退款信息.
/// </summary>
public class CiticBankRefundInfo
{
    /// <summary>
    /// 商户订单号.
    /// </summary>
    public string OutTradeNo { get; set; } = string.Empty;
    /// <summary>
    /// 退款单号.
    /// </summary>
    public string OutRefundNo { get; set; } = string.Empty;
    /// <summary>
    /// 订单总金额(单位:分).
    /// </summary>
    public int TotalFee { get; set; }
    /// <summary>
    /// 退款金额(单位:分).
    /// </summary>
    public int RefundFee { get; set; }
}
