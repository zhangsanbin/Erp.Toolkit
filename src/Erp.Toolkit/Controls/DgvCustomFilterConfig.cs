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
    public class DgvCustomFilterConfig : IComparable<DgvCustomFilterConfig>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuText { get; set; }

        /// <summary>
        /// 筛选表达式文本
        /// </summary>
        public string FilterText { get; set; } = string.Empty;

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
        public MenuShowTarget Target { get; set; } = MenuShowTarget.ToolStrip;

        /// <summary>
        /// 分组
        /// </summary>
        public int Group { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 菜单是否处于选中状态
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 对自定义类型元素，实现 IComparable<T> 接口，以满足 Sort 方法可以对列表进行排序
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DgvCustomFilterConfig other)
        {
            if (other == null)
                return 1;

            // 先比较 Group 字段
            int groupComparison = this.Group.CompareTo(other.Group);
            if (groupComparison != 0)
            {
                return groupComparison;
            }

            // 如果 Group 相同，则比较 Sort 字段
            return this.Sort.CompareTo(other.Sort);
        }
    }
}