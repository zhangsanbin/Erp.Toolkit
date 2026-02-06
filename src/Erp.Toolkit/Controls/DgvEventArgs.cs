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

using System;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 打印事件参数数据
    /// </summary>
    public class DgvPrintEventArgs : EventArgs
    {
        /// <summary>
        /// Ids 序列
        /// </summary>
        public string Ids { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = "ERP 2.0 Print Document";

        /// <summary>
        /// 设置 Handled 为 true，表示事件已被处理，内部定义的业务逻辑会被跳过
        /// </summary>
        public bool Handled { get; set; } // 添加Handled属性

        // 构造函数，如果需要的话
        public DgvPrintEventArgs(string ids)
        {
            Ids = ids;
            Handled = false; // 默认事件未被处理
        }
    }

    /// <summary>
    /// 导出事件参数数据
    /// </summary>
    public class DgvExportEventArgs : EventArgs
    {
        /// <summary>
        /// Ids 序列
        /// </summary>
        public string Ids { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = "ERP 2.0 Export Document";

        /// <summary>
        /// 设置 Handled 为 true，表示事件已被处理，内部定义的业务逻辑会被跳过
        /// </summary>
        public bool Handled { get; set; } // 添加Handled属性

        // 构造函数，如果需要的话
        public DgvExportEventArgs(string ids, string title)
        {
            Ids = ids;
            Title = title;
            Handled = false; // 默认事件未被处理
        }
    }

    /// <summary>
    /// 刷新事件参数数据
    /// </summary>
    public class DgvRefreshEventArgs : EventArgs
    {
        /// <summary>
        /// 设置 Handled 为 true，表示事件已被处理，内部定义的业务逻辑会被跳过
        /// </summary>
        public bool Handled { get; set; } // 添加Handled属性
    }

    /// <summary>
    /// 删除事件参数数据
    /// </summary>
    public class DgvDeleteEventArgs : EventArgs
    {
        /// <summary>
        /// Id 序列
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设置 Handled 为 true，表示事件已被处理，内部定义的业务逻辑会被跳过
        /// </summary>
        public bool Handled { get; set; } // 添加Handled属性

        // 构造函数，如果需要的话
        public DgvDeleteEventArgs(string id)
        {
            Id = id;
            Handled = false; // 默认事件未被处理
        }
    }
}