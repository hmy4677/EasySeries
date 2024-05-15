namespace EasySeries.Pay.Models.Ali;

/// <summary>
/// 回调通知.
/// </summary>
public class NofityModel
{
    /// <summary>
    /// 商户单号.
    /// </summary>
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 支付单号.
    /// </summary>
    public string TradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 总金额.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// 支付状态.
    /// WAIT_BUYER_PAY	交易创建，等待买家付款	交易创建
    /// TRADE_CLOSED 未付款交易超时关闭，或支付完成后全额退款 交易关闭
    /// TRADE_SUCCESS 交易支付成功，可退款 支付成功
    /// TRADE_FINISHED 交易结束，不可退款 交易完成
    /// </summary>
    public string TradeStatus { get; set; } = string.Empty;
}