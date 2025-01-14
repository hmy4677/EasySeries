using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Util;
using EasySeries.Pay.Static.Models.Ali;

namespace EasySeries.Pay.Static;

/// <summary>
/// 支付宝.
/// </summary>
public static class AliPay
{
    private const string ALI_PAY_URL = "https://openapi.alipay.com/gateway.do";

    /// <summary>
    /// App创建订单.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="payInfo">支付信息.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static AlipayTradeAppPayResponse AppOrder(AliPayConfig payConfig, AliPayInfo payInfo)
    {
        var request = new AlipayTradeAppPayRequest();
        request.SetNotifyUrl(payConfig.PayNotifyUrl);

        var bizContent = new Dictionary<string, object>
        {
            { "out_trade_no", payInfo.OutTradeNo },
            { "total_amount", payInfo.Amount },
            { "subject", payInfo.Subject },
            { "product_code", "QUICK_MSECURITY_PAY"}
        };

        request.BizContent = JsonConvert.SerializeObject(bizContent);

        return payConfig.SecurityType switch
        {
            "KEY" => CreatKEYAopClient(payConfig).SdkExecute(request),
            "CERT" => CreateCERTAopClient(payConfig).SdkExecute(request),
            _ => throw new ArgumentException("安全类型错误,应为KEY/CERT")
        };
    }

    /// <summary>
    /// Wap创建订单.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="payInfo">支付信息.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static AlipayTradeWapPayResponse WapOrder(AliPayConfig payConfig, AliPayInfo payInfo)
    {
        var request = new AlipayTradeWapPayRequest();
        request.SetNotifyUrl(payConfig.PayNotifyUrl);
        request.SetReturnUrl(payInfo.ReturnUrl);
        var bizContent = new Dictionary<string, object>
        {
            { "out_trade_no", payInfo.OutTradeNo },
            { "total_amount", payInfo.Amount },
            { "subject", payInfo.Subject },
            { "product_code", "QUICK_WAP_WAY"}
        };

        request.BizContent = JsonConvert.SerializeObject(bizContent);

        return payConfig.SecurityType switch
        {
            "KEY" => CreatKEYAopClient(payConfig).pageExecute(request),
            "CERT" => CreateCERTAopClient(payConfig).pageExecute(request),
            _ => throw new ArgumentException("安全类型错误,应为KEY/CERT")
        };
    }

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="refundInfo">退款信息.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static AlipayTradeRefundResponse Refund(AliPayConfig payConfig, AliPayRefundInfo refundInfo)
    {
        var request = new AlipayTradeRefundRequest();
        var bizContent = new Dictionary<string, object>
        {
            { "out_trade_no",  refundInfo.OutTradeNo},
            { "refund_amount", refundInfo.RefundAmount },
            { "out_request_no", refundInfo.RefundId },
            { "refund_reason", refundInfo.Reason }
        };

        request.BizContent = JsonConvert.SerializeObject(bizContent);
        return ExecuteRequest(payConfig, request);
    }

    /// <summary>
    /// 关闭订单.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="outTradeNo">商户订单号.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static AlipayTradeCloseResponse Close(AliPayConfig payConfig, string outTradeNo)
    {
        var request = new AlipayTradeCloseRequest();
        var bizContent = new Dictionary<string, object>
        {
            { "out_trade_no", outTradeNo }
        };

        request.BizContent = JsonConvert.SerializeObject(bizContent);
        return ExecuteRequest(payConfig, request);
    }

    /// <summary>
    /// 查询支付.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="outTradeNo">商户订单号.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static AlipayTradeQueryResponse QueryPay(AliPayConfig payConfig, string outTradeNo)
    {
        var request = new AlipayTradeQueryRequest();
        var bizContent = new Dictionary<string, object>
        {
            { "out_trade_no", outTradeNo }
        };

        request.BizContent = JsonConvert.SerializeObject(bizContent);
        return ExecuteRequest(payConfig, request);
    }

