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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    public partial class Dgv
    {
        #region 条件样式外观

        /// <summary>
        /// 预编译单元格样式配置事件处理程序
        /// </summary>
        private DataGridViewCellFormattingEventHandler _cachedCellFormattingHandler;

        /// <summary>
        /// 预编译行样式配置事件处理程序
        /// </summary>
        private DataGridViewRowPrePaintEventHandler _cachedRowPrePaintHandler;

        /// <summary>
        /// 预编译单元格值变化事件处理程序
        /// </summary>
        private DataGridViewCellEventHandler _cachedCellValueChangedHandler;

        /// <summary>
        /// 预编译行删除事件处理程序
        /// </summary>
        private DataGridViewRowsRemovedEventHandler _cachedRowsRemovedHandler;

        /// <summary>
        /// 根据条件规则，构建条件样式配置
        /// </summary>
        /// <param name="configs"></param>
        public void BuildConditionalFormatting(List<DgvConditionalConfig> configs)
        {
            DgvConditionalConfigs = configs;
            var dgvConditionalFormattingConfigs = new List<DgvConditionalFormattingConfig>();

            foreach (var config in configs)
            {
                var formattingConfig = new DgvConditionalFormattingConfig
                {
                    ColumnName = config.ColumnName,
                    DependentColumns = config.DependentColumns,
                    Font = config.Font,
                    ForeColor = config.ForeColor,
                    BackColor = config.BackColor,
                    FullRow = config.FullRow,
                    AllowStyleStacking = config.AllowStyleStacking
                };

                // 处理列间比较逻辑
                if (config.RuleType == RuleType.CompareOtherRecords &&
                    config.DependentColumns?.Count > 0)
                {
                    formattingConfig.RowCondition = values =>
                    {
                        if (!values.TryGetValue(config.ColumnName, out object currentValue))
                            return false;

                        return CompareWithDependentColumns(currentValue, values, config);
                    };
                }
                else
                {
                    // 根据逻辑类型，数据类型，动态拼接表达式
                    switch (config.LogicalType)
                    {
                        case LogicalType.Between:
                            if (config.ColumnType == typeof(int))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is int txt && txt > int.Parse(config.Value1) && txt <= int.Parse(config.Value2));
                            }
                            else if (config.ColumnType == typeof(double))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is double txt && txt > int.Parse(config.Value1) && txt <= int.Parse(config.Value2));
                            }
                            else if (config.ColumnType == typeof(decimal))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is decimal txt && txt > int.Parse(config.Value1) && txt <= int.Parse(config.Value2));
                            }
                            else if (config.ColumnType == typeof(DateTime))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is DateTime txt && txt > DateTime.Parse(config.Value1) && txt <= DateTime.Parse(config.Value2));
                            }
                            break;

                        case LogicalType.NotBetween:

                            break;

                        case LogicalType.Contains:
                            if (config.ColumnType == typeof(string))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is string txt && txt.Contains(config.Value1));
                            }
                            break;

                        case LogicalType.DoesNotContain:
                            if (config.ColumnType == typeof(string))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is string txt && !txt.Contains(config.Value1));
                            }
                            break;

                        case LogicalType.Equals:
                            if (config.ColumnType == typeof(int))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is int txt && txt == int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(double))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is double txt && txt == int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(decimal))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is decimal txt && txt == int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(DateTime))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is DateTime txt && txt == DateTime.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(string))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is string txt && txt == config.Value1);
                            }
                            break;

                        case LogicalType.NotEquals:
                            if (config.ColumnType == typeof(int))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is int txt && txt != int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(double))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is double txt && txt != int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(decimal))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is decimal txt && txt != int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(DateTime))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is DateTime txt && txt != DateTime.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(string))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is string txt && txt != config.Value1);
                            }
                            break;

                        case LogicalType.GreaterThan:
                            if (config.ColumnType == typeof(int))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is int txt && txt > int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(double))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is double txt && txt > int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(decimal))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is decimal txt && txt > int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(DateTime))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is DateTime txt && txt > DateTime.Parse(config.Value1));
                            }
                            break;

                        case LogicalType.LessThan:
                            if (config.ColumnType == typeof(int))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is int txt && txt < int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(double))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is double txt && txt < int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(decimal))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is decimal txt && txt < int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(DateTime))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is DateTime txt && txt < DateTime.Parse(config.Value1));
                            }
                            break;

                        case LogicalType.GreaterThanOrEquals:
                            if (config.ColumnType == typeof(int))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is int txt && txt >= int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(double))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is double txt && txt >= int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(decimal))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is decimal txt && txt >= int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(DateTime))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is DateTime txt && txt >= DateTime.Parse(config.Value1));
                            }
                            break;

                        case LogicalType.LessThanOrEquals:
                            if (config.ColumnType == typeof(int))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is int txt && txt <= int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(double))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is double txt && txt <= int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(decimal))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is decimal txt && txt <= int.Parse(config.Value1));
                            }
                            else if (config.ColumnType == typeof(DateTime))
                            {
                                formattingConfig.Condition = obj => obj != null && (obj is DateTime txt && txt <= DateTime.Parse(config.Value1));
                            }
                            break;
                    }
                }

                // 添加到配置列表
                dgvConditionalFormattingConfigs.Add(formattingConfig);
            }

            // 设置并应用条件样式
            SetConditionalFormatting(dgvConditionalFormattingConfigs);

            return;// 结束方法(不执行演示)

            // 演示配置
            var demoConfigs = new List<DgvConditionalFormattingConfig>
            {
                // 单元格样式 - 简单文本包含
                new DgvConditionalFormattingConfig
                {
                    ColumnName = "CustomerName",
                    SearchText = "机械",
                    BackColor = Color.LightBlue,
                },
                // 单元格样式 - 不区分大小写
                new DgvConditionalFormattingConfig
                {
                    ColumnName = "Model",
                    SearchText = "ac",
                    StringComparison = StringComparison.OrdinalIgnoreCase,
                    ForeColor = Color.DarkBlue,
                },
                // 单元格样式 - 自定义匹配逻辑
                new DgvConditionalFormattingConfig
                {
                    ColumnName = "CategoryName",
                    TextMatchCondition = text =>
                        text.StartsWith("新") || text.EndsWith("他"),
                    BackColor = Color.Red,
                    ForeColor = Color.White,
                },
                // 单元格样式 - 通用表达式判断逻辑
                new DgvConditionalFormattingConfig
                {
                    ColumnName = "ProductName",
                    Condition = value =>
                    {
                        if (value is string strValue)
                        {
                            return strValue.Contains("其他");
                        }
                        return false;
                    },
                    BackColor = Color.Yellow,
                    ForeColor = Color.Black,
                },
                // 单元格样式 - 使用通用条件委托
                new DgvConditionalFormattingConfig
                {
                    ColumnName = "Quantity",
                    Condition = value =>
                    {
                        // int or decimal 类型判断
                        if (value is decimal decimalValue)
                            return decimalValue > 50;
                        else if (value is int intValue)
                            return intValue > 50;
                        return false;
                    },
                    BackColor = Color.Green
                },
                // 单元格样式 - 空值处理
                new DgvConditionalFormattingConfig
                {
                    ColumnName = "TechnicalRequire",
                    NullCondition = () => true, // 空值时应用样式
                    BackColor = Color.LightGray
                },
                // 整行样式 - 文本包含
                new DgvConditionalFormattingConfig
                {
                    ColumnName = "Model",
                    FullRow = true,
                    DependentColumns = new List<string> { "Model" },
                    RowCondition = values =>
                    {
                        if (values.TryGetValue("Model", out object value) && value != null)
                        {
                            string productName = value.ToString();
                            return !string.IsNullOrEmpty(productName) &&
                                   productName.Contains("摆");
                        }
                        return false;
                    },
                    BackColor = Color.Orange
                },
                // 整行样式 - 多列搜索
                new DgvConditionalFormattingConfig
                {
                    ColumnName = "TechnicalRequire",
                    FullRow = true,
                    DependentColumns = new List<string> { "Model", "ProductName", "TechnicalRequire" },
                    RowCondition = values =>
                    {
                        string[] keywords = { "AC", "其他", "加工" };
                        foreach (var val in values.Values)
                        {
                            if (val != null)
                            {
                                string strVal = val.ToString();
                                foreach (var keyword in keywords)
                                {
                                    if (strVal.Contains(keyword))
                                        return true;
                                }
                            }
                        }
                        return false;
                    },
                    BackColor = Color.Red,
                    ForeColor = Color.White
                }
            };

            // 应用演示配置
            SetConditionalFormatting(demoConfigs);
        }

        /// <summary>
        /// 应用条件样式
        /// </summary>
        public void SetConditionalFormatting(List<DgvConditionalFormattingConfig> configs)
        {
            // 移除旧的事件处理程序
            if (_cachedCellFormattingHandler != null)
                dataGridView.CellFormatting -= _cachedCellFormattingHandler;
            if (_cachedRowPrePaintHandler != null)
                dataGridView.RowPrePaint -= _cachedRowPrePaintHandler;
            if (_cachedCellValueChangedHandler != null)
                dataGridView.CellValueChanged -= _cachedCellValueChangedHandler;
            if (_cachedRowsRemovedHandler != null)
                dataGridView.RowsRemoved -= _cachedRowsRemovedHandler;

            // 分离整行配置和单元格配置
            var rowConfigs = configs.FindAll(c => c.FullRow);
            var cellConfigs = configs.FindAll(c => !c.FullRow);

            // 预编译配置索引
            var rowConfigDict = PrecompileRowConfigs(rowConfigs);
            var cellConfigDict = PrecompileCellConfigs(cellConfigs);

            // 行样式状态缓存 [行索引 → 样式状态]
            var rowStyleCache = new Dictionary<int, RowStyleState>();

            // 单元格样式事件处理程序
            _cachedCellFormattingHandler = (sender, e) =>
            {
                var columnName = dataGridView.Columns[e.ColumnIndex].Name;
                if (cellConfigDict.TryGetValue(columnName, out var columnConfigs))
                {
                    foreach (var config in columnConfigs)
                    {
                        if (EvaluateCondition(config, e.Value))
                        {
                            ApplyStyleDirectly(e.CellStyle, config);
                            if (!config.AllowStyleStacking) break;
                        }
                    }
                }
            };

            // 行预绘制事件处理程序
            _cachedRowPrePaintHandler = (sender, e) =>
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                if (!rowStyleCache.TryGetValue(e.RowIndex, out var state) || state.IsDirty)
                {
                    lock (rowStyleCache)
                    {
                        if (!rowStyleCache.TryGetValue(e.RowIndex, out state) || state.IsDirty)
                        {
                            state = ComputeRowStyle(row, rowConfigDict);
                            rowStyleCache[e.RowIndex] = state;
                        }
                    }
                }
                if (state.Style != null)
                {
                    row.DefaultCellStyle = state.Style;
                }
            };

            // 单元格值变化事件处理程序
            _cachedCellValueChangedHandler = (s, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    lock (rowStyleCache)
                    {
                        if (rowStyleCache.TryGetValue(e.RowIndex, out var state))
                            state.IsDirty = true;
                        else
                            rowStyleCache[e.RowIndex] = new RowStyleState { IsDirty = true };
                    }
                    dataGridView.InvalidateRow(e.RowIndex);
                }
            };

            // 行删除事件处理程序
            _cachedRowsRemovedHandler = (s, e) =>
            {
                lock (rowStyleCache)
                {
                    for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                        rowStyleCache.Remove(i);
                }
            };

            // 绑定事件处理程序
            dataGridView.CellFormatting += _cachedCellFormattingHandler;
            dataGridView.RowPrePaint += _cachedRowPrePaintHandler;
            dataGridView.CellValueChanged += _cachedCellValueChangedHandler;
            dataGridView.RowsRemoved += _cachedRowsRemovedHandler;
        }

        /// <summary>
        /// 比较当前值与依赖列的值（支持多列依赖）
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="rowValues"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private bool CompareWithDependentColumns(object currentValue, Dictionary<string, object> rowValues, DgvConditionalConfig config)
        {
            if (!rowValues.TryGetValue(config.DependentColumns.LastOrDefault(), out object dependentValue))
                return false;

            if (CompareValues(currentValue, dependentValue, config.LogicalType))
                return true;

            return false;
        }

        /// <summary>
        /// 比较两个值（支持数值、日期和字符串类型）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="logicalType"></param>
        /// <returns></returns>
        private bool CompareValues(object a, object b, LogicalType logicalType)
        {
            try
            {
                // 处理空值
                if (a == null || b == null) return false;

                // 尝试数值比较
                if (TryNumericComparison(a, b, logicalType, out bool numericResult))
                    return numericResult;

                // 尝试日期比较
                if (TryDateTimeComparison(a, b, logicalType, out bool dateResult))
                    return dateResult;

                // 尝试字符串比较
                return TryStringComparison(a, b, logicalType);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 数值比较（支持所有数值类型）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="logicalType"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool TryNumericComparison(object a, object b, LogicalType logicalType, out bool result)
        {
            result = false;

            if (a is IConvertible aNum && b is IConvertible bNum)
            {
                try
                {
                    decimal decA = Convert.ToDecimal(aNum);
                    decimal decB = Convert.ToDecimal(bNum);

                    switch (logicalType)
                    {
                        case LogicalType.Equals:
                            result = decA == decB;
                            return true;

                        case LogicalType.NotEquals:
                            result = decA != decB;
                            return true;

                        case LogicalType.GreaterThan:
                            result = decA > decB;
                            return true;

                        case LogicalType.LessThan:
                            result = decA < decB;
                            return true;

                        case LogicalType.GreaterThanOrEquals:
                            result = decA >= decB;
                            return true;

                        case LogicalType.LessThanOrEquals:
                            result = decA <= decB;
                            return true;

                        default:
                            return false;
                    }
                }
                catch
                {
                    // 转换失败不是数值类型
                }
            }
            return false;
        }

        /// <summary>
        /// 日期比较（支持 DateTime 和字符串格式的日期）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="logicalType"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool TryDateTimeComparison(object a, object b, LogicalType logicalType, out bool result)
        {
            result = false;

            try
            {
                DateTime? dtA = a as DateTime?;
                if (dtA == null && a is string sA)
                    dtA = DateTime.Parse(sA);

                DateTime? dtB = b as DateTime?;
                if (dtB == null && b is string sB)
                    dtB = DateTime.Parse(sB);

                if (!dtA.HasValue || !dtB.HasValue)
                    return false;

                switch (logicalType)
                {
                    case LogicalType.Equals:
                        result = dtA.Value == dtB.Value;
                        return true;

                    case LogicalType.NotEquals:
                        result = dtA.Value != dtB.Value;
                        return true;

                    case LogicalType.GreaterThan:
                        result = dtA.Value > dtB.Value;
                        return true;

                    case LogicalType.LessThan:
                        result = dtA.Value < dtB.Value;
                        return true;

                    case LogicalType.GreaterThanOrEquals:
                        result = dtA.Value >= dtB.Value;
                        return true;

                    case LogicalType.LessThanOrEquals:
                        result = dtA.Value <= dtB.Value;
                        return true;

                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 字符串比较（支持不区分大小写的文本比较）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="logicalType"></param>
        /// <returns></returns>
        private bool TryStringComparison(object a, object b, LogicalType logicalType)
        {
            string strA = a?.ToString();
            string strB = b?.ToString();

            if (string.IsNullOrEmpty(strA)) return false;
            if (string.IsNullOrEmpty(strB)) return false;

            switch (logicalType)
            {
                case LogicalType.Equals:
                    return strA.Equals(strB, StringComparison.OrdinalIgnoreCase);

                case LogicalType.NotEquals:
                    return !strA.Equals(strB, StringComparison.OrdinalIgnoreCase);

                case LogicalType.Contains:
                    return strA.IndexOf(strB, StringComparison.OrdinalIgnoreCase) >= 0;

                case LogicalType.DoesNotContain:
                    return strA.IndexOf(strB, StringComparison.OrdinalIgnoreCase) < 0;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 预编译单元格配置
        /// </summary>
        private Dictionary<string, List<DgvConditionalFormattingConfig>> PrecompileCellConfigs(List<DgvConditionalFormattingConfig> cellConfigs)
        {
            var dict = new Dictionary<string, List<DgvConditionalFormattingConfig>>(
                StringComparer.OrdinalIgnoreCase);

            foreach (var config in cellConfigs)
            {
                if (!dict.TryGetValue(config.ColumnName, out var list))
                {
                    list = new List<DgvConditionalFormattingConfig>();
                    dict[config.ColumnName] = list;
                }
                list.Add(config);
            }
            return dict;
        }

        /// <summary>
        /// 收集单元格值
        /// </summary>
        private Dictionary<string, object> CollectCellValues(DataGridViewRow row, DgvConditionalFormattingConfig currentConfig, List<DgvConditionalFormattingConfig> allConfigs)
        {
            var cellValues = new Dictionary<string, object>();
            var collectedColumns = new HashSet<string>();

            // 收集当前配置需要的列
            foreach (var col in GetDependentColumns(currentConfig))
            {
                if (collectedColumns.Add(col))
                {
                    AddCellValue(row, col, cellValues);
                }
            }

            // 收集其他配置可能需要的公共列
            foreach (var config in allConfigs)
            {
                if (config == currentConfig) continue;

                foreach (var col in GetDependentColumns(config))
                {
                    if (collectedColumns.Add(col))
                    {
                        AddCellValue(row, col, cellValues);
                    }
                }
            }

            return cellValues;
        }

        /// <summary>
        /// 获取配置依赖的所有列
        /// </summary>
        private IEnumerable<string> GetDependentColumns(DgvConditionalFormattingConfig config)
        {
            yield return config.ColumnName;

            if (config.DependentColumns != null)
            {
                foreach (var col in config.DependentColumns)
                {
                    yield return col;
                }
            }
        }

        /// <summary>
        /// 添加单元格值到字典
        /// </summary>
        private void AddCellValue(DataGridViewRow row, string columnName, Dictionary<string, object> cellValues)
        {
            if (dataGridView.Columns[columnName] is DataGridViewColumn column)
            {
                cellValues[columnName] = row.Cells[column.Index].Value;
            }
        }

        /// <summary>
        /// 直接应用样式
        /// </summary>
        private void ApplyStyleDirectly(DataGridViewCellStyle style, DgvConditionalFormattingConfig config)
        {
            if (config.ForeColor != Color.Empty)
                style.ForeColor = config.ForeColor;

            if (config.BackColor != Color.Empty)
                style.BackColor = config.BackColor;

            if (config.Font != null)
                style.Font = config.Font;
        }

        /// <summary>
        /// 评估条件是否满足
        /// </summary>
        /// <param name="config"></param>
        /// <param name="value"></param>
        /// <param name="rowValues"></param>
        /// <returns></returns>
        private bool EvaluateCondition(DgvConditionalFormattingConfig config, object value, Dictionary<string, object> rowValues = null)
        {
            try
            {
                // 1. 空值处理
                if (value == null || (value is string str && string.IsNullOrEmpty(str)))
                    return config.NullCondition?.Invoke() ?? false;

                // 2. 优先使用行条件（需要多列值）
                if (config.RowCondition != null && rowValues != null)
                    return config.RowCondition(rowValues);

                // 3. 使用单元格条件
                if (config.Condition != null)
                    return config.Condition(value);

                // 4. 文本匹配条件
                if (!string.IsNullOrEmpty(config.SearchText))
                    return value.ToString().IndexOf(
                        config.SearchText, config.StringComparison) >= 0;

                // 5. 自定义匹配逻辑
                if (config.TextMatchCondition != null)
                    return config.TextMatchCondition(value.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"条件评估错误: {ex.Message}");
            }
            return false;
        }

        /// <summary>
        /// 预编译行配置
        /// </summary>
        /// <param name="rowConfigs"></param>
        /// <returns></returns>
        private Dictionary<string, List<DgvConditionalFormattingConfig>> PrecompileRowConfigs(List<DgvConditionalFormattingConfig> rowConfigs)
        {
            var configDict = new Dictionary<string, List<DgvConditionalFormattingConfig>>();

            foreach (var config in rowConfigs)
            {
                // 提取配置依赖的所有列
                List<string> dependentColumns = config.DependentColumns ?? new List<string>();

                // 如果没有指定依赖列，则使用默认列名
                if (dependentColumns.Count == 0 && !string.IsNullOrEmpty(config.ColumnName))
                {
                    dependentColumns.Add(config.ColumnName);
                }

                foreach (var col in dependentColumns)
                {
                    if (!configDict.ContainsKey(col))
                    {
                        configDict[col] = new List<DgvConditionalFormattingConfig>();
                    }
                    configDict[col].Add(config);
                }
            }

            return configDict;
        }

        /// <summary>
        /// 计算行样式状态
        /// </summary>
        /// <param name="row"></param>
        /// <param name="configDict"></param>
        /// <returns></returns>
        private RowStyleState ComputeRowStyle(DataGridViewRow row, Dictionary<string, List<DgvConditionalFormattingConfig>> configDict)
        {
            RowStyleState state = new RowStyleState();
            DataGridViewCellStyle rowStyle = null;

            // 收集所有依赖列的值
            var cellValues = new Dictionary<string, object>();
            foreach (var colName in configDict.Keys)
            {
                if (row.DataGridView.Columns.Contains(colName))
                {
                    DataGridViewColumn col = row.DataGridView.Columns[colName];
                    if (col.Index < row.Cells.Count)
                    {
                        DataGridViewCell cell = row.Cells[col.Index];
                        cellValues[colName] = cell.Value;
                    }
                }
            }

            // 处理所有相关配置
            foreach (var configList in configDict.Values)
            {
                foreach (var config in configList)
                {
                    bool conditionMet = false;

                    // 优先使用 RowCondition（多列条件）
                    if (config.RowCondition != null)
                    {
                        conditionMet = config.RowCondition(cellValues);
                    }
                    // 回退到 Condition（单列条件）
                    else if (config.Condition != null &&
                             cellValues.TryGetValue(config.ColumnName, out object cellValue))
                    {
                        conditionMet = EvaluateCondition(config, cellValue);
                    }
                    // 回退到文本匹配条件
                    else if (!string.IsNullOrEmpty(config.SearchText) &&
                             cellValues.TryGetValue(config.ColumnName, out cellValue))
                    {
                        conditionMet = EvaluateCondition(config, cellValue);
                    }

                    if (conditionMet)
                    {
                        // 延迟创建样式对象
                        if (rowStyle == null)
                        {
                            rowStyle = (DataGridViewCellStyle)row.DefaultCellStyle.Clone();
                        }

                        // 应用样式属性
                        ApplyStyleProperties(rowStyle, config);

                        // 如果不允许叠加则退出
                        if (!config.AllowStyleStacking) break;
                    }
                }
            }

            state.Style = rowStyle;
            state.IsDirty = false;
            return state;
        }

        /// <summary>
        /// 应用样式属性到目标样式
        /// </summary>
        /// <param name="targetStyle"></param>
        /// <param name="config"></param>
        private void ApplyStyleProperties(DataGridViewCellStyle targetStyle, DgvConditionalFormattingConfig config)
        {
            if (config.ForeColor != Color.Empty)
                targetStyle.ForeColor = config.ForeColor;

            if (config.BackColor != Color.Empty)
                targetStyle.BackColor = config.BackColor;

            if (config.Font != null)
                targetStyle.Font = config.Font;
        }

        /// <summary>
        /// 行样式状态（支持缓存）
        /// </summary>
        private class RowStyleState
        {
            public DataGridViewCellStyle Style { get; set; }
            public bool IsDirty { get; set; } = true;
        }

        #endregion 条件样式外观

        #region 自定义字段外观

        /// <summary>
        /// 设置，自定义字段（Test，需要处理防止多次调用）
        /// </summary>
        public void SetCustomColumnsConfig()
        {
            if (CustomColumnsConfigs != null)
            {
                foreach (var customColumnsConfig in CustomColumnsConfigs)
                {
                    // 添加用户虚拟列
                    DataGridViewTextBoxColumn customColumn = new DataGridViewTextBoxColumn();
                    customColumn.Name = customColumnsConfig.Name;
                    customColumn.HeaderText = customColumnsConfig.HeaderText;
                    dataGridView.Columns.Add(customColumn);

                    // 填充新列的数据
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        string customValue = string.Empty;
                        foreach (var v in customColumnsConfig.ValueList)
                        {
                            // 根据类型，拼接文本
                            if (v.ValueType == DgvCustomColumnsValueType.String)
                            {
                                customValue += v.Value;
                            }
                            else
                            {
                                customValue += row.Cells[v.Value].Value.ToString();
                            }
                        }
                        // 显示数据
                        row.Cells[customColumnsConfig.Name].Value = customValue;
                    }
                }
            }
        }

        #endregion 自定义字段外观

        #region 系统样式和主从表格外观

        /// <summary>
        /// 初始化DGV系统颜色样式
        /// </summary>
        private void SetThemeStyle()
        {
            if (_themeColors != null)
            {
                // 自定义菜单渲染器选项
                var options = new DgvRendererOptions
                {
                    BackgroundGradientStart = _themeColors.GradientDarkColor,
                    BackgroundGradientEnd = _themeColors.BackgroundColor,
                    EnableBackgroundGradient = true,
                    ButtonCornerRadius = 1,
                    EnableButtonGloss = true,
                    EnableTextShadow = false,
                    TextColor = Color.FromArgb(80, 80, 80)
                };
                var renderer = new DgvCustomRenderer(options);
                this.toolStrip1.Renderer = renderer;
                this.bindingNavigator.Renderer = renderer;
                this.DgvContextMenuStrip.Renderer = renderer;
                this.RowHeaderContextMenuStrip.Renderer = renderer;
                this.ColHeaderContextMenuStrip.Renderer = renderer;

                // 设置背景色
                this.BackColor = _themeColors.BackgroundColor;

                // 设置奇偶行背景色
                if (_alternatingStyle) this.dataGridView.AlternatingRowsDefaultCellStyle.BackColor = _themeColors.AlternatingRowsBackColor;

                // 初始化画笔
                if (_pen != null) _pen.Dispose();
                _pen = new Pen(_themeColors.CellHeaderBorderColor, (float)0.5);
            }
        }

        /// <summary>
        /// DataGridView 控件的行重绘事件，RowPostPaint 是在整行绘制完成后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // 重绘行表头按钮和引导线
            DrawRowHeaderIcon(sender, e);
        }

        /// <summary>
        /// 绘制，列表头的边框和图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawRowHeaderIcon(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            if (dataGridView == null) return;

            // 百分比图标绘制
            DrawPercentageIcon(dataGridView, e);

            // 子视图相关绘制
            if (_subview)
            {
                DrawSubviewIcons(dataGridView, e);
            }
        }

        /// <summary>
        /// 绘制，行表头的百分比图标
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="e"></param>
        private void DrawPercentageIcon(DataGridView dataGridView, DataGridViewRowPostPaintEventArgs e)
        {
            if (string.IsNullOrEmpty(_proportionColumnName)) return;
            if (!dataGridView.Columns.Contains(_proportionColumnName)) return;

            object cellValue = dataGridView.Rows[e.RowIndex].Cells[_proportionColumnName].Value;
            if (cellValue == null) return;

            int proportionValue;
            if (!int.TryParse(cellValue.ToString(), out proportionValue)) return;

            using (Image proportionIcon = GetProportionIcon(proportionValue))
            {
                if (proportionIcon == null) return;

                int leftOffset = _subview ? 45 : 20;
                var iconRect = new Rectangle(
                    e.RowBounds.Left + dataGridView.RowHeadersWidth - leftOffset,
                    e.RowBounds.Top + (dataGridView.RowTemplate.Height - proportionIcon.Height) / 2,
                    proportionIcon.Width,
                    proportionIcon.Height
                );

                e.Graphics.DrawImage(proportionIcon, iconRect);
            }
        }

        /// <summary>
        /// 绘制，行表头的展开/折叠图标和引导线
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="e"></param>
        private void DrawSubviewIcons(DataGridView dataGridView, DataGridViewRowPostPaintEventArgs e)
        {
            int rowIndex = e.RowIndex;
            bool isExpanded = _subviewCurrentRow.Contains(rowIndex);

            // 绘制切换图标
            Image toggleIcon = isExpanded ? _collapseIcon : _expandIcon;
            if (toggleIcon != null)
            {
                var iconRect = new Rectangle(
                    e.RowBounds.Left + dataGridView.RowHeadersWidth - 20,
                    e.RowBounds.Top + (dataGridView.RowTemplate.Height - toggleIcon.Height) / 2,
                    toggleIcon.Width,
                    toggleIcon.Height
                );
                e.Graphics.DrawImage(toggleIcon, iconRect);
            }

            // 绘制分隔线
            int borderX = dataGridView.RowHeadersWidth - 24;
            var borderStart = new Point(borderX, e.RowBounds.Top);
            var borderEnd = new Point(borderX, e.RowBounds.Y + dataGridView.RowTemplate.Height);
            e.Graphics.DrawLine(_pen, borderStart, borderEnd);

            // 绘制展开状态的引导线
            if (isExpanded)
            {
                DrawExpansionGuideLines(dataGridView, e);
            }
        }

        /// <summary>
        /// 绘制，行表头的展开/折叠引导线
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="e"></param>
        private void DrawExpansionGuideLines(DataGridView dataGridView, DataGridViewRowPostPaintEventArgs e)
        {
            int rowHeadersWidth = dataGridView.RowHeadersWidth;
            int halfRowHeadersWidth = rowHeadersWidth / 2;
            int startX = e.RowBounds.Left;
            int startY = e.RowBounds.Top + dataGridView.RowTemplate.Height - 1;
            int rowTemplateHeight = dataGridView.RowTemplate.Height;
            int columnHeadersHeight = dataGridView.ColumnHeadersHeight;
            int endY = startY + rowTemplateHeight + columnHeadersHeight;

            // 使用原始引导线绘制逻辑
            Point[] points =
            {
                new Point(startX, startY),
                new Point(startX + rowHeadersWidth, startY),
                new Point(startX + halfRowHeadersWidth, startY),
                new Point(startX + halfRowHeadersWidth, endY),
                new Point(startX + halfRowHeadersWidth + halfRowHeadersWidth, endY),
                new Point(startX + halfRowHeadersWidth * 2 - 11, endY - 4),
                new Point(startX + halfRowHeadersWidth * 2 - 11, endY + 4),
                new Point(startX + halfRowHeadersWidth * 2, endY)
            };

            // 使用主题色创建新画笔
            using (Pen pen = new Pen(_themeColors.CellHeaderBorderColor))
            {
                e.Graphics.DrawLines(pen, points);
                e.Graphics.DrawArc(pen, startX + halfRowHeadersWidth - 3, startY - 3, 6, 6, 0, 360);
            }
        }

        /// <summary>
        /// 图标，获取比例图标
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Image GetProportionIcon(int value)
        {
            if (value <= 0) return Properties.Resources.proportion_0;
            if (value <= 12) return Properties.Resources.proportion_12;
            if (value <= 25) return Properties.Resources.proportion_25;
            if (value <= 37) return Properties.Resources.proportion_37;
            if (value <= 50) return Properties.Resources.proportion_50;
            if (value <= 62) return Properties.Resources.proportion_62;
            if (value <= 75) return Properties.Resources.proportion_75;
            if (value <= 87) return Properties.Resources.proportion_87;
            return Properties.Resources.proportion_100;
        }

        /// <summary>
        /// 子从数据视图的行头点击事件处理，支持展开/折叠逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            bool isOnToggleIcon = IsClickOnToggleIcon(e.Location, dataGridView);

            // 前置条件检查
            if (!isOnToggleIcon || !_subview || e.Button != MouseButtons.Left)
                return;

            int rowIndex = e.RowIndex;
            _rowIndexnSubview = rowIndex;
            Rectangle masterSlaveDateRect = dataGridView.GetCellDisplayRectangle(e.ColumnIndex, rowIndex, false);

            // 获取主键值并检查有效性
            DataGridViewRow row = dataGridView.Rows[rowIndex];
            string primaryKeyCellValue = row.Cells[_primaryKey].Value?.ToString();
            if (string.IsNullOrEmpty(primaryKeyCellValue))
                return;

            // 当前行已展开：执行折叠操作
            if (_subviewCurrentRow.Contains(rowIndex))
            {
                CollapseRow(rowIndex, primaryKeyCellValue, masterSlaveDateRect, e);
                return;
            }

            // 当前行未展开：先折叠其他行再展开当前行
            CollapseExistingExpandedRow(primaryKeyCellValue, masterSlaveDateRect, e);
            ExpandRow(rowIndex, primaryKeyCellValue, masterSlaveDateRect, sender, e);
        }

        /// <summary>
        /// 折叠指定行
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="primaryKey"></param>
        /// <param name="rect"></param>
        /// <param name="e"></param>
        private void CollapseRow(int rowIndex, string primaryKey, Rectangle rect, DataGridViewCellMouseEventArgs e)
        {
            _subviewCurrentRow.Clear();
            dataGridView.Rows[rowIndex].Height = dataGridView.RowTemplate.Height;
            dataGridView.Rows[rowIndex].DividerHeight = 0;
            dataGridView.ClearSelection();
            dataGridView.Rows[rowIndex].Selected = true;
            OnMasterSlaveDataCollapse(e, primaryKey, rect);
        }

        /// <summary>
        /// 折叠其他已展开的行
        /// </summary>
        /// <param name="currentPrimaryKey"></param>
        /// <param name="rect"></param>
        /// <param name="e"></param>
        private void CollapseExistingExpandedRow(string currentPrimaryKey, Rectangle rect, DataGridViewCellMouseEventArgs e)
        {
            if (_subviewCurrentRow.Count == 0)
                return;

            int existingRow = _subviewCurrentRow[0];
            string existingPrimaryKey = dataGridView.Rows[existingRow].Cells[_primaryKey].Value?.ToString();

            CollapseRow(existingRow, existingPrimaryKey, rect, e);
        }

        /// <summary>
        /// 展开指定行
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="primaryKey"></param>
        /// <param name="rect"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExpandRow(int rowIndex, string primaryKey, Rectangle rect, object sender, DataGridViewCellMouseEventArgs e)
        {
            _subviewCurrentRow.Add(rowIndex);
            dataGridView.ClearSelection();
            dataGridView.Rows[rowIndex].Selected = true;

            int expandedHeight = dataGridView.Height / 2;
            dataGridView.Rows[rowIndex].Height = dataGridView.RowTemplate.Height + expandedHeight;
            dataGridView.Rows[rowIndex].DividerHeight = expandedHeight;

            OnMasterSlaveDataExpand(sender, e, primaryKey, rect);
        }

        /// <summary>
        /// dgv水平或垂直滚动时触发事件，自动折叠子视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            // 没有展开行时直接返回
            if (_subviewCurrentRow.Count == 0 || subview == null) return;

            var dgv = (DataGridView)sender;
            int rowIndex = _subviewCurrentRow[0];

            // 获取展开行在屏幕中的位置
            Rectangle rect = dgv.GetCellDisplayRectangle(GetFirstVisibleColumnIndex(), rowIndex, false);

            // 如果行不可见则隐藏子视图
            if (rect.Y <= 0 || rect.Y + dgv.RowTemplate.Height > dgv.ClientRectangle.Height)
            {
                if (subview.Visible) subview.Visible = false;
                return;
            }

            // 计算子视图位置
            int subviewX = rect.X > 0 ? rect.X : dgv.Location.X + 48;
            int subviewY = rect.Y + dgv.RowTemplate.Height + dgv.Location.Y;

            // 仅当位置变化时更新
            if (subview.Location.X != subviewX || subview.Location.Y != subviewY)
            {
                subview.Location = new Point(subviewX, subviewY);
            }

            // 仅当尺寸变化时更新
            if (subview.Width != dgv.Width - dgv.RowHeadersWidth - 20 ||
                subview.Height != dgv.Height / 2)
            {
                subview.Size = new Size(dgv.Width - dgv.RowHeadersWidth - 20, dgv.Height / 2);
            }

            // 确保子视图可见
            if (!subview.Visible) subview.Visible = true;
        }

        /// <summary>
        /// dgv调整尺寸后，如果主从数据视图被启用时，重新调整大小，自动折叠子视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_Resize(object sender, EventArgs e)
        {
            // 没有展开行时直接返回
            if (_subviewCurrentRow.Count == 0 || subview == null) return;

            var dgv = (DataGridView)sender;
            int rowIndex = _subviewCurrentRow[0];

            // 计算新高度（确保非负）
            int newHeight = Math.Max(0, dgv.Height / 2);
            int totalHeight = dgv.RowTemplate.Height + newHeight;

            // 仅当高度变化时更新
            if (dgv.Rows[rowIndex].Height != totalHeight ||
                dgv.Rows[rowIndex].DividerHeight != newHeight)
            {
                dgv.Rows[rowIndex].Height = totalHeight;
                dgv.Rows[rowIndex].DividerHeight = newHeight;
            }

            // 更新子视图尺寸
            subview.Size = new Size(dgv.Width - dgv.RowHeadersWidth - 20, newHeight);
        }

        /// <summary>
        /// dgv重新排序后，自动折叠子视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView_Sorted(object sender, EventArgs e)
        {
            // 折叠已展开的行
            if (_subviewCurrentRow.Count > 0 && subview != null && subview.Visible)
            {
                int rowIndex = _subviewCurrentRow[0];
                _subviewCurrentRow.Clear();
                dataGridView.Rows[rowIndex].Height = dataGridView.RowTemplate.Height;
                dataGridView.Rows[rowIndex].DividerHeight = 0;
                dataGridView.Rows[rowIndex].Selected = true; // 保持选中状态
                subview.Visible = false;
            }

            // 更新排序配置
            if (dataGridView.SortedColumn != null)
            {
                string sortedColumnName = dataGridView.SortedColumn.Name;
                DgvColumnInfoConfig currentConfig = null;

                // 查找当前排序列配置
                foreach (var config in _columnInfos)
                {
                    if (config.Name == sortedColumnName)
                    {
                        currentConfig = config;
                        break;
                    }
                }

                if (currentConfig != null)
                {
                    // 保存旧的排序索引
                    int oldSortingIndex = currentConfig.SortingIndex;

                    // 更新当前列排序设置
                    switch (dataGridView.SortOrder)
                    {
                        case SortOrder.Ascending:
                            currentConfig.SortType = SortTypeDirection.Asc;
                            break;

                        case SortOrder.Descending:
                            currentConfig.SortType = SortTypeDirection.Desc;
                            break;
                    }

                    // 将当前列设为最高优先级
                    currentConfig.SortingIndex = 1;

                    // 重新计算其他列的排序索引（保留原始顺序）
                    var sortedConfigs = _columnInfos
                        .Where(c => c.SortType != SortTypeDirection.None &&
                                    c.Name != sortedColumnName)
                        .OrderBy(c => c.SortingIndex)
                        .ToList();

                    int indexCounter = 2;
                    foreach (var config in sortedConfigs)
                    {
                        // 跳过原始索引比当前列高的项
                        if (config.SortingIndex > oldSortingIndex) continue;

                        config.SortingIndex = indexCounter++;
                    }
                }

                // 延迟保存配置
                if (Guid != null)
                    ScheduleConfigSave();
            }
        }

        #endregion 系统样式和主从表格外观
    }
}