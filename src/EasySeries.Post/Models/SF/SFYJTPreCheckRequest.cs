namespace EasySeries.Post.Models.SF;

public class SFYJTPreCheckRequest
{
    /// <summary>
    /// 1顺丰特快,2顺丰标快,5顺丰次晨,6顺丰即日,283填舱标快(到付不可用),231陆运包裹(到付不可用).
    /// </summary>
    [JsonProperty("expressType")]
    public string ExpressType { get; set; } = "283";

    /// <summary>
    /// 寄收双方信息.
    /// </summary>
    [JsonProperty("contactInfoList")]
    public List<ContactInfoPreCheck> ContactInfoList { get; set; }
}

/// <summary>
/// 寄收件人信息Pre.
/// </summary>
public class ContactInfoPreCheck
{
    /// <summary>
    /// 地址类型：1寄件方信息,2到件方信息
    /// </summary>
    [JsonProperty("contactType")]
    public int ContactType { get; set; } = 1;

    /// <summary>
    /// 省.
    /// </summary>
    [JsonProperty("province")]
    public string Province { get; set; }

    /// <summary>
    /// 市.
    /// </summary>
    [JsonProperty("city")]
    public string City { get; set; }

    /// <summary>
    /// 区县.
    /// </summary>
    [JsonProperty("county")]
    public string County { get; set; }

    /// <summary>
    /// 详细.
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; }
}