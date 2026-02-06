/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2025-10-29           Andy        the first version
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 优化显示性能的DataGridView控件
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    /// </remarks>
    public class OptimizedDataGridView : DataGridView
    {
        /// <summary>
        /// 画笔
        /// </summary>
        private Pen _pen;

        /// <summary>
        /// 缓存ColorBlend
        /// </summary>
        private ColorBlend _colorBlend;

        /// <summary>
        /// 系统样式
        /// </summary>
        private ThemeStyle _themeStyle = ThemeStyle.BlueTheme;

        /// <summary>
        /// 样式颜色
        /// </summary>
        private DgvThemeColors _themeColors;

        /// <summary>
        /// 默认字体
        /// </summary>
        private Font _defaultFont = new Font("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>
        /// 表头字体
        /// </summary>
        private Font _headerFont = new Font("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>
        /// 行表头字体
        /// </summary>
        private Font _rowHeaderFont = new Font("Segoe UI", 9f, FontStyle.Regular);

        /// <summary>
        /// 使用增强的渲染设置初始化 <see cref="OptimizedDataGridView"/> 类的新实例
        /// </summary>
        /// <remarks>此构造函数使用双缓冲和优化的绘制样式设置 <see cref="OptimizedDataGridView"/> 以提高渲染性能并减少闪烁。
        /// </remarks>
        public OptimizedDataGridView()
        {
            // 启用双缓冲和优化绘制
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();

            // 创建主题样式
            _themeColors = DgvThemeColors.GetColorsForTheme(_themeStyle);

            // 初始化主题样式
            SetThemeStyle(_themeColors);
        }

        /// <summary>
        /// 获取或设置 DataGridView 美好组件的当前系统样式。
        /// </summary>
        [DefaultValue(2)]
        [Browsable(true)]
        [Description("获取或设置 DataGridView 美好组件的当前系统样式")]
        public ThemeStyle ThemeStyle
        {
            get { return _themeStyle; }
            set
            {
                if (_themeStyle != value)
                {
                    _themeStyle = value;

                    _themeColors = DgvThemeColors.GetColorsForTheme(_themeStyle);

                    // 应用主题样式
                    SetThemeStyle(_themeColors);

                    // 使用BeginInvoke延迟重绘，避免频繁重绘
                    BeginInvoke(new Action(() => Invalidate(true)));
                }
            }
        }

        /// <summary>
        /// 获取或设置默认单元格字体
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("获取或设置默认单元格字体")]
        [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
        public Font DefaultCellFont
        {
            get { return _defaultFont; }
            set
            {
                if (_defaultFont != value)
                {
                    _defaultFont = value;
                    ApplyFontSettings();
                }
            }
        }

        /// <summary>
        /// 获取或设置列表头字体
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("获取或设置列表头字体")]
        [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
        public Font HeaderFont
        {
            get { return _headerFont; }
            set
            {
                if (_headerFont != value)
                {
                    _headerFont = value;
                    ApplyFontSettings();
                }
            }
        }

        /// <summary>
        /// 获取或设置行表头字体
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("获取或设置行表头字体")]
        [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
        public Font RowHeaderFont
        {
            get { return _rowHeaderFont; }
            set
            {
                if (_rowHeaderFont != value)
                {
                    _rowHeaderFont = value;
                    ApplyFontSettings();
                }
            }
        }

        /// <summary>
        /// 获取或设置字体抗锯齿模式
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("获取或设置字体抗锯齿模式")]
        [DefaultValue(System.Drawing.Text.TextRenderingHint.ClearTypeGridFit)]
        public System.Drawing.Text.TextRenderingHint TextRenderingHint { get; set; } = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

        /// <summary>
        /// 应用字体设置
        /// </summary>
        private void ApplyFontSettings()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(ApplyFontSettings));
                return;
            }

            SuspendLayout();
            try
            {
                // 应用默认单元格字体
                if (_defaultFont != null)
                {
                    this.DefaultCellStyle.Font = _defaultFont;
                }

                // 应用列表头字体
                if (_headerFont != null)
                {
                    this.ColumnHeadersDefaultCellStyle.Font = _headerFont;
                }

                // 应用行表头字体
                if (_rowHeaderFont != null)
                {
                    this.RowHeadersDefaultCellStyle.Font = _rowHeaderFont;
                }
            }
            finally
            {
                ResumeLayout(false);
            }
        }

        /// <summary>
        /// 初始化DGV系统颜色样式
        /// </summary>
        private void SetThemeStyle(DgvThemeColors themeColors)
        {
            if (themeColors != null)
            {
                // 批量设置属性，减少属性变更事件
                SuspendLayout();

                try
                {
                    // 设置背景色
                    this.BackColor = _themeColors.BackgroundColor;
                    this.BackgroundColor = themeColors.BackgroundColor;
                    this.DefaultCellStyle.BackColor = themeColors.BackColor;

                    // 设置奇偶行背景色
                    // this.AlternatingRowsDefaultCellStyle.BackColor = themeColors.AlternatingRowsBackColor;

                    // 设置网格边框颜色
                    this.GridColor = themeColors.GridColor;

                    // 设置 DGV SelectionBackColor
                    this.DefaultCellStyle.SelectionBackColor = themeColors.SelectionBackColor;
                    this.ColumnHeadersDefaultCellStyle.SelectionBackColor = themeColors.SelectionBackColor;
                    this.RowHeadersDefaultCellStyle.SelectionBackColor = themeColors.SelectionBackColor;

                    // 设置字体颜色
                    this.DefaultCellStyle.ForeColor = themeColors.FontColor;
                    this.DefaultCellStyle.SelectionForeColor = themeColors.SelectionFontColor;
                    this.ColumnHeadersDefaultCellStyle.ForeColor = themeColors.CellHeaderFontColor;
                    this.ColumnHeadersDefaultCellStyle.SelectionForeColor = themeColors.SelectionFontColor;
                    this.RowHeadersDefaultCellStyle.ForeColor = themeColors.FontColor;
                    this.RowHeadersDefaultCellStyle.SelectionForeColor = themeColors.SelectionFontColor;

                    // 应用字体设置
                    ApplyFontSettings();
                }
                finally
                {
                    ResumeLayout(false);
                }

                // 创建表头的渐变颜色数组
                _colorBlend = new ColorBlend
                {
                    Colors = new Color[3] { themeColors.GradientLightColor, themeColors.GradientDarkColor, themeColors.GradientLightColor },
                    Positions = new float[3] { 0f, 0.5f, 1f }
                };

                // 初始化画笔
                _pen?.Dispose();
                _pen = new Pen(themeColors.CellHeaderBorderColor, 0.5f);
            }
        }

        /// <summary>
        /// 重写单元格绘制事件以实现自定义绘制逻辑
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            // 暂停布局以提高性能
            SuspendLayout();

            try
            {
                // 使用离屏位图进行双缓冲绘制
                using (Bitmap tempBitmap = new Bitmap(e.CellBounds.Width, e.CellBounds.Height, e.Graphics))
                using (Graphics tempG = Graphics.FromImage(tempBitmap))
                {
                    // 设置抗锯齿模式
                    tempG.TextRenderingHint = TextRenderingHint;

                    // 如果是进度条列，使用自定义绘制
                    if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && Columns[e.ColumnIndex] is DgvProgressColumn)
                    {
                        bool rtl = RightToLeft == RightToLeft.Yes;

                        tempG.Clear(e.CellStyle.BackColor);

                        // 绘制单元格背景和边框
                        DrawCellBackground(tempG, e, tempBitmap.Size);
                        DrawCellBorder(tempG, e, tempBitmap.Size);

                        // 绘制进度条单元格
                        DrawProgressCell(tempG, e, tempBitmap.Size, rtl);

                        // 将离屏位图绘制到屏幕
                        e.Graphics.DrawImage(tempBitmap, e.CellBounds.Location);
                        e.Handled = true;
                    }
                    else if (e.RowIndex == -1 && e.ColumnIndex >= -1)
                    {
                        // 对于列标题行
                        DrawColumnHeaderBorder(e);

                        // 将离屏位图绘制到屏幕
                        e.Graphics.DrawImage(tempBitmap, e.CellBounds.Location);
                        e.Handled = true;
                    }
                    else if (e.ColumnIndex < 0 && e.RowIndex >= 0)
                    {
                        // 对于行标题列
                        DrawRowHeaderBorder(e);

                        // 将离屏位图绘制到屏幕
                        e.Graphics.DrawImage(tempBitmap, e.CellBounds.Location);
                        e.Handled = true;
                    }
                    else
                    {
                        // 其他区域，使用系统默认绘制
                        base.OnCellPainting(e);
                        return; // 基类已处理，直接返回
                    }

                    // 绘制焦点矩形（如果有）
                    if ((e.PaintParts & DataGridViewPaintParts.Focus) == DataGridViewPaintParts.Focus)
                    {
                        DrawFocusRectangle(e);
                    }
                }
            }
            finally
            {
                ResumeLayout(false);
            }
        }

        /// <summary>
        /// 绘制，行表头边框
        /// </summary>
        /// <param name="e"></param>
        private void DrawRowHeaderBorder(DataGridViewCellPaintingEventArgs e)
        {
            Rectangle adjustedBounds = e.CellBounds;
            adjustedBounds.Offset(-1, -1);

            // 绘制背景色（带有渐变效果）
            using (LinearGradientBrush backBrush = new LinearGradientBrush(
                e.CellBounds,
                _themeColors.GradientLightColor,
                _themeColors.GradientLightColor,
                LinearGradientMode.Vertical))
            {
                backBrush.InterpolationColors = _colorBlend;
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
            }

            // 绘制边框
            e.Graphics.DrawRectangle(_pen, adjustedBounds);

            // 绘制 Row 的 Text 信息（箭头）
            e.PaintContent(e.CellBounds);
        }

        /// <summary>
        /// 绘制，列表头边框
        /// </summary>
        /// <param name="e"></param>
        private void DrawColumnHeaderBorder(DataGridViewCellPaintingEventArgs e)
        {
            Rectangle adjustedBounds = e.CellBounds;
            adjustedBounds.Inflate(0, -1);
            adjustedBounds.Offset(-1, 0);

            // 绘制背景色（带有渐变效果）
            using (LinearGradientBrush backBrush = new LinearGradientBrush(
                new Rectangle(1, 31, 48, 26),
                _themeColors.GradientLightColor,
                _themeColors.GradientLightColor,
                LinearGradientMode.Vertical))
            {
                backBrush.InterpolationColors = _colorBlend;
                e.Graphics.FillRectangle(backBrush, adjustedBounds);
            }

            // 绘制边框
            e.Graphics.DrawRectangle(_pen, adjustedBounds);

            // 绘制 Column 的 Text 信息（标题）
            e.PaintContent(e.CellBounds);
        }

        /// <summary>
        /// 绘制单元格背景
        /// </summary>
        /// <param name="g"></param>
        /// <param name="e"></param>
        /// <param name="cellSize"></param>
        private void DrawCellBackground(Graphics g, DataGridViewCellPaintingEventArgs e, Size cellSize)
        {
            Rectangle backBounds = new Rectangle(0, 0, cellSize.Width, cellSize.Height);

            // 根据单元格状态选择背景色
            Color backColor = GetCellBackgroundColor(e);
            using (Brush backBrush = new SolidBrush(backColor))
            {
                g.FillRectangle(backBrush, backBounds);
            }
        }

        /// <summary>
        /// 绘制单元格边框
        /// </summary>
        /// <param name="g"></param>
        /// <param name="e"></param>
        /// <param name="cellSize"></param>
        private void DrawCellBorder(Graphics g, DataGridViewCellPaintingEventArgs e, Size cellSize)
        {
            Rectangle borderBounds = new Rectangle(-1, -1, cellSize.Width, cellSize.Height);

            Color borderColor = GetCellBorderColor(e);

            // 使用更细的线条宽度（0.1f）
            using (Pen borderPen = new Pen(borderColor, 0.1f))
            {
                // 设置线条对齐方式，避免线条向外扩展
                borderPen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                g.DrawRectangle(borderPen, borderBounds);
            }
        }

        /// <summary>
        /// 绘制进度条单元格
        /// </summary>
        /// <param name="g"></param>
        /// <param name="e"></param>
        /// <param name="cellSize"></param>
        /// <param name="rtl"></param>
        private void DrawProgressCell(Graphics g, DataGridViewCellPaintingEventArgs e, Size cellSize, bool rtl)
        {
            // 获取进度值
            int progressVal = 0;
            if (e.Value is int intValue)
            {
                progressVal = intValue;
            }
            else if (e.Value != null)
            {
                var typeCode = Type.GetTypeCode(e.Value.GetType());
                if (typeCode >= TypeCode.SByte && typeCode <= TypeCode.Decimal)
                {
                    double doubleValue = Convert.ToDouble(e.Value, System.Globalization.CultureInfo.InvariantCulture);
                    progressVal = (int)doubleValue;
                }
                else
                {
                    string stringValue = e.Value.ToString();
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
            var progressColumn = Columns[e.ColumnIndex] as DgvProgressColumn;
            Color progressColor = progressVal < progressColumn.LowThreshold ? progressColumn.LowColor :
                progressVal < progressColumn.MediumThreshold ? progressColumn.MediumColor :
                progressColumn.HighColor;

            // 创建渐变颜色（带透明度）
            Color startColor = Color.FromArgb(220, progressColor);
            Color endColor = Color.FromArgb(80, progressColor);

            // 进度条尺寸（带边距）
            int margin = 3;
            int progressWidth = (int)((cellSize.Width - 2 * margin) * (progressVal / 100.0));
            var progressRect = new Rectangle(
                margin,
                2,
                progressWidth,
                RowTemplate.Height - 2 * margin);

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
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillPath(brush, path);
                }
            }

            // 绘制进度文本
            string progressText = $"{progressVal}%";

            // 创建字符串格式
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;
            format.FormatFlags = StringFormatFlags.LineLimit;

            // 处理从右到左
            if (rtl)
            {
                format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }

            Rectangle textBounds = new Rectangle(0, 0, cellSize.Width, RowTemplate.Height);

            using (Brush textBrush = new SolidBrush(GetCellTextColor(e)))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.DrawString(progressText, e.CellStyle.Font, textBrush, textBounds, format);
            }
        }

        /// <summary>
        /// 创建圆角矩形路径（从DgvProgressCell复制过来）
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private System.Drawing.Drawing2D.GraphicsPath CreateRoundRect(Rectangle rect, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
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

        /// <summary>
        /// 绘制焦点矩形
        /// </summary>
        /// <param name="e"></param>
        private void DrawFocusRectangle(DataGridViewCellPaintingEventArgs e)
        {
            if (ShowFocusCues && Focused &&
                e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                CurrentCellAddress.X == e.ColumnIndex &&
                CurrentCellAddress.Y == e.RowIndex)
            {
                Rectangle focusBounds = e.CellBounds;
                focusBounds.Width--;
                focusBounds.Height--;

                if (RightToLeft == RightToLeft.Yes)
                    focusBounds.X++;

                ControlPaint.DrawFocusRectangle(e.Graphics, focusBounds);
            }
        }

        /// <summary>
        /// 获取单元格背景颜色（辅助方法）
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Color GetCellBackgroundColor(DataGridViewCellPaintingEventArgs e)
        {
            if (e.State.HasFlag(DataGridViewElementStates.Selected))
                return e.CellStyle.SelectionBackColor;

            if (e.RowIndex % 2 == 0 && AlternatingRowsDefaultCellStyle != null)
                return AlternatingRowsDefaultCellStyle.BackColor;

            return e.CellStyle.BackColor;
        }

        /// <summary>
        /// 获取单元格边框颜色（辅助方法）
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Color GetCellBorderColor(DataGridViewCellPaintingEventArgs e)
        {
            // 跳过选中状态的边框颜色变化以提升性能
            // return e.State.HasFlag(DataGridViewElementStates.Selected) ? Color.DarkBlue : Color.LightGray;
            return Color.LightGray;
        }

        /// <summary>
        /// 获取单元格文本颜色（辅助方法）
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private Color GetCellTextColor(DataGridViewCellPaintingEventArgs e)
        {
            return e.State.HasFlag(DataGridViewElementStates.Selected) ?
                e.CellStyle.SelectionForeColor : e.CellStyle.ForeColor;
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pen?.Dispose();
                _defaultFont?.Dispose();
                _headerFont?.Dispose();
                _rowHeaderFont?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}