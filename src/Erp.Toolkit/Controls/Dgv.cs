/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-07-13           Andy        Split, restructure
 */

using Erp.Toolkit.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 具有主从数据结构和多功能的 DataGridView 美好组件
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    [ToolboxBitmap(typeof(Dgv), "Erp.Toolkit.Resources.erp.toolkit.logo.ico")]
    public partial class Dgv : UserControl
    {
        /// <summary>
        /// 具有主从数据结构和多功能的 DataGridView 美好组件
        /// </summary>
        /// <remarks>
        /// 兼容框架：
        /// - .NET Framework 4.6.2+
        /// - .NET Core 3.1+ (Windows)
        /// - .NET 5/6/7/8+ (Windows)
        ///
        /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
        /// </remarks>
        public Dgv()
        {
            // 组件初始化
            InitializeComponent();

            // 初始化本地化资源
            InitializeLocalization();

            // 缓存，行头图标资源
            _collapseIcon = RowHeaderIconList.Images[(int)RowHeaderIcons.collapseIcon];
            _expandIcon = RowHeaderIconList.Images[(int)RowHeaderIcons.expandIcon];

            // 创建主题样式
            _themeColors = DgvThemeColors.GetColorsForTheme(_themeStyle);

            // 初始化主题样式
            SetThemeStyle();

            // 设置常量
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Margin = new Padding(0);
            this.dataGridView.RowHeadersWidth = 30;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.ColumnHeadersHeight = 30;
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.RowTemplate.Resizable = DataGridViewTriState.False;
            this.dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridView.BorderStyle = BorderStyle.None;
            this.dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // 初始化事件绑定
            this.dataGridView.Resize += new EventHandler(DataGridView_Resize);
            this.dataGridView.Sorted += new EventHandler(DataGridView_Sorted);
            this.dataGridView.Scroll += new ScrollEventHandler(DataGridView_Scroll);
            this.dataGridView.CellClick += new DataGridViewCellEventHandler(dataGridView_CellClick);
            this.dataGridView.CellMouseEnter += new DataGridViewCellEventHandler(dataGridView_CellMouseEnter);
            this.dataGridView.MouseClick += new MouseEventHandler(dataGridView_MouseClick);
            this.dataGridView.DataSourceChanged += new EventHandler(dataGridView_DataSourceChanged);
            this.dataGridView.CellValueChanged += new DataGridViewCellEventHandler(dataGridView_CellValueChanged);
            this.dataGridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(DataGridView_RowPostPaint);
            this.dataGridView.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(dataGridView_CellMouseDoubleClick);
            this.dataGridView.CellMouseClick += new DataGridViewCellMouseEventHandler(dataGridView_CellMouseClick);
            this.dataGridView.SelectionChanged += new EventHandler(dataGridView_SelectionChanged);
            this.dataGridView.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(DataGridView_RowHeaderMouseClick);
            this.toolStripTextBox_FindText.GotFocus += new EventHandler(toolStripTextBox_FindText_GotFocus);
            this.toolStripTextBox_FindText.LostFocus += new EventHandler(toolStripTextBox_FindText_LostFocus);
            this.dataGridView.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridView_DataBindingComplete);

            // 打印事件
            _printDocument = new PrintDocument();
            _printDocument.DefaultPageSettings.Margins.Top = 50;
            _printDocument.DefaultPageSettings.Margins.Bottom = 30;
            _printDocument.DefaultPageSettings.Margins.Left = 30;
            _printDocument.DefaultPageSettings.Margins.Right = 50;
            _printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            _printPreviewDialog = new PrintPreviewDialog();
            _printPreviewDialog.Document = _printDocument;
            _printPreviewDialog.Width = 1024;
            _printPreviewDialog.Height = 900;
            _printPreviewDialog.ShowIcon = true;
            _printPreviewDialog.Icon = Properties.Resources.mdf_ndf_dbfiles;
            _printPreviewDialog.StartPosition = FormStartPosition.CenterScreen;
            _pageSetupDialog = new PageSetupDialog();
            _pageSetupDialog.Document = _printDocument;
            _printDialog = new PrintDialog();
            _printDialog.Document = _printDocument;

            // 根据事件绑定情况，设置按钮状态
            UpdateButtonState();
        }

        #region 字段

        public int _rowIndex;//当前鼠标点击位置的行号
        public int _columnIndex;//当前鼠标点击位置的列号
        public string _columnName;//当前鼠标点击位置的列名
        public string _columnValue;//当前鼠标点击位置的值
        public string _columnValueType;//当前鼠标点击位置的类型
        private int _lastColumnIndex = -1;//最后点击的列索引
        private DataView _dataView;//主数据源
        private IReLoadListUI _owner;//父类或所有者，规范刷新接口
        private string _guid;//全球唯一标识
        private Pen _pen;//画笔
        private Image _collapseIcon;//展开图标
        private Image _expandIcon;//折叠图标
        private BindingSource _bindingSource;//数据源
        private BindingSource _subviewBindingSource;//子视图数据源
        private bool _subview;//启用了子视图
        private int _rowIndexnSubview;//子视图的当前行
        private List<int> _subviewCurrentRow = new List<int>();//主从当前行
        private SupportedLanguage _language = SupportedLanguage.English;//当前语言
        private ThemeStyle _themeStyle = ThemeStyle.BlueTheme;//系统样式
        private DgvThemeColors _themeColors;//样式颜色
        private bool _alternatingStyle = false;//是否启用，单元行交替样式
        private string _primaryKey = "Id";//数据主键
        private List<string> _filterHistory = new List<string>();//筛选历史记录
        private DgvFindType _findType = DgvFindType.Contains;//筛选模式
        private string _filterLogicalSymbols = "And";
        private List<DgvColumnInfoConfig> _columnInfos;//字段配置信息
        private PrintDocument _printDocument;//打印文档对象
        private PrintPreviewDialog _printPreviewDialog;//打印预览
        private PageSetupDialog _pageSetupDialog;//打印页面设置
        private PrintDialog _printDialog;//打印选择
        private int[] _printColumns;//要打印的列索引集合
        private Dictionary<string, string> _englishToChineseHeaders;//列标题中英文对照字典
        private List<DgvConditionalConfig> _dgvConditionalConfigs;//条件样式配置信息
        private List<DgvCustomFilterConfig> _dgvCustomFilterConfig;//自定义筛选配置信息
        private DgvAggregatorConfig _dgvAggregatorConfig = new DgvAggregatorConfig();//统计表达式
        private bool _isLoadingData = true; // 全局加载标志
        private bool _isSettingDisplayIndex = true;// 全局设置列显示顺序标志

        private StringFormat _stringFormat = new StringFormat()
        {
            FormatFlags = StringFormatFlags.LineLimit,//限制绘制的文本行数
            Trimming = StringTrimming.EllipsisCharacter,//文本超出可用空间时，末尾添加省略号
            Alignment = StringAlignment.Near,//水平靠左侧
            LineAlignment = StringAlignment.Center//垂直居中
        };//打印对齐方式

        private readonly Localization.Localizer localizer = Localization.Localizer.Instance;

        #endregion 字段

        #region 属性

        /// <summary>
        /// 主从数据的子视图对象
        /// </summary>
        public Dgv subview;

        /// <summary>
        /// 获取或设置数据的主键
        /// </summary>
        [DefaultValue("Id")]
        [Browsable(true)]
        [Description("获取或设置数据的主键")]
        public string PrimaryKey
        {
            get { return _primaryKey; }
            set
            {
                _primaryKey = value;
            }
        }

        /// <summary>
        /// 设置，表格内容只读模式，不可编辑和修改
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Description("设置，表格内容只读模式，不可编辑和修改")]
        public bool IsReadOnly
        {
            get { return dataGridView.ReadOnly; }
            set
            {
                dataGridView.ReadOnly = value;

                // 根据 Value 载入，所需的配置项或只读
                if (ColumnInfos != null && value == false)
                {
                    foreach (DgvColumnInfoConfig cInfo in ColumnInfos)
                    {
                        dataGridView.Columns[cInfo.Name].ReadOnly = cInfo.ReadOnly;
                    }
                }
            }
        }

        /// <summary>
        /// 全球唯一标识，用于保持和恢复 UI 布局配置
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [Description("全球唯一标识，用于保持和恢复 UI 布局配置")]
        public string Guid
        {
            get { return _guid; }
            set
            {
                if (value == _guid) return;

                _guid = value.ToUpper();

                // 优先尝试加载JSON格式配置，失败时回退到二进制格式
                LoadGuiConfigs();
                LoadConditionalStylesConfigs();
                LoadCustomColumnsConfigs();
                LoadStatisticsExpressionConfigs();
                LoadFilterConfigs();

                // 级联更新，子从数据字段配置
                if (_subview && _guid != null && subview.Guid == null)
                {
                    subview.Guid = _guid + ".SUBVIEW";
                }
            }
        }

        /// <summary>
        /// 获取或设置 DataGridView 美好组件的当前系统样式。
        /// </summary>
        [DefaultValue(2)]
        [Browsable(true)]
        [Description("获取或设置 DataGridView 美好组件的当前系统样式")]
        public ThemeStyle ThemeStyle
        {
            get { return _themeStyle; }
            set
            {
                if (_themeStyle != value)
                {
                    _themeStyle = value;

                    this.dataGridView.ThemeStyle = value;
                }
            }
        }

        /// <summary>
        /// 设置调用者父类，实现自动刷新的接口
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [Description("设置调用者父类或所有者，实现自动刷新的接口")]
        public IReLoadListUI SetOwner
        {
            get { return _owner; }
            set
            {
                _owner = value;

                // 指定了调用者父类，启用刷新按钮
                if (_owner != null)
                {
                    // 工具栏刷新按钮
                    toolStripButton_ReLoadList.Enabled = true;

                    // 导航栏刷新按钮
                    toolStripButtonReLoadList.Enabled = true;

                    // 菜单栏刷新按钮
                    ToolStripMenuItem_refresh.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 字段配置信息
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [Description("字段配置信息")]
        public List<DgvColumnInfoConfig> ColumnInfos
        {
            get { return _columnInfos; }
            set { _columnInfos = value; }
        }

        /// <summary>
        /// 用户自定义字段属性
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [Description("用户自定义字段属性")]
        public List<DgvCustomColumnsConfig> CustomColumnsConfigs
        {
            get { return _customColumnsConfigs; }
            set { _customColumnsConfigs = value; }
        }

        private List<DgvCustomColumnsConfig> _customColumnsConfigs;

        /// <summary>
        /// 列标题中英文对照字典
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [Description("列标题中英文对照字典")]
        public Dictionary<string, string> EnglishToChineseHeaders
        {
            get { return _englishToChineseHeaders; }
            set
            {
                _englishToChineseHeaders = value;

                // 字段属性配置的优先级高于列标题中英文对照字典（停用）
                // if (_bindingSource != null && _columnInfos == null)

                // 改用，优先应用中英文对照表字典（测试取舍阶段）
                if (_bindingSource != null)
                {
                    // 英文列名和对应的中文列名，字典
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        // 检查英文列名是否在字典中
                        if (EnglishToChineseHeaders.ContainsKey(column.Name))
                        {
                            // 如果在，则替换为对应的中文列名
                            column.HeaderText = EnglishToChineseHeaders[column.Name];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 条件样式配置信息
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [Description("条件样式配置信息")]
        public List<DgvConditionalConfig> DgvConditionalConfigs
        {
            get { return _dgvConditionalConfigs; }
            set { _dgvConditionalConfigs = value; }
        }

        /// <summary>
        /// 条件样式配置信息
        /// </summary>
        [DefaultValue(null)]
        [Browsable(false)]
        [Description("自定义筛选配置信息")]
        public List<DgvCustomFilterConfig> DgvCustomFilterConfig
        {
            get { return _dgvCustomFilterConfig; }
            set { _dgvCustomFilterConfig = value; }
        }

        /// <summary>
        /// 启用，数据行的交替样式
        /// </summary>
        [DefaultValue(false)]
        [Browsable(true)]
        [Description("启用，数据行的交替样式")]
        public bool AlternatingStyle
        {
            get { return _alternatingStyle; }
            set
            {
                _alternatingStyle = value;
            }
        }

        /// <summary>
        /// 启用，自动折叠子从数据
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Description("启用，自动折叠子从数据")]
        public bool AutoCollapse
        {
            get { return _autoCollapse; }
            set
            {
                _autoCollapse = value;
            }
        }

        private bool _autoCollapse = true;

        /// <summary>
        /// 百分比图标（数据源列名），该列的数据应当在0~100自然指数范围内，大于100的数据按100处理
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [Description("百分比图标（数据源列名），该列的数据应当在0~100自然指数范围内，大于100的数据按100处理")]
        public string ProportionColumnName
        {
            get
            {
                return _proportionColumnName;
            }

            set
            {
                _proportionColumnName = value;
            }
        }

        private string _proportionColumnName;

        /// <summary>
        /// 百分比进度条（数据源列名）集合，这些列的数据应当在0~100自然指数范围内，大于100的数据按100处理
        /// </summary>
        [Browsable(true)]
        [Description("百分比进度条（数据源列名）集合，这些列的数据应当在0~100自然指数范围内，大于100的数据按100处理")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<string> ProgressColumnsNames
        {
            get
            {
                if (_progressColumnsNames == null)
                    _progressColumnsNames = new List<string>();
                return _progressColumnsNames;
            }
            set
            {
                _progressColumnsNames = value;
            }
        }

        /// <summary>
        /// 百分比进度条（数据源列名），该列的数据应当在0~100自然指数范围内，大于100的数据按100处理
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [Description("百分比进度条（数据源列名），该列的数据应当在0~100自然指数范围内，大于100的数据按100处理")]
        public string ProgressColumnName
        {
            get
            {
                return ProgressColumnsNames.FirstOrDefault();
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ProgressColumnsNames.Clear();
                    ProgressColumnsNames.Add(value);
                }
                else
                {
                    ProgressColumnsNames.Clear();
                }
            }
        }

        private List<string> _progressColumnsNames;

        /// <summary>
        /// 启用，搜索框内过滤器模式
        /// </summary>
        [DefaultValue(false)]
        [Browsable(true)]
        [Description("启用，搜索框内过滤器模式")]
        public bool FilterMode
        {
            get { return _filterMode; }
            set
            {
                _filterMode = value;

                // 设置 toolStripButton_find 的文本为“筛选”或“查找”
                if (_filterMode)
                {
                    toolStripButton_find.Text = localizer.GetString("Filter");
                    toolStripButton_Next.Visible = false;
                }
                else
                {
                    toolStripButton_find.Text = localizer.GetString("Find");
                    toolStripButton_Next.Visible = true;
                }
            }
        }

        private bool _filterMode = false;

        /// <summary>
        /// 筛选管理器
        /// </summary>
        [DefaultValue(null)]
        [Browsable(true)]
        [Description("筛选管理器")]
        public string Filter
        {
            get
            {
                BindingSource _bs = (BindingSource)dataGridView.DataSource;
                if (_bs != null)
                {
                    return _bs.Filter;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                BindingSource _bs = (BindingSource)dataGridView.DataSource;
                // 数据为空时，返回
                if (_bs == null) { return; }

                // 折叠子视图，判断并关闭已经展开的子视图
                if (!(_subviewCurrentRow.Count == 0) && subview != null && subview.Visible)
                {
                    // 如果有其他行已经展开，先折叠它
                    var eRow = _subviewCurrentRow[0];
                    _subviewCurrentRow.Clear();
                    dataGridView.Rows[eRow].Height = dataGridView.RowTemplate.Height;
                    dataGridView.Rows[eRow].DividerHeight = 0;
                    dataGridView.ClearSelection();
                    dataGridView.Rows[eRow].Selected = true;
                    subview.Visible = false;
                }

                // 管理筛选器逻辑
                if (value == null)// 取消所有筛选
                {
                    if (_bs.Filter != null)
                    {
                        _bs.Filter = null;

                        // 关闭返回上一次筛选功能按钮
                        ToolStripMenuItem_previousFind.Enabled = false;
                        toolStripButton_cancelFind.Enabled = false;
                        toolStripButton_cancelFind2.Enabled = false;
                        ToolStripMenuItem_CancelFind.Enabled = false;
                        ToolStripMenuItem_ColHeader_CancelFind.Enabled = false;
                        ToolStripMenuItem_ColHeader_previousFind.Enabled = false;

                        // 取消所有历史记录
                        _filterHistory.Clear();
                    }
                }
                else// 执行筛选
                {
                    if (_RollbackFilter || _bs.Filter == null)// 第一次筛选（或者来自于历史记录）
                    {
                        try
                        {
                            _bs.Filter = value;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("筛选内容设置有误，" + ex.Message, "信息不完整", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Filter = null;
                            return;
                        }
                    }
                    else// 多次筛选拼接 And / Or 逻辑运算符
                    {
                        try
                        {
                            _bs.Filter += " " + _filterLogicalSymbols + " " + value;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("筛选内容设置有误，" + ex.Message, "信息不完整", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (_filterHistory.Count > 0)
                            {
                                _bs.Filter = _filterHistory[0];
                            }
                            return;
                        }
                    }

                    // 将新的过滤器表达式添加到历史记录列表的开头
                    if (!_RollbackFilter) _filterHistory.Insert(0, _bs.Filter);

                    // 开启返回上一次筛选功能按钮
                    ToolStripMenuItem_previousFind.Enabled = true;
                    toolStripButton_cancelFind.Enabled = true;
                    toolStripButton_cancelFind2.Enabled = true;
                    ToolStripMenuItem_CancelFind.Enabled = true;
                    ToolStripMenuItem_ColHeader_CancelFind.Enabled = true;
                    ToolStripMenuItem_ColHeader_previousFind.Enabled = true;
                }

                // 提示性文本，显示筛选的条件
                toolStripLabel_tip.Text = _bs.Filter;
            }
        }

        /// <summary>
        /// 筛选历史记录
        /// </summary>
        private List<string> FilterHistory
        {
            get { return _filterHistory; }
        }

        #region ToolStripAanBindingNavigator可视化变更

        /// <summary>
        /// 设置（顶部）工具条的显示状态
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Description("设置（顶部）工具条的显示状态")]
        public bool ToolStripVisible
        {
            get { return toolStrip1.Visible; }
            set
            {
                // 更新可见状态
                toolStrip1.Visible = value;

                // 重新计算布局
                RecalculateLayout();
            }
        }

        /// <summary>
        /// 设置（底部）导航条的显示状态
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Description("设置（底部）导航条的显示状态")]
        public bool BindingNavigatorVisible
        {
            get { return bindingNavigator.Visible; }
            set
            {
                // 更新可见状态
                bindingNavigator.Visible = value;

                // 重新计算布局
                RecalculateLayout();
            }
        }

        /// <summary>
        /// 重新计算布局
        /// </summary>
        private void RecalculateLayout()
        {
            int top = -1;
            int bottom = 0;

            // 计算顶部位置
            if (toolStrip1.Visible)
            {
                top = toolStrip1.Bottom - 1;
            }

            // 计算底部空间
            if (bindingNavigator.Visible)
            {
                bottom = bindingNavigator.Height;
            }

            // 设置 DataGridView 的位置和大小
            dataGridView.Location = new Point(0, top);
            dataGridView.Size = new Size(
                dataGridView.Width,
                this.ClientSize.Height - top - bottom
            );
        }

        /// <summary>
        /// 设置（底部）导航条为最小化显示状态
        /// </summary>
        [DefaultValue(false)]
        [Browsable(true)]
        [Description("设置（底部）导航条为最小化显示状态")]
        public bool MinimumOperatingStyle
        {
            get { return !bindingNavigatorMoveFirstItem.Visible; }
            set
            {
                if (!bindingNavigatorMoveFirstItem.Visible != value)
                {
                    // 更新可见状态
                    bindingNavigatorMoveFirstItem.Visible = !value;
                    bindingNavigatorMovePreviousItem.Visible = !value;
                    bindingNavigatorSeparator.Visible = !value;
                    bindingNavigatorPositionItem.Visible = !value;
                    bindingNavigatorCountItem.Visible = !value;
                    bindingNavigatorSeparator1.Visible = !value;
                    bindingNavigatorMoveNextItem.Visible = !value;
                    bindingNavigatorMoveLastItem.Visible = !value;
                    bindingNavigatorSeparator2.Visible = !value;
                    bindingNavigatorAddNewItem.Visible = !value;
                    toolStripSeparator23.Visible = !value;
                    bindingNavigatorDeleteItem.Visible = !value;
                    toolStripSeparator24.Visible = !value;
                    toolStripButtonReLoadList.Visible = !value;
                    toolStripSeparator6.Visible = !value;
                    toolStripTextBox_findColumnName.Visible = !value;
                    toolStripLabel1.Visible = !value;
                    toolStripLabel_Statistics.Visible = !value;
                }
            }
        }

        #endregion ToolStripAanBindingNavigator可视化变更

        #endregion 属性

        #region 系统快捷键和菜单按钮

        /// <summary>
        /// 在单元格任意部分单击时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _rowIndex = e.RowIndex;
            _columnIndex = e.ColumnIndex;

            if (_columnIndex >= 0)
            {
                if (e.ColumnIndex != _lastColumnIndex)
                {
                    // 当选中行变化时实时更新显示
                    UpdateStatisticsAndSelectionInfo();

                    // 记录最后点击的列
                    _lastColumnIndex = e.ColumnIndex;
                }
                // 获取列名
                _columnName = dataGridView.Columns[_columnIndex].Name;
                toolStripTextBox_findColumnName.Text = " " + dataGridView.Columns[_columnIndex].HeaderText;
            }

            // 自动折叠子从数据逻辑
            // 2024/10/05，取消点击当前行的验证  && !_subviewCurrentRow.Contains(rowIndex)
            // 2024/10/05 22:53，恢复上述当前行的验证，不然会和折叠子视图逻辑冲突
            if (_autoCollapse && !(_subviewCurrentRow.Count == 0) && subview != null && subview.Visible && !_subviewCurrentRow.Contains(_rowIndex))
            {
                // 折叠子视图，判断并关闭已经展开的子视图
                // 如果有其他行已经展开，先折叠它
                var eRow = _subviewCurrentRow[0];
                _subviewCurrentRow.Clear();
                dataGridView.Rows[eRow].Height = dataGridView.RowTemplate.Height;
                dataGridView.Rows[eRow].DividerHeight = 0;
                subview.Visible = false;
            }
        }

        /// <summary>
        /// 鼠标进入单元格时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void dataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            // 实时记录鼠标进入的单元格位置
            _rowIndex = e.RowIndex;
            _columnIndex = e.ColumnIndex;
        }

        /// <summary>
        /// 返回 DataView 对应列的数据类型
        /// </summary>
        /// <param name="dataView"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static Type GetDataViewColumnType(DataView dataView, string columnName)
        {
            // 通过 DataView 的 Table 属性获取 DataTable
            DataTable dataTable = dataView.Table;

            // 检查列是否存在，并返回其数据类型
            if (dataTable.Columns.Contains(columnName))
            {
                return dataTable.Columns[columnName].DataType;
            }
            else
            {
                throw new ArgumentException($"Column '{columnName}' does not exist in the DataView.");
            }
        }

        /// <summary>
        /// 处理 dataGridView 鼠标事件，设置上下文菜单，保持选定数据信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (_columnIndex >= 0 && dataGridView.Columns.Count > 0)
            {
                // 获取列名
                _columnName = dataGridView.Columns[_columnIndex].Name;
                toolStripTextBox_findColumnName.Text = " " + dataGridView.Columns[_columnIndex].HeaderText;
            }

            // 单击在数据表格内，判断弹出上下文菜单的生效位置
            if (_rowIndex >= 0 && _columnIndex >= 0)
            {
                // 获取鼠标指针位置单元格的值
                object cellValue;
                // 数据列格式
                Type cellType = null;
                try
                {
                    // 当执行筛选后鼠标不在单元格内触发点击，可能出现 rowIndex 和 columnIndex 位置不是正确，引发的读取 Value 异常
                    cellValue = dataGridView.Rows[_rowIndex].Cells[_columnIndex].Value;

                    // 当前列的数据存储类型
                    // Bug Andy 2024/12/05 仍未解决，当选中字段为空时，判断字段类型是错乱，无法正常切换筛选器
                    // cellType = GetDataViewColumnType(_dataView, columnName);
                    cellType = dataGridView.Rows[_rowIndex].Cells[_columnIndex].ValueType;
                }
                catch (Exception)
                {
                    cellValue = null;

                    cellType = null;
                }

                // 菜单文本
                string menuVisibleValue = "";

                if (cellValue == null || string.IsNullOrEmpty(cellValue.ToString()))
                {
                    // 单元格是空的
                    ToolStripMenuItem_findText.Visible = true;
                    ToolStripMenuItem_findNubmer.Visible = false;
                    ToolStripMenuItem_findDate.Visible = false;
                    ToolStripMenuItem_Like.Visible = false;
                    ToolStripMenuItem_NotLike.Visible = false;

                    // 菜单显示文本
                    menuVisibleValue = "";
                    _columnValueType = "string";
                }
                else if (cellType == typeof(DateTime))
                {
                    // 单元格包含日期
                    ToolStripMenuItem_findText.Visible = false;
                    ToolStripMenuItem_findNubmer.Visible = false;
                    ToolStripMenuItem_findDate.Visible = true;
                    ToolStripMenuItem_Like.Visible = false;
                    ToolStripMenuItem_NotLike.Visible = false;

                    // 菜单显示文本
                    DateTime dateValue = (DateTime)cellValue;
                    menuVisibleValue = dateValue.ToString("yyyy-MM-dd");
                    _columnValueType = "dateTime";
                }
                else if (cellType == typeof(int) || cellType == typeof(long) || cellType == typeof(float) || cellType == typeof(double) || cellType == typeof(decimal))
                {
                    // 单元格包含数字
                    ToolStripMenuItem_findText.Visible = false;
                    ToolStripMenuItem_findNubmer.Visible = true;
                    ToolStripMenuItem_findDate.Visible = false;
                    ToolStripMenuItem_Like.Visible = false;
                    ToolStripMenuItem_NotLike.Visible = false;

                    // 菜单显示文本
                    IConvertible convertible = (IConvertible)cellValue;
                    menuVisibleValue = convertible.ToDouble(null).ToString();
                    _columnValueType = "int";
                }
                else if (cellType == typeof(string))
                {
                    // 单元格包含字符串
                    ToolStripMenuItem_findText.Visible = true;
                    ToolStripMenuItem_findNubmer.Visible = false;
                    ToolStripMenuItem_findDate.Visible = false;
                    ToolStripMenuItem_Like.Visible = true;
                    ToolStripMenuItem_NotLike.Visible = true;

                    // 菜单显示文本
                    menuVisibleValue = (string)cellValue;
                    _columnValueType = "string";
                }

                // 保持鼠标点击的列值
                _columnValue = menuVisibleValue;

                // 处理鼠标右击的上下文菜单事件
                if (e.Button == MouseButtons.Right)
                {
                    // 截断过长的文本
                    menuVisibleValue = ShortenStringByWidth(menuVisibleValue);

                    // 菜单文本
                    ToolStripMenuItem_equal.Text = $"{localizer.GetString("Equal")} {menuVisibleValue}";
                    ToolStripMenuItem_NotEqual.Text = $"{localizer.GetString("NotEqual")} {menuVisibleValue}";
                    ToolStripMenuItem_Like.Text = $"{localizer.GetString("Contains")} {menuVisibleValue}";
                    ToolStripMenuItem_NotLike.Text = $"{localizer.GetString("DoesNotContain")} {menuVisibleValue}";

                    // 显示上下文菜单
                    DgvContextMenuStrip.Show(dataGridView, e.Location);
                }
            }

            // 右击在表头
            if (e.Button == MouseButtons.Right && _rowIndex == -1 && _columnIndex >= 0)
            {
                // 控制并显示表头的上下文菜单，升序、降序、筛选、查找、复制、导出...
                dataGridView.ContextMenuStrip = null;
                ColHeaderContextMenuStrip.Show(dataGridView, e.Location);
            }

            // 右击在行头
            if (e.Button == MouseButtons.Right && _rowIndex >= 0 && _columnIndex == -1)
            {
                // 控制并显示行头的上下文菜单，添加、编辑、删除、复制、导出...
                dataGridView.ContextMenuStrip = null;
                RowHeaderContextMenuStrip.Show(dataGridView, e.Location);
            }

            // 右击在首行，首列，控制显示列
            if (e.Button == MouseButtons.Right && _rowIndex == -1 && _columnIndex == -1)
            {
                // 强制取消选择，防止数据量大时影响性能。
                // dataGridView.ClearSelection();
                dataGridView.ContextMenuStrip = null;
                // 在右击在空白处，设置上下文菜单
                ColHeaderContextMenuStrip.Show(dataGridView, e.Location);
            }
        }

        /// <summary>
        /// 截断过长的文本信息
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxWidth"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private static string ShortenStringByWidth(string str, int maxWidth = 140)
        {
            if (string.IsNullOrEmpty(str)) return str;

            // 显示字体
            Font font = new Font("Arial", 12);

            // 创建一个Bitmap对象，用于获取Graphics对象
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    SizeF textSize = graphics.MeasureString(str, font);

                    if (textSize.Width <= maxWidth)
                    {
                        return str; // 字符串宽度未超出，无需截断
                    }

                    // 逐步减少字符串长度，直到其宽度满足要求
                    int ellipsisWidth = (int)graphics.MeasureString("...", font).Width;
                    int availableWidth = maxWidth - ellipsisWidth;
                    int charCount = str.Length;

                    while (charCount > 0)
                    {
                        string testString = str.Substring(0, charCount) + "...";
                        SizeF testSize = graphics.MeasureString(testString, font);

                        if (testSize.Width <= maxWidth)
                        {
                            return testString;
                        }

                        charCount--;
                    }

                    return "..."; // 字符串过短，仅显示省略号
                }
            }
        }

        /// <summary>
        /// 响应预设自定义键盘快捷键（重写 ProcessCmdKey）
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Ctrl+C 快捷键
            if (keyData == (Keys.Control | Keys.C))
            {
                if (dataGridView.Focused && ToolStripMenuItem_copy.Enabled) // 确保 DGV 有焦点
                {
                    // 执行复制单元格内容操作
                    ToolStripMenuItem_copy_Click(dataGridView, null);
                    return true; // 已处理按键消息
                }
            }
            // Alt+1 快捷键
            else if (keyData == (Keys.Alt | Keys.D1))
            {
                if (dataGridView.Focused && ToolStripMenuItem_equal.Enabled) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_equal_Click(dataGridView, null);
                    return true;
                }
            }
            // Alt+2 快捷键
            else if (keyData == (Keys.Alt | Keys.D2))
            {
                if (dataGridView.Focused && ToolStripMenuItem_NotEqual.Enabled) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_NotEqual_Click(dataGridView, null);
                    return true;
                }
            }
            // Alt+3 快捷键
            else if (keyData == (Keys.Alt | Keys.D3))
            {
                if (dataGridView.Focused && ToolStripMenuItem_Like.Enabled) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_Like_Click(dataGridView, null);
                    return true;
                }
            }
            // Alt+4 快捷键
            else if (keyData == (Keys.Alt | Keys.D4))
            {
                if (dataGridView.Focused && ToolStripMenuItem_NotLike.Enabled) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_NotLike_Click(dataGridView, null);
                    return true;
                }
            }
            // Ctrl+F 快捷键
            else if (keyData == (Keys.Control | Keys.F))
            {
                if (dataGridView.Focused && ToolStripMenuItem_query.Enabled) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_query_Click(dataGridView, null);
                    return true;
                }
            }
            // Ctrl+Shift+F 快捷键
            else if (keyData == (Keys.Control | Keys.Shift | Keys.F))
            {
                if (dataGridView.Focused && ToolStripMenuItem_find.Enabled) // 确保 DGV 有焦点
                {
                    // 文本/数字/日期
                    ToolStripMenuItem_find_Click(dataGridView, null);
                    return true;
                }
            }
            // F5 快捷键
            else if (keyData == (Keys.F5))
            {
                if (dataGridView.Focused) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_refresh_Click(dataGridView, null);
                    return true;
                }
            }
            // Ctrl+Shift+Z 快捷键
            else if (keyData == (Keys.Control | Keys.Shift | Keys.Z))
            {
                if (dataGridView.Focused && ToolStripMenuItem_CancelFind.Enabled) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_CancelFind_Click(dataGridView, null);
                    return true;
                }
            }
            // Ctrl+Z 快捷键
            else if (keyData == (Keys.Control | Keys.Z))
            {
                if (dataGridView.Focused) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_previousFind_Click(dataGridView, null);
                    return true;
                }
            }
            // Ctrl+P 快捷键
            else if (keyData == (Keys.Control | Keys.P))
            {
                if (dataGridView.Focused) // 确保 DGV 有焦点
                {
                    // ToolStripMenuItem_print_Click(dataGridView, null);
                    toolStripMenuItem_printSelectPrinter_Click(dataGridView, null);
                    return true;
                }
            }
            // Ctrl+E 快捷键
            else if (keyData == (Keys.Control | Keys.E))
            {
                if (dataGridView.Focused) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_exp_Click(dataGridView, null);
                    return true;
                }
            }
            // F2 快捷键
            else if (keyData == (Keys.F2))
            {
                if (dataGridView.Focused) // 确保 DGV 有焦点
                {
                    ToolStripMenuItem_edit_Click(dataGridView, null);
                    return true;
                }
            }
            // ESC 关闭折叠子视图
            else if (keyData == Keys.Escape)
            {
                // 如果有其他行已经展开，先折叠它
                if (_subviewCurrentRow.Count > 0 && subview != null && subview.Visible)
                {
                    var eRow = _subviewCurrentRow[0];
                    _subviewCurrentRow.Clear();
                    dataGridView.Rows[eRow].Height = dataGridView.RowTemplate.Height;
                    dataGridView.Rows[eRow].DividerHeight = 0;
                    subview.Visible = false;
                }
                return true; // 已处理按键消息
            }

            // 其他快捷键的处理逻辑

            // 未处理的按键消息传递给基类处理
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region DgvContextMenuStrip菜单事件

        private void ToolStripMenuItem_copy_Click(object sender, EventArgs e)
        {
            // 复制
            // 将文本放入剪贴板
            // Clipboard.SetText(_columnValue);

            // 2024/12/17 日，修改为复制选择行的当前列数据集合文本
            ToolStripMenuItem_ColHeader_copy_Click(sender, e);
        }

        private void ToolStripMenuItem_asc_Click(object sender, EventArgs e)
        {
            // 升序
            if (_columnName != null)
                dataGridView.Sort(dataGridView.Columns[_columnName], ListSortDirection.Ascending);
        }

        private void ToolStripMenuItem_desc_Click(object sender, EventArgs e)
        {
            // 降序
            if (_columnName != null)
                dataGridView.Sort(dataGridView.Columns[_columnName], ListSortDirection.Descending);
        }

        #region 文本筛选器

        private void ToolStripMenuItemText_eq_Click(object sender, EventArgs e)
        {
            // 文本筛选器，等于
            _findType = DgvFindType.Equals;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemText_NotEq_Click(object sender, EventArgs e)
        {
            // 文本筛选器，不等于
            _findType = DgvFindType.NotEquals;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemText_LikeBf_Click(object sender, EventArgs e)
        {
            // 文本筛选器，开头是
            _findType = DgvFindType.BeginsWith;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemText_NotLikeBf_Click(object sender, EventArgs e)
        {
            // 文本筛选器，开头不是
            _findType = DgvFindType.NotBeginsWith;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemText_Like_Click(object sender, EventArgs e)
        {
            // 文本筛选器，包含
            _findType = DgvFindType.Contains;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemText_NotLike_Click(object sender, EventArgs e)
        {
            // 文本筛选器，不包含
            _findType = DgvFindType.NotContains;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemText_EndLike_Click(object sender, EventArgs e)
        {
            // 文本筛选器，结尾是
            _findType = DgvFindType.EndsWith;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemText_EndNotLike_Click(object sender, EventArgs e)
        {
            // 文本筛选器，结尾不是
            _findType = DgvFindType.NotEndsWith;
            OpenFindWindows(1);
        }

        #endregion 文本筛选器

        #region 数字筛选器

        private void ToolStripMenuItemNumber_eq_Click(object sender, EventArgs e)
        {
            // 数字筛选器，等于
            _findType = DgvFindType.Equals;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemNumber_NotEq_Click(object sender, EventArgs e)
        {
            // 数字筛选器，不等于
            _findType = DgvFindType.NotEquals;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemNumber_less_Click(object sender, EventArgs e)
        {
            // 数字筛选器，小于
            _findType = DgvFindType.LessThan;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemNumber_greater_Click(object sender, EventArgs e)
        {
            // 数字筛选器，大于
            _findType = DgvFindType.GreaterThan;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemNumber_interval_Click(object sender, EventArgs e)
        {
            // 数字筛选器，期间
            _findType = DgvFindType.Period;
            OpenFindWindows(1);
        }

        #endregion 数字筛选器

        #region 日期筛选器

        private void ToolStripMenuItemDate_eq_Click(object sender, EventArgs e)
        {
            // 日期筛选器，等于
            _findType = DgvFindType.Equals;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemDate_NotEq_Click(object sender, EventArgs e)
        {
            // 日期筛选器，不等于
            _findType = DgvFindType.NotEquals;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemDate_less_Click(object sender, EventArgs e)
        {
            // 日期筛选器，之前
            _findType = DgvFindType.LessThan;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemDate_greater_Click(object sender, EventArgs e)
        {
            // 日期筛选器，之后
            _findType = DgvFindType.GreaterThan;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemDate_interval_Click(object sender, EventArgs e)
        {
            // 日期筛选器，期间
            _findType = DgvFindType.Period;
            OpenFindWindows(1);
        }

        private void ToolStripMenuItemDate_tomorrow_Click(object sender, EventArgs e)
        {
            // 日期筛选器，明天
            // 获取今天的日期
            DateTime today = DateTime.Today;

            // 计算下周的起始和结束日期（假设周一是周的开始）
            DateTime startOfWeek = today.AddDays(+1);

            // 加上6天得到下周的结束日期（周日）
            DateTime endOfWeek = startOfWeek.AddDays(1);

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0:yyyy-MM-dd}' AND [" + _columnName + "] <= '{1:yyyy-MM-dd}'", startOfWeek, endOfWeek);
        }

        private void ToolStripMenuItemDate_today_Click(object sender, EventArgs e)
        {
            // 日期筛选器，今天
            // 获取今天的日期
            DateTime today = DateTime.Today;

            // 计算下周的起始和结束日期（假设周一是周的开始）
            DateTime startOfWeek = today;

            // 加上6天得到下周的结束日期（周日）
            DateTime endOfWeek = startOfWeek.AddDays(1);

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0:yyyy-MM-dd}' AND [" + _columnName + "] <= '{1:yyyy-MM-dd}'", startOfWeek, endOfWeek);
        }

        private void ToolStripMenuItemDate_yesterday_Click(object sender, EventArgs e)
        {
            // 日期筛选器，昨天
            // 获取今天的日期
            DateTime today = DateTime.Today;

            // 计算下周的起始和结束日期（假设周一是周的开始）
            DateTime startOfWeek = today.AddDays(-1);

            // 加上6天得到下周的结束日期（周日）
            DateTime endOfWeek = startOfWeek.AddDays(1);

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0:yyyy-MM-dd}' AND [" + _columnName + "] <= '{1:yyyy-MM-dd}'", startOfWeek, endOfWeek);
        }

        private void ToolStripMenuItemDate_nextWeek_Click(object sender, EventArgs e)
        {
            // 日期筛选器，下周
            // 获取下周的日期
            DateTime today = DateTime.Today.AddDays(+7);

            // 计算下周的起始和结束日期（假设周一是周的开始）
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

            // 加上6天得到下周的结束日期（周日）
            DateTime endOfWeek = startOfWeek.AddDays(6);

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0:yyyy-MM-dd}' AND [" + _columnName + "] <= '{1:yyyy-MM-dd}'", startOfWeek, endOfWeek);
        }

        private void ToolStripMenuItemDate_thisWeek_Click(object sender, EventArgs e)
        {
            // 日期筛选器，本周
            // 获取今天的日期
            DateTime today = DateTime.Today;

            // 计算下周的起始和结束日期（假设周一是周的开始）
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

            // 加上6天得到下周的结束日期（周日）
            DateTime endOfWeek = startOfWeek.AddDays(6);

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0:yyyy-MM-dd}' AND [" + _columnName + "] <= '{1:yyyy-MM-dd}'", startOfWeek, endOfWeek);
        }

        private void ToolStripMenuItemDate_lastWeek_Click(object sender, EventArgs e)
        {
            // 日期筛选器，上周
            // 获取上周的日期
            DateTime today = DateTime.Today.AddDays(-7);

            // 计算上周的起始和结束日期（假设周一是周的开始）
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

            // 加上6天得到上周的结束日期（周日）
            DateTime endOfWeek = startOfWeek.AddDays(6);

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0:yyyy-MM-dd}' AND [" + _columnName + "] <= '{1:yyyy-MM-dd}'", startOfWeek, endOfWeek);
        }

        private void ToolStripMenuItemDate_nextMonth_Click(object sender, EventArgs e)
        {
            // 日期筛选器，下月
            // 设置为下月的第一天
            DateTime firstDayOfLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1);

            // 使用AddMonths方法将日期移动到下一个月的第一天
            // 然后使用AddDays方法减去一天，得到当前月的最后一天
            DateTime lastDayOfMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}' AND [" + _columnName + "] <= '{1}'", firstDayOfLastMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"));
        }

        private void ToolStripMenuItemDate_thisMonth_Click(object sender, EventArgs e)
        {
            // 日期筛选器，本月
            // 设置为该月的第一天
            DateTime firstDayOfThisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // 使用AddMonths方法将日期移动到下一个月的第一天
            // 然后使用AddDays方法减去一天，得到当前月的最后一天
            DateTime lastDayOfMonth = firstDayOfThisMonth.AddMonths(1).AddDays(-1);

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}' AND [" + _columnName + "] <= '{1}'", firstDayOfThisMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"));
        }

        private void ToolStripMenuItemDate_lastMonth_Click(object sender, EventArgs e)
        {
            // 日期筛选器，上月
            // 设置为上月的第一天
            DateTime firstDayOfLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);

            // 使用AddMonths方法将日期移动到下一个月的第一天
            // 然后使用AddDays方法减去一天，得到当前月的最后一天
            DateTime lastDayOfMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}' AND [" + _columnName + "] <= '{1}'", firstDayOfLastMonth.ToString("yyyy-MM-dd"), lastDayOfMonth.ToString("yyyy-MM-dd"));
        }

        private void ToolStripMenuItemDate_nextQuarter_Click(object sender, EventArgs e)
        {
            // 日期筛选器，下季度
            int thisYear = DateTime.Now.Year;

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}-08-01' AND [" + _columnName + "] <= '{0}-12-31'", thisYear);
        }

        private void ToolStripMenuItemDate_thisQuarter_Click(object sender, EventArgs e)
        {
            // 日期筛选器，本季度
            int thisYear = DateTime.Now.Year;

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}-04-01' AND [" + _columnName + "] <= '{0}-07-31'", thisYear);
        }

        private void ToolStripMenuItemDate_lastQuarter_Click(object sender, EventArgs e)
        {
            // 日期筛选器，上季度
            int thisYear = DateTime.Now.Year;

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}-01-01' AND [" + _columnName + "] <= '{0}-03-31'", thisYear);
        }

        private void ToolStripMenuItemDate_nextYear_Click(object sender, EventArgs e)
        {
            // 日期筛选器，明年
            int nextYear = DateTime.Now.Year + 1;

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}-01-01' AND [" + _columnName + "] <= '{0}-12-31'", nextYear);
        }

        private void ToolStripMenuItemDate_thisYear_Click(object sender, EventArgs e)
        {
            // 日期筛选器，今年
            int thisYear = DateTime.Now.Year;

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}-01-01' AND [" + _columnName + "] <= '{0}-12-31'", thisYear);
        }

        private void ToolStripMenuItemDate_soFarThisYear_Click(object sender, EventArgs e)
        {
            // 日期筛选器，今年截止到现在
            int thisYear = DateTime.Now.Year;
            DateTime currentDate = DateTime.Now;

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}-01-01' AND [" + _columnName + "] <= '{1}'", thisYear, currentDate.ToString("yyyy-MM-dd"));
        }

        private void ToolStripMenuItemDate_lastYear_Click(object sender, EventArgs e)
        {
            // 日期筛选器，去年
            int lastYear = DateTime.Now.Year - 1;

            // 构造并应用筛选表达式
            Filter = string.Format("[" + _columnName + "] >= '{0}-01-01' AND [" + _columnName + "] <= '{0}-12-31'", lastYear);
        }

        #endregion 日期筛选器

        #region 快速筛选器

        private void ToolStripMenuItem_equal_Click(object sender, EventArgs e)
        {
            // 快速筛选，等于
            Filter = "[" + _columnName + "] = '" + _columnValue + "'";
        }

        private void ToolStripMenuItem_NotEqual_Click(object sender, EventArgs e)
        {
            // 快速筛选，不等于
            Filter = "[" + _columnName + "] <> '" + _columnValue + "'";
        }

        private void ToolStripMenuItem_Like_Click(object sender, EventArgs e)
        {
            // 快速筛选，包含
            Filter = "[" + _columnName + "] like '%" + FormatFilterParameter(_columnValue) + "%'";
        }

        private void ToolStripMenuItem_NotLike_Click(object sender, EventArgs e)
        {
            // 快速筛选，不包含
            Filter = "[" + _columnName + "] not like '%" + FormatFilterParameter(_columnValue) + "%'";
        }

        #endregion 快速筛选器

        private void ToolStripMenuItem_query_Click(object sender, EventArgs e)
        {
            // 快速筛选，查找
            _findType = DgvFindType.Contains;
            OpenFindWindows(0);
        }

        private void ToolStripMenuItem_find_Click(object sender, EventArgs e)
        {
            // 快速筛选，筛选
            _findType = DgvFindType.Contains;
            OpenFindWindows(1);
        }

        /// <summary>
        /// 打开查找与筛选的窗口
        /// </summary>
        /// <param name="TabControllSelectIndex"></param>
        private void OpenFindWindows(int TabControllSelectIndex = 0)
        {
            FrmFindWindows findWindows = new FrmFindWindows();
            findWindows.TabControl1.SelectedIndex = TabControllSelectIndex;
            findWindows.txtcznr.Text = _columnValue;
            findWindows.txtsxnr.Text = _columnValue;
            findWindows.findType = _findType;
            findWindows.QueryClick += FindWindows_QueryClick;
            findWindows.FindClick += FindWindows_FindClick;
            findWindows.SaveFiltersConfigClick += FindWindows_SaveFiltersConfigClick;
            findWindows.AcceptButton = findWindows.ButtonFind2;
            findWindows.CancelButton = findWindows.ButtonClose2;
            findWindows.dgvCustomFilterConfigs = DgvCustomFilterConfig;
            findWindows.FilterText = Filter;
            findWindows.ShowDialog();
        }

        /// <summary>
        /// 查找 FindWindows_QueryClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="FindType"></param>
        /// <param name="Txt"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void FindWindows_QueryClick(object sender, EventArgs e, DgvFindType findType, string txt, string txt2, bool isOr)
        {
            // 更新txt文本到底部搜索框
            toolStripTextBox_FindText.Text = txt;

            // 查找定位到指定行
            FindAllMatchingRows(_columnName, txt);
        }

        /// <summary>
        /// 筛选 FindWindows_FindClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="FindType"></param>
        /// <param name="Txt"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void FindWindows_FindClick(object sender, EventArgs e, DgvFindType findType, string txt, string txt2, bool isOr)
        {
            // 修改，多条件筛选逻辑运算符
            if (isOr) { _filterLogicalSymbols = "Or"; }

            // 更新txt文本到底部搜索框
            toolStripTextBox_FindText.Text = txt;

            switch (findType)
            {
                case DgvFindType.Equals:
                    if (_columnValueType == "string")
                        Filter = string.Format("[" + _columnName + "] = '{0}'", txt);
                    else if (_columnValueType == "int")
                        Filter = string.Format("[" + _columnName + "] = {0}", txt);
                    else if (_columnValueType == "dateTime")
                        Filter = string.Format("[" + _columnName + "] >= '{0:yyyy-MM-dd}' AND [" + _columnName + "] <= '{1:yyyy-MM-dd}'", txt, txt);
                    break;

                case DgvFindType.NotEquals:
                    if (_columnValueType == "string")
                        Filter = string.Format("[" + _columnName + "] <> '{0}'", txt);
                    else if (_columnValueType == "int")
                        Filter = string.Format("[" + _columnName + "] <> {0}", txt);
                    else if (_columnValueType == "dateTime")
                        Filter = string.Format("[" + _columnName + "] <> '{0:yyyy-MM-dd}'", txt);
                    break;

                case DgvFindType.Contains:
                    if (_columnValueType == "string")
                        Filter = string.Format("[" + _columnName + "] like '%{0}%'", FormatFilterParameter(txt));
                    break;

                case DgvFindType.NotContains:
                    if (_columnValueType == "string")
                        Filter = string.Format("[" + _columnName + "] Not like '%{0}%'", FormatFilterParameter(txt));
                    break;

                case DgvFindType.BeginsWith:
                    if (_columnValueType == "string")
                        Filter = string.Format("[" + _columnName + "] like '{0}%'", FormatFilterParameter(txt));
                    break;

                case DgvFindType.NotBeginsWith:
                    if (_columnValueType == "string")
                        Filter = string.Format("[" + _columnName + "] Not like '{0}%'", FormatFilterParameter(txt));
                    break;

                case DgvFindType.EndsWith:
                    if (_columnValueType == "string")
                        Filter = string.Format("[" + _columnName + "] like '%{0}'", FormatFilterParameter(txt));
                    break;

                case DgvFindType.NotEndsWith:
                    if (_columnValueType == "string")
                        Filter = string.Format("[" + _columnName + "] Not like '%{0}'", FormatFilterParameter(txt));
                    break;

                case DgvFindType.LessThan:
                    if (_columnValueType == "int")
                        Filter = string.Format("[" + _columnName + "] < {0}", txt);
                    else if (_columnValueType == "dateTime")
                        Filter = string.Format("[" + _columnName + "] < '{0:yyyy-MM-dd}'", txt);
                    break;

                case DgvFindType.GreaterThan:
                    if (_columnValueType == "int")
                        Filter = string.Format("[" + _columnName + "] > {0}", txt);
                    else if (_columnValueType == "dateTime")
                        Filter = string.Format("[" + _columnName + "] > '{0:yyyy-MM-dd}'", txt);
                    break;

                case DgvFindType.Period:
                    // 缺少另外一个范围的输入值
                    // 2024/11/8 修复此问题
                    if (_columnValueType == "int")
                        Filter = string.Format("[" + _columnName + "] >= {0} AND [" + _columnName + "] <= {1}", txt, txt2);
                    else if (_columnValueType == "dateTime")
                        Filter = string.Format("[" + _columnName + "] >= '{0:yyyy-MM-dd}' AND [" + _columnName + "] <= '{1:yyyy-MM-dd}'", txt, txt2);
                    break;
            }

            // 恢复默认的，多条件筛选逻辑运算符
            if (isOr) { _filterLogicalSymbols = "And"; }
        }

        private void ToolStripMenuItem_refresh_Click(object sender, EventArgs e)
        {
            // 刷新
            DgvRefreshEventArgs args = new DgvRefreshEventArgs();
            OnRefreshDgv(sender, args);
        }

        /// <summary>
        /// 保存自定义筛选配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="dgvCustomFilterConfigs"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void FindWindows_SaveFiltersConfigClick(object sender, EventArgs e, List<DgvCustomFilterConfig> dgvCustomFilterConfigs)
        {
            // 持久化自定义筛选配置
            // 处理是否需要保持配置对象至本地磁盘
            if (Guid != null && dgvCustomFilterConfigs != null)
            {
                SaveFilterConfigs(dgvCustomFilterConfigs);

                // 保持对象
                DgvCustomFilterConfig = dgvCustomFilterConfigs;
            }
        }

        private void toolStripButtonReLoadList_Click(object sender, EventArgs e)
        {
            // 刷新
            DgvRefreshEventArgs args = new DgvRefreshEventArgs();
            OnRefreshDgv(sender, args);
        }

        private void toolStripButton_ReLoadList_Click(object sender, EventArgs e)
        {
            // 刷新
            DgvRefreshEventArgs args = new DgvRefreshEventArgs();
            OnRefreshDgv(sender, args);
        }

        private void ToolStripMenuItem_CancelFind_Click(object sender, EventArgs e)
        {
            // 取消所有筛选
            Filter = null;
        }

        private void ToolStripMenuItem_previousFind_Click(object sender, EventArgs e)
        {
            // 返回上次筛选
            RollbackFilter();
        }

        private void ToolStripMenuItem_printPreview_Click(object sender, EventArgs e)
        {
            // 打印预览
            DgvPrintEventArgs args = new DgvPrintEventArgs(GetSelectedItemIds());

            // 读取默认标题（如若已经设置拥有者）
            if (_owner != null)
            {
                args.Title = GetFormText(_owner);
            }

            // 触发事件，允许外部逻辑处理事件
            PrintDgv?.Invoke(sender, args, GetSelectedItemIds());

            // 检查事件是否被处理
            if (!args.Handled)
            {
                // 获取选择打印的数据
                _printItems = GetPrintItems();
                _printDocument.DocumentName = args.Title;
                FrmPrintPreview frmPrintPreview = new FrmPrintPreview();
                frmPrintPreview.printDocument = _printDocument;
                frmPrintPreview.ShowDialog();
            }
        }

        private void toolStripMenuItem_printPageSetup_Click(object sender, EventArgs e)
        {
            // 页面设置
            DialogResult dialogResult = _pageSetupDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                // 获取并显示用户设置的页边距
                Margins margins = _pageSetupDialog.PageSettings.Margins;
                _printDocument.DefaultPageSettings.Margins.Top = ConvertHundredthsOfAnInchToMM(margins.Top);
                _printDocument.DefaultPageSettings.Margins.Bottom = ConvertHundredthsOfAnInchToMM(margins.Bottom);
                _printDocument.DefaultPageSettings.Margins.Left = ConvertHundredthsOfAnInchToMM(margins.Left);
                _printDocument.DefaultPageSettings.Margins.Right = ConvertHundredthsOfAnInchToMM(margins.Right);
            }
        }

        private void toolStripMenuItem_printSelectPrinter_Click(object sender, EventArgs e)
        {
            // 获取选择打印的数据
            _printItems = GetPrintItems();

            // 打印
            DgvPrintEventArgs args = new DgvPrintEventArgs(GetSelectedItemIds());

            // 读取默认标题（如若已经设置拥有者）
            if (_owner != null)
            {
                args.Title = GetFormText(_owner);
            }

            // 触发事件，允许外部逻辑处理事件
            OnPrintDgv(sender, args, GetSelectedItemIds());
        }

        private void ToolStripMenuItem_print_Click(object sender, EventArgs e)
        {
            // 打印
            // DgvPrintEventArgs args = new DgvPrintEventArgs(GetSelectedItemIds());
            // OnPrintDgv(sender, args, GetSelectedItemIds());
        }

        private void ToolStripMenuItem_exp_Click(object sender, EventArgs e)
        {
            // 导出
            DgvExportEventArgs args = new DgvExportEventArgs(GetSelectedItemIds(), null);
            OnExportDgv(sender, args, GetSelectedItemIds());
        }

        /// <summary>
        /// 设置系统语言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripComboBox_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 防止递归调用
            this.ToolStripComboBox_Language.SelectedIndexChanged -= ToolStripComboBox_Language_SelectedIndexChanged;

            int SelectedIndex = ToolStripComboBox_Language.SelectedIndex;

            if (SelectedIndex != -1)
            {
                switch (SelectedIndex)
                {
                    case 0:
                        // 英语
                        LanguageService.SwitchToEnglish();
                        _language = SupportedLanguage.English;
                        break;

                    case 1:
                        // 简体中文
                        LanguageService.SwitchToSimplifiedChinese();
                        _language = SupportedLanguage.SimplifiedChinese;
                        break;

                    case 2:
                        // 繁体中文
                        LanguageService.SwitchToTraditionalChinese();
                        _language = SupportedLanguage.TraditionalChinese;
                        break;

                    case 3:
                        // 德语
                        LanguageService.SwitchToGerman();
                        _language = SupportedLanguage.German;
                        break;

                    case 4:
                        // 法语
                        LanguageService.SwitchToFrench();
                        _language = SupportedLanguage.French;
                        break;

                    case 5:
                        // 日語
                        LanguageService.SwitchToJapanese();
                        _language = SupportedLanguage.Japanese;
                        break;

                    default:
                        // 默认英语
                        LanguageService.SwitchToEnglish();
                        _language = SupportedLanguage.English;
                        break;
                }
            }

            // 重新设置选中项，防止丢失
            this.ToolStripComboBox_Language.SelectedIndex = (int)_language;

            // 恢复事件绑定
            this.ToolStripComboBox_Language.SelectedIndexChanged += ToolStripComboBox_Language_SelectedIndexChanged;
        }

        /// <summary>
        /// 设置系统主题样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripComboBox_Theme_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SelectedIndex = ToolStripComboBox_Theme.SelectedIndex;

            if (SelectedIndex != -1)
            {
                switch (SelectedIndex)
                {
                    case 0:
                        ThemeStyle = Erp.Toolkit.Controls.ThemeStyle.DarkTheme;
                        break;

                    case 1:
                        ThemeStyle = Erp.Toolkit.Controls.ThemeStyle.LightTheme;
                        break;

                    case 2:
                        ThemeStyle = Erp.Toolkit.Controls.ThemeStyle.BlueTheme;
                        break;

                    case 3:
                        ThemeStyle = Erp.Toolkit.Controls.ThemeStyle.GreenTheme;
                        break;

                    case 4:
                        ThemeStyle = Erp.Toolkit.Controls.ThemeStyle.OrangeTheme;
                        break;

                    case 5:
                        ThemeStyle = Erp.Toolkit.Controls.ThemeStyle.PurpleTheme;
                        break;

                    default:
                        ThemeStyle = Erp.Toolkit.Controls.ThemeStyle.BlueTheme;
                        break;
                }

                // 同步子视图的系统样式
                if (subview != null) subview.ThemeStyle = ThemeStyle;
            }

            this.ToolStripComboBox_Theme.SetLocalizationKey(_themeStyle.ToString());
        }

        /// <summary>
        /// 设置用户自定义菜单或工具条按钮
        /// </summary>
        /// <param name="configs">菜单配置列表</param>
        public void SetUserContextMenu(List<DgvUserContextMenuStripConfig> configs)
        {
            // 使用字典跟踪每个目标位置的分组状态
            var groupStates = new Dictionary<MenuShowTarget, int>
            {
                { MenuShowTarget.ToolStrip, 0 },
                { MenuShowTarget.ContextMenuStrip, 0 },
                { MenuShowTarget.BottomNavigator, 0 }
            };

            // 获取所有有效的单个目标标志
            var allTargets = Enum.GetValues(typeof(MenuShowTarget))
                .Cast<MenuShowTarget>()
                .Where(t => t != MenuShowTarget.None && t != MenuShowTarget.All)
                .ToArray();

            foreach (var config in configs)
            {
                // 确定实际需要处理的目标位置
                var effectiveTargets = new List<MenuShowTarget>();

                if (config.Target == MenuShowTarget.All)
                {
                    effectiveTargets.AddRange(allTargets);
                }
                else
                {
                    foreach (var target in allTargets)
                    {
                        if (config.Target.HasFlag(target))
                            effectiveTargets.Add(target);
                    }
                }

                // 为每个目标位置处理分组和添加菜单项
                foreach (var target in effectiveTargets)
                {
                    // 处理分组分隔线
                    if (groupStates[target] != config.Group)
                    {
                        AddSeparatorToTarget(target);
                        groupStates[target] = config.Group;
                    }

                    // 创建并添加菜单项
                    var menuItem = CreateMenuItem(config);
                    AddMenuItemToTarget(menuItem, target);
                }
            }
        }

        /// <summary>
        /// 创建菜单项
        /// </summary>
        private ToolStripButton CreateMenuItem(DgvUserContextMenuStripConfig config)
        {
            var menuItem = new ToolStripButton
            {
                Text = config.MenuText,
                ToolTipText = config.MenuText,
                Image = config.MenuIcon,
                Checked = config.Checked
            };

            if (config.ClickHandler != null)
            {
                menuItem.Click += (sender, e) => config.ClickHandler(sender, e);
            }

            return menuItem;
        }

        /// <summary>
        /// 将 ToolStripButton 转换为 ToolStripMenuItem
        /// </summary>
        /// <param name="toolStripButton"></param>
        /// <returns></returns>
        private ToolStripMenuItem ConversionMenuItem(ToolStripButton toolStripButton)
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = toolStripButton.Text,
                ToolTipText = toolStripButton.ToolTipText,
                Image = toolStripButton.Image,
                Checked = toolStripButton.Checked
            };

            // 复制 Click 事件处理程序
            menuItem.Click += (sender, e) => toolStripButton.PerformClick();

            return menuItem;
        }

        /// <summary>
        /// 向指定目标添加分隔符
        /// </summary>
        private void AddSeparatorToTarget(MenuShowTarget target)
        {
            switch (target)
            {
                case MenuShowTarget.ToolStrip:
                    toolStrip1.Items.Add(new ToolStripSeparator());
                    break;

                case MenuShowTarget.ContextMenuStrip:
                    DgvContextMenuStrip.Items.Add(new ToolStripSeparator());
                    break;

                case MenuShowTarget.BottomNavigator:
                    bindingNavigator.Items.Add(new ToolStripSeparator());
                    break;
            }
        }

        /// <summary>
        /// 向指定目标添加菜单项
        /// </summary>
        private void AddMenuItemToTarget(ToolStripButton menuItem, MenuShowTarget target)
        {
            switch (target)
            {
                case MenuShowTarget.ToolStrip:
                    toolStrip1.Items.Add(menuItem);
                    break;

                case MenuShowTarget.ContextMenuStrip:
                    DgvContextMenuStrip.Items.Add(ConversionMenuItem(menuItem));
                    break;

                case MenuShowTarget.BottomNavigator:
                    bindingNavigator.Items.Add(menuItem);
                    break;
            }
        }

        /// <summary>
        /// 设置用户工具条菜单过滤器
        /// </summary>
        public void SetUserToolStripMenuForFilter(List<DgvCustomFilterConfig> configs)
        {
            List<DgvUserContextMenuStripConfig> dgvUserContextMenuStripConfigs = new List<DgvUserContextMenuStripConfig>();

            foreach (DgvCustomFilterConfig config in configs)
            {
                // 构建自定义菜单
                DgvUserContextMenuStripConfig dgvUserContextMenuStripConfig = new DgvUserContextMenuStripConfig();
                dgvUserContextMenuStripConfig.Checked = config.Checked;
                dgvUserContextMenuStripConfig.MenuIcon = config.MenuIcon;
                dgvUserContextMenuStripConfig.Target = config.Target;
                dgvUserContextMenuStripConfig.Group = config.Group;
                dgvUserContextMenuStripConfig.MenuText = config.MenuText;
                dgvUserContextMenuStripConfig.ClickHandler = (senders, es) =>
                {
                    // 委托，设置数据源的筛选管理器
                    Filter = null;
                    Filter = config.FilterText;
                };

                // 添加至 List 对象中
                dgvUserContextMenuStripConfigs.Add(dgvUserContextMenuStripConfig);
            }

            // 构建用户工具栏按钮
            SetUserContextMenu(dgvUserContextMenuStripConfigs);
        }

        #endregion DgvContextMenuStrip菜单事件

        #region RowHeaderContextMenuStrip菜单事件

        private void ToolStripMenuItem_add_Click(object sender, EventArgs e)
        {
            // 新增
            bindingNavigatorAddNewItem_Click(sender, e);
        }

        private void ToolStripMenuItem_edit_Click(object sender, EventArgs e)
        {
            // 修改
        }

        private void ToolStripMenuItem_del_Click(object sender, EventArgs e)
        {
            // 删除
            bindingNavigatorDeleteItem_Click(sender, e);
        }

        private void ToolStripMenuItem_RowHeader_copy_Click(object sender, EventArgs e)
        {
            // 复制整行
            if (dataGridView.SelectedRows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                // 使用LINQ对SelectedRows进行倒序迭代
                var reversedSelectedRows = dataGridView.SelectedRows.Cast<DataGridViewRow>().Reverse();

                foreach (DataGridViewRow selectedRow in reversedSelectedRows)
                {
                    for (int i = 0; i < selectedRow.Cells.Count; i++)
                    {
                        // 添加单元格的值，如果为空则添加空字符串，并用制表符分隔单元格
                        sb.Append(selectedRow.Cells[i].Value?.ToString() ?? string.Empty).Append("\t");
                    }

                    // 移除最后一个制表符并添加换行符
                    if (sb.Length > 0)
                    {
                        sb.Length--; // 移除最后一个制表符
                    }
                    sb.AppendLine(); // 添加换行符以分隔行
                }

                // 将文本放入剪贴板
                Clipboard.SetText(sb.ToString());
            }
        }

        private void ToolStripMenuItem_RowHeader_exp_Click(object sender, EventArgs e)
        {
            // 导出
        }

        #endregion RowHeaderContextMenuStrip菜单事件

        #region ColHeaderContextMenuStrip菜单事件

        private void ToolStripMenuItem_ColHeader_asc_Click(object sender, EventArgs e)
        {
            // 升序
            toolStripButton_asc_Click(sender, e);
        }

        private void ToolStripMenuItem_ColHeader_desc_Click(object sender, EventArgs e)
        {
            // 降序
            toolStripButton_desc_Click(sender, e);
        }

        private void ToolStripMenuItem_ColHeader_query_Click(object sender, EventArgs e)
        {
            // 查找
            ToolStripMenuItem_query_Click(sender, e);
        }

        private void ToolStripMenuItem_ColHeader_find_Click(object sender, EventArgs e)
        {
            // 筛选
            ToolStripMenuItem_find_Click(sender, e);
        }

        private void ToolStripMenuItem_ColHeader_CancelFind_Click(object sender, EventArgs e)
        {
            // 取消所有筛选
            ToolStripMenuItem_CancelFind_Click(sender, e);
        }

        private void ToolStripMenuItem_ColHeader_copy_Click(object sender, EventArgs e)
        {
            // 复制选择行，指定列的值
            if (dataGridView.SelectedRows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                // 使用LINQ对SelectedRows进行倒序迭代
                var reversedSelectedRows = dataGridView.SelectedRows.Cast<DataGridViewRow>().Reverse();

                foreach (DataGridViewRow selectedRow in reversedSelectedRows)
                {
                    // 添加单元格的值，如果为空则添加空字符串，并用制表符分隔单元格
                    sb.Append(selectedRow.Cells[_columnIndex].Value?.ToString() ?? string.Empty).Append("\t");

                    // 移除最后一个制表符并添加换行符
                    if (sb.Length > 0)
                    {
                        sb.Length--; // 移除最后一个制表符
                    }
                    sb.AppendLine(); // 添加换行符以分隔行
                }

                // 将文本放入剪贴板
                Clipboard.SetText(sb.ToString());
            }
        }

        private void ToolStripMenuItem_ColHeader_exp_Click(object sender, EventArgs e)
        {
            // 导出
        }

        private void toolStripMenuItem_colWidth_Click(object sender, EventArgs e)
        {
            // 字段宽度
        }

        private void ToolStripMenuItem_ColHeader_HideCol_Click(object sender, EventArgs e)
        {
            // 隐藏字段
            dataGridView.Columns[_columnName].Visible = false;

            // 更新字段属性配置
            foreach (DgvColumnInfoConfig columnInfo in ColumnInfos)
            {
                if (columnInfo.Name == _columnName)
                {
                    columnInfo.IsVisible = false;
                }
            }

            // 处理是否需要保持配置对象至本地磁盘
            if (Guid != null)
            {
                // 延迟保存配置
                ScheduleConfigSave();
            }
        }

        private void ToolStripMenuItem_ColHeader_CancelHideCol_Click(object sender, EventArgs e)
        {
            // 取消隐藏字段
            ToolStripMenuItem_ColHeader_Att_Click(sender, e);
        }

        private void ToolStripMenuItem_ColHeader_FreezeCol_Click(object sender, EventArgs e)
        {
            // 冻结字段
            dataGridView.Columns[_columnName].Frozen = true;
        }

        private void ToolStripMenuItem_ColHeader_CancelFreezeCol_Click(object sender, EventArgs e)
        {
            // 取消冻结所有字段
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.Frozen = false;
            }
        }

        private void ToolStripMenuItem_ColHeader_addCol_Click(object sender, EventArgs e)
        {
            // 添加自定义列
            FrmCustomColumnsConfig frmCustomColumnsConfig = new FrmCustomColumnsConfig(CustomColumnsConfigs);
            frmCustomColumnsConfig.ClosedEvent += FrmCustomColumnsConfig_ClosedEvent;
            frmCustomColumnsConfig.ShowDialog();

            // 暂停，此功能的使用，由于和筛选、排序功能的冲突。
            // 解决方案，直接将配置应用于数据源 DataTable 级别。
            // 避免在UI层(DGV)动态创建列，可以尝试在数据源中添加列。
        }

        /// <summary>
        /// 自定义列属性，关闭后回传的数据对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="dgvCustomColumnsConfigs"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void FrmCustomColumnsConfig_ClosedEvent(object sender, FormClosedEventArgs e, List<DgvCustomColumnsConfig> dgvCustomColumnsConfigs)
        {
            // 自定义字段数据传递
            CustomColumnsConfigs = dgvCustomColumnsConfigs;

            // 处理是否需要保持配置对象至本地磁盘
            if (Guid != null)
            {
                SaveCustomColumnsConfigs();
            }
        }

        private void ToolStripMenuItem_ColHeader_ConditionalStyles_Click(object sender, EventArgs e)
        {
            // 条件格式
            toolStripButton_conditionalStyles_Click(sender, e);
        }

        private void ToolStripMenuItem_ColHeader_Att_Click(object sender, EventArgs e)
        {
            // 字段属性
            List<DgvColumnInfoConfig> columnInfos = new List<DgvColumnInfoConfig>();
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                bool isPrintable = column.Visible;

                try
                {
                    // 获取 Tag 数据，返回打印和导出状态
                    Dictionary<string, bool> keyValuePairs;
                    keyValuePairs = (Dictionary<string, bool>)column.Tag;
                    isPrintable = keyValuePairs?["IsPrintable"] ?? true;
                }
                catch (Exception) { }

                int rowWidth = column.Width;
                int printWidth = column.Width;
                int displayIndex = column.DisplayIndex;
                DataGridViewAutoSizeColumnMode cellAutoSizeMode = column.InheritedAutoSizeMode;
                DataGridViewContentAlignment defaultCellStyle = column.DefaultCellStyle.Alignment;
                SortTypeDirection sortType = SortTypeDirection.None;
                int sortingIndex = 0;

                // 创建并添加DataGridViewColumnInfo对象到列表中
                columnInfos.Add(new DgvColumnInfoConfig(
                            column.Name,
                            column.HeaderText,
                            column.Visible,
                            isPrintable,
                            rowWidth,
                            printWidth,
                            displayIndex,
                            cellAutoSizeMode,
                            defaultCellStyle,
                            column.ReadOnly,
                            sortType,
                            sortingIndex));
            }

            // 替换排序类型和次序，用于修改
            foreach (DgvColumnInfoConfig dgvColumnInfo in _columnInfos)
            {
                foreach (DgvColumnInfoConfig infoConfig in columnInfos)
                {
                    if (infoConfig.Name == dgvColumnInfo.Name)
                    {
                        infoConfig.SortType = dgvColumnInfo.SortType;
                        infoConfig.SortingIndex = dgvColumnInfo.SortingIndex;
                    }
                }
            }

            // 调用字段属性窗口
            FrmFieldProperties fieldProperties = new FrmFieldProperties(columnInfos);
            fieldProperties.ClosedEvent += FieldProperties_ClosedEvent;
            fieldProperties.ShowDialog();
        }

        /// <summary>
        /// 字段属性，关闭后回传的数据对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="columnInfos"></param>
        private void FieldProperties_ClosedEvent(object sender, FormClosedEventArgs e, List<DgvColumnInfoConfig> columnInfos)
        {
            ColumnInfos = columnInfos;

            // 更新字段属性配置
            SetColumnInfo(ColumnInfos);

            // 处理是否需要保持配置对象至本地磁盘
            if (Guid != null)
            {
                // 延迟保存配置
                ScheduleConfigSave();
            }
        }

        #endregion ColHeaderContextMenuStrip菜单事件

        #region ToolStrip工具栏事件

        private void toolStripButton_print_Click(object sender, EventArgs e)
        {
            // 打印预览
            ToolStripMenuItem_printPreview_Click(sender, e);
        }

        private void toolStripButton_asc_Click(object sender, EventArgs e)
        {
            // 升序
            if (_columnName != null)
                dataGridView.Sort(dataGridView.Columns[_columnName], ListSortDirection.Ascending);
        }

        private void toolStripButton_desc_Click(object sender, EventArgs e)
        {
            // 降序
            if (_columnName != null)
                dataGridView.Sort(dataGridView.Columns[_columnName], ListSortDirection.Descending);
        }

        private void toolStripButton_query_Click(object sender, EventArgs e)
        {
            // 查找
            ToolStripMenuItem_query_Click(sender, e);
        }

        private void toolStripButton_find1_Click(object sender, EventArgs e)
        {
            // 筛选
            ToolStripMenuItem_find_Click(sender, e);
        }

        private void toolStripButton_cancelFind_Click(object sender, EventArgs e)
        {
            // 取消所有筛选
            ToolStripMenuItem_CancelFind_Click(sender, e);

            // 清空搜索的记录
            ClearAllMatchingRows();
        }

        /// <summary>
        /// 底部工具栏的取消所有筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_cancelFind2_Click(object sender, EventArgs e)
        {
            // 取消所有筛选
            toolStripButton_cancelFind_Click(sender, e);
        }

        /// <summary>
        /// 统计分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_char_Click(object sender, EventArgs e)
        {
            // 统计
            // 获取筛选后的当前绑定源
            BindingSource bindingSource = (BindingSource)dataGridView.DataSource;

            // 格式转换
            DataView dataView = (DataView)bindingSource.DataSource;

            // 统计分析，聚合分析
            FrmStatisticsExpressionConfig frmStatisticsExpressionConfig = new FrmStatisticsExpressionConfig(dataView.ToTable(), _dgvAggregatorConfig, Guid);
            frmStatisticsExpressionConfig.ClosedEvent += FrmStatisticsExpressionConfig_ClosedEvent;
            frmStatisticsExpressionConfig.ShowDialog();
        }

        private void FrmStatisticsExpressionConfig_ClosedEvent(object sender, EventArgs e, DgvAggregatorConfig aggregatorConfig)
        {
            // 持久化条件样式
            // 处理是否需要保持表达式对象至本地磁盘
            if (Guid != null && aggregatorConfig != null)
            {
                SaveStatisticsExpressionConfigs(aggregatorConfig);
            }
        }

        private void toolStripButton_conditionalStyles_Click(object sender, EventArgs e)
        {
            // 载入，本地化存储条件样式配置

            // 调用条件样式管理器窗口
            using (FrmConditionalStyles frmConditional = new FrmConditionalStyles(this, DgvConditionalConfigs))
            {
                frmConditional.ClosedEvent += ConditionalStyles_ClosedEvent;

                // 初始化列名
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    frmConditional.comboBox2.Items.Add(column.Name);
                    //frmConditional.comboBox5.Items.Add(column.Name);
                }
                // 模式对话框方式显示
                frmConditional.ShowDialog();

                // Bug:2024-7-21 Andy 发现，暂未排查解决
                // 在修改格式配置信息后（新增，删除，清空），关闭窗口再次打开窗口时，UI未重新加载
            }
        }

        /// <summary>
        /// 关闭，条件样式管理器后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="configs"></param>
        private void ConditionalStyles_ClosedEvent(object sender, EventArgs e, List<DgvConditionalConfig> configs)
        {
            // 持久化条件样式
            // 处理是否需要保持配置对象至本地磁盘
            if (Guid != null && configs != null)
            {
                SaveConditionalStylesConfigs(configs);
                BuildConditionalFormatting(configs);
            }
        }

        private void toolStripButton_CancelHideCol_Click(object sender, EventArgs e)
        {
            // 字段
            ToolStripMenuItem_ColHeader_Att_Click(sender, e);
        }

        #endregion ToolStrip工具栏事件

        #region Navigator导航栏事件

        private void toolStripButton_find_Click(object sender, EventArgs e)
        {
            // 查找
            if (!this.toolStripTextBox_FindText.Text.Contains(Localizer.Instance.GetString("InputTips")))
            {
                // 判断是否为过滤器模式
                if (_filterMode)
                {
                    // 包含筛选
                    Filter = null;
                    Filter = string.Format("[" + _columnName + "] like '%{0}%'", this.toolStripTextBox_FindText.Text);
                }
                else
                {
                    // 查找
                    FindAllMatchingRows(_columnName, this.toolStripTextBox_FindText.Text);
                }
            }
        }

        private void toolStripButton_Next_Click(object sender, EventArgs e)
        {
            // 下一条
            SelectNextMatchingRow();
        }

        private void toolStripTextBox_FindText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 回车键被按下
                toolStripButton_find_Click(sender, e);

                // 回车键后听到默认的“哔”声
                e.SuppressKeyPress = true;
            }
        }

        private void toolStripLabel_tip_TextChanged(object sender, EventArgs e)
        {
            // 检查 toolStripLabel_tip 的 Text 属性是否为空或空字符串
            if (string.IsNullOrEmpty(toolStripLabel_tip.Text) && string.IsNullOrEmpty(toolStripLabel_Statistics.Text))
            {
                // 设置 toolStripSeparator1 不可见
                toolStripSeparator1.Visible = false;
            }
            else
            {
                // 如果不为空，则设置为可见
                toolStripSeparator1.Visible = true;
            }
        }

        private void toolStripLabel_Statistics_TextChanged(object sender, EventArgs e)
        {
            toolStripLabel_tip_TextChanged(sender, e);
        }

        private void toolStripTextBox_FindText_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(toolStripTextBox_FindText.Text))
            {
                // 设置输入提示
                toolStripTextBox_FindText.Text = Localizer.Instance.GetString("InputTips");
            }
        }

        private void toolStripTextBox_FindText_GotFocus(object sender, EventArgs e)
        {
            if (toolStripTextBox_FindText.Text.Contains(Localizer.Instance.GetString("InputTips")))
            {
                // 设置输入提示
                toolStripTextBox_FindText.Text = string.Empty;
            }
        }

        #endregion Navigator导航栏事件

        #endregion 系统快捷键和菜单按钮
    }
}