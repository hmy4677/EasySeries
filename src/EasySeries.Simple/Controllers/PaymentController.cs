using Aop.Api.Response;
using EasySeries.Pay;
using EasySeries.Pay.Models.Ali;
using EasySeries.Pay.Models.UnifyTrade;
using EasySeries.Pay.Models.Wechat;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasySeries.Simple.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IEasyPayWechat _easyPayWechat;
    private readonly IEasyPayAli _easyPayAli;
    private readonly IEasyPayUnifyTrade _easyPayUnifyTrade;

    public PaymentController(IEasyPayWechat easyPayWechat, IEasyPayAli easyPayAli, IEasyPayUnifyTrade easyPayUnifyTrade)
    {
        _easyPayWechat = easyPayWechat;
        _easyPayAli = easyPayAli;
        _easyPayUnifyTrade = easyPayUnifyTrade;
    }

    [HttpGet("wechat")]
    public async Task<PayQueryResponse> QueryWechatAsync(string outTradeNo)
    {
        return await _easyPayWechat.WechatQueryPayAsync(outTradeNo, "");
    }

    [HttpPost("wechat/jsapi")]
    public async Task<JSAPISignInfo> WechatPayAsync([FromBody] JSAPIPayModel pay)
    {
        var prepayId = await _easyPayWechat.WechatPrepayAsync(pay);
        return _easyPayWechat.JSAPISign(prepayId, pay.AppIdType);
    }

    [HttpPost("wechat/app")]
    public async Task<AppSignInfo> WechatPayAsync([FromBody] AppPayModel pay)
    {
        var prepayId = await _easyPayWechat.WechatPrepayAsync(pay);
        return _easyPayWechat.AppSign(prepayId);
    }

    [HttpPost("wechat/refund")]
    public async Task<dynamic> WechatRefundAsync([FromBody] RefundModel refund)
    {
        return await _easyPayWechat.WechatRefundAsync(refund);
    }

    [HttpGet("wechat/refund/query")]
    public async Task<RefundQueryResponse> WechatRefuncQueryAsync(string outRefundNO)
    {
        return await _easyPayWechat.WechatQueryRefundAsync(outRefundNO);
    }

    [HttpPost("wechat_callback")]
    public async Task<IActionResult> WechatCallbackAsync()
    {
        try
        {
            await _easyPayWechat.WechatNotifyHandleAsync(Request);
            return Ok();
        }
        catch(Exception ex)
        {
            var res = new
            {
                code = "FAIL",
                message = ex.Message
            };
            return BadRequest(JsonConvert.SerializeObject(res));
        }
    }

    [HttpPost("ali/wap")]
    public AlipayTradeWapPayResponse QueryAli([FromBody] AliPayModel model)
    {
        return _easyPayAli.AlipayWap(model);
    }

    [HttpGet("ali/query")]
    public AlipayTradeQueryResponse QueryAli(string outTradeNo)
    {
        return _easyPayAli.AlipayQuery(outTradeNo);
    }

    [HttpPost("ali/refund")]
    public AlipayTradeRefundResponse RefundAli([FromBody] AliPayRefundModel model)
    {
        return _easyPayAli.AlipayRefund(model);
    }

    [HttpGet("unify")]
    public async Task<UnifyTradeQueryResponse> UnifyQueryAsync(string ourTradeNo)
    {
        return await _easyPayUnifyTrade.UnifyTradeQueryAsync(ourTradeNo);
    }
}