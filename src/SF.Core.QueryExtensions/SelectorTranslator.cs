﻿/*******************************************************************************
* 命名空间: SF.Core.QueryExtensions.Extensions
*
* 功 能： N/A
* 类 名： SelectorTranslator
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/19 14:50:52 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SF.Core.QueryExtensions.Extensions
{
    /// <summary>
    /// Provides methods for combining and translating lambda expressions representing selectors.
    /// </summary>
    public static class SelectorTranslator
    {
        /// <summary>
        /// Starts translation of a given selector.
        /// </summary>
        /// <typeparam name="T">The type of the selector's source parameter.</typeparam>
        /// <typeparam name="U">The type of the selector's result parameter.</typeparam>
        /// <param name="selector">The selector expression to translate.</param>
        /// <returns>A translation object for the given selector.</returns>
        public static SelectorTranslation<T, U> Translate<T, U>(this Expression<Func<T, U>> selector)
        {
            return new SelectorTranslation<T, U>(selector);
        }

        /// <summary>
        /// Combines two given selectors by merging their member bindings.
        /// </summary>
        /// <typeparam name="T">The type of the selector's source parameter.</typeparam>
        /// <typeparam name="U">The type of the selector's result parameter.</typeparam>
        /// <param name="left">The first selector expression to combine.</param>
        /// <param name="right">The second selector expression to combine.</param>
        /// <returns>A single combined selector expression.</returns>
        public static Expression<Func<T, U>> Apply<T, U>(this Expression<Func<T, U>> left, Expression<Func<T, U>> right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));
            if (right == null)
                throw new ArgumentNullException(nameof(right));

            var leftInit = left.Body as MemberInitExpression;
            var rightInit = right.Body as MemberInitExpression;

            if (leftInit == null || rightInit == null)
                throw new NotSupportedException("Only member init expressions are supported yet.");
            if (leftInit.NewExpression.Arguments.Any() || rightInit.NewExpression.Arguments.Any())
                throw new NotSupportedException("Only parameterless constructors are supported yet.");

            var l = left.Parameters[0];
            var r = right.Parameters[0];

            var binder = new ParameterBinder(l, r);
            var bindings = leftInit.Bindings.Concat(rightInit.Bindings);

            return Expression.Lambda<Func<T, U>>(
                binder.Visit(Expression.MemberInit(Expression.New(typeof(U)), bindings)), r);
        }
    }
}