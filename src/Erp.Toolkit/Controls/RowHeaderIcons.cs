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
    /// 展开图标
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    public enum RowHeaderIcons
    {
        expand = 0,
        collapse = 1,
        collapseIcon = 2,
        expandIcon = 3
    }
}