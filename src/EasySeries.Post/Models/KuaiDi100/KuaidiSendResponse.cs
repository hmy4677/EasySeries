namespace EasySeries.Post.Models.KuaiDi100;

/// <summary>
/// 发快递(下单)结果响应.
/// </summary>
public class KuaidiSendResponse : SendResponseBase
{
    /// <summary>
    /// data.
    /// </summary>
    public SendResData data { get; set; }
}

public class SendResData
{
    /// <summary>
    /// string 任务ID.
    /// </summary>
    public string taskId { get; set; }

    /// <summary>
    /// string 订单ID.
    /// </summary>
    public string orderId { get; set; }

    /// <summary>
    /// string 快递单号.
    /// </summary>
    public string kuaidinum { get; set; }

    /// <summary>
    /// string 快递面单附属属性，根据各个快递公司返回属性.
    /// </summary>
    public EOrder eOrder { get; set; }
}

public class EOrder
{
    /// <summary>
    /// string 大头笔 用于显示于电子面单上规定位置，非必需，是否有值取决于快递公司.
    /// </summary>
    public string bulkpen { get; set; }

    /// <summary>
    /// string 始发地区域编码.
    /// </summary>
    public string orgCode { get; set; }

    /// <summary>
    /// string 始发地/始发网点名称.
    /// </summary>
    public string orgName { get; set; }

    /// <summary>
    /// string 目的地区域编码.
    /// </summary>
    public string destCode { get; set; }

    /// <summary>
    /// string 目的地/到达网点.
    /// </summary>
    public string destName { get; set; }

    /// <summary>
    /// string 始发分拣编码.
    /// </summary>
    public string orgSortingCode { get; set; }

    /// <summary>
    /// string 始发分拣名称.
    /// </summary>
    public string orgSortingName { get; set; }

    /// <summary>
    /// string 目的分栋编码.
    /// </summary>
    public string destSortingCode { get; set; }

    /// <summary>
    /// string 目的分栋中心名称.
    /// </summary>
    public string destSortingName { get; set; }

    /// <summary>
    /// string 始发其他信息.
    /// </summary>
    public string orgExtra { get; set; }

    /// <summary>
    /// string 目的其他信息.
    /// </summary>
    public string destExtra { get; set; }

    /// <summary>
    /// string 集包编码.
    /// </summary>
    public string pkgCode { get; set; }

    /// <summary>
    /// string 集包地名称.
    /// </summary>
    public string pkgName { get; set; }

    /// <summary>
    /// string 路区.
    /// </summary>
    public string road { get; set; }

    /// <summary>
    /// string 二维码.
    /// </summary>
    public string qrCode { get; set; }

    /// <summary>
    /// string 快递公司订单号.
    /// </summary>
    public string kdComOrderNum { get; set; }

    /// <summary>
    /// string 快递业务类型编码.
    /// </summary>
    public string expressCode { get; set; }

    /// <summary>
    /// string 快递业务类型名称.
    /// </summary>
    public string expressName { get; set; }

    /// <summary>
    /// string 水印.
    /// </summary>
    public string waterMark { get; set; }

    /// <summary>
    /// string 时效.
    /// </summary>
    public string agingName { get; set; }

    /// <summary>
    /// string 电子产品类型图标.
    /// </summary>
    public string abFlag { get; set; }

    /// <summary>
    /// string 时效产品图标.
    /// </summary>
    public string proCode { get; set; }

    /// <summary>
    /// string 进港映射码.
    /// </summary>
    public string codingMapping { get; set; }

    /// <summary>
    /// string 出港信息.
    /// </summary>
    public string codingMappingOut { get; set; }

    /// <summary>
    /// string 图标名称.
    /// </summary>
    public string printIcon { get; set; }

    /// <summary>
    /// string 目的地（路由信息）.
    /// </summary>
    public string destRouteLabel { get; set; }

    /// <summary>
    /// string 二维码信息.
    /// </summary>
    public string twoDimensionCode { get; set; }

    /// <summary>
    /// string 顺丰面单标识，快运必填，xbFlag=1，打印SX标；xbFlag=2，打印融通标.
    /// </summary>
    public string xbFlag { get; set; }
}