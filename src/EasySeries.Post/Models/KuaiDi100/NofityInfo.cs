namespace EasySeries.Post.Models.KuaiDi100;

public class NofityInfo
{
    public string taskId { get; set; }
    public string sign { get; set; }
    public NofityParam param { get; set; }
}

public class NofityParam
{
    /// <summary>
    /// 是 string 快递公司的编码，一律用小写字母，见《快递公司编码》,选填。.
    /// </summary>
    public string kuaidicom { get; set; }

    /// <summary>
    /// 是 string 快递单号，单号的最大长度是32个字符。.
    /// </summary>
    public string kuaidinum { get; set; }

    /// <summary>
    /// 是 string 状态码.
    /// </summary>
    public string status { get; set; }

    /// <summary>
    /// 是 string 状态描述.
    /// </summary>
    public string message { get; set; }

    /// <summary>
    /// 是 data 订单内容.
    /// </summary>
    public NotityParamData data { get; set; }
}

public class NotityParamData
{
    // 是 string 平台订单ID
    public string orderId { set; get; }

    // 是 int 订单状态： 0：'下单成功'； 1：'已接单'； 2：'收件中'； 9：'用户主动取消'； 10；'已取件'； 11；'揽货失败'； 12；'已退回'；
    // 13；'已签收'； 14；'异常签收'；15；'已结算' 99；'订单已取消'101；'运输中'；200：'已出单'201：'出单失败'
    public int status { set; get; }

    // 否 string 快递员姓名
    public string courierName { set; get; }

    // 否 string 快递员电话
    public string courierMobile { set; get; }

    // 否 string 计费重量，单位：kg
    public string weight { set; get; }

    // 否 string 标准运费，单位：元
    public string defPrice { set; get; }

    // 否 折后运费，单位：元
    public string freight { set; get; }

    // 否 体积，单位：cm³
    public string volume { set; get; }

    // 否 string 称重重量，单位：kg
    public string actualWeight { set; get; }
}