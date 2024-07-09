namespace EasySeries.Post.Models.SF;

/// <summary>
/// 顺丰医寄通下单响应.
/// </summary>
public class SFYJTOrderResponse : SFYJTResponseBase
{
    [JsonProperty("result")]
    public SFYJTOrderResult Result { get; set; }
}

public class SFYJTOrderResult
{
    /// <summary>
    /// 运单号.
    /// </summary>
    [JsonProperty("mailNo")]
    public string MailNo { get; set; }

    /// <summary>
    /// 客户订单号.
    /// </summary>
    [JsonProperty("merchantOrderNo")]
    public string MerchantOrderNo { get; set; }

    /// <summary>
    /// 签回单号.
    /// </summary>
    [JsonProperty("returnTrackingNo")]
    public string ReturnTrackingNo { get; set; }
}