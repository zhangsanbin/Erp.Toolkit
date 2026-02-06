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

using System;
using System.Drawing;
using System.IO;

namespace Erp.Toolkit.Controls
{
    [Serializable]
    [Flags]
    public enum MenuShowTarget
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None = 0,

        /// <summary>
        /// 工具栏
        /// </summary>
        ToolStrip = 1,

        /// <summary>
        /// 右击菜单
        /// </summary>
        ContextMenuStrip = 2,

        /// <summary>
        /// 底部导航栏
        /// </summary>
        BottomNavigator = 4,

        /// <summary>
        /// 均显示
        /// </summary>
        All = ToolStrip | ContextMenuStrip | BottomNavigator
    }

    [Serializable]
    public class DgvUserContextMenuStripConfig
    {
        /// <summary>
        /// 菜单文本
        /// </summary>
        public string MenuText { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public Image MenuIcon { get; set; }

        private string _menuIconBase64String;

        /// <summary>
        /// 菜单图标（Base64编码的字符串）
        /// </summary>
        public string MenuIconBase64String
        {
            get { return _menuIconBase64String; }
            set
            {
                _menuIconBase64String = value;

                try
                {
                    // 将Base64编码转换为二进制数组
                    byte[] imageBytes = Convert.FromBase64String(_menuIconBase64String);
                    using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                    {
                        // 转换为图片并赋值
                        MenuIcon = Image.FromStream(ms); ;
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("提供的字符串不是有效的Base64编码的图片字符串: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("发生了一个未知错误: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// 菜单显示目标
        /// </summary>
        public MenuShowTarget Target { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public int Group { get; set; }

        /// <summary>
        /// 菜单是否处于选中状态
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 点击事件委托
        /// </summary>
        public Action<object, EventArgs> ClickHandler { get; set; }// 定义一个Action<object, EventArgs>类型的属性作为点击处理器
    }
}