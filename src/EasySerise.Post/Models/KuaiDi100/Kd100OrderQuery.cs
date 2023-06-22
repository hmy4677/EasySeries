namespace EasySerise.Post.Models.KuaiDi100;

/// <summary>
/// 快递100订单查询.
/// </summary>
public class Kd100OrderQuery
{
    /// <summary>
    /// 运单号.
    /// </summary>
    public string PostNo { get; set; } = string.Empty;

    /// <summary>
    /// 快递公司(小写拼音).
    /// </summary>
    public string PostCompany { get; set; } = string.Empty;

    /// <summary>
    /// 发or收件人手机号.
    /// </summary>
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// 结果信息0-1-4.
    /// </summary>
    public int ResultType { get; set; } = 1;
}