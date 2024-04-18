using EasySeries.Pay.Models.Wechat;
using EasySeries.Pay.Options;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using RestSharp;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EasySeries.Pay.Implement;

/// <summary>
/// 微信支付Implement.
/// </summary>
public class EasyPayWechat : IEasyPayWechat
{
    private WechatPaySecurityOptions _securityOptions;

    /// <summary>
    /// 初始化.
    /// </summary>
    /// <param name="securityOptions">支付安全.</param>
    public EasyPayWechat(IOptions<WechatPaySecurityOptions> securityOptions)
    {
        _securityOptions = securityOptions.Value;
    }

    /// <summary>
    /// 生成预付订单(JSAPI).
    /// </summary>
    /// <param name="payModel">支付信息model.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>预付订单号.</returns>
    public async Task<string> WechatPrepayAsync(PayModel payModel, WechatPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        const string API_URL = "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";
        var requestBody = new PrepayRequest
        {
            AppId = _securityOptions.AppId,
            Mchid = _securityOptions.MchId,
            NotifyUrl = _securityOptions.PayNotifyUrl,

            Description = payModel.Description,
            OutTradeNo = payModel.OutTradeNO,
            Amount = new PreOrderAmount { Total = payModel.Amount },
            Payer = new PayerInfo { OpenId = payModel.OpenId }
        };
        var requestBodyJson = JsonConvert.SerializeObject(requestBody);
        var result = await RestRequestAsync<PrepayResponse>(API_URL, requestBodyJson);
        return result.PrepayId;
    }

    /// <summary>
    /// 生成预付订单(APP).
    /// </summary>
    /// <param name="payModel">支付信息model.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>预付订单号.</returns>
    public async Task<string> WechatPrepayAsync(AppPayModel payModel, WechatPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        const string API_URL = "https://api.mch.weixin.qq.com/v3/pay/transactions/app";
        var requestBody = new AppPrepayRequest
        {
            AppId = _securityOptions.MobileAppId,
            Mchid = _securityOptions.MchId,
            NotifyUrl = _securityOptions.PayNotifyUrl,

            Description = payModel.Description,
            OutTradeNo = payModel.OutTradeNO,
            Amount = new PreOrderAmount { Total = payModel.Amount }
        };
        var requestBodyJson = JsonConvert.SerializeObject(requestBody);
        var result = await RestRequestAsync<PrepayResponse>(API_URL, requestBodyJson);
        return result.PrepayId;
    }

    /// <summary>
    /// 支付查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号(2选1).</param>
    /// <param name="tradeNo">微信支付单号(2选1).</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="ArgumentNullException">单号为空.</exception>
    public async Task<PayQueryResponse> WechatQueryPayAsync(string outTradeNo, string tradeNo, WechatPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        string? type, no;
        if(!string.IsNullOrEmpty(outTradeNo))
        {
            type = "out-trade-no";
            no = outTradeNo;
        }
        else if(!string.IsNullOrEmpty(tradeNo))
        {
            type = "id";
            no = tradeNo;
        }
        else
        {
            throw new ArgumentNullException(nameof(outTradeNo), "商户单号与微信支付单号都为空");
        }

        var url = $"https://api.mch.weixin.qq.com/v3/pay/transactions/{type}/{no}?mchid={_securityOptions.MchId}";
        return await RestRequestAsync<PayQueryResponse>(url);
    }

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <param name="refundModel">退款信息.</param>
    /// <returns>结果信息.</returns>
    public async Task<RefundResponse> WechatRefundAsync(RefundModel refundModel, WechatPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        const string API_URL = "https://api.mch.weixin.qq.com/v3/refund/domestic/refunds";
        var requstBody = new RefundRequest
        {
            OutfefundNo = refundModel.RefundNo,
            OutTradeNo = refundModel.OutTradeNo,
            Reason = refundModel.Reason,
            NotifyUrl = _securityOptions.RefundNotifyUrl,
            Amount = new RefundAmount
            {
                Total = refundModel.TotalAmount,
                Refund = refundModel.RefundAmount,
            }
        };

        var requstBodyJson = JsonConvert.SerializeObject(requstBody);
        return await RestRequestAsync<RefundResponse>(API_URL, requstBodyJson);
    }

    /// <summary>
    /// 查询退款.
    /// </summary>
    /// <param name="refundNo">退款单号.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>查询结果.</returns>
    public async Task<RefundQueryResponse> WechatQueryRefundAsync(string refundNo, WechatPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var url = $"https://api.mch.weixin.qq.com/v3/refund/domestic/refunds/{refundNo}";
        return await RestRequestAsync<RefundQueryResponse>(url);
    }

