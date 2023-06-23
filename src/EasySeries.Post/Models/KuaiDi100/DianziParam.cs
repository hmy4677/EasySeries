namespace EasySeries.Post.Models.KuaiDi100;

/// <summary>
/// 电子面单参数.
/// </summary>
public class DianziParam
{
    /// <summary>
    /// 是 string 打印类型，NON：只下单不打印（默认）； IMAGE:生成图片短链； CLOUD:使用快递100云打印机打印，使用CLOUD时siid必填.
    /// </summary>
    public string printType { get; set; } = "NON";

    /// <summary>
    /// 是 string 电子面单客户账户或月结账号，需贵司向当地快递公司网点申请（参考电子面单申请指南）； 是否必填该属性，请查看参数字典.
    /// </summary>
    public string partnerId { get; set; } = string.Empty;

    /// <summary>
    /// 快递公司的编码，一律用小写字母，请查看参数字典.
    /// </summary>
    public string kuaidicom { get; set; } = "jd";

    /// <summary>
    /// 物品名称.
    /// </summary>
    public string cargo { get; set; } = "药品";

    /// <summary>
    /// 否 string 支付方式： SHIPPER：寄方付（默认） CONSIGNEE：到付 MONTHLY：月结.
    /// </summary>
    public string payType { get; set; } = "MONTHLY";

    /// <summary>
    /// 否 string 产品类型： 如标准快递（默认） 顺丰标快（陆运） EMS经济 （详细请请查看参数字典 ）.
    /// </summary>
    public string expType { get; set; } = "标准快递";

    /// <summary>
    /// 否 string 备注.
    /// </summary>
    public string remark { get; set; }

    /// <summary>
    /// 是 string 主单模板，通过管理后台的快递公司模板V2信息获取.
    /// </summary>
    public string tempId { get; set; } = "";

    /// <summary>
    /// 是 int 物品总数量。 另外该属性与子单有关，如果需要子单（指同一个订单打印出多张电子面单，即同一个订单返回多个面单号），needChild = 1、count
    /// 需要大于1，如count = 2 则一个主单 一个子单，count = 3则一个主单 二个子单；返回的子单号码见返回结果的childNum字段.
    /// </summary>
    public int count { get; set; } = 1;

    /// <summary>
    /// 否 Double 物品总重量KG，例：1.5，单位kg。极兔速递必填，其他快递公司非必填.
    /// </summary>
    public double? weight { get; set; } = 1.0;

    /// <summary>
    /// 收件人.
    /// </summary>
    public PostInfo recMan { set; get; }

    /// <summary>
    /// 寄件人.
    /// </summary>
    public PostInfo sendMan { set; get; }
}

public class PostInfo
{
    /// <summary>
    /// 是 姓名.
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// 是 手机号，手机号和电话号二者其一必填.
    /// </summary>
    public string mobile { get; set; }

    /// <summary>
    /// 是 电话号，手机号和电话号二者其一必填.
    /// </summary>
    public string tel { get; set; }

    /// <summary>
    /// 是 完整地址.
    /// </summary>
    public string printAddr { get; set; }

    /// <summary>
    /// 否 所在公司名称.
    /// </summary>
    public string company { get; set; }
}
