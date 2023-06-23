namespace EasySeries.Post.Models.JDL;

/// <summary>
/// 京东物流创建订单响应.
/// </summary>
public class JDLCreateOrderResponse : JDLResponseBase
{
    public CreateOrderData data { get; set; }
}

public class CreateOrderData
{
    /// <summary>
    /// 京东物流订单号.
    /// </summary>
    public string orderCode { get; set; }

    /// <summary>
    /// 京东物流运单号.
    /// </summary>
    public string waybillCode { get; set; }

    /// <summary>
    /// 预估运费.
    /// </summary>
    public decimal freightPre { get; set; }

    /// <summary>
    /// 需要再试(待人工预分拣时为true).
    /// </summary>
    public bool needRetry { get; set; }
}