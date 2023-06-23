using Aop.Api.Response;
using EasySeries.Pay;
using EasySeries.Pay.Models.Wechat;
using Microsoft.AspNetCore.Mvc;

namespace EasySeries.Simple.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IEasyPayWechat _easyPayWechat;
    private readonly IEasyPayAli _easyPayAli;

    public PaymentController(IEasyPayWechat easyPayWechat, IEasyPayAli easyPayAli)
    {
        _easyPayWechat = easyPayWechat;
        _easyPayAli = easyPayAli;
    }

    [HttpGet("wechat")]
    public async Task<PayQueryResponse> QueryWechatAsync(string outTradeNo)
    {
        return await _easyPayWechat.WechatQueryPayAsync(outTradeNo, "");
    }

    [HttpPost("wechat")]
    public async Task<MiniAppSignInfo> WechatPayAsync([FromBody] PayModel pay)
    {
        var prepayId = await _easyPayWechat.WechatPrepayAsync(pay);
        return _easyPayWechat.MiniAppSign(prepayId);
    }

    [HttpPost("wechat/refund")]
    public async Task<dynamic> WechatRefundAsync([FromBody] RefundModel refund)
    {
        return await _easyPayWechat.WechatRefundAsync(refund);
    }

    [HttpGet("ali")]
    public AlipayTradeQueryResponse QueryAli(string outTradeNo)
    {
        return _easyPayAli.AlipayQuery(outTradeNo);
    }
}
