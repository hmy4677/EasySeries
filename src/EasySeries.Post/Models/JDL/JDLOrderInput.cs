namespace EasySeries.Post.Models.JDL;

/// <summary>
/// 京东物流下单.
/// </summary>
public class JDLOrderInput
{
    /// <summary>
    /// 结账方式(1-寄付；2-到付；3-月结).
    /// </summary>
    public int settleType { get; set; } = 1;

    /// <summary>
    /// 订单号.
    /// </summary>
    public string orderId { get; set; }

    /// <summary>
    /// 发件人信息.
    /// </summary>
    public JDLContact senderContact { get; set; }

    /// <summary>
    /// 收件人信息.
    /// </summary>
    public JDLContact receiverContact { get; set; }

    /// <summary>
    /// 下单备注.
    /// </summary>
    public string remark { get; set; }
}