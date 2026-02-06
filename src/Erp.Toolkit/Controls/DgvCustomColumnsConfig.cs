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
using System.Collections.Generic;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 用户自定义列
    /// </summary>
    // 添加 Serializable 特性，标记为可序列化
    [Serializable]
    public class DgvCustomColumnsConfig
    {
        public string Name { get; set; }

        public string HeaderText { get; set; }

        public List<DgvCustomColumnsValue> ValueList { get; set; }

        // 构造函数，用于初始化ValueList
        public DgvCustomColumnsConfig()
        {
            ValueList = new List<DgvCustomColumnsValue>();
        }
    }

    // 添加 Serializable 特性，标记为可序列化
    [Serializable]
    public class DgvCustomColumnsValue
    {
        public string Value { get; set; }

        public DgvCustomColumnsValueType ValueType { get; set; }
    }

    // 添加 Serializable 特性，标记为可序列化
    [Serializable]
    public enum DgvCustomColumnsValueType
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        CellsName = 0,

        /// <summary>
        /// 字符串
        /// </summary>
        String = 1
    }
}