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
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 表示用于管理和应用数据网格自定义筛选配置的表单
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    public partial class FrmFindWindows : Form
    {
        public string FilterText;
        public DgvFindType findType { get; set; }
        public List<DgvCustomFilterConfig> dgvCustomFilterConfigs = new List<DgvCustomFilterConfig>();
        private readonly Localization.Localizer localizer = Localization.Localizer.Instance;

        public FrmFindWindows()
        {
            InitializeComponent();

            // 初始化本地化资源
            InitializeLocalization();
        }

        /// <summary>
        /// 初始化本地化资源
        /// </summary>
        private void InitializeLocalization()
        {
            // 窗体标题
            this.Text = localizer.GetString("FindAndFilter");

            // 设置控件本地化键
            this.TabPage1.Text = localizer.GetString("Find");
            this.TabPage2.Text = localizer.GetString("Filter");
            this.TabPage3.Text = localizer.GetString("Manager");
            this.Label1.SetLocalizationKey("FindWhat");
            this.Label2.SetLocalizationKey("SearchScope");
            this.Label3.SetLocalizationKey("MatchMode");
            this.Label4.SetLocalizationKey("SearchDirection");
            this.CheckBox1.SetLocalizationKey("CaseSensitive");
            this.CheckBox2.SetLocalizationKey("SearchByFormat");
            this.CheckBox3.SetLocalizationKey("FuzzySearchByFirstLetter");
            this.ButtonFind1.SetLocalizationKey("FindNext");
            this.ButtonClose1.SetLocalizationKey("Cancel");
            // 范围
            this.ComboBox1.Items.Clear();
            this.ComboBox1.Items.Add(localizer.GetString("CurrentField"));
            this.ComboBox1.Items.Add(localizer.GetString("GlobalSearch"));
            this.ComboBox1.SelectedIndex = 0;
            // 模式
            this.ComboBox2.Items.Clear();
            this.ComboBox2.Items.Add(localizer.GetString("AnyPartOfField"));
            this.ComboBox2.Items.Add(localizer.GetString("EntireField"));
            this.ComboBox2.Items.Add(localizer.GetString("StartOfField"));
            this.ComboBox2.Items.Add(localizer.GetString("EndOfField"));
            this.ComboBox2.Items.Add(localizer.GetString("NotEqual"));
            this.ComboBox2.Items.Add(localizer.GetString("DoesNotContain"));
            this.ComboBox2.Items.Add(localizer.GetString("DoesNotStartWith"));
            this.ComboBox2.Items.Add(localizer.GetString("DoesNotEndWith"));
            this.ComboBox2.Items.Add(localizer.GetString("LessThan"));
            this.ComboBox2.Items.Add(localizer.GetString("GreaterThan"));
            this.ComboBox2.Items.Add(localizer.GetString("Period"));
            this.ComboBox2.SelectedIndex = 0;
            // 方向
            this.ComboBox3.Items.Clear();
            this.ComboBox3.Items.Add(localizer.GetString("All"));
            this.ComboBox3.Items.Add(localizer.GetString("Up"));
            this.ComboBox3.Items.Add(localizer.GetString("Down"));
            this.ComboBox3.SelectedIndex = 0;
            // 筛选标签
            this.Label8.SetLocalizationKey("FilterContent");
            this.Label7.SetLocalizationKey("FilterScope");
            this.Label6.SetLocalizationKey("MatchMode");
            this.Label5.SetLocalizationKey("FilterDirection");
            this.CheckBox6.SetLocalizationKey("CaseSensitive");
            this.CheckBox_Find_Or.SetLocalizationKey("MeetAnyCondition");
            this.CheckBox4.SetLocalizationKey("FuzzySearchByFirstLetter");
            // 范围
            this.ComboBox6.Items.Clear();
            this.ComboBox6.Items.Add(localizer.GetString("CurrentField"));
            this.ComboBox6.Items.Add(localizer.GetString("GlobalSearch"));
            this.ComboBox6.SelectedIndex = 0;
            // 模式
            this.ComboBox5.Items.Clear();
            this.ComboBox5.Items.Add(localizer.GetString("AnyPartOfField"));
            this.ComboBox5.Items.Add(localizer.GetString("EntireField"));
            this.ComboBox5.Items.Add(localizer.GetString("StartOfField"));
            this.ComboBox5.Items.Add(localizer.GetString("EndOfField"));
            this.ComboBox5.Items.Add(localizer.GetString("NotEqual"));
            this.ComboBox5.Items.Add(localizer.GetString("DoesNotContain"));
            this.ComboBox5.Items.Add(localizer.GetString("DoesNotStartWith"));
            this.ComboBox5.Items.Add(localizer.GetString("DoesNotEndWith"));
            this.ComboBox5.Items.Add(localizer.GetString("LessThan"));
            this.ComboBox5.Items.Add(localizer.GetString("GreaterThan"));
            this.ComboBox5.Items.Add(localizer.GetString("Period"));
            this.ComboBox5.SelectedIndex = 0;
            // 方向
            this.ComboBox4.Items.Clear();
            this.ComboBox4.Items.Add(localizer.GetString("All"));
            this.ComboBox4.Items.Add(localizer.GetString("Up"));
            this.ComboBox4.Items.Add(localizer.GetString("Down"));
            this.ComboBox4.SelectedIndex = 0;
            // 按钮
            this.ButtonFind2.SetLocalizationKey("Filter");
            this.ButtonClose2.SetLocalizationKey("Cancel");

            // 自定义筛选管理器
            this.ButtonAddCFG.SetLocalizationKey("Add");
            this.ButtonSaveFiltersConfig.SetLocalizationKey("Apply");
            this.label9.SetLocalizationKey("MenuName");

            // 菜单
            this.ToolStripMenuItem_add.SetLocalizationKey("New");
            this.ToolStripMenuItem_edit.SetLocalizationKey("Modify");
            this.ToolStripMenuItem_del.SetLocalizationKey("Delete");
            this.toolStripMenuItem_moveUp.SetLocalizationKey("MoveUp");
            this.toolStripMenuItem_moveDown.SetLocalizationKey("MoveDown");

            // 应用本地化到整个窗体
            this.ApplyLocalization();
        }

        /// <summary>
        /// 触发点击事件，委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <param name="rect"></param>
        public delegate void ClickHandler(object sender, EventArgs e, DgvFindType findType, string txt, string txt2, bool isOr = false);

        /// <summary>
        /// 触发点击保存筛选配置事件，委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <param name="rect"></param>
        public delegate void SaveFiltersHandler(object sender, EventArgs e, List<DgvCustomFilterConfig> dgvCustomFilterConfigs);

        /// <summary>
        /// 触发查询，事件
        /// </summary>
        public event ClickHandler QueryClick;

        /// <summary>
        /// 触发筛选，事件
        /// </summary>
        public event ClickHandler FindClick;

        /// <summary>
        /// 保存自定义筛选配置，事件
        /// </summary>
        public event SaveFiltersHandler SaveFiltersConfigClick;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindWindows_Load(object sender, EventArgs e)
        {
            //this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 80;
            txtcznr.Enter += txtcznr_Enter;
            txtsxnr.Enter += txtsxnr_Enter;
            ButtonFind1.Click += ButtonFind1_Click;
            ButtonFind2.Click += ButtonFind2_Click;
            ButtonSaveFiltersConfig.Click += ButtonSaveFiltersConfig_Click;
            ButtonClose1.Click += ButtonClose_Click;
            ButtonClose2.Click += ButtonClose_Click;
            ButtonAddCFG.Click += ButtonAddCFG_Click;

            switch (findType)
            {
                case DgvFindType.Contains:
                    ComboBox2.Text = localizer.GetString("AnyPartOfField");
                    break;

                case DgvFindType.Equals:
                    ComboBox2.Text = localizer.GetString("EntireField");
                    break;

                case DgvFindType.BeginsWith:
                    ComboBox2.Text = localizer.GetString("StartOfField");
                    break;

                case DgvFindType.EndsWith:
                    ComboBox2.Text = localizer.GetString("EndOfField");
                    break;

                case DgvFindType.NotEquals:
                    ComboBox2.Text = localizer.GetString("NotEqual");
                    break;

                case DgvFindType.NotContains:
                    ComboBox2.Text = localizer.GetString("DoesNotContain");
                    break;

                case DgvFindType.NotBeginsWith:
                    ComboBox2.Text = localizer.GetString("DoesNotStartWith");
                    break;

                case DgvFindType.NotEndsWith:
                    ComboBox2.Text = localizer.GetString("DoesNotEndWith");
                    break;

                case DgvFindType.LessThan:
                    ComboBox2.Text = localizer.GetString("LessThan");
                    break;

                case DgvFindType.GreaterThan:
                    ComboBox2.Text = localizer.GetString("GreaterThan");
                    break;

                case DgvFindType.Period:
                    ComboBox2.Text = localizer.GetString("Period");
                    break;

                default:
                    ComboBox2.Text = localizer.GetString("AnyPartOfField");
                    break;
            }

            // 联动筛选页面的查找类型
            ComboBox5.Text = ComboBox2.Text;

            // 这将使 ComboBox 显示为下拉列表，用户只能从下拉列表中选择项，而不能在文本框部分输入文本
            ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox6.DropDownStyle = ComboBoxStyle.DropDownList;

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
            listView1.Columns.Add(localizer.GetString("MenuName"), 95, HorizontalAlignment.Center);
            listView1.Columns.Add(localizer.GetString("FilterExpression"), 250, HorizontalAlignment.Left);

            // 自定义筛选管理器
            listView1.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);

            // 显示预设配置
            if (dgvCustomFilterConfigs != null)
            {
                foreach (var cfg in dgvCustomFilterConfigs)
                {
                    ListViewItem lvItem = new ListViewItem(cfg.MenuText);
                    lvItem.SubItems.Add(cfg.FilterText.ToString());
                    listView1.Items.Add(lvItem);
                }
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // 获取选中的项并做相应处理
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // 获取选中项的索引
                int selectedIndex = selectedItem.Index;

                // 判断是否是第一个选中的项
                bool isSelectedFirst = (selectedIndex == 0);

                // 判断是否是最后一个选中的项
                bool isSelectedLast = (selectedIndex == listView1.Items.Count - 1);

                // 根据选中项，设置右击菜单的可用状态
                toolStripMenuItem_moveUp.Enabled = !isSelectedFirst;
                toolStripMenuItem_moveDown.Enabled = !isSelectedLast;
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ButtonFind1_Click(object sender, EventArgs e)
        {
            // 验证输入信息的有效性，判断空字符
            if (txtcznr.Text == string.Empty)
            {
                MessageBox.Show(localizer.GetString("SearchContentCannotBeEmpty"), localizer.GetString("InfoIncomplete"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            QueryClick?.Invoke(this, e, findType, txtcznr.Text, string.Empty);
            Close();
        }

        /// <summary>
        /// 筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ButtonFind2_Click(object sender, EventArgs e)
        {
            // 验证输入信息的有效性，判断空字符
            if (txtsxnr.Text == string.Empty || (txtsxnr2.Text == string.Empty && txtsxnr2.Visible))
            {
                MessageBox.Show(localizer.GetString("FilterContentCannotBeEmpty"), localizer.GetString("InfoIncomplete"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FindClick?.Invoke(this, e, findType, txtsxnr.Text, txtsxnr2.Text, CheckBox_Find_Or.Checked);
            Close();
        }

        /// <summary>
        /// 新增 CFC 自定义筛选配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ButtonAddCFG_Click(object sender, EventArgs e)
        {
            // 初始化
            if (dgvCustomFilterConfigs == null)
            {
                dgvCustomFilterConfigs = new List<DgvCustomFilterConfig>();
            }

            // 数据有效性验证
            if (textBox1.Visible && textBox1.Text == String.Empty)
            {
                MessageBox.Show(localizer.GetString("MenuNameContentCannotBeEmpty"), localizer.GetString("OperationFailed"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (FilterText == null || FilterText == string.Empty || FilterText == "")
            {
                MessageBox.Show(localizer.GetString("FilterContentCannotBeEmpty"), localizer.GetString("OperationFailed"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            // 构建对象
            dgvCustomFilterConfigs.Add(new DgvCustomFilterConfig { MenuText = this.textBox1.Text, FilterText = this.FilterText, Group = 1, Sort = dgvCustomFilterConfigs.Count });

            // 清除
            listView1.Items.Clear();

            // 排序
            dgvCustomFilterConfigs.Sort();

            // 显示预设配置
            if (dgvCustomFilterConfigs != null)
            {
                foreach (var cfg in dgvCustomFilterConfigs)
                {
                    ListViewItem lvItem = new ListViewItem(cfg.MenuText);
                    lvItem.SubItems.Add(cfg.FilterText.ToString());
                    listView1.Items.Add(lvItem);
                }
            }
        }

        /// <summary>
        /// 保存筛选管理器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ButtonSaveFiltersConfig_Click(object sender, EventArgs e)
        {
            SaveFiltersConfigClick?.Invoke(this, e, dgvCustomFilterConfigs);
            Close();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 进入控件时发生，设置默认相应按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtcznr_Enter(object sender, EventArgs e)
        {
            AcceptButton = ButtonFind1;
            CancelButton = ButtonClose1;
        }

        /// <summary>
        /// 进入控件时发生，设置默认相应按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtsxnr_Enter(object sender, EventArgs e)
        {
            AcceptButton = ButtonFind2;
            CancelButton = ButtonClose2;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex == 0)
            {
                // 获得焦点
                txtcznr.Focus();

                // 将光标移动到文本末尾
                txtcznr.SelectionStart = txtcznr.Text.Length;

                // 默认状态，全选已有文本
                txtcznr.SelectAll();
            }
            else
            {
                // 获得焦点
                txtsxnr.Focus();

                // 将光标移动到文本末尾
                txtsxnr.SelectionStart = txtsxnr.Text.Length;

                // 默认状态，全选已有文本
                txtsxnr.SelectAll();
            }
        }

        private void FindWindows_Shown(object sender, EventArgs e)
        {
            if (TabControl1.SelectedIndex == 0)
            {
                // 获得焦点
                txtcznr.Focus();

                // 将光标移动到文本末尾
                txtcznr.SelectionStart = txtcznr.Text.Length;

                // 默认状态，全选已有文本
                txtcznr.SelectAll();
            }
            else
            {
                // 获得焦点
                txtsxnr.Focus();

                // 将光标移动到文本末尾
                txtsxnr.SelectionStart = txtsxnr.Text.Length;

                // 默认状态，全选已有文本
                txtsxnr.SelectAll();
            }

            // 初始屏幕位置
            this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 80;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 检查 ComboBox 是否有选中的项
            if (ComboBox2.SelectedIndex != -1)
            {
                // 获取选中的项
                string selectedItem = ComboBox2.SelectedItem.ToString();

                SetFindTypeOfSelectIndex(selectedItem);
            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 检查 ComboBox 是否有选中的项
            if (ComboBox5.SelectedIndex != -1)
            {
                // 获取选中的项
                string selectedItem = ComboBox5.SelectedItem.ToString();

                SetFindTypeOfSelectIndex(selectedItem);

                // 处理类型为"期间"时的UI逻辑
                if (selectedItem == localizer.GetString("Period"))
                {
                    // 显示两个输入框UI
                    txtsxnr.Width = 140;
                    txtsxnr2.Visible = true;
                }
                else
                {
                    // 显示一个输入框UI
                    txtsxnr.Width = 282;
                    txtsxnr2.Visible = false;
                }
            }
        }

        /// <summary>
        /// 根据选择的文本，设置筛选模式
        /// </summary>
        /// <param name="selectedItem"></param>
        private void SetFindTypeOfSelectIndex(string selectedItem)
        {
            // 预先获取所有本地化字符串
            string anyPart = localizer.GetString("AnyPartOfField");
            string entireField = localizer.GetString("EntireField");
            string startOfField = localizer.GetString("StartOfField");
            string endOfField = localizer.GetString("EndOfField");
            string notEqual = localizer.GetString("NotEqual");
            string doesNotContain = localizer.GetString("DoesNotContain");
            string doesNotStartWith = localizer.GetString("DoesNotStartWith");
            string doesNotEndWith = localizer.GetString("DoesNotEndWith");
            string lessThan = localizer.GetString("LessThan");
            string greaterThan = localizer.GetString("GreaterThan");
            string period = localizer.GetString("Period");

            // 使用 if-else 进行比较
            if (selectedItem == anyPart)
                findType = DgvFindType.Contains;
            else if (selectedItem == entireField)
                findType = DgvFindType.Equals;
            else if (selectedItem == startOfField)
                findType = DgvFindType.BeginsWith;
            else if (selectedItem == endOfField)
                findType = DgvFindType.EndsWith;
            else if (selectedItem == notEqual)
                findType = DgvFindType.NotEquals;
            else if (selectedItem == doesNotContain)
                findType = DgvFindType.NotContains;
            else if (selectedItem == doesNotStartWith)
                findType = DgvFindType.NotBeginsWith;
            else if (selectedItem == doesNotEndWith)
                findType = DgvFindType.NotEndsWith;
            else if (selectedItem == lessThan)
                findType = DgvFindType.LessThan;
            else if (selectedItem == greaterThan)
                findType = DgvFindType.GreaterThan;
            else if (selectedItem == period)
                findType = DgvFindType.Period;
            else
                findType = DgvFindType.Contains; // 默认值
        }

        /// <summary>
        /// 删除一条选中的，自定义筛选配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_del_Click(object sender, EventArgs e)
        {
            // 获取选中的项并做相应处理
            if (listView1.SelectedItems.Count > 0)
            {
                // 处理选中项
                ListViewItem selectedItem = listView1.SelectedItems[0];
                if (selectedItem != null)
                {
                    dgvCustomFilterConfigs.RemoveAt(selectedItem.Index);
                    selectedItem.Remove();

                    // 重建 Sort 数据的断裂
                    if (dgvCustomFilterConfigs != null)
                    {
                        // 排序
                        dgvCustomFilterConfigs.Sort();

                        // 循环重建
                        int i = 0;
                        foreach (var cfg in dgvCustomFilterConfigs)
                        {
                            cfg.Sort = i++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 上移，调整菜单的顺序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_moveUp_Click(object sender, EventArgs e)
        {
            // 获取选中的项并做相应处理
            if (listView1.SelectedItems.Count > 0)
            {
                // 处理选中项
                ListViewItem selectedItem = listView1.SelectedItems[0];
                if (selectedItem != null)
                {
                    // 指定要交换 Sort 值的索引
                    int indexToSwap = selectedItem.Index;

                    // 检查索引是否有效
                    if (indexToSwap > 0 && indexToSwap < dgvCustomFilterConfigs.Count)
                    {
                        // 获取指定索引的对象和前一个对象的引用
                        DgvCustomFilterConfig currentItem = dgvCustomFilterConfigs[indexToSwap];
                        DgvCustomFilterConfig previousItem = dgvCustomFilterConfigs[indexToSwap - 1];

                        // 交换Sort值
                        int tempSort = currentItem.Sort;
                        currentItem.Sort = previousItem.Sort;
                        previousItem.Sort = tempSort;

                        // 清除
                        listView1.Items.Clear();

                        // 排序
                        dgvCustomFilterConfigs.Sort();

                        // 显示预设配置
                        if (dgvCustomFilterConfigs != null)
                        {
                            foreach (var cfg in dgvCustomFilterConfigs)
                            {
                                ListViewItem lvItem = new ListViewItem(cfg.MenuText);
                                lvItem.SubItems.Add(cfg.FilterText.ToString());
                                listView1.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 下移，调整菜单顺序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_moveDown_Click(object sender, EventArgs e)
        {
            // 获取选中的项并做相应处理
            if (listView1.SelectedItems.Count > 0)
            {
                // 处理选中项
                ListViewItem selectedItem = listView1.SelectedItems[0];
                if (selectedItem != null)
                {
                    // 指定要交换 Sort 值的索引
                    int indexToSwap = selectedItem.Index;

                    // 检查索引是否有效
                    if (indexToSwap >= 0 && indexToSwap < dgvCustomFilterConfigs.Count - 1)
                    {
                        // 获取指定索引的对象和前一个对象的引用
                        DgvCustomFilterConfig currentItem = dgvCustomFilterConfigs[indexToSwap];
                        DgvCustomFilterConfig nextItem = dgvCustomFilterConfigs[indexToSwap + 1];

                        // 交换Sort值
                        int tempSort = currentItem.Sort;
                        currentItem.Sort = nextItem.Sort;
                        nextItem.Sort = tempSort;

                        // 清除
                        listView1.Items.Clear();

                        // 排序
                        dgvCustomFilterConfigs.Sort();

                        // 显示预设配置
                        if (dgvCustomFilterConfigs != null)
                        {
                            foreach (var cfg in dgvCustomFilterConfigs)
                            {
                                ListViewItem lvItem = new ListViewItem(cfg.MenuText);
                                lvItem.SubItems.Add(cfg.FilterText.ToString());
                                listView1.Items.Add(lvItem);
                            }
                        }
                    }
                }
            }
        }
    }
}