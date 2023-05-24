﻿using EasySeries.Pay.Models;
using System.ComponentModel.DataAnnotations;

namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 微信支付安全信息.
/// </summary>
public class WechatPaySecurityInfo : PaySecurityInfo
{
    /// <summary>
    /// 微信商户号id.
    /// </summary>
    [Required]
    public string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 微信支付证书序列号.
    /// </summary>
    [Required]
    public string CertSerialno { get; set; } = string.Empty;

    /// <summary>
    /// 微信平台证书文件路径.
    /// </summary>
    [Required]
    public string PlatCertPath { get; set; } = string.Empty;

    /// <summary>
    /// 微信支付V3密码.
    /// </summary>
    [Required]
    public string Key { get; set; } = string.Empty;
}