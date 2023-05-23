using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Util;
using EasySerise.Pay.Models.Ali;

namespace EasySerise.Pay.Implement;

/// <summary>
/// 阿里支付Implement.
/// </summary>
public class EasyPayAli : IEasyPayAli
{
    private const string _alipayUrl = "https://openapi.alipay.com/gateway.do";

    private readonly PaySecurityInfo _securityInfo;

    /// <summary>
    /// 初始化.
    /// </summary>
    /// <param name="securityInfo">支付安全信息.</param>
    public EasyPayAli(PaySecurityInfo securityInfo)
    {
        _securityInfo = securityInfo;
    }

    /// <summary>
    /// 支付(手机网页).
    /// </summary>
    /// <param name="payModel">支付model.</param>
    /// <returns>支付响应结果.</returns>
    public AlipayTradeWapPayResponse AlipayWap(AliPayModel payModel)
    {
        var privateKey = GetKeyFromFile(_securityInfo.PrivateKeyPath);
        var publicKey = GetKeyFromFile(_securityInfo.PublicKeyPath);
        var client = new DefaultAopClient(_alipayUrl, _securityInfo.AppId, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
        var request = new AlipayTradeWapPayRequest();
        request.SetNotifyUrl(_securityInfo.PayNotifyUrl);
        request.SetReturnUrl(payModel.ReturnUrl);
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no", payModel.OutTradeNo },
                { "total_amount", payModel.Amount },
                { "subject", payModel.Subject },
                { "product_code", payModel.ProductCode}
            };
        request.BizContent = JsonConvert.SerializeObject(bizContent);
        return client.pageExecute(request);
    }

    /// <summary>
    /// 账单查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <returns>查询响应结果.</returns>
    public AlipayTradeQueryResponse AlipayQuery(string outTradeNo)
    {
        var privateKey = GetKeyFromFile(_securityInfo.PrivateKeyPath);
        var publicKey = GetKeyFromFile(_securityInfo.PublicKeyPath);
        var client = new DefaultAopClient(_alipayUrl, _securityInfo.AppId, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
        var request = new AlipayTradeQueryRequest();
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no", outTradeNo }
            };
        request.BizContent = JsonConvert.SerializeObject(bizContent);
        return client.Execute(request);
    }

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="refundModel">退款模型.</param>
    /// <returns>退款响应结果.</returns>
    public AlipayTradeRefundResponse AlipayRefund(AliPayRefundModel refundModel)
    {
        var privateKey = GetKeyFromFile(_securityInfo.PrivateKeyPath);
        var publicKey = GetKeyFromFile(_securityInfo.PublicKeyPath);
        var client = new DefaultAopClient(_alipayUrl, _securityInfo.AppId, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
        var request = new AlipayTradeRefundRequest();
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no",  refundModel.OutTradeNo},
                { "refund_amount", refundModel.RefundAmount },
                { "out_request_no", refundModel.RefundId },
                { "refund_reason", refundModel.Reason }
            };
        request.BizContent = JsonConvert.SerializeObject(bizContent);
        return client.Execute(request);
    }

    /// <summary>
    /// 关闭订单.
    /// </summary>
    /// <param name="outTradeNO">商户单号.</param>
    /// <returns>关闭退款响应结果.</returns>
    public AlipayTradeCloseResponse AlipayClose(string outTradeNO)
    {
        var privateKey = GetKeyFromFile(_securityInfo.PrivateKeyPath);
        var publicKey = GetKeyFromFile(_securityInfo.PublicKeyPath);
        var client = new DefaultAopClient(_alipayUrl, _securityInfo.AppId, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
        var request = new AlipayTradeCloseRequest();
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no", outTradeNO }
            };
        request.BizContent = JsonConvert.SerializeObject(bizContent);
        return client.Execute(request);
    }

    /// <summary>
    /// 回调通知处理.
    /// 回应:await Response.WriteAsync("success/fail");
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <returns>通知内容.</returns>
    /// <exception cref="Exception">请求异常.</exception>
    public NofityModel AlipayNotifyHandle(HttpRequest request)
    {
        var requestDic = new Dictionary<string, string>();
        foreach(var item in request.Form)
        {
            requestDic.Add(item.Key, item.Value);
        }

        if(requestDic.Count == 0)
        {
            throw new Exception("fail");
        }

        bool verify = AliVerifySign(requestDic, _securityInfo.PublicKeyPath);
        if(!verify)
        {
            throw new Exception("fail");
        }

        return new NofityModel
        {
            OutTradeNo = requestDic["out_trade_no"],
            TotalAmount = requestDic["trade_no"],
            TradeNo = requestDic["total_amount"],
            TradeStatus = requestDic["trade_status"]
        };
    }

    /// <summary>
    /// 验签.
    /// </summary>
    /// <param name="signarr">签名串.</param>
    /// <param name="alipublickeyPath">公钥文件路径.</param>
    /// <returns>验签结果.</returns>
    private static bool AliVerifySign(Dictionary<string, string> signarr, string alipublickeyPath)
    {
        using var fileStream = File.OpenRead(alipublickeyPath);
        var reader = new StreamReader(fileStream);
        var publicKey = reader.ReadToEnd();
        return AlipaySignature.RSACheckV1(signarr, publicKey, "UTF-8", "RSA2", false);
    }

    /// <summary>
    /// 从文件中获取密钥.
    /// </summary>
    /// <param name="filePath">密钥文件路径.</param>
    /// <returns>密钥内容.</returns>
    private static string GetKeyFromFile(string filePath)
    {
        if(!File.Exists(filePath))
        {
            throw new Exception("密钥文件不存在");
        }

        using var fileStream = new FileStream(filePath, FileMode.Open);
        using var reader = new StreamReader(fileStream);
        return reader.ReadToEnd();
    }
}