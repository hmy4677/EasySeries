namespace EasySerise.Post.Models.JDL;

/// <summary>
/// 京东物流查询.
/// </summary>
public class JDLQueryOrderResult : JDLResponseBase
{
    public TraceData data { get; set; } = new TraceData();
}

public class TraceData
{
    public string collectorName { get; set; }
    public string collectorPhone { get; set; }
    public List<TraceDetail> traceDetails { get; set; }
}

public class TraceDetail
{
    public string operationTitle { get; set; }
    public string operatorName { get; set; }
    public string categoryName { get; set; }
    public string operationTime { get; set; }
    public string waybillCode { get; set; }
    public string state { get; set; }
    public int category { get; set; }
    public string operationRemark { get; set; }
}