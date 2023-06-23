namespace EasySeries.Post.Models.JDL;

/// <summary>
/// 获取打印数据响应.
/// </summary>
public class JDLPullDataResponse
{
    public string code { get; set; }
    public string message { get; set; }
    public string objectId { get; set; }
    public List<JDLPrePrintDataInfo> prePrintDatas { get; set; }
}

public class JDLPrePrintDataInfo
{
    public string msg { get; set; }
    public string code { get; set; }
    public string packageCode { get; set; }
    public string wayBillNo { get; set; }
    public string perPrintData { get; set; }
}