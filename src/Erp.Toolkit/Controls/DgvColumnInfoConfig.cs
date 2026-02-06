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
using System.Linq;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    // 添加 Serializable 特性，标记为可序列化
    [Serializable]
    public enum SortTypeDirection
    {
        None = 0,
        Asc = 1,
        Desc = 2
    }

    // 添加 Serializable 特性，标记为可序列化
    [Serializable]
    public class DgvColumnInfoConfig : IEquatable<DgvColumnInfoConfig>
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题文本
        /// </summary>
        public string HeaderText { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// 是否支持打印
        /// </summary>
        public bool IsPrintable { get; set; } = true;

        /// <summary>
        /// 行宽度
        /// </summary>
        public int RowWidth { get; set; } = 100;

        /// <summary>
        /// 打印时的布局宽度
        /// </summary>
        public int PrintWidth { get; set; } = 100;

        /// <summary>
        /// 显示的位置索引
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly { get; set; } = true;

        /// <summary>
        /// 列的填充模式
        /// </summary>
        public DataGridViewAutoSizeColumnMode AutoSizeMode { get; set; } = DataGridViewAutoSizeColumnMode.NotSet;

        /// <summary>
        /// 默认单元格对齐样式
        /// </summary>
        public DataGridViewContentAlignment DefaultCellStyle { get; set; } = DataGridViewContentAlignment.NotSet;

        public SortTypeDirection SortType { get; set; } = SortTypeDirection.None;

        public int SortingIndex { get; set; } = 1;

        /// <summary>
        /// 自定义 DataGridView 美好组件，数据列的字段属性对象（构造函数）
        /// </summary>
        /// <param name="name">列名称</param>
        /// <param name="headerText">标题文本</param>
        /// <param name="isVisible">是否显示</param>
        /// <param name="isPrintable">是否支持打印</param>
        /// <param name="rowWidth">行宽度</param>
        /// <param name="printWidth">打印时的布局宽度</param>
        /// <param name="displayIndex">显示的位置索引</param>
        /// <param name="autoSizeMode">列的填充模式</param>
        /// <param name="defaultCellStyle">默认单元格对齐样式</param>
        /// <param name="readOnly">只读</param>
        /// <param name="sortType">排序类型</param>
        /// <param name="sortingIndex">排序次序</param>
        public DgvColumnInfoConfig(string name, string headerText, bool isVisible, bool isPrintable,
            int rowWidth, int printWidth, int displayIndex,
            DataGridViewAutoSizeColumnMode autoSizeMode, DataGridViewContentAlignment defaultCellStyle,
            bool readOnly, SortTypeDirection sortType, int sortingIndex)
        {
            Name = name;
            HeaderText = headerText;
            IsVisible = isVisible;
            IsPrintable = isPrintable;
            RowWidth = rowWidth;
            PrintWidth = printWidth;
            DisplayIndex = displayIndex;
            AutoSizeMode = autoSizeMode;
            DefaultCellStyle = defaultCellStyle;
            ReadOnly = readOnly;
            SortType = sortType;
            SortingIndex = sortingIndex;
        }

        /// <summary>
        /// 空构造函数
        /// </summary>
        public DgvColumnInfoConfig()
        {
        }

        public bool Equals(DgvColumnInfoConfig other)
        {
            if (other == null) return false;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is DgvColumnInfoConfig other)
            {
                return Equals(other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class MergerColumnInfoConfigList
    {
        public List<DgvColumnInfoConfig> MergeLists(List<DgvColumnInfoConfig> list1, List<DgvColumnInfoConfig> list2)
        {
            // 使用 HashSet 存储合并后的结果，它会自动处理重复项
            var merged = new HashSet<DgvColumnInfoConfig>(new DgvColumnInfoConfigEqualityComparer());

            // 添加第一个列表的所有元素到HashSet中
            merged.UnionWith(list1);

            // 遍历第二个列表，并将元素添加到 HashSe t中（如果它们不重复）
            foreach (var item in list2)
            {
                merged.Add(item); // HashSet 会自动处理重复项，不会添加重复的
            }

            // 将 HashSet 中的元素转换回列表并返回
            return merged.ToList();
        }

        // 自定义的 IEqualityComparer，用于HashSet
        private class DgvColumnInfoConfigEqualityComparer : IEqualityComparer<DgvColumnInfoConfig>
        {
            public bool Equals(DgvColumnInfoConfig x, DgvColumnInfoConfig y)
            {
                if (x == null && y == null) return true;
                if (x == null || y == null) return false;
                return x.Name == y.Name;
            }

            public int GetHashCode(DgvColumnInfoConfig obj)
            {
                if (obj == null) throw new ArgumentNullException(nameof(obj));
                return obj.Name.GetHashCode();
            }
        }

        // 使用方法
        // MergerColumnInfoConfigList merger = new MergerColumnInfoConfigList();
        // List<dgvColumnInfoConfig> mergedColumnInfos = merger.MergeLists(columnInfos1, columnInfos2);
    }
}