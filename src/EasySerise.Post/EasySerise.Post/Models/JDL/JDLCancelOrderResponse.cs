namespace EasySerise.Post.Models.JDL;

/// <summary>
/// 取消订单响应.
/// </summary>
public class JDLCancelOrderResponse : JDLResponseBase
{
    public CancelOrderData data { get; set; }
}

public class CancelOrderData
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
    /// 0 - 取消成功；1 - 拦截成功； 2 - 取消失败；3 - 拦截失败.
    /// </summary>
    public int resultType { get; set; }
}