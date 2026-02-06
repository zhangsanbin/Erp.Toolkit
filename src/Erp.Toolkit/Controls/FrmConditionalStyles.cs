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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 表示用于管理 DataGridView 中条件样式的表单
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    public partial class FrmConditionalStyles : Form
    {
        private Dgv Dgv;
        private List<DgvConditionalConfig> dgvConditionalConfigs;
        private bool edit;
        private int editIndex;
        private readonly Localization.Localizer localizer = Localization.Localizer.Instance;

        /// <summary>
        /// 关闭时传参 Configs，委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="configs"></param>
        public delegate void ClosedHandler(object sender, EventArgs e, List<DgvConditionalConfig> configs);

        /// <summary>
        /// 关闭时传参，事件
        /// </summary>
        public event ClosedHandler ClosedEvent;

        /// <summary>
        /// FrmConditionalStyles 构造函数，默认初始化
        /// </summary>
        public FrmConditionalStyles()
        {
            InitializeComponent();

            // 初始化本地化资源
            InitializeLocalization();
        }

        /// <summary>
        /// FrmConditionalStyles 构造函数，传入 DataGridView 和配置列表
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="configs"></param>
        public FrmConditionalStyles(Dgv dgv, List<DgvConditionalConfig> configs)
        {
            InitializeComponent();

            Dgv = dgv;

            if (configs != null)
            {
                dgvConditionalConfigs = configs;
            }
            else
            {
                dgvConditionalConfigs = new List<DgvConditionalConfig>();
            }

            // 初始化本地化资源
            InitializeLocalization();
        }

        /// <summary>
        /// 初始化本地化资源
        /// </summary>
        private void InitializeLocalization()
        {
            // 窗体标题
            this.Text = localizer.GetString("ConditionalStyleManager");

            // 设置控件本地化键
            this.lbRules.SetLocalizationKey("Rules");
            this.gbNewFormatRule.Text = localizer.GetString("NewFormatRule");
            this.lbSelectRuleType.SetLocalizationKey("SelectRuleType");
            this.cbFullRow.SetLocalizationKey("ApplyToEntireRow");
            this.cbAllowStyleStacking.SetLocalizationKey("AllowStyleOverlap");
            this.comboBox1.Items.Add(localizer.GetString("CheckCurrentValue"));
            this.comboBox1.Items.Add(localizer.GetString("CompareOtherRecords"));
            this.label1.SetLocalizationKey("RuleAppliesToColumn");
            this.label3.SetLocalizationKey("FormatCellsMeetingCriteria");
            this.comboBox3.Items.Add(localizer.GetString("FieldValue"));
            this.label8.SetLocalizationKey("DependentRecordColumn");
            this.label4.SetLocalizationKey("And");
            this.button1.SetLocalizationKey("Add");
            this.label5.SetLocalizationKey("FormatPreview");
            this.button2.SetLocalizationKey("Cancel");
            this.button3.SetLocalizationKey("Apply");
            this.toolStripMenuItem1.SetLocalizationKey("MoveUp");
            this.toolStripMenuItem2.SetLocalizationKey("MoveDown");
            this.toolStripMenuItem3.SetLocalizationKey("Modify");
            this.toolStripMenuItem4.SetLocalizationKey("Delete");
            this.toolStripMenuItem5.SetLocalizationKey("Clear");
            this.toolStripMenuItem6.SetLocalizationKey("Copy");

            // 应用本地化到整个窗体
            this.ApplyLocalization();
        }

        /// <summary>
        /// 窗体关闭事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionalStyles_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 关闭并回传保存
            // ClosedEvent?.Invoke(this, e, dgvConditionalConfigs);
        }

        /// <summary>
        /// 窗体加载事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionalStyles_Load(object sender, EventArgs e)
        {
            // 事件绑定并注册
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConditionalStyles_FormClosed);
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);

            // 默认值初始化
            comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            // 设置视图模式为细节视图
            listView1.View = View.Details;
            // 显示网格线
            listView1.GridLines = true;
            // 允许全行选择
            listView1.FullRowSelect = true;
            // 不允许多选
            listView1.MultiSelect = false;
            // 列标题可点击
            listView1.HeaderStyle = ColumnHeaderStyle.Clickable;

            // 添加列
            listView1.Columns.Add(localizer.GetString("ColumnName"), 130, HorizontalAlignment.Center);
            listView1.Columns.Add(localizer.GetString("RuleFormat"), 520, HorizontalAlignment.Left);

            // 显示预设配置
            if (dgvConditionalConfigs != null)
            {
                foreach (var cfg in dgvConditionalConfigs)
                {
                    ListViewItem lvItem = new ListViewItem(cfg.ColumnName);
                    lvItem.SubItems.Add(cfg.LogicalType + " " + cfg.Value1 + ", " + cfg.ForeColor.ToString() + ", " + cfg.BackColor.ToString());
                    listView1.Items.Add(lvItem);
                }
            }
        }

        /// <summary>
        /// 列表视图选中项改变事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // 获取选中的项并做相应处理
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                // 处理选中项

                // Test Remove
                //selectedItem.Remove();
            }
        }

        /// <summary>
        /// 新建规则
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // 数据有效性验证
            if (textBox1.Visible && textBox1.Text == String.Empty)
            {
                MessageBox.Show(localizer.GetString("ContentCannotBeEmpty"), localizer.GetString("OperationFailed"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (textBox2.Visible && textBox2.Text == String.Empty)
            {
                MessageBox.Show(localizer.GetString("ContentCannotBeEmpty"), localizer.GetString("OperationFailed"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            // 读取输入值
            var v1 = textBox1.Text;
            var v2 = textBox2.Text;

            // 输入值的数据类型验证
            if (label7.Text == "Int32" || label7.Text == "Double" || label7.Text == "Decimal" || label7.Text == "Boolean")
            {
                string pattern = @"^-?\d+(\.\d+)?$";

                bool v1IsNumeric = Regex.IsMatch(v1, pattern);

                bool v2IsNumeric = Regex.IsMatch(v2, pattern);

                if (!((v1IsNumeric || !textBox1.Visible) && (v2IsNumeric || !textBox2.Visible)))
                {
                    MessageBox.Show(localizer.GetString("InputValueNotNumber"), localizer.GetString("OperationFailed"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }

            // 获取列类型
            Type columnType = Dgv.dataGridView.Columns[comboBox2.Text].ValueType;

            // 规则配置项对象
            DgvConditionalConfig dgvConditionalConfig = new DgvConditionalConfig();
            dgvConditionalConfig.RuleType = (RuleType)comboBox1.SelectedIndex;
            dgvConditionalConfig.ColumnName = comboBox2.Text;
            dgvConditionalConfig.ColumnType = columnType;
            dgvConditionalConfig.Value1 = v1;
            dgvConditionalConfig.Value2 = v2;
            dgvConditionalConfig.Font = label5.Font;
            dgvConditionalConfig.ForeColor = label5.ForeColor;
            dgvConditionalConfig.BackColor = label5.BackColor;
            dgvConditionalConfig.FullRow = cbFullRow.Checked;
            dgvConditionalConfig.AllowStyleStacking = cbAllowStyleStacking.Checked;
            // 添加依赖列
            if (comboBox5.Visible && comboBox1.SelectedIndex == 1)
            {
                dgvConditionalConfig.DependentColumns.Add(comboBox2.Text);
                dgvConditionalConfig.DependentColumns.Add(comboBox5.Text);
            }

            // 获取逻辑表达式类型
            string expression = comboBox4.Text;

            // 处理表达式
            if (expression == localizer.GetString("Between"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.Between;
            }
            else if (expression == localizer.GetString("NotBetween"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.NotBetween;
            }
            else if (expression == localizer.GetString("Contains"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.Contains;
            }
            else if (expression == localizer.GetString("DoesNotContain"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.DoesNotContain;
            }
            else if (expression == localizer.GetString("Equal"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.Equals;
            }
            else if (expression == localizer.GetString("NotEqual"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.NotEquals;
            }
            else if (expression == localizer.GetString("GreaterThan"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.GreaterThan;
            }
            else if (expression == localizer.GetString("LessThan"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.LessThan;
            }
            else if (expression == localizer.GetString("GreaterThanOrEqual"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.GreaterThanOrEquals;
            }
            else if (expression == localizer.GetString("LessThanOrEqual"))
            {
                dgvConditionalConfig.LogicalType = LogicalType.LessThanOrEquals;
            }

            if (edit)
            {
                // 修改
                dgvConditionalConfigs[editIndex] = dgvConditionalConfig;
                button1.Text = localizer.GetString("Add");
                edit = false;
            }
            else
            {
                // 新增
                dgvConditionalConfigs.Add(dgvConditionalConfig);
            }

            // 显示预设配置
            if (dgvConditionalConfigs != null)
            {
                listView1.Items.Clear();

                foreach (var cfg in dgvConditionalConfigs)
                {
                    ListViewItem lvItem = new ListViewItem(cfg.ColumnName);
                    lvItem.SubItems.Add(cfg.LogicalType + " " + cfg.Value1 + ", " + cfg.ForeColor.ToString() + ", " + cfg.BackColor.ToString());
                    listView1.Items.Add(lvItem);
                }
            }

            // 清空UI输入框
            if (textBox1.Visible) { textBox1.Text = ""; }
            if (textBox2.Visible) { textBox2.Text = ""; }
            if (cbFullRow.Visible) { cbFullRow.Checked = false; }
            if (cbAllowStyleStacking.Visible) { cbAllowStyleStacking.Checked = false; }
        }

        /// <summary>
        /// 关闭窗口，不保存配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            // 关闭
            Close();
        }

        /// <summary>
        /// 关闭并回传保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // 关闭并回传保存
            ClosedEvent?.Invoke(this, e, dgvConditionalConfigs);

            Close();
        }

        /// <summary>
        /// 切换列名，自动控制 UI 显示逻辑表达式类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 检查空值
            if (comboBox2.SelectedIndex < 0) return;
            if (comboBox2.Text == string.Empty) return;

            // 获取列类型
            Type columnType = Dgv.dataGridView.Columns[comboBox2.Text].ValueType;
            if (columnType == null) return;

            // 根据类型执行不同的逻辑
            switch (columnType.ToString())
            {
                case "System.String":
                    label7.Text = "String";
                    comboBox4.Items.Clear();
                    comboBox4.Items.Add(localizer.GetString("Contains"));
                    comboBox4.Items.Add(localizer.GetString("DoesNotContain"));
                    comboBox4.Items.Add(localizer.GetString("Equal"));
                    comboBox4.Items.Add(localizer.GetString("NotEqual"));
                    comboBox4.SelectedIndex = 0;
                    break;

                case "System.Int32":
                    label7.Text = "Int32";
                    comboBox4.Items.Clear();
                    if (comboBox1.SelectedIndex == 0) comboBox4.Items.Add(localizer.GetString("Between"));
                    if (comboBox1.SelectedIndex == 0) comboBox4.Items.Add(localizer.GetString("NotBetween"));
                    comboBox4.Items.Add(localizer.GetString("Equal"));
                    comboBox4.Items.Add(localizer.GetString("NotEqual"));
                    comboBox4.Items.Add(localizer.GetString("GreaterThan"));
                    comboBox4.Items.Add(localizer.GetString("LessThan"));
                    comboBox4.Items.Add(localizer.GetString("GreaterThanOrEqual"));
                    comboBox4.Items.Add(localizer.GetString("LessThanOrEqual"));
                    comboBox4.SelectedIndex = 0;
                    break;

                case "System.Double":
                    label7.Text = "Double";
                    comboBox4.Items.Clear();
                    if (comboBox1.SelectedIndex == 0) comboBox4.Items.Add(localizer.GetString("Between"));
                    if (comboBox1.SelectedIndex == 0) comboBox4.Items.Add(localizer.GetString("NotBetween"));
                    comboBox4.Items.Add(localizer.GetString("Equal"));
                    comboBox4.Items.Add(localizer.GetString("NotEqual"));
                    comboBox4.Items.Add(localizer.GetString("GreaterThan"));
                    comboBox4.Items.Add(localizer.GetString("LessThan"));
                    comboBox4.Items.Add(localizer.GetString("GreaterThanOrEqual"));
                    comboBox4.Items.Add(localizer.GetString("LessThanOrEqual"));
                    comboBox4.SelectedIndex = 0;
                    break;

                case "System.Decimal":
                    label7.Text = "Decimal";
                    comboBox4.Items.Clear();
                    if (comboBox1.SelectedIndex == 0) comboBox4.Items.Add(localizer.GetString("Between"));
                    if (comboBox1.SelectedIndex == 0) comboBox4.Items.Add(localizer.GetString("NotBetween"));
                    comboBox4.Items.Add(localizer.GetString("Equal"));
                    comboBox4.Items.Add(localizer.GetString("NotEqual"));
                    comboBox4.Items.Add(localizer.GetString("GreaterThan"));
                    comboBox4.Items.Add(localizer.GetString("LessThan"));
                    comboBox4.Items.Add(localizer.GetString("GreaterThanOrEqual"));
                    comboBox4.Items.Add(localizer.GetString("LessThanOrEqual"));
                    comboBox4.SelectedIndex = 0;
                    break;

                case "System.DateTime":
                    label7.Text = "DateTime";
                    comboBox4.Items.Clear();
                    if (comboBox1.SelectedIndex == 0) comboBox4.Items.Add(localizer.GetString("Between"));
                    if (comboBox1.SelectedIndex == 0) comboBox4.Items.Add(localizer.GetString("NotBetween"));
                    comboBox4.Items.Add(localizer.GetString("Equal"));
                    comboBox4.Items.Add(localizer.GetString("NotEqual"));
                    comboBox4.Items.Add(localizer.GetString("GreaterThan"));
                    comboBox4.Items.Add(localizer.GetString("LessThan"));
                    comboBox4.Items.Add(localizer.GetString("GreaterThanOrEqual"));
                    comboBox4.Items.Add(localizer.GetString("LessThanOrEqual"));
                    comboBox4.SelectedIndex = 0;
                    break;

                case "System.Boolean":
                    label7.Text = "Boolean";
                    comboBox4.Items.Clear();
                    comboBox4.Items.Add(localizer.GetString("Equal"));
                    comboBox4.Items.Add(localizer.GetString("NotEqual"));
                    comboBox4.SelectedIndex = 0;
                    break;

                default:
                    // 处理未知或不支持的数据类型
                    label7.Text = columnType.ToString();
                    MessageBox.Show($"Unsupported data type: {columnType.ToString()}");
                    break;
            }

            // 控制 comboBox5 UI 显示相同类型的列
            comboBox5.Items.Clear();
            foreach (DataGridViewColumn column in Dgv.dataGridView.Columns)
            {
                if (column.ValueType == columnType && column.Name != comboBox2.Text)
                {
                    comboBox5.Items.Add(column.Name);
                }
            }
        }

        /// <summary>
        /// 切换逻辑表达式类型，自动控制 UI 显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string betweenText = localizer.GetString("Between");
            string notBetweenText = localizer.GetString("NotBetween");

            // 控制 UI 显示
            if (comboBox4.Text == betweenText || comboBox4.Text == notBetweenText)
            {
                label4.Visible = true;
                textBox2.Visible = true;
                textBox1.Width = 100;
            }
            else
            {
                label4.Visible = false;
                textBox2.Visible = false;
                textBox1.Width = 230;
            }
        }

        /// <summary>
        /// 切换加粗样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            // 检查Label的字体样式是否已包含Bold
            bool isBold = (label5.Font.Style & FontStyle.Bold) == FontStyle.Bold;

            // 根据当前状态切换Bold样式
            Font newFont;
            if (isBold)
            {
                // 如果已加粗，则创建一个新的Font对象，去除Bold样式
                // 保留原字体的其他样式（如斜体、下划线等）
                newFont = new Font(label5.Font.Name, label5.Font.Size, label5.Font.Style & ~FontStyle.Bold);
            }
            else
            {
                // 如果没有加粗，则创建一个新的Font对象，添加Bold样式
                newFont = new Font(label5.Font.Name, label5.Font.Size, label5.Font.Style | FontStyle.Bold);
            }

            // 应用新的Font到Label
            label5.Font = newFont;
        }

        /// <summary>
        /// 切换斜体样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            // 检查Label的字体样式是否已包含Bold
            bool isItalic = (label5.Font.Style & FontStyle.Italic) == FontStyle.Italic;

            // 根据当前状态切换Bold样式
            Font newFont;
            if (isItalic)
            {
                // 如果已加粗，则创建一个新的Font对象，去除Italic样式
                // 保留原字体的其他样式（如斜体、下划线等）
                newFont = new Font(label5.Font.Name, label5.Font.Size, label5.Font.Style & ~FontStyle.Italic);
            }
            else
            {
                // 如果没有加粗，则创建一个新的Font对象，添加Italic样式
                newFont = new Font(label5.Font.Name, label5.Font.Size, label5.Font.Style | FontStyle.Italic);
            }

            // 应用新的Font到Label
            label5.Font = newFont;
        }

        /// <summary>
        /// 切换下划线样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            // 检查Label的字体样式是否已包含Bold
            bool isUnderline = (label5.Font.Style & FontStyle.Underline) == FontStyle.Underline;

            // 根据当前状态切换Bold样式
            Font newFont;
            if (isUnderline)
            {
                // 如果已加粗，则创建一个新的Font对象，去除Underline样式
                // 保留原字体的其他样式（如斜体、下划线等）
                newFont = new Font(label5.Font.Name, label5.Font.Size, label5.Font.Style & ~FontStyle.Underline);
            }
            else
            {
                // 如果没有加粗，则创建一个新的Font对象，添加Underline样式
                newFont = new Font(label5.Font.Name, label5.Font.Size, label5.Font.Style | FontStyle.Underline);
            }

            // 应用新的Font到Label
            label5.Font = newFont;
        }

        /// <summary>
        /// 设置更多前景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            // 使用ColorDialog来选择前景色
            using (ColorDialog colorDialogFore = new ColorDialog())
            {
                // 初始颜色设置为Label当前的背景色
                colorDialogFore.Color = label1.ForeColor;
                if (colorDialogFore.ShowDialog() == DialogResult.OK)
                {
                    label5.ForeColor = colorDialogFore.Color;
                }
            }
        }

        /// <summary>
        /// 设置更多背景色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            // 使用ColorDialog来选择背景色
            using (ColorDialog colorDialogBack = new ColorDialog())
            {
                // 初始颜色设置为Label当前的背景色
                colorDialogBack.Color = label1.BackColor;
                if (colorDialogBack.ShowDialog() == DialogResult.OK)
                {
                    label5.BackColor = colorDialogBack.Color;
                }
            }
        }

        /// <summary>
        /// 设置更多字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            // 使用FontDialog来选择字体
            using (FontDialog fontDialog = new FontDialog())
            {
                // 初始字体
                fontDialog.Font = label5.Font;

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    label5.Font = fontDialog.Font;
                }
            }
        }

        /// <summary>
        /// 编辑配置项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // 处理选中项
                ListViewItem selectedItem = listView1.SelectedItems[0];
                if (selectedItem != null)
                {
                    DgvConditionalConfig cfg = new DgvConditionalConfig();
                    cfg = dgvConditionalConfigs[selectedItem.Index];
                    if (cfg != null)
                    {
                        try
                        {
                            // 设置规则类型 和 依赖列名
                            comboBox1.SelectedIndex = (int)cfg.RuleType;
                            comboBox2.Text = cfg?.ColumnName ?? string.Empty;
                            comboBox5.Text = cfg?.DependentColumns?.LastOrDefault() ?? string.Empty;
                        }
                        catch (Exception) { }

                        textBox1.Text = cfg.Value1;
                        textBox2.Text = cfg.Value2;
                        label5.Font = cfg.Font;
                        label5.ForeColor = cfg.ForeColor;
                        label5.BackColor = cfg.BackColor;
                        cbFullRow.Checked = cfg.FullRow;
                        cbAllowStyleStacking.Checked = cfg.AllowStyleStacking;

                        // 获取逻辑表达式类型
                        LogicalType expression = cfg.LogicalType;

                        // 处理表达式
                        if (expression == LogicalType.Between)
                        {
                            comboBox4.Text = localizer.GetString("Between");
                        }
                        else if (expression == LogicalType.NotBetween)
                        {
                            comboBox4.Text = localizer.GetString("NotBetween");
                        }
                        else if (expression == LogicalType.Contains)
                        {
                            comboBox4.Text = localizer.GetString("Contains");
                        }
                        else if (expression == LogicalType.DoesNotContain)
                        {
                            comboBox4.Text = localizer.GetString("DoesNotContain");
                        }
                        else if (expression == LogicalType.Equals)
                        {
                            comboBox4.Text = localizer.GetString("Equal");
                        }
                        else if (expression == LogicalType.NotEquals)
                        {
                            comboBox4.Text = localizer.GetString("NotEqual");
                        }
                        else if (expression == LogicalType.GreaterThan)
                        {
                            comboBox4.Text = localizer.GetString("GreaterThan");
                        }
                        else if (expression == LogicalType.LessThan)
                        {
                            comboBox4.Text = localizer.GetString("LessThan");
                        }
                        else if (expression == LogicalType.GreaterThanOrEquals)
                        {
                            comboBox4.Text = localizer.GetString("GreaterThanOrEqual");
                        }
                        else if (expression == LogicalType.LessThanOrEquals)
                        {
                            comboBox4.Text = localizer.GetString("LessThanOrEqual");
                        }

                        // 修改标记位
                        edit = true;
                        button1.Text = localizer.GetString("Modify");
                        editIndex = selectedItem.Index;
                    }
                }
            }
        }

        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            // 获取选中的项并做相应处理
            if (listView1.SelectedItems.Count > 0)
            {
                // 处理选中项
                ListViewItem selectedItem = listView1.SelectedItems[0];
                if (selectedItem != null)
                {
                    dgvConditionalConfigs.RemoveAt(selectedItem.Index);
                    selectedItem.Remove();
                }
            }
        }

        /// <summary>
        /// 双击开启修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            // 修改
            toolStripMenuItem3_Click(sender, e);
        }

        /// <summary>
        /// 清空配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            // 清空
            dgvConditionalConfigs.Clear();
            listView1.Items.Clear();
        }

        /// <summary>
        /// 选择，选择规则类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选择规则类型
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    label8.Visible = false;
                    textBox1.ReadOnly = false;
                    comboBox5.Visible = false;
                    cbFullRow.Checked = false;
                    cbFullRow.Enabled = true;
                    textBox1.Visible = true;
                    textBox2.Visible = true;
                    label4.Visible = true;
                    textBox1.Text = string.Empty;
                    break;

                case 1:
                    label8.Visible = true;
                    textBox1.ReadOnly = true;
                    comboBox5.Visible = true;
                    cbFullRow.Checked = true;
                    cbFullRow.Enabled = false;
                    textBox1.Visible = false;
                    textBox2.Visible = false;
                    label4.Visible = false;
                    textBox1.Text = "N/A";
                    break;
            }

            comboBox2.SelectedIndex = -1; // 清除列选择
        }

        #region 颜色选择

        private void label9_Click(object sender, EventArgs e)
        {
            label5.Font = label9.Font;
            label5.ForeColor = label9.ForeColor;
            label5.BackColor = label9.BackColor;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            label5.Font = label10.Font;
            label5.ForeColor = label10.ForeColor;
            label5.BackColor = label10.BackColor;
        }

        private void label11_Click(object sender, EventArgs e)
        {
            label5.Font = label11.Font;
            label5.ForeColor = label11.ForeColor;
            label5.BackColor = label11.BackColor;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            label5.Font = label12.Font;
            label5.ForeColor = label12.ForeColor;
            label5.BackColor = label12.BackColor;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            label5.Font = label13.Font;
            label5.ForeColor = label13.ForeColor;
            label5.BackColor = label13.BackColor;
        }

        private void label14_Click(object sender, EventArgs e)
        {
            label5.Font = label14.Font;
            label5.ForeColor = label14.ForeColor;
            label5.BackColor = label14.BackColor;
        }

        private void label15_Click(object sender, EventArgs e)
        {
            label5.Font = label15.Font;
            label5.ForeColor = label15.ForeColor;
            label5.BackColor = label15.BackColor;
        }

        private void label16_Click(object sender, EventArgs e)
        {
            label5.Font = label16.Font;
            label5.ForeColor = label16.ForeColor;
            label5.BackColor = label16.BackColor;
        }

        private void label17_Click(object sender, EventArgs e)
        {
            label5.Font = label17.Font;
            label5.ForeColor = label17.ForeColor;
            label5.BackColor = label17.BackColor;
        }

        private void label18_Click(object sender, EventArgs e)
        {
            label5.Font = label18.Font;
            label5.ForeColor = label18.ForeColor;
            label5.BackColor = label18.BackColor;
        }

        #endregion 颜色选择
    }
}