namespace EasySerise.Post.Models.JDL;

/// <summary>
/// 预检订单请求.
/// </summary>
public class JDLCheckPreOrderRequest
{
    /// <summary>
    /// 商家号.
    /// </summary>
    public string customerCode { get; set; }

    /// <summary>
    /// 固定值1.
    /// </summary>
    public int orderOrigin { get; set; } = 1;

    /// <summary>
    /// 发件人信息.
    /// </summary>
    public JDLContact senderContact { get; set; }

    /// <summary>
    /// 收件人信息.
    /// </summary>
    public JDLContact receiverContact { get; set; }
}