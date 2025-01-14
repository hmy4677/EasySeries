using EasySeries.MiniProgram.Models.Wechat;
using EasySeries.MiniProgram.Static;
using Microsoft.AspNetCore.Mvc;

namespace EasySeries.Simple.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MiniProgramController : ControllerBase
{
    [HttpGet("")]
    public async Task<FileContentResult> Test()
    {
        var request = new WechatUnLimitQrCodeRequest
        {
            AccessToken = "xxx"
        };

        var buffer = await Wechat.GetUnLimitQrCodeAsync(request);
        if(buffer != null)
        {
            return new FileContentResult(buffer, "image/jpeg");
        }

        throw new Exception("content is null");
    }

    public async Task AllWechatAPIs()
    {
        await Wechat.GetSessionAsync("code", "appid", "secret");
        await Wechat.GetWechatAccessTokenAsync("appid", "secret");
        await Wechat.GetWechatUserMobileAsync("token", "code");
        await Wechat.GetUnLimitQrCodeAsync(new WechatUnLimitQrCodeRequest());
        await Wechat.GetLimitQrCodeAsync(new WechatLimitQrCodeRequest());
        await Wechat.GetUrlSchemeAsync(new WechatUrlSchemeRequest());
        await Wechat.SendSubscribeMessageAsync(new WechatSendSubMsgRequest());
    }
}
