namespace EasySeries.Post.Models.KuaiDi100;

/// <summary>
/// 发件请求.
/// </summary>
public class KuaidiSendRequest : SendRequestBase
{
    /// <summary>
    /// param 由其他字段拼接.
    /// </summary>
    //public KuaidiSendParam param { get; set; }
    public string param { get; set; }
}

public class KuaidiSendParam
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
    /// string 寄件人所在的完整地址，如广东深圳市深圳市南山区科技南十二路2号金蝶软件园B10.
    /// </summary>
    public string sendManPrintAddr { get; set; }

    /// <summary>
    /// string callBackUrl订单信息回调，默认仅支持http.
    /// </summary>
    //public string callBackUrl { get; set; }

    /// <summary>
    /// 否 string 物品名称, 例：文件。当kuaidicom=jd，yuantong时，必填
    /// </summary>
    public string? cargo { get; set; } = "药品";

    /// <summary>
    /// 否 string 支付方式，SHIPPER: 寄付（默认）。不支持到付.
    /// </summary>
    public string? payment { get; set; } = "SHIPPER";

    /// <summary>
    /// 否 string 业务类型，默认为标准快递，各快递公司业务类型对照参考：七、业务类型参数表.
    /// </summary>
    public string? serviceType { get; set; } = "标准快递";

    /// <summary>
    /// 否 string 物品总重量KG，不需带单位，例：1.5.
    /// </summary>
    public string? weight { get; set; } = "1";

    /// <summary>
    /// 否 string 备注.
    /// </summary>
    public string? remark { get; set; } = "";

    public string salt { get; set; } = "";//否   string 签名用随机字符串，用于验证签名sign。salt值不为null时，推送数据将包含该加密签名，加密方式：md5(param+salt)。注意： salt值为空串时，推送的数据也会包含sign，此时可忽略sign的校验。
    public string dayType { get; set; } = "";//否   string 预约日期，例如：今天/明天/后天
    public string pickupStartTime { get; set; } = "";//否 string 预约起始时间（HH:mm），例如：09:00，顺丰必填
    public string pickupEndTime { get; set; } = "";//否 string 预约截止时间（HH:mm），例如：10:00，顺丰必填
    public string valinsPay { get; set; } = "";//否 string 保价额度，单位：元
    public string passwordSigning { get; set; } = "Y";//否 string 是否口令签收，Y：需要 N: 不需要，默认值为N（德邦快递专属参数）
    public string op { get; set; } = "0";//否   string 是否开启订阅功能 0：不开启(默认) 1：开启 说明开启订阅功能时：pollCallBackUrl必须填入 此功能只针对有快递单号的单
    public string pollCallBackUrl { get; set; } = "";//否   string 如果op设置为1时，pollCallBackUrl必须填入，用于跟踪回调
    public string resultv2 { get; set; } = "0";//否 string 添加此字段表示开通行政区域解析或地图轨迹功能 。
    public string returnType { get; set; } = "";//否   string 面单返回类型，默认为空，不返回面单内容。10：设备打印，20：图片回调。
    public string siid { get; set; } = "";//否   string 设备码，returnType为10时必填
    public string tempid { get; set; } = "";//否 string 模板编码，通过管理后台的电子面单模板信息获取 ，returnType不为空时必填
    public string printCallBackUrl { get; set; } = "";//否 string 打印状态回调地址，returnType为10时必填
}