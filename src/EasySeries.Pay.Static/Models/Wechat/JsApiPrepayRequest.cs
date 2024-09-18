namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// JsApi预付请求.
/// </summary>
internal class JsApiPrepayRequest : PrepayRequest
{
    /// <summary>
    /// 支付者信息.
    /// </summary>
    [JsonProperty("payer")]
    internal WechatPayer Payer { get; set; } = new WechatPayer();
}
