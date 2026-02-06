/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-01-10           Andy        Split, restructure
 * 2025-12-11           Andy        Code refactoring for better maintainability
 * 2025-12-15           Andy        Allocate the remaining space to the last column
 * 2025-12-16           Andy        Add footer with page numbers
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    public partial class Dgv
    {
        #region 打印相关字段

        /// <summary>
        /// 要打印的项目（行和列索引）
        /// </summary>
        private PrintItems _printItems;

        /// <summary>
        /// 已打印的行（避免重复打印）
        /// </summary>
        private readonly List<DataGridViewRow> _printedRows = new List<DataGridViewRow>();

        /// <summary>
        /// 计算出的列宽信息（用于统一表头和行）
        /// </summary>
        private List<ColumnWidthInfo> _calculatedColumnWidths;

        /// <summary>
        /// 当前页码
        /// </summary>
        private int _currentPageNumber;

        /// <summary>
        /// 页脚高度
        /// </summary>
        private const float FOOTER_HEIGHT = 40f;

        #endregion 打印相关字段

        #region 打印辅助类

        /// <summary>
        /// 打印项目容器
        /// </summary>
        private class PrintItems
        {
            public List<DataGridViewRow> Rows { get; }
            public int[] ColumnIndices { get; }

            public PrintItems(List<DataGridViewRow> rows, int[] columnIndices)
            {
                Rows = rows ?? throw new ArgumentNullException(nameof(rows));
                ColumnIndices = columnIndices ?? throw new ArgumentNullException(nameof(columnIndices));
            }
        }

        /// <summary>
        /// 列宽信息
        /// </summary>
        private class ColumnWidthInfo
        {
            public DataGridViewColumn Column { get; set; }
            public float Width { get; set; }
            public bool IsLastColumn { get; set; }
        }

        #endregion 打印辅助类

        #region 打印事件处理

        /// <summary>
        /// 开始打印事件处理
        /// </summary>
        private void PrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            // 重置页码计数
            _currentPageNumber = 0;

            // 获取打印项目
            _printItems = GetPrintItems();
        }

        /// <summary>
        /// 结束打印事件处理
        /// </summary>
        private void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            // 重置状态
            _printedRows.Clear();
            _printItems = null;
            _calculatedColumnWidths = null;
            _currentPageNumber = 0;
        }

        /// <summary>
        /// 处理打印页面事件
        /// </summary>
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // 如果打印项目未初始化，则初始化
            if (_printItems == null)
            {
                _printItems = GetPrintItems();
            }

            // 更新当前页码
            _currentPageNumber++;

            // 移除已打印的行
            RemovePrintedRows();

            var rowsToPrint = _printItems.Rows;
            var columnIndices = _printItems.ColumnIndices;
            var marginBounds = e.MarginBounds;

            // 计算起始位置（考虑页脚空间）
            float currentY = marginBounds.Top;
            float maxY = marginBounds.Bottom - FOOTER_HEIGHT;

            // 每页都绘制标题（优化：不再只在第一页绘制）
            currentY = DrawTitle(e.Graphics, marginBounds, currentY);

            // 计算列宽（在绘制表头之前）
            _calculatedColumnWidths = CalculateColumnWidths(marginBounds.Width, columnIndices);

            // 绘制表头
            currentY = DrawHeader(e.Graphics, marginBounds, columnIndices, currentY);

            // 绘制数据行
            var result = DrawRows(e.Graphics, marginBounds, rowsToPrint, columnIndices, currentY, maxY, e);

            // 绘制页脚
            DrawFooter(e.Graphics, marginBounds, e);

            // 更新打印状态
            UpdatePrintState(result, rowsToPrint, e);

            // 为预览模式重置状态
            ResetForPreview(e);
        }

        #endregion 打印事件处理

        #region 打印核心方法

        /// <summary>
        /// 获取要打印的行和列
        /// </summary>
        private PrintItems GetPrintItems()
        {
            // 获取倒序排列的选中行
            var selectedRows = dataGridView.SelectedRows
                .Cast<DataGridViewRow>()
                .Reverse()
                .ToList();

            // 如果未设置打印列，初始化默认打印列
            if (_printColumns == null || _printColumns.Length == 0)
            {
                _printColumns = Enumerable.Range(0, dataGridView.Columns.Count).ToArray();
            }

            // 重置已打印行跟踪
            _printedRows.Clear();

            return new PrintItems(selectedRows, _printColumns);
        }

        /// <summary>
        /// 从打印项目中移除已打印的行
        /// </summary>
        private void RemovePrintedRows()
        {
            if (_printItems == null) return;

            foreach (var printedRow in _printedRows)
            {
                _printItems.Rows.Remove(printedRow);
            }
        }

        #endregion 打印核心方法

        #region 绘制方法

        /// <summary>
        /// 绘制文档标题
        /// </summary>
        private float DrawTitle(Graphics graphics, RectangleF margins, float startY)
        {
            float titleHeight = dataGridView.ColumnHeadersHeight;
            var titleRect = new RectangleF(
                margins.Left,
                startY,
                margins.Width,
                titleHeight);

            var originalFont = dataGridView.Font;
            float titleFontSize = originalFont.Size + 5f;

            using (var titleFont = new Font(originalFont.FontFamily, titleFontSize, FontStyle.Bold))
            using (var titleFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                var title = _printDocument?.DocumentName ?? string.Empty;
                graphics.DrawString(title, titleFont, Brushes.Black, titleRect, titleFormat);
            }

            return startY + titleHeight;
        }

        /// <summary>
        /// 绘制列标题
        /// </summary>
        private float DrawHeader(Graphics graphics, RectangleF margins, int[] columnIndices, float startY)
        {
            float headerHeight = dataGridView.ColumnHeadersHeight;
            var headerRect = new RectangleF(
                margins.Left,
                startY,
                margins.Width,
                headerHeight);

            // 绘制背景
            graphics.FillRectangle(Brushes.LightGray, headerRect);

            // 绘制表头单元格和边框
            DrawHeaderCells(graphics, headerRect, columnIndices);

            return startY + headerHeight;
        }

        /// <summary>
        /// 计算列宽（考虑剩余空间分配）
        /// </summary>
        private List<ColumnWidthInfo> CalculateColumnWidths(float availableWidth, int[] columnIndices)
        {
            var columnWidths = new List<ColumnWidthInfo>();
            var columns = dataGridView.Columns;

            // 1. 收集所有可打印的列
            var printableColumns = new List<DataGridViewColumn>();
            foreach (DataGridViewColumn column in columns)
            {
                if (IsColumnToPrint(column.Index, columnIndices))
                {
                    printableColumns.Add(column);
                }
            }

            if (printableColumns.Count == 0)
                return columnWidths;

            // 2. 计算原始总宽度
            float totalOriginalWidth = 0f;
            foreach (var column in printableColumns)
            {
                totalOriginalWidth += column.Width;
            }

            // 3. 处理两种情况：
            //    a. 所有列都能放下：将剩余空间分配给最后一列
            //    b. 列太多放不下：只取能放下的列，并将剩余空间分配给最后一列
            float currentWidth = 0f;
            float remainingSpace = 0f;

            if (totalOriginalWidth <= availableWidth)
            {
                // 情况a：所有列都能放下
                foreach (var column in printableColumns)
                {
                    columnWidths.Add(new ColumnWidthInfo
                    {
                        Column = column,
                        Width = column.Width,
                        IsLastColumn = false
                    });
                    currentWidth += column.Width;
                }

                // 计算剩余空间
                remainingSpace = availableWidth - currentWidth;

                // 将剩余空间分配给最后一列
                if (columnWidths.Count > 0 && remainingSpace > 0)
                {
                    var lastColumn = columnWidths[columnWidths.Count - 1];
                    lastColumn.Width += remainingSpace;
                    lastColumn.IsLastColumn = true;
                }
            }
            else
            {
                // 情况b：列太多放不下，只取能放下的列
                foreach (var column in printableColumns)
                {
                    float columnWidth = column.Width;

                    // 检查加上这一列是否会超出可用宽度
                    if (currentWidth + columnWidth > availableWidth)
                    {
                        // 如果这一列会导致超出，跳过这一列，结束
                        break;
                    }

                    columnWidths.Add(new ColumnWidthInfo
                    {
                        Column = column,
                        Width = columnWidth,
                        IsLastColumn = false
                    });
                    currentWidth += columnWidth;
                }

                // 如果至少有1列能放下
                if (columnWidths.Count > 0)
                {
                    // 计算剩余空间
                    remainingSpace = availableWidth - currentWidth;

                    // 将剩余空间分配给最后一列
                    if (remainingSpace > 0)
                    {
                        var lastColumn = columnWidths[columnWidths.Count - 1];
                        lastColumn.Width += remainingSpace;
                        lastColumn.IsLastColumn = true;
                    }
                }
            }

            return columnWidths;
        }

        /// <summary>
        /// 绘制表头单元格（居中对齐）
        /// </summary>
        private void DrawHeaderCells(Graphics graphics, RectangleF containerRect, int[] columnIndices)
        {
            if (_calculatedColumnWidths == null || _calculatedColumnWidths.Count == 0)
                return;

            using (var borderPen = new Pen(Color.Black))
            using (var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center, // 表头水平居中
                LineAlignment = StringAlignment.Center, // 垂直居中
                FormatFlags = StringFormatFlags.NoWrap, // 不换行
                Trimming = StringTrimming.EllipsisCharacter // 过长文本用...截断
            })
            {
                float currentX = containerRect.Left;
                int columnCount = _calculatedColumnWidths.Count;

                for (int i = 0; i < columnCount; i++)
                {
                    var columnInfo = _calculatedColumnWidths[i];
                    float cellWidth = columnInfo.Width;

                    // 创建绘制矩形，留出边距
                    var cellRect = new RectangleF(
                        currentX + 2f,
                        containerRect.Top + 2f,
                        cellWidth - 4f,
                        containerRect.Height - 4f);

                    // 设置裁剪区域
                    var clipRect = new RectangleF(
                        currentX,
                        containerRect.Top,
                        cellWidth,
                        containerRect.Height);
                    graphics.SetClip(clipRect);

                    try
                    {
                        // 绘制表头文本
                        graphics.DrawString(columnInfo.Column.HeaderText, dataGridView.Font, Brushes.Black, cellRect, stringFormat);
                    }
                    finally
                    {
                        graphics.ResetClip();
                    }

                    // 绘制列边框（竖线）
                    graphics.DrawLine(borderPen,
                        currentX, containerRect.Top,
                        currentX, containerRect.Bottom);

                    currentX += cellWidth;
                }

                // 绘制最右侧边框
                graphics.DrawLine(borderPen,
                    containerRect.Right, containerRect.Top,
                    containerRect.Right, containerRect.Bottom);

                // 绘制顶部边框
                graphics.DrawLine(borderPen,
                    containerRect.Left, containerRect.Top,
                    containerRect.Right, containerRect.Top);

                // 绘制底部边框
                graphics.DrawLine(borderPen,
                    containerRect.Left, containerRect.Bottom,
                    containerRect.Right, containerRect.Bottom);
            }
        }

        /// <summary>
        /// 绘制数据行（支持分页）
        /// </summary>
        private (int rowsPrinted, bool hasMorePages) DrawRows(Graphics graphics,
            RectangleF margins, List<DataGridViewRow> rows, int[] columnIndices,
            float startY, float maxY, PrintPageEventArgs e)
        {
            float rowHeight = dataGridView.RowTemplate.Height;
            float currentY = startY;
            int rowsPrinted = 0;

            foreach (var row in rows)
            {
                // 检查是否需要新页面
                if (currentY + rowHeight > maxY)
                {
                    return (rowsPrinted, true);
                }

                var rowRect = new RectangleF(
                    margins.Left,
                    currentY,
                    margins.Width,
                    rowHeight);

                // 绘制行单元格和边框
                DrawRowCells(graphics, rowRect, row, columnIndices);

                currentY += rowHeight;
                rowsPrinted++;
                _printedRows.Add(row);
            }

            return (rowsPrinted, false);
        }

        /// <summary>
        /// 绘制数据行单元格（对齐方式与DGV单元格匹配）
        /// </summary>
        private void DrawRowCells(Graphics graphics, RectangleF containerRect,
            DataGridViewRow row, int[] columnIndices)
        {
            if (_calculatedColumnWidths == null || _calculatedColumnWidths.Count == 0)
                return;

            using (var borderPen = new Pen(Color.Black))
            {
                float currentX = containerRect.Left;
                int columnCount = _calculatedColumnWidths.Count;

                for (int i = 0; i < columnCount; i++)
                {
                    var columnInfo = _calculatedColumnWidths[i];
                    float cellWidth = columnInfo.Width;

                    // 获取单元格样式（包括对齐方式）
                    var cell = row.Cells[columnInfo.Column.Index];
                    var cellStyle = cell.InheritedStyle ?? columnInfo.Column.DefaultCellStyle;

                    // 创建绘制矩形，留出边距
                    var cellRect = new RectangleF(
                        currentX + 2f,
                        containerRect.Top + 2f,
                        cellWidth - 4f,
                        containerRect.Height - 4f);

                    // 设置裁剪区域
                    var clipRect = new RectangleF(
                        currentX,
                        containerRect.Top,
                        cellWidth,
                        containerRect.Height);
                    graphics.SetClip(clipRect);

                    try
                    {
                        // 获取水平对齐方式
                        var horizontalAlignment = GetHorizontalAlignment(cellStyle.Alignment);

                        // 创建字符串格式
                        using (var stringFormat = new StringFormat
                        {
                            Alignment = horizontalAlignment, // 使用单元格的对齐方式
                            LineAlignment = StringAlignment.Center, // 垂直居中
                            FormatFlags = StringFormatFlags.NoWrap, // 不换行
                            Trimming = StringTrimming.EllipsisCharacter // 过长文本用...截断
                        })
                        {
                            // 绘制单元格文本
                            var cellText = cell.Value?.ToString() ?? string.Empty;
                            graphics.DrawString(cellText, dataGridView.Font, Brushes.Black, cellRect, stringFormat);
                        }
                    }
                    finally
                    {
                        graphics.ResetClip();
                    }

                    // 绘制列边框（竖线）
                    graphics.DrawLine(borderPen,
                        currentX, containerRect.Top,
                        currentX, containerRect.Bottom);

                    currentX += cellWidth;
                }

                // 绘制最右侧边框
                graphics.DrawLine(borderPen,
                    containerRect.Right, containerRect.Top,
                    containerRect.Right, containerRect.Bottom);

                // 绘制底部边框
                graphics.DrawLine(borderPen,
                    containerRect.Left, containerRect.Bottom,
                    containerRect.Right, containerRect.Bottom);
            }
        }

        /// <summary>
        /// 将DataGridViewContentAlignment转换为StringAlignment（用于水平对齐）
        /// </summary>
        private StringAlignment GetHorizontalAlignment(DataGridViewContentAlignment alignment)
        {
            switch (alignment)
            {
                case DataGridViewContentAlignment.TopLeft:
                case DataGridViewContentAlignment.MiddleLeft:
                case DataGridViewContentAlignment.BottomLeft:
                    return StringAlignment.Near; // 左对齐

                case DataGridViewContentAlignment.TopCenter:
                case DataGridViewContentAlignment.MiddleCenter:
                case DataGridViewContentAlignment.BottomCenter:
                    return StringAlignment.Center; // 居中

                case DataGridViewContentAlignment.TopRight:
                case DataGridViewContentAlignment.MiddleRight:
                case DataGridViewContentAlignment.BottomRight:
                    return StringAlignment.Far; // 右对齐

                default:
                    // 默认情况下，如果未设置或为NotSet，使用列的默认对齐方式
                    // 如果列也未设置，则使用左对齐
                    return StringAlignment.Near;
            }
        }

        #endregion 绘制方法

        #region 页脚相关

        /// <summary>
        /// 绘制页脚信息
        /// </summary>
        private void DrawFooter(Graphics graphics, RectangleF margins, PrintPageEventArgs e)
        {
            var footerRect = new RectangleF(
                margins.Left,
                margins.Bottom - FOOTER_HEIGHT,
                margins.Width,
                FOOTER_HEIGHT);

            // 获取打印时间
            var printTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // 准备页脚文本
            string footerText;
            if (!string.IsNullOrEmpty(_printDocument?.DocumentName))
            {
                footerText = $"{_printDocument.DocumentName} | Page {_currentPageNumber} | {localizer.GetString("PrintTime")}：{printTime}";
            }
            else
            {
                footerText = $"Page {_currentPageNumber} | {localizer.GetString("PrintTime")}：{printTime}";
            }

            using (var footerFont = new Font(dataGridView.Font.FontFamily, 9, FontStyle.Regular))
            using (var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                // 绘制页脚文本
                graphics.DrawString(footerText, footerFont, Brushes.Black, footerRect, stringFormat);

                // 绘制分隔线
                using (var pen = new Pen(Color.LightGray, 0.5f))
                {
                    float lineY = footerRect.Top + 5;
                    graphics.DrawLine(pen,
                        margins.Left, lineY,
                        margins.Right, lineY);
                }
            }
        }

        #endregion 页脚相关

        #region 打印状态管理

        /// <summary>
        /// 根据绘制结果更新打印状态
        /// </summary>
        private void UpdatePrintState((int rowsPrinted, bool hasMorePages) result,
            List<DataGridViewRow> rowsToPrint, PrintPageEventArgs e)
        {
            e.HasMorePages = result.hasMorePages;

            // 如果没有更多页面，重置页码
            if (!result.hasMorePages)
            {
                _currentPageNumber = 0;
                _printedRows.Clear();
            }
        }

        /// <summary>
        /// 为预览模式重置状态
        /// </summary>
        private void ResetForPreview(PrintPageEventArgs e)
        {
            if (e.HasMorePages) return;

            _printedRows.Clear();
            _printItems = GetPrintItems();
            _calculatedColumnWidths = null;
            _currentPageNumber = 0;
        }

        #endregion 打印状态管理

        #region 辅助方法

        /// <summary>
        /// 检查列是否应该打印
        /// </summary>
        private bool IsColumnToPrint(int columnIndex, int[] columnIndices)
        {
            return Array.IndexOf(columnIndices, columnIndex) >= 0;
        }

        #endregion 辅助方法
    }
}