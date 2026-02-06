/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-07-16           Andy        Split, restructure
 * 2025-10-30           Andy        Add full support for ToolStripMenuItem and other standard controls
 */

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 自定义 ToolStrip 渲染器配置选项
    /// </summary>
    public class DgvRendererOptions
    {
        #region 背景渐变设置

        /// <summary>
        /// 背景渐变开始颜色
        /// </summary>
        public Color BackgroundGradientStart { get; set; } = Color.FromArgb(219, 232, 247);

        /// <summary>
        /// 背景渐变结束颜色
        /// </summary>
        public Color BackgroundGradientEnd { get; set; } = Color.FromArgb(188, 206, 230);

        /// <summary>
        /// 菜单背景颜色
        /// </summary>
        public Color MenuBackgroundColor { get; set; } = Color.FromArgb(0, 188, 206, 230);// 使其透明

        /// <summary>
        /// 菜单项背景颜色
        /// </summary>
        public Color MenuItemBackgroundColor { get; set; } = Color.White;

        /// <summary>
        /// 禁用菜单项背景颜色
        /// </summary>
        public Color DisabledMenuItemBackgroundColor { get; set; } = Color.Transparent;

        /// <summary>
        /// 渐变方向
        /// </summary>
        public LinearGradientMode GradientDirection { get; set; } = LinearGradientMode.Vertical;

        /// <summary>
        /// 是否启用背景渐变
        /// </summary>
        public bool EnableBackgroundGradient { get; set; } = true;

        #endregion 背景渐变设置

        #region 按钮设置

        /// <summary>
        /// 按钮默认颜色
        /// </summary>
        public Color ButtonColor { get; set; } = Color.FromArgb(40, 219, 232, 247);

        /// <summary>
        /// 按钮悬停颜色
        /// </summary>
        public Color ButtonHoverColor { get; set; } = Color.FromArgb(255, 247, 235, 178);

        /// <summary>
        /// 按钮按下颜色
        /// </summary>
        public Color ButtonPressedColor { get; set; } = Color.FromArgb(255, 247, 225, 138);

        /// <summary>
        /// 菜单项悬停渐变开始颜色
        /// </summary>
        public Color MenuItemHoverGradientStart { get; set; } = Color.FromArgb(255, 251, 245, 217);

        /// <summary>
        /// 菜单项悬停渐变结束颜色
        /// </summary>
        public Color MenuItemHoverGradientEnd { get; set; } = Color.FromArgb(255, 247, 225, 138);

        /// <summary>
        /// 菜单项按下渐变开始颜色
        /// </summary>
        public Color MenuItemPressedGradientStart { get; set; } = Color.FromArgb(255, 247, 235, 178);

        /// <summary>
        /// 菜单项按下渐变结束颜色
        /// </summary>
        public Color MenuItemPressedGradientEnd { get; set; } = Color.FromArgb(255, 247, 225, 138);

        /// <summary>
        /// 是否启用菜单项渐变效果
        /// </summary>
        public bool EnableMenuItemGradient { get; set; } = true;

        /// <summary>
        /// 按钮圆角半径
        /// </summary>
        public int ButtonCornerRadius { get; set; } = 1;

        /// <summary>
        /// 菜单项圆角半径
        /// </summary>
        public int MenuItemCornerRadius { get; set; } = 1;

        /// <summary>
        /// 是否启用按钮圆角
        /// </summary>
        public bool EnableButtonRoundedCorners { get; set; } = true;

        /// <summary>
        /// 是否启用菜单项圆角
        /// </summary>
        public bool EnableMenuItemRoundedCorners { get; set; } = true;

        #endregion 按钮设置

        #region 文本和边框

        /// <summary>
        /// 文本颜色
        /// </summary>
        public Color TextColor { get; set; } = Color.FromArgb(30, 30, 30);

        /// <summary>
        /// 菜单文本颜色
        /// </summary>
        public Color MenuTextColor { get; set; } = Color.FromArgb(30, 30, 30);

        /// <summary>
        /// 选中文本颜色
        /// </summary>
        public Color SelectedTextColor { get; set; } = Color.FromArgb(0, 0, 0);

        /// <summary>
        /// 禁用文本颜色
        /// </summary>
        public Color DisabledTextColor { get; set; } = Color.FromArgb(128, 128, 128);

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor { get; set; } = Color.FromArgb(200, 200, 200);

        /// <summary>
        /// 菜单边框颜色
        /// </summary>
        public Color MenuBorderColor { get; set; } = Color.FromArgb(128, 200, 200, 200);

        /// <summary>
        /// 箭头颜色
        /// </summary>
        public Color ArrowColor { get; set; } = Color.FromArgb(120, 120, 120);

        /// <summary>
        /// 选中箭头颜色
        /// </summary>
        public Color SelectedArrowColor { get; set; } = Color.FromArgb(80, 80, 80);

        /// <summary>
        /// 禁用箭头颜色
        /// </summary>
        public Color DisabledArrowColor { get; set; } = Color.FromArgb(180, 180, 180);

        /// <summary>
        /// 边框宽度
        /// </summary>
        public float BorderWidth { get; set; } = 1f;

        /// <summary>
        /// 菜单边框宽度
        /// </summary>
        public float MenuBorderWidth { get; set; } = 1f;

        /// <summary>
        /// 是否启用文本阴影效果
        /// </summary>
        public bool EnableTextShadow { get; set; } = false;

        /// <summary>
        /// 复选框颜色
        /// </summary>
        public Color CheckBoxColor { get; set; } = Color.FromArgb(100, 100, 100);

        /// <summary>
        /// 选中复选框颜色
        /// </summary>
        public Color CheckBoxSelectedColor { get; set; } = Color.FromArgb(0, 120, 215);

        /// <summary>
        /// 禁用复选框颜色
        /// </summary>
        public Color DisabledCheckBoxColor { get; set; } = Color.FromArgb(180, 180, 180);

        #endregion 文本和边框

        #region 阴影和光泽效果

        /// <summary>
        /// 是否启用按钮光泽效果
        /// </summary>
        public bool EnableButtonGloss { get; set; } = true;

        /// <summary>
        /// 是否启用菜单项光泽效果
        /// </summary>
        public bool EnableMenuItemGloss { get; set; } = false;

        /// <summary>
        /// 光泽效果高度比例 (0.0 - 1.0)
        /// </summary>
        public float GlossHeightRatio { get; set; } = 0.4f;

        /// <summary>
        /// 光泽效果透明度
        /// </summary>
        public int GlossOpacity { get; set; } = 60;

        #endregion 阴影和光泽效果

        #region 分隔符和把手

        /// <summary>
        /// 分隔符颜色
        /// </summary>
        public Color SeparatorColor { get; set; } = Color.FromArgb(128, 128, 128);

        /// <summary>
        /// 菜单分隔符颜色
        /// </summary>
        public Color MenuSeparatorColor { get; set; } = Color.FromArgb(220, 220, 220);

        /// <summary>
        /// 分隔符宽度
        /// </summary>
        public float SeparatorWidth { get; set; } = 0.5f;

        /// <summary>
        /// 菜单分隔符宽度
        /// </summary>
        public float MenuSeparatorWidth { get; set; } = 0.5f;

        /// <summary>
        /// 分隔符边距（像素）
        /// </summary>
        public int SeparatorMargin { get; set; } = 6;

        /// <summary>
        /// 菜单分隔符边距（像素）
        /// </summary>
        public int MenuSeparatorMargin { get; set; } = 8;

        /// <summary>
        /// 是否启用分隔符虚线样式
        /// </summary>
        public bool EnableSeparatorDashed { get; set; } = true;

        /// <summary>
        /// 分隔符虚线样式
        /// </summary>
        public DashStyle SeparatorDashStyle { get; set; } = DashStyle.Dot;

        /// <summary>
        /// 把手颜色
        /// </summary>
        public Color GripColor { get; set; } = Color.FromArgb(150, 150, 150);

        #endregion 分隔符和把手

        #region 图像设置

        /// <summary>
        /// 图像边距背景颜色
        /// </summary>
        public Color ImageMarginBackgroundColor { get; set; } = Color.FromArgb(240, 240, 240);

        /// <summary>
        /// 图像边距渐变开始颜色
        /// </summary>
        public Color ImageMarginGradientStart { get; set; } = Color.FromArgb(240, 240, 240);

        /// <summary>
        /// 图像边距渐变结束颜色
        /// </summary>
        public Color ImageMarginGradientEnd { get; set; } = Color.FromArgb(220, 220, 220);

        #endregion 图像设置
    }

    /// <summary>
    /// 自定义 ToolStrip 渲染器
    /// </summary>
    public class DgvCustomRenderer : ToolStripProfessionalRenderer
    {
        private readonly DgvRendererOptions _options;

        #region 构造函数

        /// <summary>
        /// 使用默认选项创建渲染器
        /// </summary>
        public DgvCustomRenderer() : this(new DgvRendererOptions())
        {
        }

        /// <summary>
        /// 使用自定义选项创建渲染器
        /// </summary>
        public DgvCustomRenderer(DgvRendererOptions options) : base(new DgvCustomColorTable(options))
        {
            _options = options ?? new DgvRendererOptions();
            this.RoundedEdges = false; // 禁用系统圆角，使用自定义圆角
        }

        #endregion 构造函数

        #region 渲染方法重写

        /// <summary>
        /// 渲染工具栏背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            // 菜单使用不同的背景色
            if (e.ToolStrip is ToolStripDropDownMenu)
            {
                using (var brush = new SolidBrush(_options.MenuItemBackgroundColor))
                {
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);
                }
            }
            else if (_options.EnableBackgroundGradient)
            {
                // 绘制渐变背景
                using (var brush = new LinearGradientBrush(
                    e.AffectedBounds,
                    _options.BackgroundGradientStart,
                    _options.BackgroundGradientEnd,
                    _options.GradientDirection))
                {
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);
                }
            }
            else
            {
                // 绘制纯色背景
                using (var brush = new SolidBrush(_options.BackgroundGradientStart))
                {
                    e.Graphics.FillRectangle(brush, e.AffectedBounds);
                }
            }

            // 添加细微的顶部高光（不适用于菜单）
            if (!(e.ToolStrip is MenuStrip) && !(e.ToolStrip is ToolStripDropDownMenu))
            {
                using (var pen = new Pen(Color.FromArgb(30, Color.White), 1))
                {
                    e.Graphics.DrawLine(pen,
                        e.AffectedBounds.Left, e.AffectedBounds.Top + 1,
                        e.AffectedBounds.Right, e.AffectedBounds.Top + 1);
                }
            }
        }

        /// <summary>
        /// 渲染按钮背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            var button = e.Item as ToolStripButton;
            if (button == null) return;

            RenderItemBackground(e.Graphics, e.Item, e.ToolStrip);
        }

        /// <summary>
        /// 渲染菜单项背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            RenderItemBackground(e.Graphics, e.Item, e.ToolStrip);
        }

        /// <summary>
        /// 渲染项背景的通用方法
        /// </summary>
        /// <param name="g"></param>
        /// <param name="item"></param>
        /// <param name="toolStrip"></param>
        private new void RenderItemBackground(Graphics g, ToolStripItem item, ToolStrip toolStrip)
        {
            if (!item.Enabled) return;

            Rectangle bounds = new Rectangle(Point.Empty, item.Size);
            bool isMenu = toolStrip is MenuStrip || toolStrip is ToolStripDropDownMenu;

            // 状态检测
            bool isPressed = IsItemPressed(item, toolStrip);
            bool isSelected = item.Selected && !isPressed; // 选中但不包括按下状态

            // 处理顶级菜单项
            if (toolStrip is MenuStrip && item is ToolStripMenuItem menuItem)
            {
                RenderTopLevelMenuItemBackground(g, menuItem, bounds, isPressed);
                return;
            }

            // 正常渲染逻辑
            RenderNormalItemBackground(g, item, toolStrip, bounds, isMenu, isSelected, isPressed);
        }

        /// <summary>
        /// 检测菜单项是否处于按下状态
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toolStrip"></param>
        /// <returns></returns>
        private bool IsItemPressed(ToolStripItem item, ToolStrip toolStrip)
        {
            // 直接使用 Pressed 属性
            if (item.Pressed) return true;

            // 对于顶级菜单项，检查下拉菜单是否可见
            if (toolStrip is MenuStrip && item is ToolStripMenuItem menuItem)
            {
                return menuItem.DropDown.Visible;
            }

            return false;
        }

        /// <summary>
        /// 渲染顶级菜单项背景
        /// </summary>
        /// <param name="g"></param>
        /// <param name="menuItem"></param>
        /// <param name="bounds"></param>
        /// <param name="isPressed"></param>
        private void RenderTopLevelMenuItemBackground(Graphics g, ToolStripMenuItem menuItem, Rectangle bounds, bool isPressed)
        {
            Color fillColor;

            if (isPressed)
            {
                // 按下状态 - 使用按下颜色
                fillColor = _options.MenuItemPressedGradientEnd;
            }
            else if (menuItem.Selected)
            {
                // 悬停状态 - 使用悬停颜色
                fillColor = _options.MenuItemHoverGradientEnd;
            }
            else
            {
                // 正常状态 - 透明或菜单背景色
                fillColor = _options.MenuBackgroundColor;
            }

            using (var brush = new SolidBrush(fillColor))
            {
                g.FillRectangle(brush, bounds);
            }

            // 绘制边框
            if (isPressed || menuItem.Selected)
            {
                using (var pen = new Pen(_options.MenuBorderColor, _options.MenuBorderWidth))
                {
                    g.DrawRectangle(pen,
                        new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1));
                }
            }
        }

        /// <summary>
        /// 渲染普通菜单项背景
        /// </summary>
        private void RenderNormalItemBackground(Graphics g, ToolStripItem item, ToolStrip toolStrip,
            Rectangle bounds, bool isMenu, bool isSelected, bool isPressed)
        {
            if (!isSelected && !isPressed) return;

            // 使用正确的颜色
            Color startColor, endColor;

            if (isPressed)
            {
                startColor = _options.MenuItemPressedGradientStart;
                endColor = _options.MenuItemPressedGradientEnd;
            }
            else
            {
                startColor = _options.MenuItemHoverGradientStart;
                endColor = _options.MenuItemHoverGradientEnd;
            }

            // 渲染逻辑
            if (isMenu && _options.EnableMenuItemRoundedCorners)
            {
                // 圆角渲染
                using (var path = GetRoundedRectangle(bounds, _options.MenuItemCornerRadius))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    if (_options.EnableMenuItemGradient)
                    {
                        // 圆角，渐变填充
                        using (var brush = new LinearGradientBrush(bounds, startColor, endColor, LinearGradientMode.Vertical))
                        {
                            g.FillPath(brush, path);
                        }
                    }
                    else
                    {
                        // 圆角，纯色填充
                        using (var brush = new SolidBrush(isPressed ? endColor : _options.MenuItemHoverGradientEnd))
                        {
                            g.FillPath(brush, path);
                        }
                    }

                    // 绘制边框
                    using (var pen = new Pen(_options.MenuBorderColor, _options.MenuBorderWidth))
                    {
                        g.DrawPath(pen, path);
                    }

                    g.SmoothingMode = SmoothingMode.Default;
                }
            }
            else
            {
                // 直角渲染
                if (_options.EnableMenuItemGradient)
                {
                    // 直角，渐变填充
                    using (var brush = new LinearGradientBrush(bounds, startColor, endColor, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(brush, bounds);
                    }
                }
                else
                {
                    // 直角，纯色填充
                    using (var brush = new SolidBrush(isPressed ? endColor : _options.MenuItemHoverGradientEnd))
                    {
                        g.FillRectangle(brush, bounds);
                    }
                }

                // 绘制边框
                using (var pen = new Pen(_options.MenuBorderColor, _options.MenuBorderWidth))
                {
                    g.DrawRectangle(pen,
                        new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1));
                }
            }
        }

        /// <summary>
        /// 渲染文本
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            bool isMenu = e.ToolStrip is MenuStrip || e.ToolStrip is ToolStripDropDownMenu;
            bool isSelected = e.Item.Selected || e.Item.Pressed;

            // 设置文本颜色
            if (!e.Item.Enabled)
            {
                // 禁用状态的文本颜色
                e.TextColor = _options.DisabledTextColor;
            }
            else if (isSelected && !string.IsNullOrEmpty(e.Text))
            {
                e.TextColor = _options.SelectedTextColor;
            }
            else
            {
                e.TextColor = isMenu ? _options.MenuTextColor : _options.TextColor;
            }

            // 为按下状态添加轻微的阴影效果
            if (_options.EnableTextShadow && e.Item.Pressed && e.Item.Enabled)
            {
                var shadowRect = e.TextRectangle;
                shadowRect.Offset(1, 1);

                using (var shadowBrush = new SolidBrush(Color.FromArgb(30, Color.Black)))
                {
                    TextRenderer.DrawText(e.Graphics, e.Text, e.TextFont, shadowRect,
                        shadowBrush.Color, GetDefaultTextFormatFlags(e.TextFormat));
                }
            }

            base.OnRenderItemText(e);
        }

        /// <summary>
        /// 渲染箭头
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (!e.Item.Enabled)
            {
                e.ArrowColor = _options.DisabledArrowColor;
            }
            else
            {
                bool isSelected = e.Item.Selected || e.Item.Pressed;
                e.ArrowColor = isSelected ? _options.SelectedArrowColor : _options.ArrowColor;
            }

            // 为箭头添加抗锯齿
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            base.OnRenderArrow(e);
            e.Graphics.SmoothingMode = SmoothingMode.Default;
        }

        /// <summary>
        /// 渲染复选框和单选按钮
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            if (e.Image == null)
            {
                Rectangle checkRect = GetCheckRectangle(e);
                bool isSelected = e.Item.Selected;
                Color checkColor = !e.Item.Enabled ? _options.DisabledCheckBoxColor :
                                 (isSelected ? _options.CheckBoxSelectedColor : _options.CheckBoxColor);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // 绘制复选框背景
                using (var brush = new SolidBrush(Color.White))
                using (var pen = new Pen(checkColor, 1.5f))
                {
                    e.Graphics.FillRectangle(brush, checkRect);
                    e.Graphics.DrawRectangle(pen, checkRect);

                    // 绘制勾选标记
                    if (e.Item is ToolStripMenuItem menuItem && menuItem.Checked)
                    {
                        DrawCheckMark(e.Graphics, checkRect, checkColor);
                    }
                }

                e.Graphics.SmoothingMode = SmoothingMode.Default;
            }
            else
            {
                base.OnRenderItemCheck(e);
            }
        }

        /// <summary>
        /// 获取复选框矩形区域
        /// </summary>
        private Rectangle GetCheckRectangle(ToolStripItemImageRenderEventArgs e)
        {
            int size = 13;
            Rectangle imageRect = e.ImageRectangle;

            // 居中显示
            int x = imageRect.X + (imageRect.Width - size) / 2;
            int y = imageRect.Y + (imageRect.Height - size) / 2;

            return new Rectangle(x, y, size, size);
        }

        /// <summary>
        /// 绘制勾选标记
        /// </summary>
        private void DrawCheckMark(Graphics g, Rectangle rect, Color color)
        {
            using (var pen = new Pen(color, 2f))
            {
                // 绘制勾选标记 (✓)
                Point[] points = {
                    new Point(rect.Left + 2, rect.Top + 6),
                    new Point(rect.Left + 5, rect.Top + 9),
                    new Point(rect.Right - 3, rect.Top + 2)
                };

                g.DrawLines(pen, points);
            }
        }

        /// <summary>
        /// 渲染分隔符
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            bool isMenu = e.ToolStrip is ToolStripDropDownMenu;
            Color separatorColor = isMenu ? _options.MenuSeparatorColor : _options.SeparatorColor;
            float separatorWidth = isMenu ? _options.MenuSeparatorWidth : _options.SeparatorWidth;
            int margin = isMenu ? _options.MenuSeparatorMargin : _options.SeparatorMargin;

            using (var pen = new Pen(separatorColor, separatorWidth))
            {
                // 设置虚线样式
                if (_options.EnableSeparatorDashed)
                {
                    pen.DashStyle = _options.SeparatorDashStyle;
                }

                // 根据方向绘制分隔符
                if (e.ToolStrip is ToolStripDropDownMenu)
                {
                    // 菜单中的分隔符 - 横向贯穿，带有边距
                    int y = e.Item.Height / 2;
                    e.Graphics.DrawLine(pen,
                        new Point(e.Item.Bounds.Left + margin + 20, y),
                        new Point(e.Item.Bounds.Right - margin, y));
                }
                else if (e.Vertical)
                {
                    // 垂直方向的分隔符 - 竖向，高度适应
                    int centerX = e.Item.Width / 2;
                    e.Graphics.DrawLine(pen,
                        new Point(centerX, margin),
                        new Point(centerX, e.Item.Height - margin));
                }
                else
                {
                    // 水平方向的分隔符 - 横向贯穿整个宽度
                    int centerY = e.Item.Height / 2;
                    e.Graphics.DrawLine(pen,
                        new Point(0, centerY),
                        new Point(e.Item.Width, centerY));
                }
            }
        }

        /// <summary>
        /// 渲染把手
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            int gripSize = 3;
            int gripSpacing = 2;

            Rectangle gripBounds = e.GripBounds;
            using (var brush = new SolidBrush(_options.GripColor))
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        int x = gripBounds.X + (j * (gripSize + gripSpacing)) + 2;
                        int y = gripBounds.Y + (i * (gripSize + gripSpacing)) + 5;

                        e.Graphics.FillRectangle(brush, x, y, gripSize, gripSize);
                    }
                }
            }
        }

        /// <summary>
        /// 渲染下拉按钮背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
        {
            RenderItemBackground(e.Graphics, e.Item, e.ToolStrip);
        }

        /// <summary>
        /// 渲染溢出按钮背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
        {
            var button = e.Item as ToolStripOverflowButton;
            if (button == null) return;

            Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);
            Color fillColor = button.Pressed ? _options.ButtonPressedColor :
                             button.Selected ? _options.ButtonHoverColor : _options.ButtonColor;

            // 绘制背景
            using (var brush = new SolidBrush(fillColor))
            {
                e.Graphics.FillRectangle(brush, bounds);
            }

            // 绘制光泽效果
            if (_options.EnableButtonGloss && (button.Selected || button.Pressed))
            {
                DrawGlossEffect(e.Graphics, bounds, _options.GlossHeightRatio, _options.GlossOpacity);
            }

            // 绘制箭头
            using (var brush = new SolidBrush(_options.ArrowColor))
            {
                int arrowSize = 4;
                int centerX = bounds.Width / 2;
                int centerY = bounds.Height / 2;

                Point[] arrowPoints = new Point[]
                {
                    new Point(centerX - arrowSize, centerY - arrowSize / 2),
                    new Point(centerX + arrowSize, centerY - arrowSize / 2),
                    new Point(centerX, centerY + arrowSize / 2)
                };

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPolygon(brush, arrowPoints);
                e.Graphics.SmoothingMode = SmoothingMode.Default;
            }
        }

        /// <summary>
        /// 渲染标签背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
        {
            // 标签背景保持透明，除非被选中
            if (e.Item.Selected && e.Item.Enabled)
            {
                RenderItemBackground(e.Graphics, e.Item, e.ToolStrip);
            }
        }

        /// <summary>
        /// 渲染边框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            bool isMenu = e.ToolStrip is ToolStripDropDownMenu;
            Color borderColor = isMenu ? _options.MenuBorderColor : _options.BorderColor;
            float borderWidth = isMenu ? _options.MenuBorderWidth : _options.BorderWidth;

            using (var pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawRectangle(pen,
                    new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y,
                                 e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
            }

            if (isMenu)
            {
                // 添加图标和文本分割线（仅适用于菜单）
                using (var pen = new Pen(_options.MenuSeparatorColor, 1))
                {
                    // 设置虚线样式
                    if (_options.EnableSeparatorDashed)
                    {
                        pen.DashStyle = _options.SeparatorDashStyle;
                    }

                    e.Graphics.DrawLine(pen,
                        e.AffectedBounds.X + 25, e.AffectedBounds.Y + 5,
                        e.AffectedBounds.X + 25, e.AffectedBounds.Bottom - 5);
                }
            }
        }

        /// <summary>
        /// 渲染图像边距背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            // 不希望绘制图像边距背景，注释掉以下代码
            // 使用菜单项背景色绘制图像边距
            /*using (var brush = new SolidBrush(_options.MenuItemBackgroundColor))
            {
                e.Graphics.FillRectangle(brush, e.AffectedBounds);
            }

            // 添加右边框
            using (var pen = new Pen(_options.MenuSeparatorColor, 1))
            {
                e.Graphics.DrawLine(pen,
                    e.AffectedBounds.Right, e.AffectedBounds.Top,
                    e.AffectedBounds.Right, e.AffectedBounds.Bottom);
            }*/
        }

        /// <summary>
        /// 渲染工具栏面板背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
        {
            if (_options.EnableBackgroundGradient)
            {
                using (var brush = new LinearGradientBrush(
                    e.ToolStripPanel.Bounds,
                    _options.BackgroundGradientStart,
                    _options.BackgroundGradientEnd,
                    _options.GradientDirection))
                {
                    e.Graphics.FillRectangle(brush, e.ToolStripPanel.Bounds);
                }
            }
            else
            {
                using (var brush = new SolidBrush(_options.BackgroundGradientStart))
                {
                    e.Graphics.FillRectangle(brush, e.ToolStripPanel.Bounds);
                }
            }
        }

        #endregion 渲染方法重写

        #region 辅助方法

        /// <summary>
        /// 创建圆角矩形路径
        /// </summary>
        private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

            // 左上角
            path.AddArc(arc, 180, 90);

            // 右上角
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // 右下角
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // 左下角
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 绘制光泽效果
        /// </summary>
        private void DrawGlossEffect(Graphics g, Rectangle bounds, float heightRatio, int opacity)
        {
            if (bounds.Height < 10) return;

            int glossHeight = (int)(bounds.Height * heightRatio);
            Rectangle glossRect = new Rectangle(
                bounds.X,
                bounds.Y,
                bounds.Width,
                glossHeight);

            using (var glossBrush = new LinearGradientBrush(
                glossRect,
                Color.FromArgb(opacity, Color.White),
                Color.FromArgb(opacity / 3, Color.White),
                LinearGradientMode.Vertical))
            {
                if (_options.EnableButtonRoundedCorners)
                {
                    using (var path = GetRoundedRectangle(glossRect, _options.ButtonCornerRadius))
                    {
                        // 只填充上半部分的圆角
                        var clipRect = new Rectangle(bounds.X, bounds.Y, bounds.Width, glossHeight + _options.ButtonCornerRadius);
                        var oldClip = g.Clip;
                        g.SetClip(clipRect);
                        g.FillPath(glossBrush, path);
                        g.Clip = oldClip;
                    }
                }
                else
                {
                    g.FillRectangle(glossBrush, glossRect);
                }
            }
        }

        /// <summary>
        /// 获取默认的文本格式标志
        /// </summary>
        private TextFormatFlags GetDefaultTextFormatFlags(TextFormatFlags baseFormat)
        {
            return baseFormat | TextFormatFlags.NoPadding;
        }

        #endregion 辅助方法
    }

    /// <summary>
    /// 自定义颜色表
    /// </summary>
    public class DgvCustomColorTable : ProfessionalColorTable
    {
        private readonly DgvRendererOptions _options;

        public DgvCustomColorTable(DgvRendererOptions options)
        {
            _options = options;
        }

        // ToolStrip 背景色
        public override Color ToolStripGradientBegin => _options.BackgroundGradientStart;

        public override Color ToolStripGradientMiddle => _options.BackgroundGradientStart;
        public override Color ToolStripGradientEnd => _options.BackgroundGradientStart;

        // MenuStrip 背景色
        public override Color MenuStripGradientBegin => _options.MenuBackgroundColor;

        public override Color MenuStripGradientEnd => _options.MenuBackgroundColor;

        // 菜单边框
        public override Color MenuBorder => _options.MenuBorderColor;

        public override Color MenuItemBorder => Color.Transparent;

        // 图像边距
        public override Color ImageMarginGradientBegin => _options.ImageMarginGradientStart;

        public override Color ImageMarginGradientMiddle => _options.ImageMarginGradientStart;
        public override Color ImageMarginGradientEnd => _options.ImageMarginGradientStart;

        // 菜单项选中状态
        public override Color MenuItemSelected => _options.MenuItemHoverGradientEnd;

        public override Color MenuItemSelectedGradientBegin => _options.MenuItemHoverGradientStart;
        public override Color MenuItemSelectedGradientEnd => _options.MenuItemHoverGradientEnd;

        // 按钮选中边框
        public override Color ButtonSelectedBorder => _options.BorderColor;

        // 菜单项按下状态
        public override Color MenuItemPressedGradientBegin => _options.MenuItemPressedGradientStart;

        public override Color MenuItemPressedGradientMiddle => _options.MenuItemPressedGradientStart;
        public override Color MenuItemPressedGradientEnd => _options.MenuItemPressedGradientEnd;

        // 使用自定义分隔符颜色
        public override Color SeparatorDark => _options.SeparatorColor;

        public override Color SeparatorLight => Color.Transparent;

        // 溢出按钮
        public override Color OverflowButtonGradientBegin => _options.BackgroundGradientStart;

        public override Color OverflowButtonGradientMiddle => _options.BackgroundGradientStart;
        public override Color OverflowButtonGradientEnd => _options.BackgroundGradientStart;

        // 工具栏内容面板
        public override Color ToolStripContentPanelGradientBegin => _options.BackgroundGradientStart;

        public override Color ToolStripContentPanelGradientEnd => _options.BackgroundGradientStart;

        // 工具栏面板
        public override Color ToolStripPanelGradientBegin => _options.BackgroundGradientStart;

        public override Color ToolStripPanelGradientEnd => _options.BackgroundGradientStart;
    }
}