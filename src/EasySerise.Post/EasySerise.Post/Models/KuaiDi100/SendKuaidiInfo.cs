namespace EasySerise.Post.Models.KuaiDi100;

/// <summary>
/// 寄件信息.
/// </summary>
public class SendKuaidiInfo
{
    /// <summary>
    /// string 快递公司的编码，一律用小写字母，见《快递公司编码》.
    /// </summary>
    public string kuaidicom { get; set; }

    /// <summary>
    /// string 收件人姓名.
    /// </summary>
    public string recManName { get; set; }

    /// <summary>
    /// string 收件人的手机号，手机号和电话号二者其一必填.
    /// </summary>
    public string recManMobile { get; set; }

    /// <summary>
    /// string 收件人所在完整地址，如广东深圳市深圳市南山区科技南十二路2号金蝶软件园.
    /// </summary>
    public string recManPrintAddr { get; set; }

    /// <summary>
    /// string 寄件人姓名.
    /// </summary>
    public string sendManName { get; set; }

    /// <summary>
    /// string 寄件人的手机号，手机号和电话号二者其一必填.
    /// </summary>
    public string sendManMobile { get; set; }

    /// <summary>
    /// 寄件人详细地址.
    /// </summary>
    public string sendManPrintAddr { get; set; }

    /// <summary>
    /// string callBackUrl订单信息回调，默认仅支持http.
    /// </summary>
    public string callBackUrl { get; set; }

    /// <summary>
    /// 否 string 物品名称, 例：文件。当kuaidicom=jd，yuantong时，必填.
    /// </summary>
    public string? cargo { get; set; }

    /// <summary>
    /// 否 string 物品总重量KG，不需带单位，例：1.5.
    /// </summary>
    public string? weight { get; set; }

    /// <summary>
    /// 否 string 备注.
    /// </summary>
    public string? remark { get; set; }
}