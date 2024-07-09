namespace EasySeries.Post.Models.SF;

/// <summary>
/// 顺丰医寄通查询订单响应.
/// </summary>
public class SFYJTQueryResponse : SFYJTResponseBase
{
    /// <summary>
    /// 结果.
    /// </summary>
    [JsonProperty("result")]
    public List<SFYJTQueryResult> Result { get; set; } = new List<SFYJTQueryResult>();
}

/// <summary>
/// 顺丰医寄通查询结果.
/// </summary>
public class SFYJTQueryResult
{
    /// <summary>
    /// 运单号
    /// </summary>
    [JsonProperty("mailno")]
    public string Mailno { get; set; } = string.Empty;

    /// <summary>
    /// 位置
    /// </summary>
    [JsonProperty("acceptAddress")]
    public string AcceptAddress { get; set; } = string.Empty;

    /// <summary>
    /// 日期
    /// </summary>
    [JsonProperty("acceptDate")]
    public string AcceptDate { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// 操作码见附录.
    /// </summary>
    [JsonProperty("opcode")]
    public string Opcode { get; set; } = string.Empty;

    /// <summary>
    /// 时间
    /// </summary>
    [JsonProperty("acceptTime")]
    public string AcceptTime { get; set; } = string.Empty;

    /// <summary>
    /// 详细时间
    /// </summary>
    [JsonProperty("acceptTotaltime")]
    public string AcceptTotaltime { get; set; } = string.Empty;
}