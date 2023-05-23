﻿using System.ComponentModel.DataAnnotations;

namespace EasySerise.Pay.Models.Ali;

/// <summary>
/// 支付安全信息.
/// </summary>
public class PaySecurityInfo
{
    /// <summary>
    /// 私钥文件路径.
    /// </summary>
    [Required]
    public string PrivateKeyPath { get; set; } = string.Empty;

    /// <summary>
    /// 公钥文件路径.
    /// </summary>
    [Required]
    public string PublicKeyPath { get; set; } = string.Empty;

    /// <summary>
    /// 应用id.
    /// </summary>
    [Required]
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 通知回调url.
    /// </summary>
    [Required]
    public string PayNotifyUrl { get; set; } = string.Empty;
}