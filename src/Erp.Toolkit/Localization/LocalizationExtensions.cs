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

using System.Windows.Forms;

namespace Erp.Toolkit.Localization
{
    /// <summary>
    /// 本地化扩展方法
    /// </summary>
    public static class LocalizationExtensions
    {
        /// <summary>
        /// 应用本地化到控件及其子控件
        /// </summary>
        public static void ApplyLocalization(this Control control, bool recursive = true)
        {
            var localizer = Localizer.Instance;

            // 立即应用一次本地化
            ApplyToControl(control, localizer, recursive);

            // 订阅语言变化事件
            localizer.CultureChanged += (s, e) => ApplyToControl(control, localizer, recursive);
        }

        /// <summary>
        /// 应用本地化到 ContextMenuStrip
        /// </summary>
        public static void ApplyLocalization(this ContextMenuStrip menu)
        {
            var localizer = Localizer.Instance;

            // 立即应用一次本地化
            ApplyToToolStripItems(menu.Items, localizer);

            // 订阅语言变化事件
            localizer.CultureChanged += (s, e) => ApplyToToolStripItems(menu.Items, localizer);
        }

        /// <summary>
        /// 应用本地化到 ToolStrip
        /// </summary>
        public static void ApplyLocalization(this ToolStrip toolStrip)
        {
            var localizer = Localizer.Instance;

            // 立即应用一次本地化
            ApplyToToolStripItems(toolStrip.Items, localizer);

            // 订阅语言变化事件
            localizer.CultureChanged += (s, e) => ApplyToToolStripItems(toolStrip.Items, localizer);
        }

        /// <summary>
        /// 应用本地化到控件
        /// </summary>
        private static void ApplyToControl(Control control, Localizer localizer, bool recursive)
        {
            // 处理控件文本
            if (control.Tag is string resourceKey && !string.IsNullOrEmpty(resourceKey))
            {
                control.Text = localizer[resourceKey];
            }

            // 特殊处理常见控件类型
            ApplyControlSpecificLocalization(control, localizer);

            // 处理上下文菜单 ContextMenuStrip
            if (control.ContextMenuStrip != null)
            {
                ApplyToToolStripItems(control.ContextMenuStrip.Items, localizer);

                // 确保 ContextMenuStrip 也订阅文化变更事件
                var menu = control.ContextMenuStrip;
                localizer.CultureChanged += (s, e) => ApplyToToolStripItems(menu.Items, localizer);
            }

            // 递归处理子控件
            if (recursive)
            {
                foreach (Control child in control.Controls)
                {
                    ApplyToControl(child, localizer, recursive);
                }
            }
        }

        /// <summary>
        /// 应用控件特定的本地化处理
        /// </summary>
        private static void ApplyControlSpecificLocalization(Control control, Localizer localizer)
        {
            switch (control)
            {
                case Button button when button.Tag is string buttonKey:
                    button.Text = localizer[buttonKey];
                    break;

                case Label label when label.Tag is string labelKey:
                    label.Text = localizer[labelKey];
                    break;

                case ToolStrip toolStrip:
                    ApplyToToolStripItems(toolStrip.Items, localizer);
                    break;

                case DataGridView dataGridView:
                    // 处理 DataGridView 列标题
                    ApplyToDataGridView(dataGridView, localizer);
                    break;
            }
        }

        /// <summary>
        /// 应用本地化到 DataGridView 列标题
        /// </summary>
        private static void ApplyToDataGridView(DataGridView dataGridView, Localizer localizer)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.HeaderText != null && localizer.HasKey(column.HeaderText))
                {
                    column.HeaderText = localizer[column.HeaderText];
                }

                if (column.Tag is string columnKey)
                {
                    column.HeaderText = localizer[columnKey];
                }
            }
        }

        /// <summary>
        /// 应用本地化到 ToolStrip 项目集合
        /// </summary>
        private static void ApplyToToolStripItems(ToolStripItemCollection items, Localizer localizer)
        {
            foreach (ToolStripItem item in items)
            {
                if (item.Tag is string resourceKey && !string.IsNullOrEmpty(resourceKey))
                {
                    item.Text = localizer[resourceKey];

                    // 工具提示文本可以使用带后缀的键名
                    if (localizer.HasKey(resourceKey + "_ToolTip"))
                    {
                        item.ToolTipText = localizer[resourceKey + "_ToolTip"];
                    }
                }

                // 递归处理下拉菜单项
                if (item is ToolStripDropDownItem dropDownItem && dropDownItem.HasDropDownItems)
                {
                    ApplyToToolStripItems(dropDownItem.DropDownItems, localizer);
                }
            }
        }

        /// <summary>
        /// 设置控件本地化键
        /// </summary>
        public static T SetLocalizationKey<T>(this T control, string resourceKey) where T : Control
        {
            control.Tag = resourceKey;
            return control;
        }

        /// <summary>
        /// 设置工具条项目本地化键
        /// </summary>
        public static ToolStripItem SetLocalizationKey(this ToolStripItem item, string resourceKey)
        {
            item.Tag = resourceKey;
            return item;
        }

        /// <summary>
        /// 设置 DataGridView 列本地化键
        /// </summary>
        public static DataGridViewColumn SetLocalizationKey(this DataGridViewColumn column, string resourceKey)
        {
            column.Tag = resourceKey;
            return column;
        }
    }
}