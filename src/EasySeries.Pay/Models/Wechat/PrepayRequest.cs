namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// JSAPI预支付请求.
/// </summary>
internal class PrepayRequest : AppPrepayRequest
{
    /// <summary>
    /// 支付者信息.
    /// </summary>
    [JsonProperty("payer")]
    internal PayerInfo Payer { get; set; } = new PayerInfo();
}