    /// <summary>
    /// 查询退款.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="outTradeNo">商户订单号.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static AlipayTradeFastpayRefundQueryResponse QueryRefund(AliPayConfig payConfig, string outTradeNo)
    {
        var request = new AlipayTradeFastpayRefundQueryRequest();
        var bizContent = new Dictionary<string, object>
        {
            { "out_trade_no", outTradeNo }
        };

        request.BizContent = JsonConvert.SerializeObject(bizContent);
        return ExecuteRequest(payConfig, request);
    }

    /// <summary>
    /// 回调通知处理.
    /// 回应:await Response.WriteAsync("success/fail");
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="dictionary">通知Form转Dictionary,获取示例(foreach(var item in Request.Form) { dictionary.Add(item.Key, item.Value); }).</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static AliPayNotify NotifyHandel(AliPayConfig payConfig, Dictionary<string, string> dictionary)
    {
        if(dictionary.Count == 0)
        {
            throw new Exception("fail");
        }

        if(payConfig.IsVerifySign)
        {
            bool verify = VerifySign(payConfig, dictionary);
            if(!verify)
            {
                throw new Exception("fail");
            }
        }

        var isParse = decimal.TryParse(dictionary["total_amount"], out decimal totalAmout);
        return new AliPayNotify
        {
            OutTradeNo = dictionary["out_trade_no"],
            TotalAmount = isParse ? totalAmout : 0,
            TradeNo = dictionary["trade_no"],
            TradeStatus = dictionary["trade_status"]
        };
    }

    //创建Aop Key Client.
    private static DefaultAopClient CreatKEYAopClient(AliPayConfig payConfig)
    {
        var privateKey = GetKeyFromFile(payConfig.PrivateKeyPath);
        var publicKey = GetKeyFromFile(payConfig.AliPublicKeyPath);
        return new DefaultAopClient(ALI_PAY_URL, payConfig.AppId, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
    }

    //创建Aop Cert Client.
    private static DefaultAopClient CreateCERTAopClient(AliPayConfig payConfig)
    {
        var privateKey = GetKeyFromFile(payConfig.PrivateKeyPath);
        var alipayConfig = new AlipayConfig
        {
            ServerUrl = ALI_PAY_URL,
            AppId = payConfig.AppId,
            PrivateKey = privateKey,
            Format = "json",
            SignType = "RSA2",
            Charset = "UTF-8",
            AlipayPublicCertPath = payConfig.AliPublicCertPath,
            AppCertPath = payConfig.AliAppPublicCertPath,
            RootCertPath = payConfig.AliRootCertPath
        };
        return new DefaultAopClient(alipayConfig);
    }

    //执行请求.
    private static T ExecuteRequest<T>(AliPayConfig payConfig, IAopRequest<T> request)
        where T : AopResponse
    {
        return payConfig.SecurityType switch
        {
            "KEY" => CreatKEYAopClient(payConfig).Execute(request),
            "CERT" => CreateCERTAopClient(payConfig).CertificateExecute(request),
            _ => throw new ArgumentException("安全类型错误,应为KEY/CERT")
        };
    }

    //获取Key.
    private static string GetKeyFromFile(string filePath)
    {
        if(!File.Exists(filePath))
        {
            throw new Exception("密钥文件不存在");
        }

        return File.ReadAllText(filePath);
    }

    // 验签.
    private static bool VerifySign(AliPayConfig payConfig, Dictionary<string, string> signature)
    {
        if(payConfig.SecurityType == "KEY")
        {
            var publicKey = GetKeyFromFile(payConfig.AliPublicKeyPath);
            return AlipaySignature.RSACheckV1(signature, publicKey, "UTF-8", "RSA2", false);
        }

        if(payConfig.SecurityType == "CERT")
        {
            return AlipaySignature.RSACertCheckV1(signature, payConfig.AliPublicCertPath, "UTF-8", "RSA2");
        }

        throw new ArgumentException("安全类型错误,应为KEY/CERT");
    }
}
