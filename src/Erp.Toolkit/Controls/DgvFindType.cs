/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-07-16           Andy        Split, restructure
 */

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 筛选类型
    /// </summary>
    public enum DgvFindType
    {
        /// <summary>
        /// 等于（整个字段）
        /// </summary>
        Equals = 0,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEquals = 1,

        /// <summary>
        /// 开头是（字段开头）
        /// </summary>
        BeginsWith = 2,

        /// <summary>
        /// 开头不是
        /// </summary>
        NotBeginsWith = 3,

        /// <summary>
        /// 包含（字段任何部分）
        /// </summary>
        Contains = 4,

        /// <summary>
        /// 不包含
        /// </summary>
        NotContains = 5,

        /// <summary>
        /// 结尾是（字段结尾）
        /// </summary>
        EndsWith = 6,

        /// <summary>
        /// 结尾不是
        /// </summary>
        NotEndsWith = 7,

        /// <summary>
        /// 小于
        /// </summary>
        LessThan = 8,

        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan = 9,

        /// <summary>
        /// 期间
        /// </summary>
        Period = 10,
    }
}