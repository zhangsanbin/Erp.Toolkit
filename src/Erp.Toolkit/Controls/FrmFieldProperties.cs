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

using Erp.Toolkit.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 字段属性设置窗体
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    public partial class FrmFieldProperties : Form
    {
        public List<DgvColumnInfoConfig> ColumnInfos;
        private ComboBox comboBox = new ComboBox();
        private readonly Localization.Localizer localizer = Localization.Localizer.Instance;

        /// <summary>
        /// 关闭时传参 ColumnInfos，委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="columnInfos"></param>
        public delegate void ClosedHandler(object sender, FormClosedEventArgs e, List<DgvColumnInfoConfig> columnInfos);

        /// <summary>
        /// 关闭时传参，事件
        /// </summary>
        public event ClosedHandler ClosedEvent;

        public FrmFieldProperties()
        {
            InitializeComponent();

            // 初始化本地化资源
            InitializeLocalization();
        }

        public FrmFieldProperties(List<DgvColumnInfoConfig> columnInfos)
        {
            InitializeComponent();

            dataGridView.DataSource = columnInfos;

            ColumnInfos = columnInfos;

            // 初始化本地化资源
            InitializeLocalization();
        }

        /// <summary>
        /// 初始化本地化资源
        /// </summary>
        private void InitializeLocalization()
        {
            // 窗体标题
            this.Text = localizer.GetString("FieldPropertyManager");

            // 应用本地化到整个窗体
            this.ApplyLocalization();
        }

        /// <summary>
        /// 加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FieldProperties_Load(object sender, EventArgs e)
        {
            // 系统样式
            ThemeStyle _themeStyle = ThemeStyle.BlueTheme;

            // 样式颜色
            DgvThemeColors _themeColors;

            // 初始化样式
            _themeColors = DgvThemeColors.GetColorsForTheme(_themeStyle);

            // 设置背景色
            this.dataGridView.BackgroundColor = _themeColors.BackgroundColor;
            this.dataGridView.DefaultCellStyle.BackColor = _themeColors.BackColor;

            // 设置奇偶行背景色
            this.dataGridView.AlternatingRowsDefaultCellStyle.BackColor = _themeColors.AlternatingRowsBackColor;

            // 设置网格边框颜色
            this.dataGridView.GridColor = _themeColors.GridColor;

            // 设置 DGV SelectionBackColor
            this.dataGridView.DefaultCellStyle.SelectionBackColor = _themeColors.SelectionBackColor;
            this.dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = _themeColors.SelectionBackColor;
            this.dataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = _themeColors.SelectionBackColor;

            // 设置字体颜色
            this.dataGridView.DefaultCellStyle.ForeColor = _themeColors.FontColor;
            this.dataGridView.DefaultCellStyle.SelectionForeColor = _themeColors.SelectionFontColor;
            this.dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = _themeColors.CellHeaderFontColor;
            this.dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = _themeColors.SelectionFontColor;
            this.dataGridView.RowHeadersDefaultCellStyle.ForeColor = _themeColors.FontColor;
            this.dataGridView.RowHeadersDefaultCellStyle.SelectionForeColor = _themeColors.SelectionFontColor;

            // 文本对齐方式
            this.dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // 绑定事件
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FieldProperties_FormClosed);
            this.dataGridView.CellBeginEdit += new DataGridViewCellCancelEventHandler(DataGridView_CellBeginEdit);
            this.dataGridView.CellEndEdit += new DataGridViewCellEventHandler(DataGridView_CellEndEdit);
            this.dataGridView.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(DataGridView_EditingControlShowing);
            this.comboBox.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);
            this.comboBox.LostFocus += new EventHandler(ComboBox_LostFocus);
            this.comboBox.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
        }

        private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // 在单元格开始编辑之前，不需要执行特定操作
            // 验证是否可以编辑等
        }

        private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 当单元格编辑结束时，DataGridView会自动更新绑定的数据源
            // 验证输入值等
        }

        /// <summary>
        /// 编辑单元格时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var currentCell = dataGridView.CurrentCell;
            if (currentCell == null || !IsRelevantColumn(currentCell.ColumnIndex))
            {
                return; // 提前退出
            }

            // 获取位置和大小
            var cellRect = dataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, false);
            var cellLocation = new Point(cellRect.X, cellRect.Y);

            // 设置外观
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.BackColor = dataGridView.DefaultCellStyle.BackColor;
            comboBox.ForeColor = dataGridView.DefaultCellStyle.ForeColor;
            comboBox.Location = cellLocation;
            comboBox.Size = cellRect.Size;
            comboBox.Width = cellRect.Size.Width - 2;
            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.ItemHeight = cellRect.Size.Height - 8;

            // 根据列索引设置数据源
            SetComboBoxDataSource(comboBox, currentCell.ColumnIndex);
            comboBox.Text = dataGridView.Rows[currentCell.RowIndex].Cells[currentCell.ColumnIndex].Value.ToString();

            // 将组合框添加到窗体控件集合中并显示
            this.Controls.Add(comboBox);
            comboBox.BringToFront();
            comboBox.Visible = true;
            comboBox.Focus();
        }

        /// <summary>
        /// 判断是否关系的列
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private bool IsRelevantColumn(int columnIndex)
        {
            return columnIndex == dataGridView.Columns["AutoSizeMode"].Index
                || columnIndex == dataGridView.Columns["DefaultCellStyle"].Index
                || columnIndex == dataGridView.Columns["SortType"].Index
                || columnIndex == dataGridView.Columns["SortingIndex"].Index;
        }

        /// <summary>
        /// 动态分配数据源
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="columnIndex"></param>
        private void SetComboBoxDataSource(ComboBox comboBox, int columnIndex)
        {
            switch (columnIndex)
            {
                case var index when index == dataGridView.Columns["AutoSizeMode"].Index:
                    // Bug.由于无法手动设定下拉框的文本，被停用以下代码
                    // comboBox.DataSource = GetAutoSizeModeComboBoxItems();

                    AddItemsFromList(GetAutoSizeModeComboBoxItems());
                    break;

                case var index when index == dataGridView.Columns["DefaultCellStyle"].Index:
                    // Bug.由于无法手动设定下拉框的文本，被停用以下代码
                    // comboBox.DataSource = GetDefaultCellStyleComboBoxItems();

                    AddItemsFromList(GetDefaultCellStyleComboBoxItems());
                    break;

                case var index when index == dataGridView.Columns["SortType"].Index:
                    // Bug.由于无法手动设定下拉框的文本，被停用以下代码
                    // comboBox.DataSource = GetSortTypeComboBoxItems();

                    AddItemsFromList(GetSortTypeComboBoxItems());
                    break;

                case var index when index == dataGridView.Columns["SortingIndex"].Index:
                    // Bug.由于无法手动设定下拉框的文本，被停用以下代码
                    // comboBox.DataSource = GetSortingIndexComboBoxItems();

                    AddItemsFromList(GetSortingIndexComboBoxItems());
                    break;
            }
        }

        /// <summary>
        /// 手动循环将字符添加至下拉框
        /// </summary>
        /// <param name="items"></param>
        private void AddItemsFromList(List<string> items)
        {
            comboBox.Items.Clear();
            foreach (var item in items)
            {
                comboBox.Items.Add(item);
            }
        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_LostFocus(object sender, EventArgs e)
        {
            comboBox.Visible = false;
        }

        /// <summary>
        /// 变更选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ColumnInfos = (List<DgvColumnInfoConfig>)dataGridView.DataSource;

            // 将选择的值，更新到数据源
            dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells[dataGridView.CurrentCell.ColumnIndex].Value = comboBox.Text;

            // 修改 SortType 排序类型列时，验证逻辑
            if (dataGridView.Columns[dataGridView.CurrentCell.ColumnIndex].Name == "SortType")
            {
                // 自动更新 SortingIndex 的值
                if (comboBox.Text != "None" && dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["SortingIndex"].Value.ToString() == "0")
                {
                    // 找到最大 SortingIndex
                    int maxSortingIndex = ColumnInfos.Where(p => p.SortType != SortTypeDirection.None).Max(p => p.SortingIndex);

                    // 将 SortingIndex + 1 的结果，更新到数据源
                    dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["SortingIndex"].Value = maxSortingIndex + 1;
                }
                else if (comboBox.Text == "None")
                {
                    // 取消排序后，重置 SortingIndex
                    dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["SortingIndex"].Value = 0;

                    // 查询所有已经设置排序的对象
                    var infos = ColumnInfos
                        .Where(p => p.SortType != SortTypeDirection.None)
                        .OrderBy(p => p.SortingIndex)
                        .ToList();

                    // 循环并迭代判断，重新计算 SortingIndex
                    for (int i = 0; i <= infos.Count - 1; i++)
                    {
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            // 检查当前行是否不是新行
                            if (!row.IsNewRow)
                            {
                                object cellValue = row.Cells["Name"].Value;
                                string valueAsString = cellValue?.ToString();
                                if (!string.IsNullOrEmpty(valueAsString) && valueAsString == infos[i].Name)
                                {
                                    row.Cells["SortingIndex"].Value = i + 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // 修改 SortingIndex 排序顺序时，验证逻辑
            if (dataGridView.Columns[dataGridView.CurrentCell.ColumnIndex].Name == "SortingIndex")
            {
                // 查询 SortingIndex 大于目标值的对象
                var infos = ColumnInfos
                    .Where(p => p.SortingIndex >= int.Parse(comboBox.Text)
                    && p.Name != dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells["Name"].Value.ToString())
                    .ToList();

                // 找到修改节点向前的最大 SortingIndex
                int maxSortingIndex = ColumnInfos
                    .Where(p => p.SortType != SortTypeDirection.None &&
                    p.SortingIndex <= int.Parse(comboBox.Text))
                    .Max(p => p.SortingIndex);

                // 循环并迭代判断，重新计算 SortingIndex
                for (int i = 0; i <= infos.Count - 1; i++)
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        // 检查当前行是否不是新行
                        if (!row.IsNewRow)
                        {
                            object cellValue = row.Cells["Name"].Value;
                            string valueAsString = cellValue?.ToString();
                            if (!string.IsNullOrEmpty(valueAsString) && valueAsString == infos[i].Name)
                            {
                                row.Cells["SortingIndex"].Value = maxSortingIndex + i + 1;
                                break;
                            }
                        }
                    }
                }
            }

            // 完成逻辑后，隐藏选择框
            comboBox.Visible = false;
        }

        /// <summary>
        /// 绘制下拉列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // 如果索引小于 0，意味着正在绘制
            if (e.Index < 0)
                return;

            // 获取 ComboBox 控件
            ComboBox comboBox = (ComboBox)sender;

            // 使用 KnownColor 替代 SystemColors 以避免冲突
            Color highlightColor, highlightTextColor;

#if NETFRAMEWORK
            // .NET Framework 使用 SystemColors
            highlightColor = System.Drawing.SystemColors.Highlight;
            highlightTextColor = System.Drawing.SystemColors.HighlightText;
#else
            // .NET Core 使用 KnownColor 值
            highlightColor = Color.FromKnownColor(KnownColor.Highlight);
            highlightTextColor = Color.FromKnownColor(KnownColor.HighlightText);
#endif

            SolidBrush backgroundBrush = new SolidBrush(e.State.HasFlag(DrawItemState.Selected) ?
                highlightColor : comboBox.BackColor);
            SolidBrush textBrush = new SolidBrush(e.State.HasFlag(DrawItemState.Selected) ?
                highlightTextColor : comboBox.ForeColor);

            // 使用 Graphics 对象绘制项的背景
            e.Graphics.FillRectangle(backgroundBrush, e.Bounds);

            // 创建StringFormat对象来设置文本对齐方式
            StringFormat stringFormat = new StringFormat
            {
                //限制绘制的文本行数
                FormatFlags = StringFormatFlags.LineLimit,

                //文本超出可用空间时，末尾添加省略号
                Trimming = StringTrimming.EllipsisCharacter,

                // 水平居中
                Alignment = StringAlignment.Center,

                // 垂直居中
                LineAlignment = StringAlignment.Center
            };

            // 使用 Graphics 对象绘制项的文本
            e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds, stringFormat);

            // 释放 SolidBrush 对象
            backgroundBrush.Dispose();
            textBrush.Dispose();
        }

        private void FieldProperties_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 保存最后的修改数据
            dataGridView.EndEdit();

            // 关闭并回传保存
            ClosedEvent?.Invoke(this, e, ColumnInfos);
        }

        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // 设置列的宽度
            dataGridView.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["HeaderText"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Name"].ReadOnly = true;
            dataGridView.Columns["HeaderText"].Width = 140;
            dataGridView.Columns["IsVisible"].Width = 60;
            dataGridView.Columns["IsPrintable"].Width = 60;
            dataGridView.Columns["RowWidth"].Width = 80;
            dataGridView.Columns["PrintWidth"].Width = 80;
            dataGridView.Columns["DisplayIndex"].Width = 80;
            dataGridView.Columns["AutoSizeMode"].Width = 120;
            dataGridView.Columns["DefaultCellStyle"].Width = 120;
            dataGridView.Columns["ReadOnly"].Width = 60;
            dataGridView.Columns["SortType"].Width = 100;
            dataGridView.Columns["SortingIndex"].Width = 80;

            // 设置表头显示
            dataGridView.Columns["Name"].HeaderText = localizer.GetString("Field");
            dataGridView.Columns["HeaderText"].HeaderText = localizer.GetString("DisplayName");
            dataGridView.Columns["IsVisible"].HeaderText = localizer.GetString("Display");
            dataGridView.Columns["IsPrintable"].HeaderText = localizer.GetString("Print");
            dataGridView.Columns["RowWidth"].HeaderText = localizer.GetString("Width");
            dataGridView.Columns["PrintWidth"].HeaderText = localizer.GetString("PrintWidth");
            dataGridView.Columns["DisplayIndex"].HeaderText = localizer.GetString("PositionIndex");
            dataGridView.Columns["AutoSizeMode"].HeaderText = localizer.GetString("WidthMode");
            dataGridView.Columns["DefaultCellStyle"].HeaderText = localizer.GetString("AlignmentStyle");
            dataGridView.Columns["ReadOnly"].HeaderText = localizer.GetString("ReadOnly");
            dataGridView.Columns["SortType"].HeaderText = localizer.GetString("SortType");
            dataGridView.Columns["SortingIndex"].HeaderText = localizer.GetString("SortOrder");

            // 设置内容为居中对齐
            dataGridView.Columns["RowWidth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["PrintWidth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["DisplayIndex"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["AutoSizeMode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["DefaultCellStyle"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["SortType"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns["SortingIndex"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 隐藏不需要的参数
            dataGridView.Columns["Name"].Visible = false;
            dataGridView.Columns["PrintWidth"].Visible = false;
        }

        /// <summary>
        /// 填充宽度模式，下拉数据的方法
        /// </summary>
        /// <returns></returns>
        private List<string> GetAutoSizeModeComboBoxItems()
        {
            List<string> items = new List<string>
            {
                "NotSet",
                "None",
                "AllCells",
                "AllCellsExceptHeader",
                "DisplayedCells",
                "DisplayedCellsExceptHeader",
                "ColumnHeader",
                "Fill"
            };
            return items;
        }

        /// <summary>
        /// 填充对齐样式，下拉数据的方法
        /// </summary>
        /// <returns></returns>
        private List<string> GetDefaultCellStyleComboBoxItems()
        {
            List<string> items = new List<string>
            {
                "NotSet",
                "TopLeft",
                "TopCenter",
                "TopRight",
                "MiddleLeft",
                "MiddleCenter",
                "MiddleRight",
                "BottomLeft",
                "BottomCenter",
                "BottomRight"
            };
            return items;
        }

        /// <summary>
        /// 填充排序类型，下拉数据的方法
        /// </summary>
        /// <returns></returns>
        private List<string> GetSortTypeComboBoxItems()
        {
            List<string> items = new List<string>
            {
                "Asc",
                "Desc",
                "None"
            };
            return items;
        }

        /// <summary>
        /// 填充排序顺序，下拉数据的方法
        /// </summary>
        /// <returns></returns>
        private List<string> GetSortingIndexComboBoxItems()
        {
            // ColumnInfos = (List<DgvColumnInfoConfig>)dataGridView.DataSource;

            List<string> items = new List<string>();

            // 查询所有设定排序的对象
            var infos = ColumnInfos
                .Where(p => p.SortType != SortTypeDirection.None)
                .OrderBy(p => p.SortingIndex)
                .ToList();

            for (int i = 0; i <= infos.Count - 1; i++)
            {
                items.Add((i + 1).ToString());
            }

            return items;
        }

        /// <summary>
        /// 填充数据结构
        /// </summary>
        private class MyData
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}