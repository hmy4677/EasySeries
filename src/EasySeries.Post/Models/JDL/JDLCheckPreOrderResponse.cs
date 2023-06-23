namespace EasySeries.Post.Models.JDL;

/// <summary>
/// 预检订单响应数据.
/// </summary>
public class JDLCheckPreOrderResponse : JDLResponseBase
{
    public CheckPreOrderData data { get; set; }
}

public class CheckPreOrderData
{
    public decimal totalFreightPre { get; set; }
    public decimal totalFreightStandard { get; set; }
    public ShipmentInfo shipmentInfo { get; set; }
}

public class ShipmentInfo
{
    public int startStationNo { get; set; }
    public string startStationName { get; set; }
    public int endStationNo { get; set; }
    public string endStationName { get; set; }
}