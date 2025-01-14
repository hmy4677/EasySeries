namespace EasySeries.MiniProgram.Models.Wechat;

/// <summary>
/// 无限小程序码请求.
/// </summary>
public class WechatUnLimitQrCodeRequest : WechatRequest
{
    /// <summary>
    /// 默认是主页，页面 page，例如 pages/index/index，根路径前不要填加 /，不能携带参数（参数请放在scene字段里）.
    /// </summary>
    public string Path { get; set; } = "index";

    /// <summary>
    /// 最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）.
    /// </summary>
    public string Scene { get; set; } = "a=1";

    /// <summary>
    /// 二维码的宽度,默认430,最小280px-1280px.
    /// </summary>
    public int Width { get; set; } = 430;

    /// <summary>
    /// 自动配置线条颜色,默认 true.
    /// </summary>
    public bool AutoColor { get; set; } = true;

    /// <summary>
    /// 是否需要透明底色,默认false.
    /// </summary>
    public bool IsHyaline { get; set; } = false;
}