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

using Erp.Toolkit.Localization;
using System;
using System.Data;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 动态统计窗体
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    public partial class FrmStatistics : Form
    {
        private DataTable _dataTable = new DataTable();
        private Erp.Toolkit.Controls.Dgv dgv = new Erp.Toolkit.Controls.Dgv();
        private DgvAggregatorConfig _aggregateConfig = new DgvAggregatorConfig();
        private readonly Localization.Localizer localizer = Localization.Localizer.Instance;

        public FrmStatistics(DataTable dataTable, DgvAggregatorConfig aggregateConfig, string guid)
        {
            InitializeComponent();

            _dataTable = dataTable;

            _aggregateConfig = aggregateConfig;

            try
            {
                // 执行聚合
                DataTable resultTable = DgvAggregator.AggregateDataTable(
                    _dataTable,
                    aggregateConfig.GroupColumns,
                    aggregateConfig.AggregateColumns
                );

                dataGridView1.DataSource = resultTable;
                dataGridView1.Visible = false;
            }
            catch (global::System.Exception)
            {
                Console.WriteLine("未能加载文件或程序集“System.Linq.Dynamic.Core, Version=1.5.1.0 或它的某一个依赖项。");
                throw;
            }

            // 呈现在UI层
            Controls.Add(dgv);
            dgv.Dock = DockStyle.Fill;
            dgv.Visible = true;
            dgv.BringToFront();
            dgv.FillTable(ConvertDataGridViewToDataTable(dataGridView1), guid + "_EXP");

            // 设置AutoSizeColumnsMode为AllCells，以确保列宽根据内容自动调整
            dgv.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // 调用AutoResizeColumns方法
            dgv.dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // 初始化本地化资源
            InitializeLocalization();
        }

        /// <summary>
        /// 初始化本地化资源
        /// </summary>
        private void InitializeLocalization()
        {
            // 窗体标题
            this.Text = localizer.GetString("StatisticalIndicator");

            // 应用本地化到整个窗体
            this.ApplyLocalization();
        }

        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dgv.dataGridView.Columns)
            {
                // 检查该列是否为数值型
                if (IsNumericType(column.ValueType))
                    column.DefaultCellStyle.Format = "N2"; // "N2" 保留两位小数
            }
        }

        /// <summary>
        /// 从 DataGridView 的行和列中手动转换并创建 DataTable
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        private DataTable ConvertDataGridViewToDataTable(DataGridView dataGridView)
        {
            DataTable dataTable = new DataTable();

            // 添加列
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                dataTable.Columns.Add(column.Name, column.ValueType);
            }

            // 添加行
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow) // 跳过新行（通常是最后一行，用于添加新数据）
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        dataRow[i] = row.Cells[i].Value;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 检查类型是否为数值型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsNumericType(System.Type type)
        {
            return type == typeof(int) || type == typeof(decimal) || type == typeof(double) ||
                   type == typeof(float) || type == typeof(long) || type == typeof(short) ||
                   type == typeof(byte) || type == typeof(ulong) || type == typeof(ushort) ||
                   type == typeof(uint) || type == typeof(sbyte);
        }
    }
}