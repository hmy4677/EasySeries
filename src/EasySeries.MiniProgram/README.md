## EasySeries.MiniProgram

Easy小程序，Easy系列第四个应用，用于微信，抖音，快手，百度等小程序。

### 使用说明:全static方法直接调用即可

```c#

using EasySeries.MiniProgram.Models.Wechat;
using EasySeries.MiniProgram.Static;
using Microsoft.AspNetCore.Mvc;

namespace EasySeries.Simple.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MiniProgramController : ControllerBase
{
    [HttpGet("")]
    public async Task<FileContentResult> QrCodeAsync()
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
        //微信.
        await Wechat.GetSessionAsync("code", "appid", "secret");
        await Wechat.GetWechatAccessTokenAsync("appid", "secret");
        await Wechat.GetWechatUserMobileAsync("token", "code");
        await Wechat.GetUnLimitQrCodeAsync(new WechatUnLimitQrCodeRequest());
        await Wechat.GetLimitQrCodeAsync(new WechatLimitQrCodeRequest());
        await Wechat.GetUrlSchemeAsync(new WechatUrlSchemeRequest());
        await Wechat.SendSubscribeMessageAsync(new WechatSendSubMsgRequest());

        //抖音.
        await Douyin.GetSessionAsync("code", "anonymousCode", "appid", "secret");

        //快手.
        await Kuaishou.GetSessionAsync("code", "appid", "secret");


        //百度.
        await Baidu.GetSessionAsync("accessToken", "code");
        await Baidu.GetAccessTokenAsync("appkey", "secret");

        //抖音,快手.
        MobileData.Decrypt("encryptedData", "sessionkey", "iv");

        //百度.
        MobileData.DecryptForBaidu("encryptedData", "sessionkey", "iv");
    }
}

```
