﻿// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Fur.ConfigurableOptions;

namespace Fur.DataValidation
{
    /// <summary>
    /// 验证消息配置选项
    /// </summary>
    [OptionsSettings("AppSettings:ValidationTypeMessageSettings")]
    public sealed class ValidationTypeMessageSettingsOptions : IConfigurableOptions
    {
        /// <summary>
        /// 验证消息配置表
        /// </summary>
        public object[][] Definitions { get; set; }
    }
}