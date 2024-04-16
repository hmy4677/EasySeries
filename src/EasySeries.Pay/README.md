## EasySeries.Pay

Easy支付，Easy系列的首个应用，用于微信支付，阿里支付。

### 使用说明:

#### 配置模式-支付配置Appsettings
```json
//appsettings.json
  "AliPaySecurityOptions": {
    "AppId": "xxx", //应用id(微信JSAPI,支付宝WAP).
    "PrivateKeyPath": "xx/xx.txt", //私钥文件路径.
    "AliPublicKeyPath": "xx/xx.txt", //阿里公钥文件路径.
    "PayNotifyUrl": "https://xx/api/xxx" //支付回调通知url(必要).
  },
  "WechatPaySecurityOptions": {
    "AppId": "xxx", //应用id(微信JSAPI,支付宝WAP).
    "MobileAppId": "xxx", //移动应用Id.
    "MchId": "xxxxx", //商户id.
    "Key": "xxxxxx", //v3 key.
    "CertSerialno": "xxx",//公钥证书序列号
    "IsVerifySign": false, //回调通知是否验签(暂无平台证书公钥的可不验签-不安全).
    "PlatCertPath": "xx/xx.pem", //平台证书公钥文件路径.
    "PrivateKeyPath": "xx/xx.pem", //私钥文件路径.
    "PayNotifyUrl": "https://xxx.xx", //支付回调通知url(必要).
    "RefundNotifyUrl": "https://xxx.xx" //退款回调通知url(必要).
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
```
#### API列表
```c#

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
    Task<string> WechatPrepayAsync(PayModel payModel, WechatPaySecurityOptions? securityOptions = null);

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
    /// 小程序支付签名.
    /// </summary>
    /// <param name="prepayid">预付订单id.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>小程序支付签名包.</returns>
    MiniAppSignInfo MiniAppSign(string prepayid, WechatPaySecurityOptions? securityOptions = null);

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

```
