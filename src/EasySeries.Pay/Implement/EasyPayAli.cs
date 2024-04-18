using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Util;
using EasySeries.Pay.Models.Ali;
using EasySeries.Pay.Options;

namespace EasySeries.Pay.Implement;

/// <summary>
/// 阿里支付Implement.
/// </summary>
public class EasyPayAli : IEasyPayAli
{
    private const string ALI_PAY_URL = "https://openapi.alipay.com/gateway.do";

    private AliPaySecurityOptions _securityOptions;

    /// <summary>
    /// 初始化.
    /// </summary>
    /// <param name="securityOptions">支付安全信息.</param>
    public EasyPayAli(IOptions<AliPaySecurityOptions> securityOptions)
    {
        _securityOptions = securityOptions.Value;
    }

    /// <summary>
    /// 支付(手机网页).
    /// </summary>
    /// <param name="payModel">支付model.</param>
    /// <param name="securityOptions">支付安全信息(即时模式用).</param>
    /// <returns>支付响应结果.</returns>
    public AlipayTradeWapPayResponse AlipayWap(AliPayModel payModel, AliPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var request = new AlipayTradeWapPayRequest();
        request.SetNotifyUrl(_securityOptions.PayNotifyUrl);
        request.SetReturnUrl(payModel.ReturnUrl);
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no", payModel.OutTradeNo },
                { "total_amount", payModel.Amount },
                { "subject", payModel.Subject },
                { "product_code", payModel.ProductCode}
            };
        request.BizContent = JsonConvert.SerializeObject(bizContent);

        return _securityOptions.SecurityType switch
        {
            "KEY" => CreatKEYAopClient().pageExecute(request),
            "CERT" => CreateCERTAopClient().pageExecute(request),
            _ => throw new ArgumentException("安全类型错误,应为KEY/CERT")
        };
    }

    /// <summary>
    /// 账单查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="securityOptions">支付安全信息(即时模式用).</param>
    /// <returns>查询响应结果.</returns>
    public AlipayTradeQueryResponse AlipayQuery(string outTradeNo, AliPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var request = new AlipayTradeQueryRequest();
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no", outTradeNo }
            };
        request.BizContent = JsonConvert.SerializeObject(bizContent);

        return _securityOptions.SecurityType switch
        {
            "KEY" => CreatKEYAopClient().Execute(request),
            "CERT" => CreateCERTAopClient().CertificateExecute(request),
            _ => throw new ArgumentException("安全类型错误,应为KEY/CERT")
        };
    }

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="refundModel">退款模型.</param>
    /// <param name="securityOptions">支付安全信息(即时模式用).</param>
    /// <returns>退款响应结果.</returns>
    public AlipayTradeRefundResponse AlipayRefund(AliPayRefundModel refundModel, AliPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var request = new AlipayTradeRefundRequest();
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no",  refundModel.OutTradeNo},
                { "refund_amount", refundModel.RefundAmount },
                { "out_request_no", refundModel.RefundId },
                { "refund_reason", refundModel.Reason }
            };
        request.BizContent = JsonConvert.SerializeObject(bizContent);

        return _securityOptions.SecurityType switch
        {
            "KEY" => CreatKEYAopClient().Execute(request),
            "CERT" => CreateCERTAopClient().CertificateExecute(request),
            _ => throw new ArgumentException("安全类型错误,应为KEY/CERT")
        };
    }

    /// <summary>
    /// 关闭订单.
    /// </summary>
    /// <param name="outTradeNO">商户单号.</param>
    /// <param name="securityOptions">支付安全信息(即时模式用).</param>
    /// <returns>关闭退款响应结果.</returns>
    public AlipayTradeCloseResponse AlipayClose(string outTradeNO, AliPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var request = new AlipayTradeCloseRequest();
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no", outTradeNO }
            };
        request.BizContent = JsonConvert.SerializeObject(bizContent);

        return _securityOptions.SecurityType switch
        {
            "KEY" => CreatKEYAopClient().Execute(request),
            "CERT" => CreateCERTAopClient().CertificateExecute(request),
            _ => throw new ArgumentException("安全类型错误,应为KEY/CERT")
        };
    }

    /// <summary>
    /// 回调通知处理. 回应:await Response.WriteAsync("success/fail");
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <param name="securityOptions">支付安全信息(即时模式用).</param>
    /// <returns>通知内容.</returns>
    /// <exception cref="Exception">请求异常.</exception>
    public NofityModel AlipayNotifyHandle(HttpRequest request, AliPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var requestDic = new Dictionary<string, string>();
        foreach(var item in request.Form)
        {
            requestDic.Add(item.Key, item.Value);
        }

        if(requestDic.Count == 0)
        {
            throw new Exception("fail");
        }

        if(_securityOptions.IsVerifySign)
        {
            bool verify = AliVerifySign(requestDic);
            if(!verify)
            {
                throw new Exception("fail");
            }
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
    /// <returns>验签结果.</returns>
    private bool AliVerifySign(Dictionary<string, string> signarr)
    {
        if(_securityOptions.SecurityType == "KEY")
        {
            var publicKey = GetKeyFromFile(_securityOptions.AliPublicKeyPath);
            return AlipaySignature.RSACheckV1(signarr, publicKey, "UTF-8", "RSA2", false);
        }

        if(_securityOptions.SecurityType == "CERT")
        {

            return AlipaySignature.RSACertCheckV1(signarr, _securityOptions.AliPublicCertPath, "UTF-8", "RSA2");
        }

        throw new ArgumentException("安全类型错误,应为KEY/CERT");
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

        return File.ReadAllText(filePath);
    }

    //创建AopClient.
    private DefaultAopClient CreatKEYAopClient()
    {
        var privateKey = GetKeyFromFile(_securityOptions.PrivateKeyPath);
        var publicKey = GetKeyFromFile(_securityOptions.AliPublicKeyPath);
        return new DefaultAopClient(ALI_PAY_URL, _securityOptions.AppId, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
    }

    //创建AopClient.
    private DefaultAopClient CreateCERTAopClient()
    {
        var privateKey = GetKeyFromFile(_securityOptions.PrivateKeyPath);
        var alipayConfig = new AlipayConfig
        {
            ServerUrl = ALI_PAY_URL,
            AppId = _securityOptions.AppId,
            PrivateKey = privateKey,
            Format = "json",
            SignType = "RSA2",
            Charset = "UTF-8",
            AlipayPublicCertPath = _securityOptions.AliPublicCertPath,
            AppCertPath = _securityOptions.AliAppPublicCertPath,
            RootCertPath = _securityOptions.AliRootCertPath
        };
        return new DefaultAopClient(alipayConfig);
    }
}