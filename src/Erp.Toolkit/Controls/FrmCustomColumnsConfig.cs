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
using System.Collections.Generic;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    public partial class FrmCustomColumnsConfig : Form
    {
        private List<DgvCustomColumnsConfig> customColumnsConfigs;

        /// <summary>
        /// 关闭时传参 dgvCustomColumnsConfigs，委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="dgvCustomColumnsConfigs"></param>
        public delegate void ClosedHandler(object sender, FormClosedEventArgs e, List<DgvCustomColumnsConfig> dgvCustomColumnsConfigs);

        /// <summary>
        /// 关闭时传参，事件
        /// </summary>
        public event ClosedHandler ClosedEvent;

        public FrmCustomColumnsConfig()
        {
            InitializeComponent();

            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCustomColumnsConfig_FormClosed);
        }

        public FrmCustomColumnsConfig(List<DgvCustomColumnsConfig> dgvCustomColumnsConfigs)
        {
            InitializeComponent();

            if (dgvCustomColumnsConfigs != null)
            {
                customColumnsConfigs = dgvCustomColumnsConfigs;
            }
            else
            {
                customColumnsConfigs = new List<DgvCustomColumnsConfig>();
            }

            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmCustomColumnsConfig_FormClosed);
        }

        private void FrmCustomColumnsConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 关闭并回传保存
            ClosedEvent?.Invoke(this, e, customColumnsConfigs);
        }

        private void FrmCustomColumnsConfig_Load(object sender, EventArgs e)
        {
            // TEST Data
            DgvCustomColumnsConfig dgvCustomColumnsConfig = new DgvCustomColumnsConfig()
            {
                Name = "Diam",
                HeaderText = "DiamValue",
                ValueList = new List<DgvCustomColumnsValue>
                {
                    new DgvCustomColumnsValue { Value = "Φ", ValueType = DgvCustomColumnsValueType.String },
                    new DgvCustomColumnsValue { Value = "external_diam", ValueType = DgvCustomColumnsValueType.CellsName },
                    new DgvCustomColumnsValue { Value = "X", ValueType = DgvCustomColumnsValueType.String },
                    new DgvCustomColumnsValue { Value = "length", ValueType = DgvCustomColumnsValueType.CellsName }
                }
            };

            // 测试数据
            customColumnsConfigs.Add(dgvCustomColumnsConfig);
        }
    }
}