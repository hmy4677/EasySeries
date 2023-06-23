## EasySeries.Pay

Easy支付，Easy系列的首个应用，用于微信支付，阿里支付。

### 使用说明:

#### 预先配置模式
```json
//appsettings.json
  "AliPaySecurityOptions": {
    "AppId": "xxx",
    "PrivateKeyPath": "xx/xx.txt",
    "AliPublicKeyPath": "xx/xx.txt",
    "PayNotifyUrl": "https://xx/api/xxx"
  },
  "WechatPaySecurityOptions": {
    "AppId": "xxx",
    "MchId": "xxx",
    "Key": "v3 key",
    "CertSerialno": "xxx",
    "PlatCertPath": "xx/xx.pem",
    "PrivateKeyPath": "xx/xx.pem",
    "PayNotifyUrl": "https://xx/api/xxx"
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

#### 即时配置模式

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
