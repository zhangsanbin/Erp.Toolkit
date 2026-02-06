/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-07-13           Andy        Split, restructure
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    public partial class Dgv
    {
        #region 辅助方法

        /// <summary>
        /// 更新统计信息和选中行数显示
        /// </summary>
        private void UpdateStatisticsAndSelectionInfo()
        {
            // 清空之前的统计信息
            toolStripLabel_Statistics.Text = "";

            int selectedRowCount = dataGridView.SelectedRows.Count;

            // 如果没有选中行，直接返回
            if (selectedRowCount == 0)
            {
                return;
            }

            // 如果有选中行，先显示选中行数
            if (selectedRowCount > 1)
            {
                toolStripLabel_Statistics.Text = $"{localizer.GetString("Selected")}: {selectedRowCount}";
            }

            // 尝试获取当前活动列
            int columnIndex = GetCurrentColumnIndex();

            // 如果获取到有效的列索引，进行统计
            if (columnIndex >= 0 && columnIndex < dataGridView.Columns.Count)
            {
                UpdateColumnStatistics(columnIndex, selectedRowCount);
            }
        }

        /// <summary>
        /// 获取当前操作的列索引
        /// </summary>
        private int GetCurrentColumnIndex()
        {
            // 优先级1：当前焦点单元格的列
            if (dataGridView.CurrentCell != null)
            {
                return dataGridView.CurrentCell.ColumnIndex;
            }

            // 优先级2：当前选中单元格的列
            if (dataGridView.SelectedCells.Count > 0)
            {
                return dataGridView.SelectedCells[0].ColumnIndex;
            }

            // 优先级3：记录的最后点击列
            if (_lastColumnIndex >= 0) return _lastColumnIndex;

            return -1;
        }

        /// <summary>
        /// 更新列统计信息
        /// </summary>
        private void UpdateColumnStatistics(int columnIndex, int selectedRowCount)
        {
            // 检查列是否是数值类型
            Type columnType = GetColumnType(columnIndex);

            if (IsNumericType(columnType))
            {
                // 统计数值列信息
                double max = double.MinValue;
                double min = double.MaxValue;
                double sum = 0;
                int numericCount = 0;

                if (selectedRowCount > 1)
                {
                    // 统计选中行
                    foreach (DataGridViewRow row in dataGridView.SelectedRows)
                    {
                        if (row.IsNewRow || row.Index == dataGridView.NewRowIndex) continue;

                        if (TryGetNumericValue(row.Cells[columnIndex].Value, out double value))
                        {
                            max = Math.Max(max, value);
                            min = Math.Min(min, value);
                            sum += value;
                            numericCount++;
                        }
                    }
                }
                else
                {
                    // 统计整个列
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (row.IsNewRow || row.Index == dataGridView.NewRowIndex) continue;

                        if (TryGetNumericValue(row.Cells[columnIndex].Value, out double value))
                        {
                            max = Math.Max(max, value);
                            min = Math.Min(min, value);
                            sum += value;
                            numericCount++;
                        }
                    }
                }

                // 如果有数值数据，显示统计信息
                if (numericCount > 0)
                {
                    double average = sum / numericCount;
                    string statistics = $"{localizer.GetString("Max")}: {Math.Round(max, 2)} | " +
                                      $"{localizer.GetString("Min")}: {Math.Round(min, 2)} | " +
                                      $"{localizer.GetString("Avg")}: {Math.Round(average, 2)} | " +
                                      $"{localizer.GetString("Sum")}: {Math.Round(sum, 2)}";

                    // 合并显示
                    if (selectedRowCount > 1)
                    {
                        toolStripLabel_Statistics.Text = $"{statistics} | {localizer.GetString("Selected")}: {selectedRowCount}";
                    }
                    else
                    {
                        toolStripLabel_Statistics.Text = statistics;
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否是数值类型
        /// </summary>
        private bool IsNumericType(Type type)
        {
            return type == typeof(int) || type == typeof(long) ||
                   type == typeof(float) || type == typeof(double) ||
                   type == typeof(decimal) || type == typeof(short);
        }

        /// <summary>
        /// 获取数值
        /// </summary>
        private bool TryGetNumericValue(object cellValue, out double result)
        {
            result = 0;

            if (cellValue == null) return false;

            return double.TryParse(cellValue.ToString(), out result);
        }

        /// <summary>
        /// 获取列的数据类型
        /// </summary>
        private Type GetColumnType(int columnIndex)
        {
            // 尝试从第一行获取列类型
            if (dataGridView.Rows.Count > 0)
            {
                var cell = dataGridView.Rows[0].Cells[columnIndex];
                if (cell.Value != null)
                {
                    return cell.ValueType;
                }
            }

            // 兜底，从DataPropertyName获取类型
            var column = dataGridView.Columns[columnIndex];
            if (!string.IsNullOrEmpty(column.DataPropertyName) && _dataView?.Table?.Columns.Contains(column.DataPropertyName) == true)
            {
                return _dataView.Table.Columns[column.DataPropertyName].DataType;
            }

            return typeof(object);
        }

        #endregion 辅助方法

        #region 公共方法

        /// <summary>
        /// 设置自动排序
        /// </summary>
        public void AutoSort()
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        /// <summary>
        /// 判断字体是否存在
        /// </summary>
        /// <param name="fontName"></param>
        /// <returns></returns>
        private static bool IsFontInstalled(string fontName)
        {
            using (InstalledFontCollection installedFontCollection = new InstalledFontCollection())
            {
                return installedFontCollection.Families.Any(f => f.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        /// 将以百分之一英寸（hundredths of an inch）为单位的长度转换为毫米（mm）
        /// </summary>
        /// <param name="hundredthsOfAnInch"></param>
        /// <returns></returns>
        private int ConvertHundredthsOfAnInchToMM(int hundredthsOfAnInch)
        {
            return (int)(hundredthsOfAnInch / 10.0 * 25.4);
        }

        /// <summary>
        /// 格式化过滤参数，转义敏感字符（*, /, \）
        /// </summary>
        /// <param name="parameter">用户输入的过滤参数</param>
        /// <returns>转义后的安全字符串</returns>
        private string FormatFilterParameter(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                return string.Empty;

            return parameter
                .Replace("'", "''")      // 防御单引号注入
                .Replace("*", "[*]")     // 转义通配符
                .Replace("/", "[/]")     // 转义路径符
                .Replace("\\", "[\\]");  // 转义反斜杠
        }

        #endregion 公共方法

        #region 从数据点击事件相关

        /// <summary>
        /// 判断点击位置是否在小图标上
        /// </summary>
        /// <param name="location"></param>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        private bool IsClickOnToggleIcon(Point location, System.Windows.Forms.DataGridView dataGridView)
        {
            Rectangle rect = new Rectangle((int)dataGridView.RowHeadersWidth - 20, (int)((dataGridView.RowTemplate.Height - 16) / 2), 16, 16);
            return rect.Contains(location);
        }

        #endregion 从数据点击事件相关

        #region 数据源类型转换

        /// <summary>
        /// 将一个对象列表，数组或任何可枚举的集合，转换为 DataTable 的表格形式，以便DGV支持排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(List<T> items)
        {
            if (items == null || items.Count == 0) return null;

            DataTable dataTable = new DataTable(typeof(T).Name);

            // 获取T的属性，并创建DataTable的列
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                try
                {
                    dataTable.Columns.Add(prop.Name, prop.PropertyType);
                }
                catch (Exception)
                {
                    dataTable.Columns.Add(prop.Name);
                    // Console.WriteLine($"ConvertToDataTable Error: {prop.Name} Property type is null.");
                }
            }

            // 填充数据到DataTable的行中
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #endregion 数据源类型转换

        #region 数据获取

        /// <summary>
        /// 获取，所选择行的主键列值 Ids 字符串
        /// </summary>
        /// <returns>将 Id 列表转换为一个以逗号分隔的字符串</returns>
        public string GetSelectedItemIds()
        {
            // 创建一个列表来存储Id的值
            List<string> ids = new List<string>();

            // 遍历所有选中的行
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                // 检查值是否为null，以避免NullReferenceException
                if (row.Cells[_primaryKey].Value != null)
                {
                    ids.Add(row.Cells[_primaryKey].Value.ToString()); // 将 _primaryKey 主键列的值添加到列表中
                }
            }

            // 将列表进行反转
            ids.Reverse();

            // 将Id列表转换为一个以逗号分隔的字符串
            string idString = string.Join(",", ids);

            return idString;
        }

        /// <summary>
        /// 获取当前选中行的指定列单元格值
        /// </summary>
        /// <param name="cellName">列名称</param>
        /// <returns>单元格值或空字符串</returns>
        public string GetSelectItemValue(string cellName)
        {
            // 检查列是否存在且存在选中的行
            if (dataGridView.Columns.Contains(cellName) && dataGridView.SelectedRows.Count > 0)
            {
                // 获取第一个选中行的指定列值（优先取 SelectedRows）
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                object value = selectedRow.Cells[cellName].Value;
                return value?.ToString() ?? string.Empty; // 安全处理 null
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取，所选择行的指定列值 Value 字符串
        /// </summary>
        /// <param name="cellName">列名称</param>
        /// <returns>将 Value 列表转换为一个以逗号分隔的字符串</returns>
        public string GetSelectedItemValues(string cellName)
        {
            // 检查列是否存在且存在选中的行
            if (dataGridView.Columns.Contains(cellName) && dataGridView.SelectedRows.Count > 0)
            {
                // 创建一个列表来存储cellName的值
                List<string> values = new List<string>();

                // 遍历所有选中的行
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    // 检查值是否为null，以避免NullReferenceException
                    if (row.Cells[cellName].Value != null)
                    {
                        values.Add(row.Cells[cellName].Value.ToString()); // 将 cellName 列的值添加到列表中
                    }
                }

                // 将列表进行反转
                values.Reverse();

                // 将Id列表转换为一个以逗号分隔的字符串
                string idString = string.Join(",", values);

                return idString;
            }
            return string.Empty;
        }

        /// <summary>
        /// 设置，指定行的指定列单元格文本
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellName"></param>
        /// <param name="valueString"></param>
        public void SetItemValue(int rowIndex, string cellName, string valueString)
        {
            // 确保在 UI 线程操作
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(new Action(() => SetItemValue(rowIndex, cellName, valueString)));
                return;
            }

            // 校验行索引有效性
            if (rowIndex < 0 || rowIndex >= dataGridView.Rows.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "行索引超出有效范围");
            }

            // 查找列（大小写不敏感）
            DataGridViewColumn column = dataGridView.Columns
                .Cast<DataGridViewColumn>()
                .FirstOrDefault(c => c.Name.Equals(cellName, StringComparison.OrdinalIgnoreCase));

            if (column == null)
            {
                throw new ArgumentException($"列名 '{cellName}' 不存在", nameof(cellName));
            }

            // 更新单元格值
            dataGridView.Rows[rowIndex].Cells[column.Index].Value = valueString;
        }

        /// <summary>
        /// 设置当前选中行的指定列单元格文本
        /// </summary>
        /// <param name="cellName">列名称</param>
        /// <param name="valueString">要设置的文本值</param>
        public void SetSelectItemValue(string cellName, string valueString)
        {
            // 同时验证列存在性、选中行有效性、单元格可写性
            if (dataGridView.Columns.Contains(cellName)
                && dataGridView.SelectedRows.Count > 0
                && !dataGridView.Columns[cellName].ReadOnly)
            {
                // 获取第一个选中行并设置值
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                selectedRow.Cells[cellName].Value = valueString;

                // 标记数据修改状态
                selectedRow.DataGridView?.EndEdit(); // 提交编辑
            }
        }

        #endregion 数据获取

        #region 查找与筛选相关方法

        /// <summary>
        /// 回退到上一个过滤器
        /// </summary>
        public void RollbackFilter()
        {
            // 如果历史记录为空，则无法回退
            if (_filterHistory.Count <= 0)
            {
                return;
            }

            // 移除当前（第一个）过滤器表达式
            _filterHistory.RemoveAt(0);

            // 触发标志位（用于区分过滤条件拼接）
            _RollbackFilter = true;

            // 如果列表不为空，则应用上一个过滤器；否则清除过滤器
            Filter = _filterHistory.Count > 0 ? _filterHistory[0] : null;

            // 关闭标志位
            _RollbackFilter = false;
        }

        private bool _RollbackFilter;

        // 用于保存符合搜索条件的行索引的集合
        private List<int> matchingRowIndices = new List<int>();

        /// <summary>
        /// 在 DataGridView 指定列搜索指定文本，并保存所有匹配的行索引
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="searchText">查找的文本</param>
        private void FindAllMatchingRows(string columnName, string searchText)
        {
            // 清空之前的匹配结果
            matchingRowIndices.Clear();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                // 获取指定列的单元格
                DataGridViewCell cell = row.Cells[columnName];

                // 检查单元格值是否包含搜索文本
                if (cell.Value != null && cell.Value.ToString().Contains(searchText))
                {
                    // 保存匹配行的索引
                    matchingRowIndices.Add(row.Index);
                }
            }

            // 更新“下一个”按钮的可用性
            toolStripButton_Next.Enabled = matchingRowIndices.Count > 0;

            // 如果没有找到匹配项，则不执行任何操作
            if (matchingRowIndices.Count > 0)
            {
                // 记录符合匹配的总条数
                tempMatchingCount = matchingRowIndices.Count;

                // 默认情况下选择第一个匹配项
                SelectNextMatchingRow();
            }
        }

        // 记录符合匹配的总条数
        private int tempMatchingCount = 0;

        /// <summary>
        /// 选择下一个匹配的行（如果有的话）
        /// </summary>
        private void SelectNextMatchingRow()
        {
            if (matchingRowIndices.Count == 0) return; // 如果没有匹配项，则不执行任何操作

            // 清除先前的选择
            dataGridView.ClearSelection();

            try
            {
                // 选择行并滚动到它
                DataGridViewRow row = dataGridView.Rows[matchingRowIndices[0]];
                row.Selected = true;
                // 设置活动状态单元格
                dataGridView.CurrentCell = row.Cells[_columnName];
                dataGridView.FirstDisplayedScrollingRowIndex = row.Index;

                // 移除已经选择项目
                matchingRowIndices.Remove(row.Index);

                // 更新“下一个”按钮的可用性
                toolStripButton_Next.Enabled = matchingRowIndices.Count > 0;

                // 文本提示符合匹配的，当前位置/总条数
                if (toolStripButton_Next.Enabled)
                {
                    toolStripButton_Next.Text = localizer.GetString("Next") + matchingRowIndices.Count + "/" + tempMatchingCount + "]";
                }
                else
                {
                    toolStripButton_Next.Text = localizer.GetString("Next");
                }
            }
            catch (Exception)
            {
                ClearAllMatchingRows();
            }
        }

        /// <summary>
        /// 取消所有匹配项的选择，并清空匹配结果
        /// </summary>
        private void ClearAllMatchingRows()
        {
            // 清除选择并清空匹配项集合
            dataGridView.ClearSelection();
            matchingRowIndices.Clear();

            // 更新“下一个”按钮的可用性
            toolStripButton_Next.Enabled = false;
            toolStripButton_Next.Text = localizer.GetString("Next");
        }

        #endregion 查找与筛选相关方法
    }
}