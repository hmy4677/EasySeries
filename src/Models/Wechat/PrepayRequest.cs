namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 预支付请求.
/// </summary>
internal class PrepayRequest
{
    /// <summary>
    /// 应用id.
    /// </summary>
    internal string appid { get; set; } = string.Empty;

    /// <summary>
    /// 直连商户号.
    /// </summary>
    internal string mchid { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述.
    /// </summary>
    internal string description { get; set; } = string.Empty;

    /// <summary>
    /// 商户订单号.
    /// </summary>
    internal string out_trade_no { get; set; } = string.Empty;

    /// <summary>
    /// 通知地址.
    /// </summary>
    internal string notify_url { get; set; } = string.Empty;

    /// <summary>
    /// 订单金额信息.
    /// </summary>
    internal PreOrderAmount amount { get; set; } = new PreOrderAmount();

    /// <summary>
    /// 支付者信息.
    /// </summary>
    internal PayerInfo payer { get; set; } = new PayerInfo();
}