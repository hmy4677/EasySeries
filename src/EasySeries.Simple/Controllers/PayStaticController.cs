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

    [HttpPost("notify")]
    public async Task<ActionResult> NotifyAsync()
    {
        var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();

        var config = GetWechatPayConfig();
        var notify = new WechatNotify
        {
            Body = body,
            IsVerifySign = true,
            Nonce = Request.Headers["Wechatpay-Nonce"]!,
            Signature = Request.Headers["Wechatpay-Signature"]!,
            Stamp = Request.Headers["Wechatpay-Timestamp"]!
        };

        try
        {
            var result = WechatPay.PayNotifyHandel(config, notify);
            if(result.TradeState == "SUCCESS")
            {

            }
        }
        catch(Exception)
        {
            return BadRequest(new
            {
                code = "FAIL",
                messag = "xxx"
            });
        }

        return Ok();
    }

    private static WechatPayConfig GetWechatPayConfig()
    {
        return new WechatPayConfig
        {
            AppId = "xxx",
            CertSerialNo = "xxx",
            MchId = "xxx",
            V3Key = "xxx",
            PrivateKeyPath = "D:\\IIS\\cert\\wechatpay\\wuhou\\apiclient_key.pem",
            PlatformCertPath = "D:\\IIS\\cert\\wechatpay\\wuhou\\platform_cert.pem",
            PayNotifyUrl = "https://xxx/api/PayService/wechat_notify_1",
            RefundNotifyUrl = "https://xxx/api/PayService/wechat_notify_1"
        };
    }

    [HttpGet("ali_pay")]
    public dynamic AliPay1()
    {
        return AliPay.AppOrder(new AliPayConfig(), new AliPayInfo());
    }
}