namespace EasySeries.Post.Models.KuaiDi100;

/// <summary>
/// 电子面单响应.
/// </summary>
public class DianzimiandanResponse : DianzimiandanResponseBase
{
    public DianziResponseData data { get; set; }
}

/// <summary>
/// 电子面单响应data.
/// </summary>
public class DianziResponseData
{
    /// <summary>
    /// 任务ID.
    /// </summary>
    public string taskId { get; set; }

    /// <summary>
    /// 快递公司编码.
    /// </summary>
    public string kuaidicom { get; set; }

    /// <summary>
    /// 快递单号.
    /// </summary>
    public string kuaidinum { get; set; }

    /// <summary>
    /// 子单号 多个子单时使用”,”隔开。 比如JD6666666,JD888888,JD99999.
    /// </summary>
    public string childNum { get; set; }

    /// <summary>
    /// 回单号 部分快递公司回单会返回回单号.
    /// </summary>
    public string backNum { get; set; }

    /// <summary>
    /// 面单短链，printType为IMAGE时返回多个面单时使用”,”隔开。 比如http:///api.kuaidi100.com/label/1,
    /// http:///api.kuaidi100.com/label/2, http:///api.kuaidi100.com/label/3 图片异步生成，请求还未生成时，可以稍后重试.
    /// </summary>
    public string label { get; set; }

    /// <summary>
    /// 大头笔 用于显示于电子面单上规定位置，非必需，是否有值取决于快递公司.
    /// </summary>
    public string bulkpen { get; set; }

    /// <summary>
    /// 始发地区域编码.
    /// </summary>
    public string orgCode { get; set; }

    /// <summary>
    /// 始发地/始发网点名称.
    /// </summary>
    public string orgName { get; set; }

    /// <summary>
    /// 目的地区域编码.
    /// </summary>
    public string destCode { get; set; }

    /// <summary>
    /// 目的地/到达网点.
    /// </summary>
    public string destName { get; set; }

    /// <summary>
    /// 始发分拣编码.
    /// </summary>
    public string orgSortingCode { get; set; }

    /// <summary>
    /// 始发分拣名称.
    /// </summary>
    public string orgSortingName { get; set; }

    /// <summary>
    /// 目的分栋编码.
    /// </summary>
    public string destSortingCode { get; set; }

    /// <summary>
    /// 目的分栋中心名称.
    /// </summary>
    public string destSortingName { get; set; }

    /// <summary>
    /// 始发其他信息.
    /// </summary>
    public string orgExtra { get; set; }

    /// <summary>
    /// 目的其他信息.
    /// </summary>
    public string destExtra { get; set; }

    /// <summary>
    /// 集包编码.
    /// </summary>
    public string pkgCode { get; set; }

    /// <summary>
    /// 集包地名称.
    /// </summary>
    public string pkgName { get; set; }

    /// <summary>
    /// 路区.
    /// </summary>
    public string road { get; set; }

    /// <summary>
    /// 二维码.
    /// </summary>
    public string qrCode { get; set; }

    /// <summary>
    /// 快递公司订单号.
    /// </summary>
    public string kdComOrderNum { get; set; }

    /// <summary>
    /// 快递业务类型编码.
    /// </summary>
    public string expressCode { get; set; }

    /// <summary>
    /// 快递业务类型名称.
    /// </summary>
    public string expressName { get; set; }
}