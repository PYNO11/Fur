﻿// 框架名称：Fur
// 框架作者：百小僧
// 框架版本：1.0.0
// 开源协议：MIT
// 项目地址：https://gitee.com/monksoul/Fur

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Fur.DatabaseAccessor
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IRepository<TEntity> : IRepository<TEntity, DbContextLocator>
        where TEntity : class, IEntityBase, new()
    {
    }

    /// <summary>
    /// 非泛型仓储
    /// </summary>
    public partial interface IRepository
    {
        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        IRepository<TEntity> Use<TEntity>()
            where TEntity : class, IEntityBase, new();

        /// <summary>
        /// 切换多数据库上下文仓储
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>仓储</returns>
        IRepository<TEntity, TDbContextLocator> Use<TEntity, TDbContextLocator>()
            where TEntity : class, IEntityBase, new()
            where TDbContextLocator : class, IDbContextLocator, new();
    }

    /// <summary>
    /// 多数据库上下文仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial interface IRepository<TEntity, TDbContextLocator> : IWritableRepository<TEntity, TDbContextLocator>, IReadableRepository<TEntity, TDbContextLocator>
        where TEntity : class, IEntityBase, new()
        where TDbContextLocator : class, IDbContextLocator, new()
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        DbContext DbContext { get; }

        /// <summary>
        /// 实体集合
        /// </summary>
        DbSet<TEntity> Entities { get; }

        /// <summary>
        /// 不跟踪的（脱轨）实体
        /// </summary>
        IQueryable<TEntity> DerailEntities { get; }

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        DbConnection DbConnection { get; }

        /// <summary>
        /// 实体追综器
        /// </summary>
        ChangeTracker ChangeTracker { get; }

        /// <summary>
        /// 租户Id
        /// </summary>
        Guid? TenantId { get; }

        /// <summary>
        /// 判断上下文是否更改
        /// </summary>
        /// <returns>bool</returns>
        bool HasChanges();

        /// <summary>
        /// 将实体加入数据上下文托管
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>EntityEntry</returns>
        EntityEntry Entry(object entity);

        /// <summary>
        /// 将实体加入数据上下文托管
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        EntityEntry<TEntity> Entry(TEntity entity);

        /// <summary>
        /// 获取实体状态
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        EntityState EntityEntryState(object entity);

        /// <summary>
        /// 获取实体状态
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>EntityState</returns>
        EntityState EntityEntryState(TEntity entity);

        /// <summary>
        /// 将实体属性加入托管
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>PropertyEntry</returns>
        PropertyEntry EntityPropertyEntry(object entity, string propertyName);

        /// <summary>
        /// 将实体属性加入托管
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>PropertyEntry</returns>
        PropertyEntry EntityPropertyEntry(TEntity entity, string propertyName);

        /// <summary>
        /// 将实体属性加入托管
        /// </summary>
        /// <typeparam name="TProperty">属性类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="propertyPredicate">属性表达式</param>
        /// <returns>PropertyEntry</returns>
        PropertyEntry<TEntity, TProperty> EntityPropertyEntry<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> propertyPredicate);

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        bool IsAttach(object entity);

        /// <summary>
        /// 判断是否被附加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        bool IsAttach(TEntity entity);

        /// <summary>
        /// 附加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>EntityEntry</returns>
        EntityEntry Attach(object entity);

        /// <summary>
        /// 附加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>EntityEntry</returns>
        EntityEntry<TEntity> Attach(TEntity entity);

        /// <summary>
        /// 附加多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        void AttachRange(params object[] entities);

        /// <summary>
        /// 附加多个实体
        /// </summary>
        /// <param name="entities">多个实体</param>
        void AttachRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 获取所有数据库上下文
        /// </summary>
        /// <returns>ConcurrentBag<DbContext></returns>
        public ConcurrentBag<DbContext> GetDbContexts();

        /// <summary>
        /// 判断实体是否设置了主键
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        bool IsKeySet(TEntity entity);

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <returns>IQueryable<TEntity></returns>
        IQueryable<TEntity> AsQueryable(bool noTracking = false);

        /// <summary>
        /// 构建查询分析器
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="noTracking">是否跟踪实体</param>
        /// <param name="ignoreQueryFilters">是否忽略查询过滤器</param>
        /// <param name="asSplitQuery">是否切割查询</param>
        /// <returns>IQueryable<TEntity></returns>
        IQueryable<TEntity> AsQueryable(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, bool ignoreQueryFilters = false, bool asSplitQuery = true);

        /// <summary>
        /// 切换仓储
        /// </summary>
        /// <typeparam name="TUseEntity">实体类型</typeparam>
        /// <returns>仓储</returns>
        IRepository<TUseEntity> Use<TUseEntity>()
            where TUseEntity : class, IEntityBase, new();

        /// <summary>
        /// 切换多数据库上下文仓储
        /// </summary>
        /// <typeparam name="TUseEntity">实体类型</typeparam>
        /// <typeparam name="TUseDbContextLocator">数据库上下文定位器</typeparam>
        /// <returns>仓储</returns>
        IRepository<TUseEntity, TUseDbContextLocator> Use<TUseEntity, TUseDbContextLocator>()
            where TUseEntity : class, IEntityBase, new()
            where TUseDbContextLocator : class, IDbContextLocator, new();
    }
}