    /// <summary>
    /// 获取平台证书公钥(验签用).
    /// </summary>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付平台证书.</returns>
    public async Task<List<PlatCert>> WechatGetCertificatesAsync(WechatPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        const string API_URL = "https://api.mch.weixin.qq.com/v3/certificates";
        var result = await RestRequestAsync<PlactCertResponse>(API_URL);
        return result.Data.ConvertAll(p => new PlatCert
        {
            SerialNo = p.SerialNo,
            EffectiveTime = p.EffectiveTime,
            ExpireTime = p.ExpireTime,
            DecryptText = AesGcmDecrypt(p.EncryptCertificate.AssociatedData, p.EncryptCertificate.Nonce, p.EncryptCertificate.Ciphertext)
        });
    }

    /// <summary>
    /// 小程序支付签名.
    /// </summary>
    /// <param name="prepayid">预付订单id.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>小程序支付签名包.</returns>
    public MiniAppSignInfo MiniAppSign(string prepayid, WechatPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var appId = _securityOptions.AppId;
        var timeStamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var nonceStr = Path.GetRandomFileName();
        var package = $"prepay_id={prepayid}";
        var signStr = $"{appId}\n{timeStamp}\n{nonceStr}\n{package}\n";
        var paySign = SHA256WithRSASign(signStr);
        return new MiniAppSignInfo
        {
            AppId = appId,
            NonceStr = nonceStr,
            Package = package,
            PaySign = paySign,
            TimeStamp = timeStamp,
        };
    }

    /// <summary>
    /// APP支付签名.
    /// </summary>
    /// <param name="prepayid">预付订单id.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>APP支付签名包.</returns>
    public AppSignInfo AppSign(string prepayid, WechatPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var appId = _securityOptions.MobileAppId;
        var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var nonceStr = Path.GetRandomFileName();
        var signStr = $"{appId}\n{timeStamp}\n{nonceStr}\n{prepayid}\n";
        var paySign = SHA256WithRSASign(signStr);
        return new AppSignInfo
        {
            Appid = appId,
            Noncestr = nonceStr,
            Package = "Sign=WXPay",
            Sign = paySign,
            Timestamp = timeStamp,
            Prepayid = prepayid,
            Partnerid = _securityOptions.MchId
        };
    }

    /// <summary>
    /// 回调通知处理. 回应:return Ok()/BadRequest("'code':'FAIL','message':'验签失败'");
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="Exception">处理异常.</exception>
    public async Task<PayQueryResponse> WechatNotifyHandleAsync(HttpRequest request, WechatPaySecurityOptions? securityOptions = null)
    {
        return await NotifyHandleAsync<PayQueryResponse>(request, securityOptions);
    }

    /// <summary>
    /// 退款回调通知处理. 回应:return Ok()/BadRequest("'code':'FAIL','message':'验签失败'");
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="Exception">处理异常.</exception>
    public async Task<RefundNotify> WechatRefundNotifyHandleAsync(HttpRequest request, WechatPaySecurityOptions? securityOptions = null)
    {
        return await NotifyHandleAsync<RefundNotify>(request, securityOptions);
    }

    /// <summary>
    /// 回调通知处理.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="securityOptions"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    private async Task<T> NotifyHandleAsync<T>(HttpRequest request, WechatPaySecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var reader = new StreamReader(request.Body);
        var bodyJson = await reader.ReadToEndAsync();
        if(string.IsNullOrEmpty(bodyJson))
        {
            throw new ArgumentNullException(nameof(request), "内容为空");
        }

        var signature = request.Headers["Wechatpay-Signature"];
        var stamp = request.Headers["Wechatpay-Timestamp"];
        var nonce = request.Headers["Wechatpay-Nonce"];

        if(_securityOptions.IsVerifySign)
        {
            var verify = WechatVerifySign(signature, stamp, nonce, bodyJson);
            if(!verify)
            {
                throw new ArgumentException("验签失败");
            }
        }

