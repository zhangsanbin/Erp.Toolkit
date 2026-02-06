/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2025-04-19           Andy        the first version
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 自定义进度条单元格（带渐变和圆角效果）
    /// </summary>
    public class DgvProgressCell : DataGridViewTextBoxCell
    {
        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // 基础绘制（背景、边框等）
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                value, formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts & ~DataGridViewPaintParts.ContentForeground);

            // 获取进度值
            int progressVal = 0;
            if (value is int intValue)
            {
                progressVal = intValue;
            }
            else if (value != null)
            {
                // 获取类型代码以判断是否为数值类型
                var typeCode = Type.GetTypeCode(value.GetType());
                if (typeCode >= TypeCode.SByte && typeCode <= TypeCode.Decimal)
                {
                    // 将数值统一转换为double后截断处理
                    double doubleValue = Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);
                    progressVal = (int)doubleValue;
                }
                else
                {
                    // 非数值类型尝试转换为字符串解析
                    string stringValue = value.ToString();
                    if (double.TryParse(stringValue,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double parsedValue))
                    {
                        progressVal = (int)parsedValue;
                    }
                }
            }
            progressVal = Math.Max(0, Math.Min(progressVal, 100));

            // 获取颜色配置
            var progressColumn = this.OwningColumn as DgvProgressColumn;
            Color progressColor = progressVal < progressColumn.LowThreshold ? progressColumn.LowColor :
                progressVal < progressColumn.MediumThreshold ? progressColumn.MediumColor :
                progressColumn.HighColor;

            // 创建渐变颜色（带透明度）
            Color startColor = Color.FromArgb(220, progressColor);
            Color endColor = Color.FromArgb(80, progressColor);

            // 进度条尺寸（带边距）
            int margin = 3;
            int progressWidth = (int)((cellBounds.Width - 2 * margin) * (progressVal / 100.0));
            var progressRect = new Rectangle(
                cellBounds.X + margin,
                cellBounds.Y + 2,
                progressWidth,
                this.DataGridView.RowTemplate.Height - 2 * margin/*cellBounds.Height - 2 * margin*/);

            if (progressWidth > 0)
            {
                // 创建圆角路径
                using (var path = CreateRoundRect(progressRect, 3))
                using (var brush = new LinearGradientBrush(
                    progressRect,
                    startColor,
                    endColor,
                    LinearGradientMode.Horizontal))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.FillPath(brush, path);
                }
            }

            // 重新计算文本边界
            Rectangle textBounds = new Rectangle(
                cellBounds.X,
                cellBounds.Y,
                cellBounds.Width,
                this.DataGridView.RowTemplate.Height);

            // 绘制进度文本
            TextRenderer.DrawText(
                graphics,
                $"{progressVal}%",
                cellStyle.Font,
                textBounds,
                cellStyle.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // 创建圆角矩形路径
        private GraphicsPath CreateRoundRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            // 处理过小尺寸的情况
            if (rect.Width < diameter) diameter = rect.Width;
            if (rect.Height < diameter) diameter = rect.Height;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}