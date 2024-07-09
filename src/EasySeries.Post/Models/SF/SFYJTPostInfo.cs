namespace EasySeries.Post.Models.SF;

/// <summary>
/// 顺丰医寄通寄件信息.
/// </summary>
public class SFYJTPostInfo
{
    /// <summary>
    /// 客户订单号.
    /// </summary>
    public string MerchantOrderNo { get; set; } = string.Empty;

    /// <summary>
    /// 1顺丰特快,2顺丰标快,5顺丰次晨,6顺丰即日,283填舱标快(到付不可用),231陆运包裹(到付不可用).
    /// </summary>
    public string ExpressType { get; set; } = "283填舱标快";

    /// <summary>
    /// 1:寄付(如果monthlyCard不为空则为寄付月结，否则为寄付现结) 2:到付，请不要传monthlyCard 3:第三方付，monthlyCard不可为空.
    /// </summary>
    public int PayMethod { get; set; }

    /// <summary>
    /// 备注.
    /// </summary>
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// 收寄双方信息
    /// </summary>
    public ContactInfo ContactInfo { get; set; } = new ContactInfo();

    /// <summary>
    /// 患者信息.
    /// </summary>
    public PatientInfo PatientInfo { get; set; } = new PatientInfo();

    /// <summary>
    /// 代收货款金额.
    /// </summary>
    public decimal? CollectionMoney { get; set; }
}
