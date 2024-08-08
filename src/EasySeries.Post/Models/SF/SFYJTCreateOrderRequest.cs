namespace EasySeries.Post.Models.SF;

/// <summary>
/// 顺丰医寄通创建订单请求.
/// </summary>
public class SFYJTCreateOrderRequest
{
    /// <summary>
    /// 客户订单号.
    /// </summary>
    [JsonProperty("merchantOrderNo")]
    public string MerchantOrderNo { get; set; } = string.Empty;

    /// <summary>
    /// 1顺丰特快,2顺丰标快,5顺丰次晨,6顺丰即日,283填舱标快(到付不可用),231陆运包裹(到付不可用).
    /// </summary>
    [JsonProperty("expressType")]
    public string ExpressType { get; set; } = "283";

    /// <summary>
    /// 1:寄付(如果monthlyCard不为空则为寄付月结，否则为寄付现结) 2:到付，请不要传monthlyCard 3:第三方付，monthlyCard不可为空.
    /// </summary>
    [JsonProperty("payMethod")]
    public int PayMethod { get; set; } = 1;

    /// <summary>
    /// 顺丰月结卡号 月结支付时传值，现结不需传值.
    /// </summary>
    [JsonProperty("monthlyCard")]
    public string? MonthlyCard { get; set; }

    /// <summary>
    /// 包裹数.
    /// </summary>
    [JsonProperty("packagesNo")]
    public int PackagesNo { get; set; } = 1;

    /// <summary>
    /// 产品Code,病案寄递:MEDICAL_CHART,药品寄递:CHINESE_HERBAL,其他:OTHER.
    /// </summary>
    [JsonProperty("productCode")]
    public string ProductCode { get; set; } = "CHINESE_HERBAL";

    /// <summary>
    /// 备注.
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// 收寄双方信息.
    /// </summary>
    [JsonProperty("contactInfo")]
    public ContactInfo ContactInfo { get; set; } = new ContactInfo();

    /// <summary>
    /// 患者信息.
    /// </summary>
    [JsonProperty("patientInfo")]
    public PatientInfo PatientInfo { get; set; } = new PatientInfo();

    /// <summary>
    /// 其它服务(代收货款等).
    /// </summary>
    [JsonProperty("service")]
    public ServiceInfo? Service { get; set; }

    /// <summary>
    /// 设置代收金额.
    /// </summary>
    /// <param name="money"></param>
    /// <param name="monthlyCard"></param>
    public void SetCollectionMoney(decimal? money, string monthlyCard)
    {
        if(money is > 0)
        {
            Service = new ServiceInfo
            {
                CollectionMoney = (int)(money * 100),
                CollectionNo = monthlyCard
            };
        }
    }
}

/// <summary>
/// 寄件信息(正式).
/// </summary>
public class ContactInfo
{
    /// <summary>
    /// 寄件人姓名
    /// </summary>
    [JsonProperty("srcName")]
    public string SrcName { get; set; } = string.Empty;

    /// <summary>
    /// 寄件人电话
    /// </summary>
    [JsonProperty("srcPhone")]
    public string SrcPhone { get; set; } = string.Empty;

    /// <summary>
    /// 寄件人省份
    /// </summary>
    [JsonProperty("srcProvince")]
    public string SrcProvince { get; set; } = string.Empty;

    /// <summary>
    /// 寄件人城市
    /// </summary>
    [JsonProperty("srcCity")]
    public string SrcCity { get; set; } = string.Empty;

    /// <summary>
    /// 寄件人镇区
    /// </summary>
    [JsonProperty("srcDistrict")]
    public string SrcDistrict { get; set; } = string.Empty;

    /// <summary>
    /// 寄件人详细地址
    /// </summary>
    [JsonProperty("srcAddress")]
    public string SrcAddress { get; set; } = string.Empty;

    /// <summary>
    /// 收件人姓名
    /// </summary>
    [JsonProperty("destName")]
    public string DestName { get; set; } = string.Empty;

    /// <summary>
    /// 收件人电话
    /// </summary>
    [JsonProperty("destPhone")]
    public string DestPhone { get; set; } = string.Empty;

    /// <summary>
    /// 收件人省份
    /// </summary>
    [JsonProperty("destProvince")]
    public string DestProvince { get; set; } = string.Empty;

    /// <summary>
    /// 收件人城市
    /// </summary>
    [JsonProperty("destCity")]
    public string DestCity { get; set; } = string.Empty;

    /// <summary>
    /// 收件人镇区
    /// </summary>
    [JsonProperty("destDistrict")]
    public string DestDistrict { get; set; } = string.Empty;

    /// <summary>
    /// 收件人详细地址
    /// </summary>
    [JsonProperty("destAddress")]
    public string DestAddress { get; set; } = string.Empty;
}

/// <summary>
/// 患者信息.
/// </summary>
public class PatientInfo
{
    /// <summary>
    /// 患者姓名.
    /// </summary>
    [JsonProperty("patientName")]
    public string PatientName { get; set; } = string.Empty;

    /// <summary>
    /// 取药凭证名称，如‘手机号’，‘处方号’，‘身份证尾号’等，最长10位.
    /// </summary>
    [JsonProperty("takeMedicineIdentityName")]
    public string TakeMedicineIdentityName { get; private set; } = "手机号";

    /// <summary>
    /// 取药凭证内容，对应取药凭证的值，如‘18611888888’.
    /// </summary>
    [JsonProperty("takeMedicineIdentityValue")]
    public string TakeMedicineIdentityValue { get; set; } = string.Empty;
}

/// <summary>
/// 其它服务信息.
/// </summary>
public class ServiceInfo
{
    /// <summary>
    /// 代收货款(分).
    /// </summary>
    [JsonProperty("collectionMoney")]
    public int CollectionMoney { get; set; }

    /// <summary>
    /// 代收货款月结卡.
    /// </summary>
    [JsonProperty("collectionNo")]
    public string CollectionNo { get; set; } = string.Empty;
}