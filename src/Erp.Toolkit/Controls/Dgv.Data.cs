/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-07-13           Andy        Split, restructure
 * 2025-11-08           Andy        SortDataGridView private to public, allow external calls
 * 2025-12-01           Andy        Fixed an issue where multilingual localization required display; now compatible with .NET Framework 4.6.2 and above.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    public partial class Dgv
    {
        #region 主数据

        /// <summary>
        /// 填充数据（设置数据源）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">序列化数组对象集合</param>
        /// <param name="guid">全球唯一标识，用于保持和恢复 UI 布局配置</param>
        public void FillList<T>(List<T> items, string guid)
        {
            // 标记加载开始
            _isLoadingData = true;

            try
            {
                // 暂停布局逻辑
                dataGridView.SuspendLayout();

                // 删除事件
                this.dataGridView.ColumnWidthChanged -= new
                    DataGridViewColumnEventHandler(DataGridView_ColumnWidthChanged);
                this.dataGridView.ColumnDisplayIndexChanged -=
                    new DataGridViewColumnEventHandler(DataGridView_ColumnDisplayIndexChanged);

                if (!(items is IList))
                {
                    return;
                }

                // 封装数据源
                _bindingSource = new BindingSource();

                // 对象转换
                DataTable table = ConvertToDataTable(items);

                // 创建一个 DataView 并设置排序条件
                _dataView = new DataView(table);

                // 将排序后的，数据源绑定
                _bindingSource.DataSource = _dataView;

                // 保持，筛选器的状态，完成载入数据或刷新后，用于恢复筛选器
                string oldFilter = Filter;

                // 设置 DGV 数据源
                dataGridView.DataSource = _bindingSource;

                // 设置导航条数据源
                bindingNavigator.BindingSource = _bindingSource;

                // 设置全局 ID 用于本地化存储配置文件
                Guid = guid;

                // 根据事件绑定，更新UI按钮的可用状态
                UpdateButtonState();

                // 先清空，筛选器
                Filter = null;

                // 恢复，筛选器，检查并应用 Filter 筛选管理器
                Filter = oldFilter;
            }
            finally
            {
                // 注册事件
                this.dataGridView.ColumnWidthChanged +=
                    new DataGridViewColumnEventHandler(DataGridView_ColumnWidthChanged);
                this.dataGridView.ColumnDisplayIndexChanged +=
                    new DataGridViewColumnEventHandler(DataGridView_ColumnDisplayIndexChanged);

                // 恢复布局
                dataGridView.ResumeLayout();
            }
        }

        /// <summary>
        /// 填充数据（设置数据源）
        /// </summary>
        /// <param name="table">DataTable 对象集合</param>
        /// <param name="guid">全球唯一标识，用于保持和恢复 UI 布局配置</param>
        public void FillTable(DataTable table, string guid)
        {
            // 标记加载开始
            _isLoadingData = true;

            try
            {
                // 暂停布局逻辑
                dataGridView.SuspendLayout();

                // 删除事件
                this.dataGridView.ColumnWidthChanged -=
                    new DataGridViewColumnEventHandler(DataGridView_ColumnWidthChanged);
                this.dataGridView.ColumnDisplayIndexChanged -=
                    new DataGridViewColumnEventHandler(DataGridView_ColumnDisplayIndexChanged);

                if (!(table is DataTable))
                {
                    return;
                }

                // 封装数据源
                _bindingSource = new BindingSource();

                // 创建一个 DataView 并设置排序条件
                _dataView = new DataView(table);

                // 将排序后的，数据源绑定
                _bindingSource.DataSource = _dataView;

                // 保持，筛选器的状态，完成载入数据或刷新后，用于恢复筛选器
                string oldFilter = Filter;

                // 设置 DGV 数据源
                dataGridView.DataSource = _bindingSource;

                // 设置导航条数据源
                bindingNavigator.BindingSource = _bindingSource;

                // 设置全局 ID 用于本地化存储配置文件
                Guid = guid;

                // 根据事件绑定，更新UI按钮的可用状态
                UpdateButtonState();

                // 先清空，筛选器
                Filter = null;

                // 恢复，筛选器，检查并应用 Filter 筛选管理器
                Filter = oldFilter;
            }
            finally
            {
                // 注册事件
                this.dataGridView.ColumnWidthChanged +=
                    new DataGridViewColumnEventHandler(DataGridView_ColumnWidthChanged);
                this.dataGridView.ColumnDisplayIndexChanged +=
                    new DataGridViewColumnEventHandler(DataGridView_ColumnDisplayIndexChanged);

                // 恢复布局
                dataGridView.ResumeLayout();
            }
        }

        /// <summary>
        /// 填充数据（设置数据源）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">序列化数组对象集合</param>
        /// <param name="owner">调用者父类，用于实现刷新接口</param>
        public void FillList<T>(List<T> items, IReLoadListUI owner)
        {
            // 尝试通过所有者转换为 Form 并取得 Name 值设定为 guid
            string formName = GetFormName(owner);

            // 正确获取到 From 的 Name
            if (!string.IsNullOrEmpty(formName))
            {
                // 调用，基本数据填充
                FillList(items, formName);
            }

            // 设置，调用者父类
            SetOwner = owner;
        }

        /// <summary>
        /// 填充数据（设置数据源）
        /// </summary>
        /// <param name="table">DataTable 对象集合</param>
        /// <param name="owner">调用者父类，用于实现刷新接口</param>
        public void FillTable(DataTable table, IReLoadListUI owner)
        {
            // 尝试通过所有者转换为 Form 并取得 Name 值设定为 guid
            string formName = GetFormName(owner);

            // 正确获取到 From 的 Name
            if (!string.IsNullOrEmpty(formName))
            {
                // 调用，基本数据填充
                FillTable(table, formName);
            }

            // 设置，调用者父类
            SetOwner = owner;
        }

        /// <summary>
        /// 获取调用所有者/父窗体的类名，用于标志唯一 guid 实现 cfg 的本地持久化
        /// </summary>
        /// <param name="owner">接口，调用者或拥有者</param>
        /// <returns></returns>
        private string GetFormName(IReLoadListUI owner)
        {
            // 检查 base.ParentForm，如果它不为 null，则返回其 Name 属性
            // 否则，检查owner是否可以转换为Form类型，并返回其Name属性
            // 如果都不满足，则返回 null
            return base.ParentForm != null ? base.ParentForm.Name
                 : (owner is Form form) ? form.Name
                 : null;
        }

        /// <summary>
        /// 获取调用所有者/父窗体的标题，用于打印或导出时显示
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        private string GetFormText(IReLoadListUI owner)
        {
            // 检查 base.ParentForm，如果它不为 null，则返回其 Text 属性
            // 否则，检查owner是否可以转换为Form类型，并返回其 Text 属性
            // 如果都不满足，则返回 null
            return base.ParentForm != null ? base.ParentForm.Text
                 : (owner is Form form) ? form.Text
                 : null;
        }

        /// <summary>
        /// 根据 Order By 字符，对数据进行排序
        /// </summary>
        /// <param name="OrderByText">排序字符串</param>
        public void SortDataGridView(string OrderByText)
        {
            if (_dataView == null || string.IsNullOrEmpty(OrderByText))
                return;

            try
            {
                // 尝试设置排序
                _dataView.Sort = OrderByText;
            }
            catch (IndexOutOfRangeException ex)
            {
                System.Diagnostics.Debug.WriteLine($"排序错误(列不存在): {ex.Message}");
                return;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"排序错误: {ex.Message}");
                return;
            }

            // 重新绑定数据源
            _bindingSource.DataSource = _dataView;
            dataGridView.DataSource = _bindingSource;
            bindingNavigator.BindingSource = _bindingSource;
        }

        /// <summary>
        /// dgv绑定数据源后发生，处理预设表头显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            if (EnglishToChineseHeaders != null)
            {
                // 英文列名和对应的中文列名，字典
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    // 检查英文列名是否在字典中
                    if (EnglishToChineseHeaders.ContainsKey(column.Name))
                    {
                        // 如果在，则替换为对应的中文列名
                        column.HeaderText = EnglishToChineseHeaders[column.Name];
                    }
                }
            }

            // 默认值，更新字段属性配置
            if (ColumnInfos == null)
            {
                List<DgvColumnInfoConfig> columnInfos = new List<DgvColumnInfoConfig>();
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    bool isPrintable = column.Visible;
                    int rowWidth = column.Width;
                    int printWidth = column.Width;
                    int displayIndex = column.DisplayIndex;
                    DataGridViewAutoSizeColumnMode cellAutoSizeMode = column.InheritedAutoSizeMode;
                    DataGridViewContentAlignment defaultCellStyle = column.DefaultCellStyle.Alignment;
                    SortTypeDirection sortType = SortTypeDirection.None;
                    int sortingIndex = 0;

                    // 默认，不可修改
                    column.ReadOnly = true;

                    // 创建并添加DataGridViewColumnInfo对象到列表中
                    columnInfos.Add(new DgvColumnInfoConfig(
                                    column.Name,
                                    column.HeaderText,
                                    column.Visible,
                                    isPrintable,
                                    rowWidth,
                                    printWidth,
                                    displayIndex,
                                    cellAutoSizeMode,
                                    defaultCellStyle,
                                    column.ReadOnly,
                                    sortType,
                                    sortingIndex));
                }
                // 初始化配置信息
                ColumnInfos = columnInfos;
            }

            // 更新字段属性配置
            //SetColumnInfo(ColumnInfos);// Bug. 在填充数据时，调用含Guid的重构时，此方法会被执行两次。

            // 提示性文本，显示筛选的条件
            toolStripLabel_tip.Text = null;

            // 设置工具按钮的状态
            toolStripButton_print.Enabled = true;
            toolStripButton_asc.Enabled = true;
            toolStripButton_desc.Enabled = true;
            toolStripButton_query.Enabled = true;
            toolStripButton_find1.Enabled = true;
            toolStripButton_char.Enabled = true;
            toolStripButton_conditionalStyles.Enabled = true;
            toolStripButton_CancelHideCol.Enabled = true;
            toolStripButton_find.Enabled = true;
            toolStripButton_ReLoadList.Enabled = true;

            // 判断 Id 是否存在
            if (!dataGridView.Columns.Contains(_primaryKey))
            {
                // 存在列
                if (dataGridView.Columns.Count > 0)
                {
                    // 不存在，默认第一列数据为主键
                    _primaryKey = dataGridView.Columns[0].Name;
                }
            }
        }

        /// <summary>
        /// 数据绑定完成后，处理进度条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // 处理进度条列
            HandleProgressColumns();

            // 如果是第一次数据绑定完成
            if (_isLoadingData)
            {
                // 使用计时器延迟重置加载标志，确保所有数据处理完成
                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 100;
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                    timer.Dispose();
                    _isLoadingData = false;
                };
                timer.Start();
            }
        }

        /// <summary>
        /// 处理进度条列
        /// </summary>
        private void HandleProgressColumns()
        {
            if (_progressColumnsNames == null || _progressColumnsNames.Count == 0)
                return;

            // 遍历所有需要替换为进度条的列名
            foreach (var columnName in _progressColumnsNames)
            {
                if (string.IsNullOrWhiteSpace(columnName))
                    continue;

                // 查找目标列
                var targetColumn = dataGridView.Columns
                    .Cast<DataGridViewColumn>()
                    .FirstOrDefault(col => col.DataPropertyName == columnName);

                if (targetColumn != null)
                {
                    // 替换为进度列
                    ReplaceWithProgressColumn(targetColumn);
                }
            }
        }

        /// <summary>
        /// 替换为进度列
        /// </summary>
        /// <param name="originalColumn"></param>
        private void ReplaceWithProgressColumn(DataGridViewColumn originalColumn)
        {
            // 创建进度列并复制属性
            var progressCol = new DgvProgressColumn()
            {
                SortMode = originalColumn.SortMode,
                ValueType = originalColumn.ValueType,
                DataPropertyName = originalColumn.DataPropertyName,
                HeaderText = originalColumn.HeaderText,
                Name = originalColumn.Name,
                Width = originalColumn.Width,
                Visible = originalColumn.Visible,
                DisplayIndex = originalColumn.DisplayIndex,
                ReadOnly = originalColumn.ReadOnly,
                //LowThreshold = 30,
                //MediumThreshold = 70,
                //LowColor = Color.Red,
                //MediumColor = Color.Orange,
                //HighColor = Color.Green,
            };

            // 替换原列
            int index = originalColumn.Index;
            dataGridView.Columns.Remove(originalColumn);
            dataGridView.Columns.Insert(index, progressCol);
        }

        /// <summary>
        /// 设置字段属性配置
        /// </summary>
        /// <param name="columnInfos"></param>
        public void SetColumnInfo(List<DgvColumnInfoConfig> columnInfos)
        {
            // 合并自定义字段属性配置项
            MergerColumnInfoConfigList merger = new MergerColumnInfoConfigList();
            List<DgvColumnInfoConfig> mergedColumnInfos = merger.MergeLists(columnInfos, _columnInfos);

            // 临时禁用显示索引变更事件
            _isSettingDisplayIndex = true;

            // 临时数据初始化
            int i = 0;
            List<int> printColumnsList = new List<int>();
            string orderByText = "";

            foreach (DgvColumnInfoConfig cInfo in mergedColumnInfos)
            {
                // 更新需要打印的列
                if (cInfo.IsPrintable)
                {
                    printColumnsList.Add(i);
                }

                // 表头设置
                if (dataGridView.Columns.Contains(cInfo.Name))
                {
                    dataGridView.Columns[cInfo.Name].ToolTipText = cInfo.Name;
                    dataGridView.Columns[cInfo.Name].HeaderText = cInfo.HeaderText;
                    dataGridView.Columns[cInfo.Name].Visible = cInfo.IsVisible;
                    dataGridView.Columns[cInfo.Name].AutoSizeMode = cInfo.AutoSizeMode;//要在设置Width之前
                    dataGridView.Columns[cInfo.Name].Width = cInfo.RowWidth;
                    if (cInfo.DisplayIndex < dataGridView.ColumnCount)
                        dataGridView.Columns[cInfo.Name].DisplayIndex = cInfo.DisplayIndex;
                    dataGridView.Columns[cInfo.Name].DefaultCellStyle.Alignment = cInfo.DefaultCellStyle;

                    // 保存，打印和导出Tag数据
                    dataGridView.Columns[cInfo.Name].Tag = new Dictionary<string, bool> { { "IsPrintable", cInfo.IsPrintable } };

                    // 设置，只读 Or 可修改
                    dataGridView.Columns[cInfo.Name].ReadOnly = cInfo.ReadOnly;
                }
                // 临时数据处理
                i++;
            }

            // LINQ 排序 SortingIndex 的值
            var sortedConfig = mergedColumnInfos
                .Where(p => p.SortType != SortTypeDirection.None)
                .OrderBy(c => c.SortingIndex).ToList();

            foreach (DgvColumnInfoConfig cInfo in sortedConfig)
            {
                // 拼接 OrderBy 排序字符串
                orderByText += cInfo.Name + " " + cInfo.SortType + ",";
            }

            // 处理 OrderBy 排序字符串，末尾的逗号文本
            if (orderByText.Length > 0)
            {
                // 通过索引去掉最后一个字符
                orderByText = orderByText.Remove(orderByText.Length - 1);

                // DEBUG OrderBy
                // Console.WriteLine("OrderBy Text:" + orderByText);

                // 执行 Order By 排序
                SortDataGridView(orderByText);
            }

            // 判断，主键列是否存在
            if (dataGridView.Columns.Contains(_primaryKey))
            {
                // 强制关闭，主键列的修改功能
                dataGridView.Columns[_primaryKey].ReadOnly = true;
            }

            // 将需要打印的索引列表，转换成 int[] 数组
            _printColumns = printColumnsList.ToArray();

            // 恢复显示索引变更事件
            _isSettingDisplayIndex = false;

            // 保持更新后的数据
            ColumnInfos = mergedColumnInfos;
        }

        #endregion 主数据

        #region 从数据

        /// <summary>
        /// 启用子从数据，并初始化
        /// </summary>
        public void SubviewsEnable()
        {
            // 只有在子视图尚未初始化时才进行初始化，防止重复初始化
            if (!_subview)
            {
                subview = new Dgv();
                Controls.Add(subview);
                subview.Location = new Point(-subview.Width, -subview.Height);
                subview.Visible = true;// 多国语言本地化需要显示，兼容.NET Framework4.6.2 及以上版本
                subview.BringToFront();
                subview.toolStrip1.Visible = false;
                subview.dataGridView.Location = new Point(0, -1);
                subview.dataGridView.Height += subview.toolStrip1.Height;
                subview.dataGridView.RowHeadersWidth = 30;
                this.dataGridView.RowHeadersWidth = 48;
                bindingNavigator.BringToFront();
                _subview = true;
                if (Guid != null && subview.Guid == null)
                {
                    subview.Guid = Guid + ".SUBVIEW";
                }
            }
        }

        /// <summary>
        /// 填充子视图数据（设置数据源）并自动初始化子视图相关基础设置
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="items">序列化数组对象集合</param>
        public void FillSubviewWithList<T>(List<T> items)
        {
            // 删除事件
            if (subview != null)
            {
                this.subview.dataGridView.ColumnWidthChanged -=
                    new DataGridViewColumnEventHandler(Subview_dataGridView_ColumnWidthChanged);
            }

            if (!(items is IList))
            {
                return;
            }

            // 启用并初始化子视图
            SubviewsEnable();

            // 封装数据源
            _subviewBindingSource = new BindingSource();

            // 对象转换
            DataTable table = ConvertToDataTable(items);

            // 数据源绑定
            _subviewBindingSource.DataSource = table;

            // 设置 DGV 数据源
            subview.dataGridView.DataSource = _subviewBindingSource;

            // 设置导航条数据源
            subview.bindingNavigator.BindingSource = _subviewBindingSource;

            // 设置全局 ID 用于本地化存储配置文件
            if (subview.Guid != null && subview.ColumnInfos != null)
            {
                subview.SetColumnInfo(subview.ColumnInfos);
            }

            // 根据事件绑定，更新UI按钮的可用状态
            subview.UpdateButtonState();

            // 注册事件
            this.subview.dataGridView.ColumnWidthChanged +=
                new DataGridViewColumnEventHandler(Subview_dataGridView_ColumnWidthChanged);
        }

        /// <summary>
        /// 填充子视图数据（设置数据源）并自动初始化子视图相关基础设置
        /// </summary>
        /// <param name="table">DataTable 对象集合</param>
        public void FillSubviewWithTable(DataTable table)
        {
            // 删除事件
            this.subview.dataGridView.ColumnWidthChanged -=
                new DataGridViewColumnEventHandler(Subview_dataGridView_ColumnWidthChanged);

            if (!(table is DataTable))
            {
                return;
            }

            // 启用并初始化子视图
            SubviewsEnable();

            // 封装数据源
            _subviewBindingSource = new BindingSource();

            // 数据源绑定
            _subviewBindingSource.DataSource = table;

            // 设置 DGV 数据源
            subview.dataGridView.DataSource = _subviewBindingSource;

            // 设置导航条数据源
            subview.bindingNavigator.BindingSource = _subviewBindingSource;

            // 设置全局 ID 用于本地化存储配置文件
            if (subview.Guid != null && subview.ColumnInfos != null)
            {
                subview.SetColumnInfo(subview.ColumnInfos);
            }

            // 根据事件绑定，更新UI按钮的可用状态
            subview.UpdateButtonState();

            // 注册事件
            this.subview.dataGridView.ColumnWidthChanged +=
                new DataGridViewColumnEventHandler(Subview_dataGridView_ColumnWidthChanged);
        }

        #endregion 从数据
    }
}