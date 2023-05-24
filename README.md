# EasySerise
Easy系列，简单好用！

## EasySerise.Pay
Easy支付，Easy系列的首个应用，用于微信支付，阿里支付。

### 使用说明:

#### 预先配置模式

```csharp
//注册
using EasySeries.Pay.Models.Ali;
using EasySerise.Pay.AspNetCore;
using EasySerise.Pay.Models.Wechat;

var aliInfo = new AliPaySecurityInfo
{
    AppId = "your appid",
    PrivateKeyPath = "file path .txt",
    AliPublicKeyPath = "file path .txt",
    PayNotifyUrl = "your api url"
};
var wechatInfo = new WechatPaySecurityInfo
{
    AppId = "your appid",
    MchId = "your MchId",
    Key = "v3 key",
    CertSerialno = "",
    PlatCertPath = "file path .pem",
    PrivateKeyPath = "file path .pem",
    PayNotifyUrl = "your api url"
};

builder.Services.AddEasyPayService(aliInfo, wechatInfo);

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


#### 即时配置模式

```csharp
//注册
using EasySerise.Pay.AspNetCore;

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
    var wechatInfo = new WechatPaySecurityInfo
    {
      AppId = "your appid",
      MchId = "your MchId",
      Key = "v3 key",
      CertSerialno = "",
      PlatCertPath = "file path .pem",
      PrivateKeyPath = "file path .pem",
      PayNotifyUrl = "your api url"
    };
    return await _easyPayWechat.WechatQueryPayAsync(outTradeNo, "", wechatInfo);
}
    
[HttpGet("ali")]
public AlipayTradeQueryResponse QueryAli(string outTradeNo)
{
    var aliInfo = new AliPaySecurityInfo
    {
      AppId = "your appid",
      PrivateKeyPath = "file path .txt",
      AliPublicKeyPath = "file path .txt",
      PayNotifyUrl = "your api url"
    };
    return _easyPayAli.AlipayQuery(outTradeNo, aliInfo);
}
```
