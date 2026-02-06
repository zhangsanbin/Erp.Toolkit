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
    /// 主动点击刷新或弹出式模态窗口保存后，自动刷新接口
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    public interface IReLoadListUI
    {
        /// <summary>
        /// 触发刷新或弹出式模态窗口保存后，自动刷新实现
        /// </summary>
        /// <param name="Id">要传递给拥有者的Id实参值（可选参数）</param>
        void LoadLists(int Id = 0);
    }
}