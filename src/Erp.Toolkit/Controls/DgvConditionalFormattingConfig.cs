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
using System.Drawing;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 条件格式配置项（文本匹配）
    /// </summary>
    public class DgvConditionalFormattingConfig
    {
        /// <summary>
        /// 目标列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 行级条件判断函数
        /// </summary>
        public Func<Dictionary<string, object>, bool> RowCondition { get; set; }

        /// <summary>
        /// 通用条件判断函数
        /// </summary>
        public Func<object, bool> Condition { get; set; }

        /// <summary>
        /// 文本匹配条件函数
        /// </summary>
        public Func<string, bool> TextMatchCondition { get; set; } = null;

        /// <summary>
        /// 空值处理函数
        /// </summary>
        public Func<bool> NullCondition { get; set; }

        /// <summary>
        /// 搜索文本
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// 字符串比较方式
        /// </summary>
        public StringComparison StringComparison { get; set; } = StringComparison.Ordinal;

        /// <summary>
        /// 依赖的列（用于多列判断条件）
        /// </summary>
        public List<string> DependentColumns { get; set; } = new List<string>();

        /// <summary>
        /// 前景色（Color.Empty表示不修改）
        /// </summary>
        public Color ForeColor { get; set; } = Color.Empty;

        /// <summary>
        /// 背景色（Color.Empty表示不修改）
        /// </summary>
        public Color BackColor { get; set; } = Color.Empty;

        /// <summary>
        /// 字体（null表示不修改）
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 是否应用整行样式
        /// </summary>
        public bool FullRow { get; set; }

        /// <summary>
        /// 是否允许多样式叠加
        /// </summary>
        public bool AllowStyleStacking { get; set; }

        public DgvConditionalFormattingConfig()
        {
            // 安全初始化所有委托
            RowCondition = null;
            Condition = null;
            TextMatchCondition = null;
            NullCondition = () => false;
        }
    }

    /// <summary>
    /// 条件样式配置
    /// </summary>
    [Serializable]
    public class DgvConditionalConfig
    {
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; } = 0;

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 依赖的列（用于多列判断条件）
        /// </summary>
        public List<string> DependentColumns { get; set; } = new List<string>();

        /// <summary>
        /// 列数据类型
        /// </summary>
        public Type ColumnType { get; set; }

        /// <summary>
        /// 规则类型
        /// </summary>
        public RuleType RuleType { get; set; }

        /// <summary>
        /// 逻辑计算类型
        /// </summary>
        public LogicalType LogicalType { get; set; }

        /// <summary>
        /// 值1
        /// </summary>
        public string Value1 { get; set; }

        /// <summary>
        /// 值2
        /// </summary>
        public string Value2 { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        public Font Font { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// 整行应用
        /// </summary>
        public bool FullRow { get; set; } = false;

        /// <summary>
        /// 是否允许多样式叠加
        /// </summary>
        public bool AllowStyleStacking { get; set; } = false;
    }

    /// <summary>
    /// 规则类型枚举
    /// </summary>
    public enum RuleType
    {
        /// <summary>
        /// 检查当前记录值
        /// </summary>
        CheckCurrentRecordValue = 0,

        /// <summary>
        /// 比较其他记录
        /// </summary>
        CompareOtherRecords = 1,
    }

    /// <summary>
    /// 逻辑类型枚举
    /// </summary>
    public enum LogicalType
    {
        /// <summary>
        /// 介于两个值之间
        /// </summary>
        Between = 0,

        /// <summary>
        /// 不介于两个值之间
        /// </summary>
        NotBetween = 1,

        /// <summary>
        /// 包含某个值
        /// </summary>
        Contains = 2,

        /// <summary>
        /// 不包含某个值
        /// </summary>
        DoesNotContain = 3,

        /// <summary>
        /// 等于某个值
        /// </summary>
        Equals = 4,

        /// <summary>
        /// 不等于某个值
        /// </summary>
        NotEquals = 5,

        /// <summary>
        /// 大于某个值
        /// </summary>
        GreaterThan = 6,

        /// <summary>
        /// 小于某个值
        /// </summary>
        LessThan = 7,

        /// <summary>
        /// 大于或等于某个值
        /// </summary>
        GreaterThanOrEquals = 8,

        /// <summary>
        /// 小于或等于某个值
        /// </summary>
        LessThanOrEquals = 9,
    }
}