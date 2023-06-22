namespace EasySerise.Post.Models.JDL;

/// <summary>
/// 取消订单请求.
/// </summary>
public class JDLCancelOrderRequest
{
    /// <summary>
    /// 京东物流运单号.
    /// </summary>
    public string waybillCode { get; set; }

    /// <summary>
    /// 月结账号.
    /// </summary>
    public string customerCode { get; set; }

    public string cancelReason { get; set; } = "收件人信息有误";
    public int orderOrigin { get; set; } = 1;
    public int cancelReasonCode { get; set; } = 1;
}