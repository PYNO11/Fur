﻿using Fur.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fur.AppCore.Inflations
{
    /// <summary>
    /// 类型包装器
    /// </summary>
    [NonInflated]
    internal sealed class TypeInflation
    {
        /// <summary>
        /// 所在程序集
        /// </summary>
        internal Assembly ThisAssembly { get; set; }

        /// <summary>
        /// 当前类型
        /// </summary>
        internal Type ThisType { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// 类型完整名称
        /// </summary>
        internal string FullName { get; set; }

        /// <summary>
        /// 是否是泛型类型
        /// </summary>
        internal bool IsGenericType { get; set; }

        /// <summary>
        /// 是否是静态类型
        /// </summary>
        internal bool IsStaticType { get; set; }

        /// <summary>
        /// 是否可以创建实例
        /// </summary>
        internal bool CanBeNew { get; set; }

        /// <summary>
        /// 泛型参数
        /// </summary>
        /// <remarks>
        /// <para>只有 <see cref="IsGenericType"/> = <c>true</c> 有作用</para>
        /// </remarks>
        internal IEnumerable<Type> GenericArgumentTypes { get; set; }

        /// <summary>
        /// 子属性集合
        /// </summary>
        /// <remarks>
        /// <para>包含：公开实例属性、公开静态属性</para>
        /// </remarks>
        internal IEnumerable<PropertyInflation> SubPropertis { get; set; }

        /// <summary>
        /// 子方法集合
        /// </summary>
        /// <remarks>
        /// <para>包含：公开实例方法、公开静态方法</para>
        /// </remarks>
        internal IEnumerable<MethodInflation> SubMethods { get; set; }

        /// <summary>
        /// 类型特性集合
        /// </summary>
        internal IEnumerable<Attribute> CustomAttributes { get; set; }

        /// <summary>
        /// 是否是控制器类型
        /// </summary>
        /// <remarks>
        /// 参见：<see cref="Fur.AppCore.App.IsControllerType(Type, bool)"/>
        /// </remarks>
        internal bool IsControllerType { get; set; }

        /// <summary>
        /// Swagger 接口文档 分组名
        /// </summary>
        /// <remarks>
        /// <para>只有 <see cref="IsControllerType"/> = <c>true</c> 有作用</para>
        /// </remarks>
        internal string[] SwaggerGroups { get; set; }

        /// <summary>
        /// 是否是数据库实体关联类型
        /// </summary>
        internal bool IsDbRelevanceEntityType { get; set; }
    }
}