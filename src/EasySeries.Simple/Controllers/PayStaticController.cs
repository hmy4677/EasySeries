using EasySeries.Pay.Static;
using EasySeries.Pay.Static.Models.Ali;
using EasySeries.Pay.Static.Models.Wechat;
using Microsoft.AspNetCore.Mvc;

namespace EasySeries.Simple.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PayStaticController : ControllerBase
{
    [HttpPost("order")]
    public async Task<WechatJsApiSignInfo> CreateOrderAsync([FromBody] WechatPayInfo payInfo)
    {
        var payConfig = GetWechatPayConfig();
        return await WechatPay.JsApiOrderAsync(payConfig, payInfo);
    }

    [HttpGet("close")]
    public async Task<dynamic> CloseOrderAsync(string outTradeNo)
    {
        var payConfig = GetWechatPayConfig();
        return await WechatPay.CloseAsync(payConfig, outTradeNo);
    }

    [HttpGet("query")]
    public async Task<dynamic> QueryPayAsync(string outTradeNo)
    {
        var payConfig = GetWechatPayConfig();
        return await WechatPay.QueryPayAsync(payConfig, outTradeNo);
    }

    private static WechatPayConfig GetWechatPayConfig()
    {
        return new WechatPayConfig
        {
            AppId = "wxddc6a0d965a89098",
            CertSerialNo = "1D45FD09BF2A78D3CCD1363EC878F0215EFD0651",
            MchId = "1624617346",
            V3Key = "cheng8duo8xi8nan8er8tong8yi8yuan",
            PrivateKeyPath = "D:\\IIS\\cert\\wechatpay\\wuhou\\apiclient_key.pem",
            PlatformCertPath = "D:\\IIS\\cert\\wechatpay\\wuhou\\platform_cert.pem",
            PayNotifyUrl = "https://api.xnetyy.com/api/PayService/wechat_notify_1",
            RefundNotifyUrl = "https://api.xnetyy.com/api/PayService/wechat_notify_1"
        };
    }

    [HttpGet("ali_pay")]
    public dynamic AliPay1()
    {
        return AliPay.AppOrder(new AliPayConfig(), new AliPayInfo());
    }
}