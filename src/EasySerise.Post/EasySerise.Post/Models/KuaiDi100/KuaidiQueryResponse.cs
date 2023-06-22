namespace EasySerise.Post.Models.KuaiDi100;

/// <summary>
/// 快递查询响应.
/// </summary>
public class KuaidiQueryResponse
{
    /// <summary>
    /// 消息体，请忽略.
    /// </summary>
    public string message { get; set; }

    /// <summary>
    /// 快递单当前状态，默认为0在途，1揽收，2疑难，3签收，4退签，5派件，8清关，14拒签等10个基础物流状态，如需要返回高级物流状态，请参考 resultv2 传值.
    /// </summary>
    public string state { get; set; }

    /// <summary>
    /// 通讯状态，请忽略.
    /// </summary>
    public string status { get; set; }

    /// <summary>
    /// 快递单明细状态标记，暂未实现，请忽略.
    /// </summary>
    public string condition { get; set; }

    /// <summary>
    /// 是否签收标记，0未签收，1已签收，请忽略，明细状态请参考state字段.
    /// </summary>
    public string ischeck { get; set; }

    /// <summary>
    /// 快递公司编码, 一律用小写字母.
    /// </summary>
    public string com { get; set; }

    /// <summary>
    /// 单号.
    /// </summary>
    public string nu { get; set; }

    /// <summary>
    /// 查询响应数据.
    /// </summary>
    public List<KuaidiQueryData> data { get; set; }
}

/// <summary>
/// 查询响应data.
/// </summary>
public class KuaidiQueryData
{
    /// <summary>
    /// 内容.
    /// </summary>
    public string context { get; set; }

    /// <summary>
    /// 时间，原始格式.
    /// </summary>
    public string time { get; set; }

    /// <summary>
    /// 格式化后时间.
    /// </summary>
    public string ftime { get; set; }

    /// <summary>
    /// 本数据元对应的物流状态名称或者高级状态名称，实时查询接口中提交resultv2=1或者resultv2=4标记后才会出现.
    /// </summary>
    public string status { get; set; }

    /// <summary>
    /// 本数据元对应的行政区域的编码，实时查询接口中提交resultv2=1或者resultv2=4标记后才会出现.
    /// </summary>
    public string areaCode { get; set; }

    /// <summary>
    /// 本数据元对应的行政区域的名称，实时查询接口中提交resultv2=1或者resultv2=4标记后才会出现.
    /// </summary>
    public string areaName { get; set; }

    /// <summary>
    /// 本数据元对应的高级物流状态值，实时查询接口中提交resultv2=4标记后才会出现.
    /// </summary>
    public string statusCode { get; set; }

    /// <summary>
    /// 本数据元对应的行政区域经纬度，实时查询接口中提交resultv2=4标记后才会出现.
    /// </summary>
    public string areaCenter { get; set; }

    /// <summary>
    /// 本数据元对应的快件当前地点，实时查询接口中提交resultv2=4标记后才会出现.
    /// </summary>
    public string location { get; set; }

    /// <summary>
    /// 本数据元对应的行政区域拼音，实时查询接口中提交resultv2=4标记后才会出现.
    /// </summary>
    public string areaPinYin { get; set; }
}