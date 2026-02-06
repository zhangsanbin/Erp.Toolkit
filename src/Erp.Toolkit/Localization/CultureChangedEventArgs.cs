/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2025-11-27           Andy        the first version
 * 2025-12-14           Andy        Refactoring, splitting files
 */

using System;
using System.Globalization;

namespace Erp.Toolkit.Localization
{
    /// <summary>
    /// 文化变更事件参数
    /// </summary>
    public class CultureChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 旧文化信息
        /// </summary>
        public CultureInfo OldCulture { get; }

        /// <summary>
        /// 新文化信息
        /// </summary>
        public CultureInfo NewCulture { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="oldCulture"></param>
        /// <param name="newCulture"></param>
        public CultureChangedEventArgs(CultureInfo oldCulture, CultureInfo newCulture)
        {
            OldCulture = oldCulture;
            NewCulture = newCulture;
        }
    }
}