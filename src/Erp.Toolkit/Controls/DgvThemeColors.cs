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

using System.Collections.Generic;
using System.Drawing;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 主题样式
    /// </summary>
    public enum ThemeStyle
    {
        /// <summary>
        /// 深色主题
        /// </summary>
        DarkTheme = 0,

        /// <summary>
        /// 浅色主题
        /// </summary>
        LightTheme = 1,

        /// <summary>
        /// 蓝色主题
        /// </summary>
        BlueTheme = 2,

        /// <summary>
        /// 绿色主题
        /// </summary>
        GreenTheme = 3,

        /// <summary>
        /// 橙色主题
        /// </summary>
        OrangeTheme = 4,

        /// <summary>
        /// 紫色主题
        /// </summary>
        PurpleTheme = 5
    }

    /// <summary>
    /// 主题颜色映射
    /// </summary>
    public class DgvThemeColors
    {
        #region 属性

        /// <summary>
        /// 渐变色（深）
        /// </summary>
        public Color GradientDarkColor { get; set; }

        /// <summary>
        /// 渐变色（浅）
        /// </summary>
        public Color GradientLightColor { get; set; }

        /// <summary>
        /// 列头边框颜色
        /// </summary>
        public Color CellHeaderBorderColor { get; set; }

        /// <summary>
        /// 列头字体颜色
        /// </summary>
        public Color CellHeaderFontColor { get; set; }

        /// <summary>
        /// 单元背景色
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// 奇偶行背景色
        /// </summary>
        public Color AlternatingRowsBackColor { get; set; }

        /// <summary>
        /// 单元格边框颜色
        /// </summary>
        public Color GridColor { get; set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// 按钮颜色
        /// </summary>
        public Color ButtonColor { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public Color FontColor { get; set; }

        /// <summary>
        /// 选区字体颜色
        /// </summary>
        public Color SelectionFontColor { get; set; }

        /// <summary>
        /// 选区颜色
        /// </summary>
        public Color SelectionBackColor { get; set; }

        #endregion 属性

        // 定义一个字典来映射主题样式到颜色集合
        private static Dictionary<ThemeStyle, DgvThemeColors> ThemeColorsMap = new Dictionary<ThemeStyle, DgvThemeColors>
        {
            {
                // 深色主题
                ThemeStyle.DarkTheme, new DgvThemeColors
                {
                    GradientDarkColor = Color.FromArgb(30, 30, 30), // 深黑色
                    GradientLightColor = Color.FromArgb(50, 50, 50), // 较浅的深黑色
                    CellHeaderBorderColor = Color.FromArgb(60, 60, 60), // 深灰色边框
                    CellHeaderFontColor = Color.FromArgb(255, 255, 255), // 白色字体
                    BackColor = Color.FromArgb(50, 50, 50), // 浅深色背景色
                    AlternatingRowsBackColor = Color.FromArgb(45, 45, 45), // 浅色奇偶背景色
                    GridColor = Color.FromArgb(100, 100, 100), // 灰色网格线
                    BackgroundColor = Color.FromArgb(40, 40, 40), // 深黑色背景
                    ButtonColor = Color.FromArgb(120, 130, 150), // 浅灰色按钮
                    FontColor = Color.FromArgb(255, 255, 255), // 白色字体
                    SelectionFontColor = Color.FromArgb(120, 130, 150), // 灰色字体
                    SelectionBackColor = Color.FromArgb(70, 80, 100) // 深蓝色选中背景
                }
            },
            {
                // 浅色主题
                ThemeStyle.LightTheme, new DgvThemeColors
                {
                    GradientDarkColor = Color.FromArgb(245, 245, 245), // 浅灰色
                    GradientLightColor = Color.FromArgb(255, 255, 255), // 白色
                    CellHeaderBorderColor = Color.FromArgb(219, 220, 221), // 浅灰色边框
                    CellHeaderFontColor = Color.FromArgb(0, 0, 0), // 黑色字体
                    BackColor = Color.FromArgb(255, 255, 255), // 白色背景色
                    AlternatingRowsBackColor = Color.FromArgb(250, 250, 250), // 浅色奇偶背景色
                    GridColor = Color.FromArgb(230, 230, 230), // 稍暗的灰色网格线
                    BackgroundColor = Color.FromArgb(248, 248, 248), // 接近白色的背景
                    ButtonColor = Color.FromArgb(145, 160, 180), // 蓝色按钮
                    FontColor = Color.FromArgb(80, 80, 80), // 深灰色字体
                    SelectionFontColor = Color.FromArgb(120, 130, 150), // 灰色字体
                    SelectionBackColor = Color.FromArgb(220, 235, 250) // 浅蓝色选中背景
                }
            },
            {
                // 蓝色主题
                ThemeStyle.BlueTheme, new DgvThemeColors
                {
                    GradientDarkColor = Color.FromArgb(219, 232, 247),// 浅蓝色渐变深色，接近于天空或淡海水色
                    GradientLightColor = Color.FromArgb(243, 248, 254),// 浅蓝色渐变浅色，明亮且清新
                    CellHeaderBorderColor = Color.FromArgb(145, 160, 180),// 浅灰色
                    CellHeaderFontColor = Color.FromArgb(0, 0, 0), // 黑色字体
                    BackColor = Color.FromArgb(255, 255, 255), // 白色背景色
                    AlternatingRowsBackColor = Color.FromArgb(250, 250, 250), // 浅色奇偶背景色
                    GridColor = Color.FromArgb(219, 220, 221),// 浅灰色网格线
                    BackgroundColor = Color.FromArgb(188, 206, 230),// 浅蓝色背景
                    ButtonColor = Color.FromArgb(0, 0, 0),// 黑色按钮
                    FontColor = Color.FromArgb(0, 0, 0),// 黑色字体
                    SelectionFontColor = Color.FromArgb(0, 0, 0), // 黑色字体
                    SelectionBackColor = Color.FromArgb(183, 219, 255)// 淡蓝色选中背景
                }
            },
            {
                // 绿色主题
                ThemeStyle.GreenTheme, new DgvThemeColors
                {
                    GradientDarkColor = Color.FromArgb(50, 100, 50), // 深绿色
                    GradientLightColor = Color.FromArgb(120, 200, 120), // 亮绿色
                    CellHeaderBorderColor = Color.FromArgb(80, 150, 80), // 适中绿色边框
                    CellHeaderFontColor = Color.FromArgb(0, 0, 0), // 黑色字体
                    BackColor = Color.FromArgb(120, 200, 120), // 亮绿色背景色
                    AlternatingRowsBackColor = Color.FromArgb(115, 195, 115), // 浅色奇偶背景色
                    GridColor = Color.FromArgb(70, 130, 70), // 稍暗的绿色网格线
                    BackgroundColor = Color.FromArgb(180, 220, 180), // 浅绿色背景
                    ButtonColor = Color.FromArgb(30, 60, 30), // 深绿色按钮
                    FontColor = Color.FromArgb(50, 50, 50), // 深灰色字体
                    SelectionFontColor = Color.FromArgb(120, 130, 150), // 灰色字体
                    SelectionBackColor = Color.FromArgb(100, 180, 100) // 鲜绿色选中背景
                }
            },
            {
                // 橙色主题
                ThemeStyle.OrangeTheme, new DgvThemeColors
                {
                    GradientDarkColor = Color.FromArgb(220, 100, 0), // 深橙色
                    GradientLightColor = Color.FromArgb(255, 160, 50), // 亮橙色
                    CellHeaderBorderColor = Color.FromArgb(230, 120, 20), // 适中橙色边框
                    CellHeaderFontColor = Color.FromArgb(0, 0, 0), // 黑色字体
                    BackColor = Color.FromArgb(255, 160, 50), // 亮橙色背景色
                    AlternatingRowsBackColor = Color.FromArgb(250, 155, 45), // 浅色奇偶背景色
                    GridColor = Color.FromArgb(210, 90, 0), // 稍暗的橙色网格线
                    BackgroundColor = Color.FromArgb(250, 220, 180), // 浅橙色背景
                    ButtonColor = Color.FromArgb(180, 80, 0), // 橙红色按钮
                    FontColor = Color.FromArgb(20, 20, 20), // 黑色字体
                    SelectionFontColor = Color.FromArgb(120, 130, 150), // 灰色字体
                    SelectionBackColor = Color.FromArgb(240, 140, 50) // 鲜橙色选中背景
                }
            },
            {
                // 紫色主题
                ThemeStyle.PurpleTheme, new DgvThemeColors
                {
                    GradientDarkColor = Color.FromArgb(100, 50, 150), // 深紫色
                    GradientLightColor = Color.FromArgb(160, 100, 220), // 亮紫色
                    CellHeaderBorderColor = Color.FromArgb(120, 70, 180), // 适中紫色边框
                    CellHeaderFontColor = Color.FromArgb(0, 0, 0), // 黑色字体
                    BackColor = Color.FromArgb(160, 100, 220), // 亮紫色背景色
                    AlternatingRowsBackColor = Color.FromArgb(155, 95, 215), // 浅色奇偶背景色
                    GridColor = Color.FromArgb(110, 60, 160), // 稍暗的紫色网格线
                    BackgroundColor = Color.FromArgb(220, 200, 240), // 浅紫色背景
                    ButtonColor = Color.FromArgb(70, 20, 90), // 深紫色按钮
                    FontColor = Color.FromArgb(255, 255, 255), // 白色字体
                    SelectionFontColor = Color.FromArgb(120, 130, 150), // 灰色字体
                    SelectionBackColor = Color.FromArgb(140, 80, 200) // 鲜紫色选中背景
                }
            },
        };

        // 静态方法，根据主题样式获取颜色集合
        public static DgvThemeColors GetColorsForTheme(ThemeStyle themeStyle)
        {
            if (ThemeColorsMap.TryGetValue(themeStyle, out DgvThemeColors themeColors))
            {
                return themeColors;
            }
            else
            {
                // 如果没有找到对应的主题样式，返回默认颜色
                return new DgvThemeColors
                {
                    // 设置默认颜色
                    GradientDarkColor = Color.FromArgb(219, 232, 247),// 浅蓝色渐变深色，接近于天空或淡海水色
                    GradientLightColor = Color.FromArgb(243, 248, 254),// 浅蓝色渐变浅色，明亮且清新
                    CellHeaderBorderColor = Color.FromArgb(145, 160, 180),// 浅灰色
                    CellHeaderFontColor = Color.FromArgb(0, 0, 0), // 黑色字体
                    BackColor = Color.FromArgb(255, 255, 255), // 白色背景色
                    AlternatingRowsBackColor = Color.FromArgb(250, 250, 250), // 浅色奇偶背景色
                    GridColor = Color.FromArgb(219, 220, 221),// 浅灰色网格线
                    BackgroundColor = Color.FromArgb(188, 206, 230),// 浅蓝色背景
                    ButtonColor = Color.FromArgb(0, 0, 0),// 黑色按钮
                    FontColor = Color.FromArgb(0, 0, 0),// 黑色字体
                    SelectionFontColor = Color.FromArgb(120, 130, 150), // 灰色字体
                    SelectionBackColor = Color.FromArgb(183, 219, 255)// 淡蓝色选中背景
                };
            }
        }

        /// <summary>
        /// 获取主题样式名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetThemeStyleName(ThemeStyle value)
        {
            return value.ToString();
        }
    }
}