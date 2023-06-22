namespace EasySerise.Post.Models.JDL;

/// <summary>
/// 获取打印数据请求.
/// </summary>
public class JDLPullDataRequest
{
    /// <summary>
    /// 运单号列表.
    /// </summary>
    public List<JDLWayBillInfo> wayBillInfos { get; set; }

    public string cpCode { get; set; } = "JD";
    public string objectId { get; set; } = Guid.NewGuid().ToString();
    public Dictionary<string, string> parameters { get; set; }
}

public class JDLWayBillInfo
{
    public string jdWayBillCode { get; set; }
}