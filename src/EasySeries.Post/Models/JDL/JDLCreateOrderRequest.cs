namespace EasySeries.Post.Models.JDL;

/// <summary>
/// 京东物流创建订单.
/// </summary>
public class JDLCreateOrderRequest
{
    /// <summary>
    /// 结账类型(1-寄付；2-到付；3-月结).
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

    /// <summary>
    /// 月结账号.
    /// </summary>
    public string customerCode { get; set; }

    public int orderOrigin { get; set; } = 1;
    public JDLProductInfo productsReq { get; set; } = new JDLProductInfo();
    public List<JDLCargoInfo> cargoes { get; set; } = new List<JDLCargoInfo> { new JDLCargoInfo() };
}

public class JDLCargoInfo
{
    public string name { get; set; } = "药品";
    public int quantity { get; set; } = 1;
    public decimal weight { get; set; } = 1;
    public decimal volume { get; set; } = 1000;
}

public class JDLProductInfo
{
    public string productCode { get; set; } = "ed-m-0001";
}