        var notify = JsonConvert.DeserializeObject<NotifyModel>(bodyJson);
        var decrypt = AesGcmDecrypt(notify.Resource.AssociatedData, notify.Resource.Nonce, notify.Resource.Ciphertext);
        return JsonConvert.DeserializeObject<T>(decrypt);
    }

    /// <summary>
    /// Rest请求.
    /// </summary>
    /// <typeparam name="T">请求结果类型.</typeparam>
    /// <param name="url">请求url.</param>
    /// <param name="requestBodyJson">请求body(json).</param>
    /// <returns>请求结果</returns>
    /// <exception cref="Exception">请求结果异常.</exception>
    private async Task<T> RestRequestAsync<T>(string url, string? requestBodyJson = null)
    {
        var client = new RestClient(url);
        var request = new RestRequest();
        var method = "GET";
        if(requestBodyJson != null)
        {
            method = "POST";
            request.AddJsonBody(requestBodyJson);
            request.Method = Method.Post;
        }

        var urlTrim = url.Replace("https://api.mch.weixin.qq.com", string.Empty, StringComparison.CurrentCulture).TrimEnd('/');
        var authorization = BuildAuthorization(method, urlTrim, requestBodyJson);
        request.AddHeader("Authorization", authorization);

        var response = await client.ExecuteAsync(request);
        if(response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<T>(response?.Content ?? "");
        }
        else
        {
            throw new ArgumentException(response.Content);
        }
    }

    /// <summary>
    /// 微信解密.
    /// </summary>
    /// <param name="associatedData"></param>
    /// <param name="nonce"></param>
    /// <param name="ciphertext"></param>
    /// <returns>解密结果.</returns>
    private string AesGcmDecrypt(string associatedData, string nonce, string ciphertext)
    {
        if(string.IsNullOrEmpty(associatedData) || string.IsNullOrEmpty(nonce) || string.IsNullOrEmpty(ciphertext))
        {
            throw new ArgumentNullException($"解密失败:参数为空");
        }

        var gcmBlockCipher = new GcmBlockCipher(new AesEngine());
        var aeadParameters = new AeadParameters(
            new KeyParameter(Encoding.UTF8.GetBytes(_securityOptions.Key)),
            128,
            Encoding.UTF8.GetBytes(nonce),
            Encoding.UTF8.GetBytes(associatedData));
        gcmBlockCipher.Init(false, aeadParameters);

        byte[] data = Convert.FromBase64String(ciphertext);
        byte[] plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
        int length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
        gcmBlockCipher.DoFinal(plaintext, length);
        return Encoding.UTF8.GetString(plaintext);
    }

    /// <summary>
    /// 微信验签.
    /// </summary>
    /// <param name="signature">签名串.</param>
    /// <param name="stamp">时间戳.</param>
    /// <param name="nonce">随机串.</param>
    /// <param name="bodyjson">请求体json.</param>
    /// <returns>验签结果.</returns>
    private bool WechatVerifySign(string signature, string stamp, string nonce, string? bodyjson)
    {
        if(!File.Exists(_securityOptions.PlatCertPath))
        {
            throw new Exception("微信平台证书文件不存在");
        }

        var messageData = Encoding.UTF8.GetBytes($"{stamp}\n{nonce}\n{bodyjson}\n");

        var cert = new X509Certificate(_securityOptions.PlatCertPath);
        var resultsTrue = cert.GetPublicKey();

        var myrsa = RSA.Create();
        myrsa.ImportRSAPublicKey(resultsTrue, out _);

        var signature64 = Convert.FromBase64String(signature);
        return myrsa.VerifyData(messageData, signature64, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    /// <summary>
    /// 构建微信支付后端请求Authorization.
    /// </summary>
    /// <param name="method"></param>
    /// <param name="url"></param>
    /// <param name="requestBodyJson"></param>
    /// <returns></returns>
    private string BuildAuthorization(string method, string url, string? requestBodyJson = null)
    {
        var timestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var nonce = Path.GetRandomFileName();
        var signStr = $"{method}\n{url}\n{timestamp}\n{nonce}\n{requestBodyJson ?? string.Empty}\n";
        var signature = SHA256WithRSASign(signStr);

        return $" WECHATPAY2-SHA256-RSA2048 mchid=\"{_securityOptions.MchId}\",nonce_str=\"{nonce}\",signature=\"{signature}\",timestamp=\"{timestamp}\",serial_no=\"{_securityOptions.CertSerialno}\"";
    }

    /// <summary>
    /// SHA256 With RSA 签名.
    /// </summary>
    /// <param name="signStr">被签串.</param>
    /// <returns>签名.</returns>
    private string SHA256WithRSASign(string signStr)
    {
        if(!File.Exists(_securityOptions.PrivateKeyPath))
        {
            throw new ArgumentException("微信私钥证书文件不存在");
        }

        var keyText = File.ReadAllText(_securityOptions.PrivateKeyPath);
        keyText = keyText.Replace("-----BEGIN PRIVATE KEY-----", string.Empty).Replace("-----END PRIVATE KEY-----", string.Empty).Trim();
        var keyBuffer = Convert.FromBase64String(keyText);

        using RSA rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(keyBuffer, out _);
        var signBuffer = Encoding.UTF8.GetBytes(signStr);
        return Convert.ToBase64String(rsa.SignData(signBuffer, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
    }
}