## EasySeries.Pay

Easy支付，Easy系列的首个应用，用于微信支付，阿里支付, 中信全付通。

### 使用说明:

#### 配置模式-支付配置Appsettings
```json
//appsettings.json
  "AliPaySecurityOptions": {
    "AppId": "xxx", //支付宝WAP 应用id(现只支持wap).
    "PrivateKeyPath": "xx/xx.txt", //私钥文件路径(安全类型为KEY时必要).
    "AliPublicKeyPath": "xx/xx.txt", //阿里公钥文件路径(安全类型为KEY时必要).
    "PayNotifyUrl": "https://xx/api/xxx" //支付回调通知url(必要).
    "IsVerifySign": false, //回调通知是否验签(false不安全)
    "SecurityType": "CERT", //安全类型:KEY-密钥 或者 CERT-证书
    "AliAppPublicCertPath": "xxx.crt", //应用公钥证书文件路径(安全类型为CERT时必要).
    "AliRootCertPath": "xxx.crt", //阿里根证书文件路径(安全类型为CERT时必要).
    "AliPublicCertPath": "xxx.crt" //阿里公钥证书文件路径(安全类型为CERT时必要).
  },
  "WechatPaySecurityOptions": {
    "AppId": "xxx", //微信小程序Id.
    "MobileAppId": "xxx", //移动应用Id.
    "CommonAppId": "xxx", //微信公众号Id.
    "MchId": "xxxxx", //商户id.
    "Key": "xxxxxx", //v3 key.
    "CertSerialno": "xxx",//公钥证书序列号
    "IsVerifySign": false, //回调通知是否验签(暂无平台证书公钥的可不验签-不安全).
    "PlatCertPath": "xx/xx.pem", //平台证书公钥文件路径.
    "PrivateKeyPath": "xx/xx.pem", //私钥文件路径.
    "PayNotifyUrl": "https://xxx.xx", //支付回调通知url(必要).
    "RefundNotifyUrl": "https://xxx.xx" //退款回调通知url(必要).
  },
  "UnifyTradeSecurityOptions": {
    "IsVerifySign": true, //返回结果是否验签.
    "CerFilePath": "D:\\IIS\\cert\\zx_cert\\中信生产公钥.cer",
    "KeyFilePath": "D:\\IIS\\cert\\zx_cert\\private_key.txt", //需要用提供的java先解密保存txt再用.
    "MchId": "xxx", //商户号.
    "SubAppId": "", //微信小程序id,需要绑定.
    "NotifyUrl": "" //回调通知url.
  }
```

```c#
//注册
builder.Services.AddEasyPayService();

//使用
public YourController(IEasyPayWechat easyPayWechat, IEasyPayAli easyPayAli)
{
  _easyPayWechat = easyPayWechat;
  _easyPayAli = easyPayAli;
}
    
[HttpGet("wechat")]
public async Task<PayQueryResponse> QueryAsync(string outTradeNo)
{
  return await _easyPayWechat.WechatQueryPayAsync(outTradeNo, "");
}
    
[HttpGet("ali")]
public AlipayTradeQueryResponse QueryAli(string outTradeNo)
{
  return _easyPayAli.AlipayQuery(outTradeNo);
}

[HttpGet("unify")]
public async Task<UnifyTradeQueryResponse> UnifyQueryAsync(string ourTradeNo)
{
    return await _easyPayUnifyTrade.UnifyTradeQueryAsync(ourTradeNo);
}

```

#### 即时模式-支付配置-可从数据库读取

