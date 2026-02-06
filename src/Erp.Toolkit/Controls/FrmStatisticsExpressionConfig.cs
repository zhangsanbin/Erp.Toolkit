/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-07-16           Andy        Split, restructure
 * 2026-01-02           Andy        Fixed issues, string support for DistinctCount
 * 2026-01-03           Andy        Added new statistical indicators to UI
 */

using Erp.Toolkit.Localization;
using System;
using System.Data;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 统计表达式配置窗体
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    public partial class FrmStatisticsExpressionConfig : Form
    {
        private DataTable _dataTable;
        private DgvAggregatorConfig _aggregatorConfig;
        private string _guid;
        private readonly Localization.Localizer localizer = Localization.Localizer.Instance;

        /// <summary>
        /// 关闭时传参 Configs，委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="configs"></param>
        public delegate void ClosedHandler(object sender, EventArgs e, DgvAggregatorConfig aggregatorConfig);

        /// <summary>
        /// 关闭时传参，事件
        /// </summary>
        public event ClosedHandler ClosedEvent;

        public FrmStatisticsExpressionConfig(DataTable dataTable, DgvAggregatorConfig aggregatorConfig, string guid)
        {
            InitializeComponent();

            _dataTable = dataTable?.Copy() ?? new DataTable();
            _aggregatorConfig = aggregatorConfig ?? new DgvAggregatorConfig();
            _guid = guid;

            // 初始化分组列（支持所有数据类型）
            foreach (DataColumn column in _dataTable.Columns)
            {
                checkedListBox1.Items.Add(column.ColumnName);
            }

            // 初始化统计字段下拉列表（根据当前选择的统计方法）
            UpdateStatisticFieldsComboBox();

            // 设置视图模式为细节视图
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
            listView1.HeaderStyle = ColumnHeaderStyle.Clickable;

            // 添加列
            listView1.Columns.Add(localizer.GetString("ColumnName"), 130, HorizontalAlignment.Center);
            listView1.Columns.Add(localizer.GetString("ExpressionDescription"), 270, HorizontalAlignment.Left);

            // 显示预设配置
            if (_aggregatorConfig != null)
            {
                LoadExistingConfig();
            }

            // 默认值初始化
            comboBox1.SelectedIndex = 0;
            UpdateStatisticFieldsComboBox();

            // 初始化本地化资源
            InitializeLocalization();
        }

        /// <summary>
        /// 显示参数输入控件
        /// </summary>
        private void ShowParameterControls(string parameterType = "text")
        {
            lblParameter.Visible = true;

            if (parameterType == "text")
            {
                txtParameter.Visible = true;
                cmbQuartile.Visible = false;
            }
            else if (parameterType == "quartile")
            {
                txtParameter.Visible = false;
                cmbQuartile.Visible = true;
                if (cmbQuartile.SelectedIndex == -1)
                    cmbQuartile.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 隐藏参数输入控件
        /// </summary>
        private void HideParameterControls()
        {
            lblParameter.Visible = false;
            txtParameter.Visible = false;
            cmbQuartile.Visible = false;
        }

        /// <summary>
        /// 加载已有的配置
        /// </summary>
        private void LoadExistingConfig()
        {
            // 加载分组列
            foreach (var groupExpression in _aggregatorConfig.GroupColumns)
            {
                int index = checkedListBox1.Items.IndexOf(groupExpression);
                if (index >= 0)
                {
                    checkedListBox1.SetItemChecked(index, true);
                }
            }

            // 加载聚合列
            foreach (var selectExpression in _aggregatorConfig.AggregateColumns)
            {
                ListViewItem lvItem = new ListViewItem(selectExpression.ResultColumn);
                string description = selectExpression.AggregateType.ToString();
                if (selectExpression.AggregateType == DgvAggregatorType.Custom)
                {
                    description = $"{selectExpression.AggregateType}({selectExpression.CustomExpression})";
                }
                else
                {
                    description = $"{selectExpression.AggregateType}({selectExpression.SourceColumn})";
                }

                // 如果有参数，显示参数
                if (selectExpression.Parameters != null && selectExpression.Parameters.Count > 0)
                {
                    foreach (var param in selectExpression.Parameters)
                    {
                        description += $" [{param.Key}:{param.Value}]";
                    }
                }

                lvItem.SubItems.Add(description);
                listView1.Items.Add(lvItem);
            }
        }

        /// <summary>
        /// 初始化本地化资源
        /// </summary>
        private void InitializeLocalization()
        {
            // 窗体标题
            this.Text = localizer.GetString("DataStatistics");

            // 标签
            this.label6.SetLocalizationKey("CategoryField");
            this.label1.SetLocalizationKey("AggregationExpression");
            this.groupBox1.SetLocalizationKey("NewAggregationExpression");
            this.label2.SetLocalizationKey("StatisticalIndicator");
            this.label3.SetLocalizationKey("StatisticField");
            this.label4.SetLocalizationKey("Rename");
            this.label7.SetLocalizationKey("CustomAggregation");
            this.button1.SetLocalizationKey("Add");
            this.button2.SetLocalizationKey("Cancel");
            this.button3.SetLocalizationKey("Apply");

            // 指标 - 添加新增的统计指标
            this.comboBox1.Items.Clear();
            this.comboBox1.Items.AddRange(new string[]
            {
                localizer.GetString("Sum"),
                localizer.GetString("Avg"),
                localizer.GetString("Max"),
                localizer.GetString("Min"),
                localizer.GetString("Count"),
                localizer.GetString("DistinctCount"),
                localizer.GetString("Product"),
                localizer.GetString("SumProduct"),
                localizer.GetString("Variance"),
                localizer.GetString("PopulationVariance"),
                localizer.GetString("StandardDeviation"),
                localizer.GetString("PopulationStandardDeviation"),
                localizer.GetString("Median"),
                localizer.GetString("Mode"),
                localizer.GetString("Quartile"),
                localizer.GetString("Percentile"),
                localizer.GetString("Skewness"),
                localizer.GetString("Kurtosis"),
                localizer.GetString("CoefficientOfVariation"),
                localizer.GetString("GeometricMean"),
                localizer.GetString("HarmonicMean"),
                localizer.GetString("Range"),
                localizer.GetString("SumAbsolute"),
                localizer.GetString("SumSquares"),
                localizer.GetString("RootMeanSquare"),
                localizer.GetString("Custom"),
            });

            // 默认选择“Count”
            this.comboBox1.SelectedIndex = 4;

            // 右键菜单
            this.toolStripMenuItem1.SetLocalizationKey("Up");
            this.toolStripMenuItem2.SetLocalizationKey("Down");
            this.toolStripMenuItem3.SetLocalizationKey("Modify");
            this.toolStripMenuItem4.SetLocalizationKey("Delete");
            this.toolStripMenuItem5.SetLocalizationKey("Clear");
            this.toolStripMenuItem6.SetLocalizationKey("Copy");

            // 应用本地化到整个窗体
            this.ApplyLocalization();
        }

        /// <summary>
        /// 判断数据类型是否为数值 Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsNumericType(Type type)
        {
            if (type == null) return false;

            Type underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            return underlyingType == typeof(int) ||
                   underlyingType == typeof(long) ||
                   underlyingType == typeof(short) ||
                   underlyingType == typeof(byte) ||
                   underlyingType == typeof(uint) ||
                   underlyingType == typeof(ulong) ||
                   underlyingType == typeof(ushort) ||
                   underlyingType == typeof(sbyte) ||
                   underlyingType == typeof(float) ||
                   underlyingType == typeof(double) ||
                   underlyingType == typeof(decimal);
        }

        /// <summary>
        /// 判断统计方法是否支持非数值类型
        /// </summary>
        /// <param name="statisticMethod">统计方法名称</param>
        /// <returns></returns>
        private bool SupportsNonNumericTypes(string statisticMethod)
        {
            string count = localizer.GetString("Count");
            string distinctCount = localizer.GetString("DistinctCount");

            return statisticMethod == count || statisticMethod == distinctCount;
        }

        /// <summary>
        /// 判断统计方法是否仅支持数值类型
        /// </summary>
        /// <param name="statisticMethod">统计方法名称</param>
        /// <returns></returns>
        private bool RequiresNumericTypes(string statisticMethod)
        {
            string sum = localizer.GetString("Sum");
            string avg = localizer.GetString("Avg");
            string product = localizer.GetString("Product");
            string sumProduct = localizer.GetString("SumProduct");
            string variance = localizer.GetString("Variance");
            string populationVariance = localizer.GetString("PopulationVariance");
            string standardDeviation = localizer.GetString("StandardDeviation");
            string populationStandardDeviation = localizer.GetString("PopulationStandardDeviation");
            string median = localizer.GetString("Median");
            string mode = localizer.GetString("Mode");
            string quartile = localizer.GetString("Quartile");
            string percentile = localizer.GetString("Percentile");
            string skewness = localizer.GetString("Skewness");
            string kurtosis = localizer.GetString("Kurtosis");
            string coefficientOfVariation = localizer.GetString("CoefficientOfVariation");
            string geometricMean = localizer.GetString("GeometricMean");
            string harmonicMean = localizer.GetString("HarmonicMean");
            string range = localizer.GetString("Range");
            string sumAbsolute = localizer.GetString("SumAbsolute");
            string sumSquares = localizer.GetString("SumSquares");
            string rootMeanSquare = localizer.GetString("RootMeanSquare");
            string custom = localizer.GetString("Custom");

            return statisticMethod == sum ||
                   statisticMethod == avg ||
                   statisticMethod == product ||
                   statisticMethod == sumProduct ||
                   statisticMethod == variance ||
                   statisticMethod == populationVariance ||
                   statisticMethod == standardDeviation ||
                   statisticMethod == populationStandardDeviation ||
                   statisticMethod == median ||
                   statisticMethod == mode ||
                   statisticMethod == quartile ||
                   statisticMethod == percentile ||
                   statisticMethod == skewness ||
                   statisticMethod == kurtosis ||
                   statisticMethod == coefficientOfVariation ||
                   statisticMethod == geometricMean ||
                   statisticMethod == harmonicMean ||
                   statisticMethod == range ||
                   statisticMethod == sumAbsolute ||
                   statisticMethod == sumSquares ||
                   statisticMethod == rootMeanSquare ||
                   statisticMethod == custom;
        }

        /// <summary>
        /// 判断统计方法是否支持字符串类型
        /// </summary>
        /// <param name="statisticMethod">统计方法名称</param>
        /// <returns></returns>
        private bool SupportsStringTypes(string statisticMethod)
        {
            string max = localizer.GetString("Max");
            string min = localizer.GetString("Min");
            string count = localizer.GetString("Count");
            string distinctCount = localizer.GetString("DistinctCount");
            string mode = localizer.GetString("Mode");

            return statisticMethod == max ||
                   statisticMethod == min ||
                   statisticMethod == count ||
                   statisticMethod == distinctCount ||
                   statisticMethod == mode;
        }

        /// <summary>
        /// 选择统计指标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string product = localizer.GetString("Product");
            string sumProduct = localizer.GetString("SumProduct");
            string quartile = localizer.GetString("Quartile");
            string percentile = localizer.GetString("Percentile");
            string custom = localizer.GetString("Custom");
            string selectedText = comboBox1.Text;

            // 默认禁用自定义表达式输入
            textBox1.Enabled = false;

            // 控制 UI 显示
            if (selectedText == product || selectedText == sumProduct)
            {
                comboBox2.Width = 138;
                label5.Text = "*";
                label5.Visible = true;
                comboBox3.Visible = true;
                HideParameterControls();
            }
            else if (selectedText == quartile)
            {
                comboBox2.Width = 306;
                label5.Visible = false;
                comboBox3.Visible = false;
                ShowParameterControls("quartile");
                lblParameter.Text = localizer.GetString("Quartile") + " (1-3)";
            }
            else if (selectedText == percentile)
            {
                comboBox2.Width = 306;
                label5.Visible = false;
                comboBox3.Visible = false;
                ShowParameterControls("text");
                lblParameter.Text = localizer.GetString("Percentile") + " (0-100)";
                txtParameter.Text = "50";
            }
            else if (selectedText == custom)
            {
                textBox1.Enabled = true;
            }
            else
            {
                comboBox2.Width = 306;
                label5.Visible = false;
                comboBox3.Visible = false;
                HideParameterControls();
            }

            // 更新统计字段下拉列表
            UpdateStatisticFieldsComboBox();

            // 调用选择字段后事件
            comboBox2_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// 更新统计字段下拉列表
        /// </summary>
        private void UpdateStatisticFieldsComboBox()
        {
            string selectedStatistic = comboBox1.Text;
            string previouslySelectedField2 = comboBox2.Text;
            string previouslySelectedField3 = comboBox3.Text;

            // 清空下拉列表
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();

            // 根据统计方法确定支持的字段类型
            if (string.IsNullOrEmpty(selectedStatistic))
            {
                // 默认情况：只显示数值型字段
                LoadNumericFieldsToComboBoxes();
            }
            else if (SupportsNonNumericTypes(selectedStatistic))
            {
                // 支持非数值类型的统计方法（如计数、去重计次）
                LoadAllFieldsToComboBoxes();
            }
            else if (RequiresNumericTypes(selectedStatistic))
            {
                // 仅支持数值类型的统计方法
                LoadNumericFieldsToComboBoxes();
            }
            else
            {
                // 其他情况，默认加载所有字段
                LoadAllFieldsToComboBoxes();
            }

            // 尝试恢复之前的选择
            if (!string.IsNullOrEmpty(previouslySelectedField2) && comboBox2.Items.Contains(previouslySelectedField2))
            {
                comboBox2.SelectedItem = previouslySelectedField2;
            }
            else if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(previouslySelectedField3) && comboBox3.Items.Contains(previouslySelectedField3))
            {
                comboBox3.SelectedItem = previouslySelectedField3;
            }
            else if (comboBox3.Items.Count > 0)
            {
                comboBox3.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 加载所有字段到下拉列表
        /// </summary>
        private void LoadAllFieldsToComboBoxes()
        {
            foreach (DataColumn column in _dataTable.Columns)
            {
                comboBox2.Items.Add(column.ColumnName);
                comboBox3.Items.Add(column.ColumnName);
            }
        }

        /// <summary>
        /// 加载数值型字段到下拉列表
        /// </summary>
        private void LoadNumericFieldsToComboBoxes()
        {
            foreach (DataColumn column in _dataTable.Columns)
            {
                if (IsNumericType(column.DataType))
                {
                    comboBox2.Items.Add(column.ColumnName);
                    comboBox3.Items.Add(column.ColumnName);
                }
            }
        }

        /// <summary>
        /// 选择字段后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 选择字段
            if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(comboBox2.Text))
            {
                string count = localizer.GetString("Count");
                string distinctCount = localizer.GetString("DistinctCount");

                if (comboBox1.Text == count || comboBox1.Text == distinctCount)
                {
                    // 对于计数和去重计次，使用简单的命名
                    textBox2.Text = comboBox1.Text + "-" + comboBox2.Text;
                }
                else
                {
                    textBox2.Text = comboBox2.Text + "-" + comboBox1.Text;
                }
            }
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // 添加
            ConcatenationExpression(comboBox1.Text);
        }

        // 拼接表达式
        private void ConcatenationExpression(string type)
        {
            DgvAggregatorColumnConfig dgvAggregatorColumnConfig = new DgvAggregatorColumnConfig();

            string selectedField = comboBox2.Text;
            string aliasName = textBox2.Text;

            if (string.IsNullOrEmpty(selectedField))
            {
                MessageBox.Show(localizer.GetString("PleaseSelectStatisticalField"));
                return;
            }

            if (string.IsNullOrEmpty(aliasName))
            {
                MessageBox.Show(localizer.GetString("PleaseSelectRenameField"));
                return;
            }

            // 验证字段类型是否适合统计方法
            if (!ValidateFieldTypeForStatistic(selectedField, type))
            {
                string message = string.Format(localizer.GetString("FieldTypeNotSupportedForStatistic"),
                    selectedField, type);
                MessageBox.Show(message);
                return;
            }

            string sum = localizer.GetString("Sum");
            string avg = localizer.GetString("Avg");
            string max = localizer.GetString("Max");
            string min = localizer.GetString("Min");
            string count = localizer.GetString("Count");
            string distinctCount = localizer.GetString("DistinctCount");
            string product = localizer.GetString("Product");
            string sumProduct = localizer.GetString("SumProduct");
            string variance = localizer.GetString("Variance");
            string populationVariance = localizer.GetString("PopulationVariance");
            string standardDeviation = localizer.GetString("StandardDeviation");
            string populationStandardDeviation = localizer.GetString("PopulationStandardDeviation");
            string median = localizer.GetString("Median");
            string mode = localizer.GetString("Mode");
            string quartile = localizer.GetString("Quartile");
            string percentile = localizer.GetString("Percentile");
            string skewness = localizer.GetString("Skewness");
            string kurtosis = localizer.GetString("Kurtosis");
            string coefficientOfVariation = localizer.GetString("CoefficientOfVariation");
            string geometricMean = localizer.GetString("GeometricMean");
            string harmonicMean = localizer.GetString("HarmonicMean");
            string range = localizer.GetString("Range");
            string sumAbsolute = localizer.GetString("SumAbsolute");
            string sumSquares = localizer.GetString("SumSquares");
            string rootMeanSquare = localizer.GetString("RootMeanSquare");
            string custom = localizer.GetString("Custom");

            // 获取源列的数据类型
            DataColumn sourceColumn = _dataTable.Columns[selectedField];
            Type sourceDataType = sourceColumn?.DataType ?? typeof(object);

            // 根据统计指标配置对象
            if (type == sum)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Sum{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Sum;
                dgvAggregatorColumnConfig.DataType = typeof(decimal);
            }
            else if (type == avg)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Avg{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Avg;
                dgvAggregatorColumnConfig.DataType = typeof(decimal);
            }
            else if (type == max)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Max{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Max;
                dgvAggregatorColumnConfig.DataType = sourceDataType;
            }
            else if (type == min)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Min{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Min;
                dgvAggregatorColumnConfig.DataType = sourceDataType;
            }
            else if (type == count)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Count{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Count;
                dgvAggregatorColumnConfig.DataType = typeof(int);
            }
            else if (type == distinctCount)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"DistinctCount{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.DistinctCount;
                dgvAggregatorColumnConfig.DataType = typeof(int);
            }
            else if (type == product)
            {
                comboBox2.Width = 138;
                label5.Text = "*";
                label5.Visible = true;
                comboBox3.Visible = true;
                // 拼接
                if (!string.IsNullOrEmpty(comboBox3.Text))
                {
                    dgvAggregatorColumnConfig.SourceColumn = $"{comboBox2.Text} * {comboBox3.Text}";
                    dgvAggregatorColumnConfig.ResultColumn = $"Product{textBox2.Text}";
                    dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Product;
                    dgvAggregatorColumnConfig.DataType = typeof(decimal);
                }
                else
                {
                    MessageBox.Show(localizer.GetString("PleaseSelectStatisticalField"));
                    return;
                }
            }
            else if (type == sumProduct)
            {
                comboBox2.Width = 138;
                label5.Text = "*";
                label5.Visible = true;
                comboBox3.Visible = true;
                // 拼接
                if (!string.IsNullOrEmpty(comboBox3.Text))
                {
                    dgvAggregatorColumnConfig.SourceColumn = $"{comboBox2.Text} * {comboBox3.Text}";
                    dgvAggregatorColumnConfig.ResultColumn = $"SumProduct{textBox2.Text}";
                    dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.SumProduct;
                    dgvAggregatorColumnConfig.DataType = typeof(decimal);
                }
                else
                {
                    MessageBox.Show(localizer.GetString("PleaseSelectStatisticalField"));
                    return;
                }
            }
            else if (type == variance)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Variance{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Variance;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == populationVariance)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"PopulationVariance{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.PopulationVariance;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == standardDeviation)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"StandardDeviation{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.StandardDeviation;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == populationStandardDeviation)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"PopulationStandardDeviation{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.PopulationStandardDeviation;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == median)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Median{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Median;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == mode)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Mode{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Mode;
                dgvAggregatorColumnConfig.DataType = sourceDataType;
            }
            else if (type == quartile)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Quartile{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Quartile;
                dgvAggregatorColumnConfig.DataType = typeof(double);

                // 添加四分位数参数
                if (cmbQuartile.SelectedItem != null)
                {
                    int quartileValue = (int)cmbQuartile.SelectedIndex + 1;
                    dgvAggregatorColumnConfig.Parameters["quartile"] = quartileValue;
                }
            }
            else if (type == percentile)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Percentile{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Percentile;
                dgvAggregatorColumnConfig.DataType = typeof(double);

                // 添加百分位数参数
                if (!string.IsNullOrEmpty(txtParameter.Text) && double.TryParse(txtParameter.Text, out double percentileValue))
                {
                    if (percentileValue >= 0 && percentileValue <= 100)
                    {
                        dgvAggregatorColumnConfig.Parameters["percentile"] = percentileValue;
                    }
                    else
                    {
                        MessageBox.Show(localizer.GetString("PercentileValueRangeError"));
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(localizer.GetString("InvalidPercentileValue"));
                    return;
                }
            }
            else if (type == skewness)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Skewness{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Skewness;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == kurtosis)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Kurtosis{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Kurtosis;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == coefficientOfVariation)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"CoefficientOfVariation{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.CoefficientOfVariation;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == geometricMean)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"GeometricMean{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.GeometricMean;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == harmonicMean)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"HarmonicMean{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.HarmonicMean;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == range)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"Range{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Range;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == sumAbsolute)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"SumAbsolute{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.SumAbsolute;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == sumSquares)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"SumSquares{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.SumSquares;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == rootMeanSquare)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{selectedField}";
                dgvAggregatorColumnConfig.ResultColumn = $"RootMeanSquare{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.RootMeanSquare;
                dgvAggregatorColumnConfig.DataType = typeof(double);
            }
            else if (type == custom)
            {
                comboBox2.Width = 306;
                label5.Text = "";
                label5.Visible = false;
                comboBox3.Visible = false;
                dgvAggregatorColumnConfig.SourceColumn = $"{textBox1.Text}";
                dgvAggregatorColumnConfig.ResultColumn = $"Custom{selectedField}";
                dgvAggregatorColumnConfig.AggregateType = DgvAggregatorType.Custom;
                dgvAggregatorColumnConfig.DataType = typeof(decimal);
                dgvAggregatorColumnConfig.CustomExpression = textBox1.Text;
            }
            else
            {
                throw new ArgumentException(localizer.GetString("UnknownTypeParameter") + type);
            }

            dgvAggregatorColumnConfig.ResultColumn = aliasName; // 创建聚合函数对应的 As Name

            // 更新到记录中
            _aggregatorConfig.AggregateColumns.Add(dgvAggregatorColumnConfig);

            ListViewItem lvItem = new ListViewItem(dgvAggregatorColumnConfig.ResultColumn);
            string description = dgvAggregatorColumnConfig.AggregateType.ToString();
            if (dgvAggregatorColumnConfig.AggregateType == DgvAggregatorType.Custom)
            {
                description = $"{dgvAggregatorColumnConfig.AggregateType}({dgvAggregatorColumnConfig.CustomExpression})";
            }
            else
            {
                description = $"{dgvAggregatorColumnConfig.AggregateType}({dgvAggregatorColumnConfig.SourceColumn})";
            }

            // 显示参数
            if (dgvAggregatorColumnConfig.Parameters != null && dgvAggregatorColumnConfig.Parameters.Count > 0)
            {
                foreach (var param in dgvAggregatorColumnConfig.Parameters)
                {
                    description += $" [{param.Key}:{param.Value}]";
                }
            }

            lvItem.SubItems.Add(description);
            listView1.Items.Add(lvItem);
        }

        /// <summary>
        /// 验证字段类型是否适合统计方法
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="statisticMethod">统计方法</param>
        /// <returns></returns>
        private bool ValidateFieldTypeForStatistic(string fieldName, string statisticMethod)
        {
            if (string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(statisticMethod))
                return false;

            DataColumn column = _dataTable.Columns[fieldName];
            if (column == null)
                return false;

            Type fieldType = column.DataType;
            bool isNumeric = IsNumericType(fieldType);
            bool isString = fieldType == typeof(string);

            string sum = localizer.GetString("Sum");
            string avg = localizer.GetString("Avg");
            string product = localizer.GetString("Product");
            string sumProduct = localizer.GetString("SumProduct");
            string variance = localizer.GetString("Variance");
            string populationVariance = localizer.GetString("PopulationVariance");
            string standardDeviation = localizer.GetString("StandardDeviation");
            string populationStandardDeviation = localizer.GetString("PopulationStandardDeviation");
            string median = localizer.GetString("Median");
            string quartile = localizer.GetString("Quartile");
            string percentile = localizer.GetString("Percentile");
            string skewness = localizer.GetString("Skewness");
            string kurtosis = localizer.GetString("Kurtosis");
            string coefficientOfVariation = localizer.GetString("CoefficientOfVariation");
            string geometricMean = localizer.GetString("GeometricMean");
            string harmonicMean = localizer.GetString("HarmonicMean");
            string range = localizer.GetString("Range");
            string sumAbsolute = localizer.GetString("SumAbsolute");
            string sumSquares = localizer.GetString("SumSquares");
            string rootMeanSquare = localizer.GetString("RootMeanSquare");

            // 需要数值类型的统计方法
            if (statisticMethod == sum || statisticMethod == avg ||
                statisticMethod == product || statisticMethod == sumProduct ||
                statisticMethod == variance || statisticMethod == populationVariance ||
                statisticMethod == standardDeviation || statisticMethod == populationStandardDeviation ||
                statisticMethod == median || statisticMethod == quartile || statisticMethod == percentile ||
                statisticMethod == skewness || statisticMethod == kurtosis ||
                statisticMethod == coefficientOfVariation || statisticMethod == geometricMean ||
                statisticMethod == harmonicMean || statisticMethod == range ||
                statisticMethod == sumAbsolute || statisticMethod == sumSquares ||
                statisticMethod == rootMeanSquare)
            {
                return isNumeric;
            }

            // 支持字符串类型的统计方法
            string count = localizer.GetString("Count");
            string distinctCount = localizer.GetString("DistinctCount");

            if (statisticMethod == count || statisticMethod == distinctCount)
            {
                // 计数和去重计次支持所有类型
                return true;
            }

            // 其他统计方法（如Max, Min, Mode, Custom）支持数值和字符串类型
            string max = localizer.GetString("Max");
            string min = localizer.GetString("Min");
            string mode = localizer.GetString("Mode");
            string custom = localizer.GetString("Custom");

            if (statisticMethod == max || statisticMethod == min ||
                statisticMethod == mode || statisticMethod == custom)
            {
                return isNumeric || isString;
            }

            return false;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // 处理选择的 GroupExpression
            if (checkedListBox1.CheckedItems.Count <= 0)
            {
                MessageBox.Show(localizer.GetString("PleaseSelectAtLeastOneCategoryField"));
                return;
            }

            // 判断对象是否为空
            if (_aggregatorConfig.AggregateColumns.Count <= 0)
            {
                MessageBox.Show(localizer.GetString("PleaseAddAtLeastOneAggregationExpression"));
                return;
            }

            // 清除原列表项目
            _aggregatorConfig.GroupColumns.Clear();

            // 遍历CheckedListBox的CheckedItems集合
            foreach (var item in checkedListBox1.CheckedItems)
            {
                // 将已选择的项目添加到列表中
                _aggregatorConfig.GroupColumns.Add(item.ToString());
            }

            // 创建 DataTable 的深拷贝副本，防止被修改污染原始数据
            DataTable dataTableCopy = _dataTable.Copy();

            // 执行查询并显示
            FrmStatistics frmStatistics = new FrmStatistics(dataTableCopy, _aggregatorConfig, _guid);
            frmStatistics.ShowDialog();

            // 关闭并回传保存
            ClosedEvent?.Invoke(this, e, _aggregatorConfig);

            // 关闭
            // this.Close();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            // 获取选中的项并做相应处理
            if (listView1.SelectedItems.Count > 0)
            {
                // 处理选中项
                ListViewItem selectedItem = listView1.SelectedItems[0];
                if (selectedItem != null)
                {
                    int index = selectedItem.Index;
                    if (index >= 0 && index < _aggregatorConfig.AggregateColumns.Count)
                    {
                        _aggregatorConfig.AggregateColumns.RemoveAt(index);
                        selectedItem.Remove();
                    }
                }
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            // 清空
            _aggregatorConfig.AggregateColumns.Clear();
            listView1.Items.Clear();
        }

        /// <summary>
        /// 自定义表达式文本框变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 当自定义表达式变化时，更新别名
            if (!string.IsNullOrEmpty(comboBox2.Text) && comboBox1.Text == localizer.GetString("Custom"))
            {
                textBox2.Text = "Custom-" + comboBox2.Text;
            }
        }
    }
}