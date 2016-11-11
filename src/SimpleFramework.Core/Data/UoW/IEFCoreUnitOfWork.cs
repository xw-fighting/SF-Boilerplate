﻿/*******************************************************************************
* 命名空间: SimpleFramework.Core.Data.UoW
*
* 功 能： N/A
* 类 名： IEFCoreUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:58:04 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SimpleFramework.Core.Abstraction.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Data.UoW
{
    /// <summary>
    /// Represents an Unit of Work specialized for the Entity Framework Core.
    /// </summary>
    /// <typeparam name="TDbContext">The database context type</typeparam>
    public interface IEFCoreUnitOfWork<out TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        TDbContext Context { get; }
    }

    /// <summary>
    /// Represents an Unit of Work specialized for the Entity Framework Core.
    /// </summary>
    public interface IEFCoreUnitOfWork : IEFCoreUnitOfWork<DbContext>
    {

    }
}
