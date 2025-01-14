namespace EasySeries.MiniProgram.Models.Wechat;

public class WechatLimitQrCodeRequest : WechatRequest
{
    /// <summary>
    /// 默认是主页，页面 page，例如 pages/index/index?foo=bar.
    /// </summary>
    public string Path { get; set; } = "index";

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
