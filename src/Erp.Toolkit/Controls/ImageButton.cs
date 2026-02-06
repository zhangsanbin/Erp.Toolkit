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
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 按钮图像枚举
    /// </summary>
    public enum ImageButtonEnum
    {
        Check = 0,
        Cancel = 3,
    }

    /// <summary>
    /// 图像按钮
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    public partial class ImageButton : UserControl
    {
        // 内部属性
        private bool _enabled = true;

        private ImageButtonEnum _buttonImage;

        /// <summary>
        /// 图像按钮
        /// </summary>
        public ImageButton()
        {
            InitializeComponent();

            // 设置鼠标提示
            toolTip.SetToolTip(this, "确认");

            // 初始时状态下的背景图
            ButtonImage = ImageButtonEnum.Check;

            // 鼠标事件处理器
            this.MouseEnter += MyUserControl_MouseEnter;
            this.MouseLeave += MyUserControl_MouseLeave;
            this.MouseDown += MyUserControl_MouseDown;
            this.MouseUp += MyUserControl_MouseUp;
        }

        /// <summary>
        /// 获取指定状态的图像
        /// </summary>
        /// <param name="buttonImage">按钮类型</param>
        /// <param name="stateOffset">状态偏移量 (0=正常, 1=悬停, 2=禁用)</param>
        /// <returns>对应的图像</returns>
        private System.Drawing.Image GetButtonImage(ImageButtonEnum buttonImage, int stateOffset)
        {
            int imageIndex = (int)buttonImage + stateOffset;

            switch (imageIndex)
            {
                case 0: // Check normal
                    return Properties.Resources.Check_normal;

                case 1: // Check hover
                    return Properties.Resources.Check_hover;

                case 2: // Check disabled
                    return Properties.Resources.Check_disabled;

                case 3: // Cancel normal
                    return Properties.Resources.Cancel_normal;

                case 4: // Cancel hover
                    return Properties.Resources.Cancel_hover;

                case 5: // Cancel disabled
                    return Properties.Resources.Cancel_disabled;

                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取工具提示文本
        /// </summary>
        /// <param name="buttonImage">按钮类型</param>
        /// <returns>工具提示文本</returns>
        private string GetToolTipText(ImageButtonEnum buttonImage)
        {
            switch (buttonImage)
            {
                case ImageButtonEnum.Check:
                    return "确认";

                case ImageButtonEnum.Cancel:
                    return "取消";

                default:
                    return buttonImage.ToString();
            }
        }

        /// <summary>
        /// 鼠标进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyUserControl_MouseEnter(object sender, EventArgs e)
        {
            if (_enabled)
                this.BackgroundImage = GetButtonImage(_buttonImage, 1); // 悬停状态
        }

        /// <summary>
        /// 鼠标离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyUserControl_MouseLeave(object sender, EventArgs e)
        {
            if (_enabled)
                this.BackgroundImage = GetButtonImage(_buttonImage, 0); // 正常状态
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (_enabled)
                this.BackgroundImage = GetButtonImage(_buttonImage, 0); // 按下时显示正常状态
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyUserControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (_enabled)
                this.BackgroundImage = GetButtonImage(_buttonImage, 1); // 弹起时显示悬停状态
        }

        /// <summary>
        /// 按钮的图标
        /// </summary>
        public ImageButtonEnum ButtonImage
        {
            get { return _buttonImage; }
            set
            {
                _buttonImage = value;
                this.BackgroundImage = GetButtonImage(_buttonImage, 0); // 正常状态

                // 设置鼠标提示
                toolTip.SetToolTip(this, GetToolTipText(_buttonImage));
            }
        }

        /// <summary>
        /// 是否被启用
        /// </summary>
        public new bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                base.Enabled = _enabled;

                if (_enabled)
                {
                    // 启用状态 - 正常图像
                    this.BackgroundImage = GetButtonImage(_buttonImage, 0);
                }
                else
                {
                    // 禁用状态
                    this.BackgroundImage = GetButtonImage(_buttonImage, 2);
                }
            }
        }
    }
}