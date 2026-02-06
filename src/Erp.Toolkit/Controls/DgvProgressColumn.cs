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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 自定义进度条列
    /// </summary>
    public class DgvProgressColumn : DataGridViewColumn
    {
        private int _lowThreshold = 30;
        private int _mediumThreshold = 70;

        public DgvProgressColumn()
        {
            CellTemplate = new DgvProgressCell();
            LowColor = Color.Red;
            MediumColor = Color.Orange;
            HighColor = Color.Green;
        }

        /// <summary>
        /// 进度条(低)阈值配置
        /// </summary>
        [Category("Appearance")]
        [Description("进度条低阈值")]
        [DefaultValue(30)]
        public int LowThreshold
        {
            get => _lowThreshold;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(LowThreshold), "阈值必须在0-100之间");
                if (value >= MediumThreshold)
                    throw new ArgumentException("低阈值必须小于中阈值");
                _lowThreshold = value;
            }
        }

        /// <summary>
        /// 进度条(中)阈值配置
        /// </summary>
        [Category("Appearance")]
        [Description("进度条中阈值")]
        [DefaultValue(70)]
        public int MediumThreshold
        {
            get => _mediumThreshold;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(MediumThreshold), "阈值必须在0-100之间");
                if (value <= LowThreshold)
                    throw new ArgumentException("中阈值必须大于低阈值");
                _mediumThreshold = value;
            }
        }

        /// <summary>
        /// 进度条(低)颜色配置
        /// </summary>
        [Category("Appearance")]
        [Description("低进度颜色")]
        [DefaultValue(typeof(Color), "Red")]
        public Color LowColor { get; set; }

        /// <summary>
        /// 进度条(中)颜色配置
        /// </summary>
        [Category("Appearance")]
        [Description("中进度颜色")]
        [DefaultValue(typeof(Color), "Orange")]
        public Color MediumColor { get; set; }

        /// <summary>
        /// 进度条(高)颜色配置
        /// </summary>
        [Category("Appearance")]
        [Description("高进度颜色")]
        [DefaultValue(typeof(Color), "Green")]
        public Color HighColor { get; set; }
    }
}