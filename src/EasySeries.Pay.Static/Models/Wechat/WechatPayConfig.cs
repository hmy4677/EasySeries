﻿namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信支付配置.
/// </summary>
public class WechatPayConfig
{
    /// <summary>
    /// 应用id.
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 微信商户id.
    /// </summary>
    public string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 微信支付公钥证书序列号.
    /// </summary>
    public string CertSerialNo { get; set; } = string.Empty;

    /// <summary>
    /// 微信商户私钥文件路径.
    /// </summary>
    public string PrivateKeyPath { get; set; } = string.Empty;

    /// <summary>
    /// 微信支付平台公钥证书路径.
    /// </summary>
    public string PlatformCertPath { get; set; } = string.Empty;

    /// <summary>
    /// 微信支付V3 KEY.
    /// </summary>
    public string V3Key { get; set; } = string.Empty;

    /// <summary>
    /// 支付通知回调url.
    /// </summary>
    public string PayNotifyUrl { get; set; } = string.Empty;

    /// <summary>
    /// 退款通知回调url.
    /// </summary>
    public string RefundNotifyUrl { get; set; } = string.Empty;
}