```c#
//注册
builder.Services.AddEasyPayService();

//使用
public YourController(IEasyPayWechat easyPayWechat, IEasyPayAli easyPayAli)
{
        _easyPayWechat = easyPayWechat;
        _easyPayAli = easyPayAli;
}
    
[HttpGet("wechat")]
public async Task<PayQueryResponse> QueryAsync(string outTradeNo)
{
    var options = new WechatPaySecurityOptions
    {
      AppId = "your appid",
      MchId = "your MchId",
      Key = "v3 key",
      CertSerialno = "",
      PlatCertPath = "file path .pem",
      PrivateKeyPath = "file path .pem",
      PayNotifyUrl = "your api url"
    };
    return await _easyPayWechat.WechatQueryPayAsync(outTradeNo, "", options);
}
    
[HttpGet("ali")]
public AlipayTradeQueryResponse QueryAli(string outTradeNo)
{
    var options = new AliPaySecurityOptions
    {
      AppId = "your appid",
      PrivateKeyPath = "file path .txt",
      AliPublicKeyPath = "file path .txt",
      PayNotifyUrl = "your api url"
    };
    return _easyPayAli.AlipayQuery(outTradeNo, options);
}

[HttpGet("unify")]
public async Task<UnifyTradeQueryResponse> UnifyQueryAsync(string ourTradeNo)
{
    var options = new UnifyTradeSecurityOptions
    {
        ...
    };
    return await _easyPayUnifyTrade.UnifyTradeQueryAsync(ourTradeNo, options);
}

```
#### API列表
```c#

/// <summary>
/// 微信支付Interface.
/// </summary>
public interface IEasyPayWechat
{
    /// <summary>
    /// 生成预付订单(JSAPI).
    /// </summary>
    /// <param name="payModel">支付信息model.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>预付订单号.</returns>
    Task<string> WechatPrepayAsync(JSAPIPayModel payModel, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 生成预付订单(APP).
    /// </summary>
    /// <param name="payModel">支付信息model.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>预付订单号.</returns>
    Task<string> WechatPrepayAsync(AppPayModel payModel, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 支付查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号(2选1).</param>
    /// <param name="tradeNo">微信支付单号(2选1).</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="ArgumentNullException">单号为空.</exception>
    Task<PayQueryResponse> WechatQueryPayAsync(string outTradeNo, string tradeNo, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="refundModel">退款信息.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>结果信息.</returns>
    Task<RefundResponse> WechatRefundAsync(RefundModel refundModel, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 查询退款.
    /// </summary>
    /// <param name="refundNo">退款单号.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>查询结果.</returns>
    Task<RefundQueryResponse> WechatQueryRefundAsync(string refundNo, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 回调通知处理.
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="Exception">处理异常.</exception>
    Task<PayQueryResponse> WechatNotifyHandleAsync(HttpRequest request, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 退款回调通知处理.
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="Exception">处理异常.</exception>
    Task<RefundNotify> WechatRefundNotifyHandleAsync(HttpRequest request, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// JSAPI签名.
    /// </summary>
    /// <param name="prepayid">预付订单id.</param>
    /// <param name="appIdType">appId类型.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>JSAPI签名信息.</returns>
    JSAPISignInfo JSAPISign(string prepayid, JSAPIAppIdTypes appIdType, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// APP支付签名.
    /// </summary>
    /// <param name="prepayid">预付订单id.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>APP支付签名包.</returns>
    AppSignInfo AppSign(string prepayid, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 获取支付平台证书(验签用).
    /// </summary>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付平台证书.</returns>
    Task<List<PlatCert>> WechatGetCertificatesAsync(WechatPaySecurityOptions? securityOptions = null);
}

/// <summary>
/// 阿里支付Interface.
/// </summary>
public interface IEasyPayAli
{
    /// <summary>
    /// 关闭订单.
    /// </summary>
    /// <param name="outTradeNO">商户单号.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>关闭退款响应结果.</returns>
    AlipayTradeCloseResponse AlipayClose(string outTradeNO, AliPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 回调通知处理.
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>通知内容.</returns>
    /// <exception cref="Exception">请求异常.</exception>
    NofityModel AlipayNotifyHandle(HttpRequest request, AliPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 账单查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>查询响应结果.</returns>
    AlipayTradeQueryResponse AlipayQuery(string outTradeNo, AliPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="refundModel">退款模型.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>退款响应结果.</returns>
    AlipayTradeRefundResponse AlipayRefund(AliPayRefundModel refundModel, AliPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 支付(手机网页).
    /// </summary>
    /// <param name="payModel">支付model.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付响应结果.</returns>
    AlipayTradeWapPayResponse AlipayWap(AliPayModel payModel, AliPaySecurityOptions? securityOptions = null);
}

/// <summary>
/// 中信全付通Interface.
/// </summary>
public interface IEasyPayUnifyTrade
{
    /// <summary>
    /// 中信全付通回调处理.回应:await Response.WriteAsync("SUCCESS/FAIL");
    /// </summary>
    /// <param name="request">网络请求.</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>回调通知处理.</returns>
    /// <exception cref="Exception"></exception>
    Task<dynamic> UnifyTradeCallbackHandleAsync(HttpRequest request, UnifyTradeSecurityOptions? securityOptions = null);

    /// <summary>
    /// 中信全付通支付下单.
    /// </summary>
    /// <param name="native"></param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>支付下单响应.</returns>
    Task<dynamic> UnifyTradeNativeAsync(UnifyTradeNative native, UnifyTradeSecurityOptions? securityOptions = null);

    /// <summary>
    /// 中信全付通查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>查询响应.</returns>
    Task<UnifyTradeQueryResponse> UnifyTradeQueryAsync(string outTradeNo, UnifyTradeSecurityOptions? securityOptions = null);

    /// <summary>
    /// 中信全付通查询退款.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="outRefundNo">退款单号.</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>查询退款响应.</returns>
    Task<UnifyTradeQueryRefundResponse> UnifyTradeQueryRefundAsync(string outTradeNo, string outRefundNo, UnifyTradeSecurityOptions? securityOptions = null);

    /// <summary>
    /// 中信全付通退款.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="outRefundNo">商户退款单号.</param>
    /// <param name="totalFee">订单总额(单位:分).</param>
    /// <param name="refundFee">退款金额(单位:分).</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>退款响应.</returns>
    Task<UnifyTradeRefundResponse> UnifyTradeRefundAsync(string outTradeNo, string outRefundNo, int totalFee, int refundFee, UnifyTradeSecurityOptions? securityOptions = null);
}

```
