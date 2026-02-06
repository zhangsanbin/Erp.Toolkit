using Erp.Toolkit.Localization;

namespace Erp.Toolkit.Controls
{
    partial class Dgv
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                _pen?.Dispose();
                _stringFormat?.Dispose();
                _printDocument?.Dispose();
                _printPreviewDialog?.Dispose();
                _pageSetupDialog?.Dispose();
                _printDialog?.Dispose();
                Localizer.Instance.CultureChanged -= OnCultureChanged;

                // 释放子视图资源
                if (subview != null)
                {
                    subview.Dispose();
                    subview = null;
                }
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dgv));
            this.dataGridView = new Erp.Toolkit.Controls.OptimizedDataGridView();
            this.DgvContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_asc = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_desc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_findText = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemText_eq = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemText_NotEq = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemText_LikeBf = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemText_NotLikeBf = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemText_Like = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemText_NotLike = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemText_EndLike = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemText_EndNotLike = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_findNubmer = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNumber_eq = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNumber_NotEq = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNumber_less = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNumber_greater = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNumber_interval = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_findDate = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_eq = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_NotEq = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_less = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_greater = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_interval = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemDate_tomorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_today = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_yesterday = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemDate_nextWeek = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_thisWeek = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_lastWeek = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemDate_nextMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_thisMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_lastMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemDate_nextQuarter = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_thisQuarter = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_lastQuarter = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemDate_nextYear = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_thisYear = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_soFarThisYear = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDate_lastYear = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_equal = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_NotEqual = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Like = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_NotLike = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_query = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_find = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_CancelFind = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_previousFind = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_print = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_printPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_printPageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_printSelectPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_exp = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_sysTheme = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripComboBox_Language = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripComboBox_Theme = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonReLoadList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox_findColumnName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox_FindText = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_find = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Next = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_cancelFind2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel_tip = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel_Statistics = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_print = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_asc = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_desc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_query = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_find1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_cancelFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_ReLoadList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_char = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_conditionalStyles = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_CancelHideCol = new System.Windows.Forms.ToolStripButton();
            this.RowHeaderIconList = new System.Windows.Forms.ImageList(this.components);
            this.RowHeaderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_add = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_del = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_RowHeader_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_RowHeader_exp = new System.Windows.Forms.ToolStripMenuItem();
            this.ColHeaderContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_ColHeader_asc = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_desc = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_ColHeader_query = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_find = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_CancelFind = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_previousFind = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_ColHeader_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_exp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_colWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_HideCol = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_CancelHideCol = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_FreezeCol = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_CancelFreezeCol = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_ColHeader_addCol = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_ConditionalStyles = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ColHeader_Att = new System.Windows.Forms.ToolStripMenuItem();
            this.VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.DgvContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.RowHeaderContextMenuStrip.SuspendLayout();
            this.ColHeaderContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeight = 30;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Location = new System.Drawing.Point(0, 25);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 48;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(800, 397);
            this.dataGridView.TabIndex = 0;
            // 
            // DgvContextMenuStrip
            // 
            this.DgvContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DgvContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_asc,
            this.ToolStripMenuItem_desc,
            this.toolStripSeparator22,
            this.ToolStripMenuItem_copy,
            this.toolStripSeparator2,
            this.ToolStripMenuItem_findText,
            this.ToolStripMenuItem_findNubmer,
            this.ToolStripMenuItem_findDate,
            this.ToolStripSeparator9,
            this.ToolStripMenuItem_equal,
            this.ToolStripMenuItem_NotEqual,
            this.ToolStripMenuItem_Like,
            this.ToolStripMenuItem_NotLike,
            this.toolStripSeparator4,
            this.ToolStripMenuItem_query,
            this.ToolStripMenuItem_find,
            this.ToolStripSeparator5,
            this.ToolStripMenuItem_refresh,
            this.ToolStripMenuItem_CancelFind,
            this.ToolStripMenuItem_previousFind,
            this.ToolStripSeparator15,
            this.ToolStripMenuItem_print,
            this.ToolStripMenuItem_exp,
            this.ToolStripSeparator8,
            this.ToolStripMenuItem_sysTheme,
            this.toolStripSeparator3});
            this.DgvContextMenuStrip.Name = "ContextMenuStrip1";
            this.DgvContextMenuStrip.Size = new System.Drawing.Size(226, 470);
            // 
            // ToolStripMenuItem_asc
            // 
            this.ToolStripMenuItem_asc.Image = global::Erp.Toolkit.Properties.Resources.toolStripAsc_Image;
            this.ToolStripMenuItem_asc.Name = "ToolStripMenuItem_asc";
            this.ToolStripMenuItem_asc.ShortcutKeyDisplayString = "";
            this.ToolStripMenuItem_asc.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_asc.Text = "升序";
            this.ToolStripMenuItem_asc.Click += new System.EventHandler(this.ToolStripMenuItem_asc_Click);
            // 
            // ToolStripMenuItem_desc
            // 
            this.ToolStripMenuItem_desc.Image = global::Erp.Toolkit.Properties.Resources.toolStripDesc_Image;
            this.ToolStripMenuItem_desc.Name = "ToolStripMenuItem_desc";
            this.ToolStripMenuItem_desc.ShortcutKeyDisplayString = "";
            this.ToolStripMenuItem_desc.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_desc.Text = "降序";
            this.ToolStripMenuItem_desc.Click += new System.EventHandler(this.ToolStripMenuItem_desc_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(222, 6);
            // 
            // ToolStripMenuItem_copy
            // 
            this.ToolStripMenuItem_copy.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_copy.Image")));
            this.ToolStripMenuItem_copy.Name = "ToolStripMenuItem_copy";
            this.ToolStripMenuItem_copy.ShortcutKeyDisplayString = "Ctrl+C";
            this.ToolStripMenuItem_copy.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_copy.Text = "复制";
            this.ToolStripMenuItem_copy.Click += new System.EventHandler(this.ToolStripMenuItem_copy_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(222, 6);
            // 
            // ToolStripMenuItem_findText
            // 
            this.ToolStripMenuItem_findText.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemText_eq,
            this.ToolStripMenuItemText_NotEq,
            this.ToolStripMenuItemText_LikeBf,
            this.ToolStripMenuItemText_NotLikeBf,
            this.ToolStripMenuItemText_Like,
            this.ToolStripMenuItemText_NotLike,
            this.ToolStripMenuItemText_EndLike,
            this.ToolStripMenuItemText_EndNotLike});
            this.ToolStripMenuItem_findText.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_findText.Image")));
            this.ToolStripMenuItem_findText.Name = "ToolStripMenuItem_findText";
            this.ToolStripMenuItem_findText.ShortcutKeyDisplayString = "Ctrl+Shift+F";
            this.ToolStripMenuItem_findText.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_findText.Text = "文本筛选器";
            // 
            // ToolStripMenuItemText_eq
            // 
            this.ToolStripMenuItemText_eq.Name = "ToolStripMenuItemText_eq";
            this.ToolStripMenuItemText_eq.Size = new System.Drawing.Size(126, 22);
            this.ToolStripMenuItemText_eq.Text = "等于";
            this.ToolStripMenuItemText_eq.Click += new System.EventHandler(this.ToolStripMenuItemText_eq_Click);
            // 
            // ToolStripMenuItemText_NotEq
            // 
            this.ToolStripMenuItemText_NotEq.Name = "ToolStripMenuItemText_NotEq";
            this.ToolStripMenuItemText_NotEq.Size = new System.Drawing.Size(126, 22);
            this.ToolStripMenuItemText_NotEq.Text = "不等于";
            this.ToolStripMenuItemText_NotEq.Click += new System.EventHandler(this.ToolStripMenuItemText_NotEq_Click);
            // 
            // ToolStripMenuItemText_LikeBf
            // 
            this.ToolStripMenuItemText_LikeBf.Name = "ToolStripMenuItemText_LikeBf";
            this.ToolStripMenuItemText_LikeBf.Size = new System.Drawing.Size(126, 22);
            this.ToolStripMenuItemText_LikeBf.Text = "开头是";
            this.ToolStripMenuItemText_LikeBf.Click += new System.EventHandler(this.ToolStripMenuItemText_LikeBf_Click);
            // 
            // ToolStripMenuItemText_NotLikeBf
            // 
            this.ToolStripMenuItemText_NotLikeBf.Name = "ToolStripMenuItemText_NotLikeBf";
            this.ToolStripMenuItemText_NotLikeBf.Size = new System.Drawing.Size(126, 22);
            this.ToolStripMenuItemText_NotLikeBf.Text = "开头不是";
            this.ToolStripMenuItemText_NotLikeBf.Click += new System.EventHandler(this.ToolStripMenuItemText_NotLikeBf_Click);
            // 
            // ToolStripMenuItemText_Like
            // 
            this.ToolStripMenuItemText_Like.Name = "ToolStripMenuItemText_Like";
            this.ToolStripMenuItemText_Like.Size = new System.Drawing.Size(126, 22);
            this.ToolStripMenuItemText_Like.Text = "包含";
            this.ToolStripMenuItemText_Like.Click += new System.EventHandler(this.ToolStripMenuItemText_Like_Click);
            // 
            // ToolStripMenuItemText_NotLike
            // 
            this.ToolStripMenuItemText_NotLike.Name = "ToolStripMenuItemText_NotLike";
            this.ToolStripMenuItemText_NotLike.Size = new System.Drawing.Size(126, 22);
            this.ToolStripMenuItemText_NotLike.Text = "不包含";
            this.ToolStripMenuItemText_NotLike.Click += new System.EventHandler(this.ToolStripMenuItemText_NotLike_Click);
            // 
            // ToolStripMenuItemText_EndLike
            // 
            this.ToolStripMenuItemText_EndLike.Name = "ToolStripMenuItemText_EndLike";
            this.ToolStripMenuItemText_EndLike.Size = new System.Drawing.Size(126, 22);
            this.ToolStripMenuItemText_EndLike.Text = "结尾是";
            this.ToolStripMenuItemText_EndLike.Click += new System.EventHandler(this.ToolStripMenuItemText_EndLike_Click);
            // 
            // ToolStripMenuItemText_EndNotLike
            // 
            this.ToolStripMenuItemText_EndNotLike.Name = "ToolStripMenuItemText_EndNotLike";
            this.ToolStripMenuItemText_EndNotLike.Size = new System.Drawing.Size(126, 22);
            this.ToolStripMenuItemText_EndNotLike.Text = "结尾不是";
            this.ToolStripMenuItemText_EndNotLike.Click += new System.EventHandler(this.ToolStripMenuItemText_EndNotLike_Click);
            // 
            // ToolStripMenuItem_findNubmer
            // 
            this.ToolStripMenuItem_findNubmer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemNumber_eq,
            this.ToolStripMenuItemNumber_NotEq,
            this.ToolStripMenuItemNumber_less,
            this.ToolStripMenuItemNumber_greater,
            this.ToolStripMenuItemNumber_interval});
            this.ToolStripMenuItem_findNubmer.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_findNubmer.Image")));
            this.ToolStripMenuItem_findNubmer.Name = "ToolStripMenuItem_findNubmer";
            this.ToolStripMenuItem_findNubmer.ShortcutKeyDisplayString = "Ctrl+Shift+F";
            this.ToolStripMenuItem_findNubmer.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_findNubmer.Text = "数字筛选器";
            this.ToolStripMenuItem_findNubmer.Visible = false;
            // 
            // ToolStripMenuItemNumber_eq
            // 
            this.ToolStripMenuItemNumber_eq.Name = "ToolStripMenuItemNumber_eq";
            this.ToolStripMenuItemNumber_eq.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItemNumber_eq.Text = "等于";
            this.ToolStripMenuItemNumber_eq.Click += new System.EventHandler(this.ToolStripMenuItemNumber_eq_Click);
            // 
            // ToolStripMenuItemNumber_NotEq
            // 
            this.ToolStripMenuItemNumber_NotEq.Name = "ToolStripMenuItemNumber_NotEq";
            this.ToolStripMenuItemNumber_NotEq.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItemNumber_NotEq.Text = "不等于";
            this.ToolStripMenuItemNumber_NotEq.Click += new System.EventHandler(this.ToolStripMenuItemNumber_NotEq_Click);
            // 
            // ToolStripMenuItemNumber_less
            // 
            this.ToolStripMenuItemNumber_less.Name = "ToolStripMenuItemNumber_less";
            this.ToolStripMenuItemNumber_less.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItemNumber_less.Text = "小于";
            this.ToolStripMenuItemNumber_less.Click += new System.EventHandler(this.ToolStripMenuItemNumber_less_Click);
            // 
            // ToolStripMenuItemNumber_greater
            // 
            this.ToolStripMenuItemNumber_greater.Name = "ToolStripMenuItemNumber_greater";
            this.ToolStripMenuItemNumber_greater.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItemNumber_greater.Text = "大于";
            this.ToolStripMenuItemNumber_greater.Click += new System.EventHandler(this.ToolStripMenuItemNumber_greater_Click);
            // 
            // ToolStripMenuItemNumber_interval
            // 
            this.ToolStripMenuItemNumber_interval.Name = "ToolStripMenuItemNumber_interval";
            this.ToolStripMenuItemNumber_interval.Size = new System.Drawing.Size(113, 22);
            this.ToolStripMenuItemNumber_interval.Text = "期间";
            this.ToolStripMenuItemNumber_interval.Click += new System.EventHandler(this.ToolStripMenuItemNumber_interval_Click);
            // 
            // ToolStripMenuItem_findDate
            // 
            this.ToolStripMenuItem_findDate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDate_eq,
            this.ToolStripMenuItemDate_NotEq,
            this.ToolStripMenuItemDate_less,
            this.ToolStripMenuItemDate_greater,
            this.ToolStripMenuItemDate_interval,
            this.ToolStripSeparator10,
            this.ToolStripMenuItemDate_tomorrow,
            this.ToolStripMenuItemDate_today,
            this.ToolStripMenuItemDate_yesterday,
            this.ToolStripSeparator11,
            this.ToolStripMenuItemDate_nextWeek,
            this.ToolStripMenuItemDate_thisWeek,
            this.ToolStripMenuItemDate_lastWeek,
            this.ToolStripSeparator12,
            this.ToolStripMenuItemDate_nextMonth,
            this.ToolStripMenuItemDate_thisMonth,
            this.ToolStripMenuItemDate_lastMonth,
            this.ToolStripSeparator13,
            this.ToolStripMenuItemDate_nextQuarter,
            this.ToolStripMenuItemDate_thisQuarter,
            this.ToolStripMenuItemDate_lastQuarter,
            this.ToolStripSeparator14,
            this.ToolStripMenuItemDate_nextYear,
            this.ToolStripMenuItemDate_thisYear,
            this.ToolStripMenuItemDate_soFarThisYear,
            this.ToolStripMenuItemDate_lastYear});
            this.ToolStripMenuItem_findDate.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_findDate.Image")));
            this.ToolStripMenuItem_findDate.Name = "ToolStripMenuItem_findDate";
            this.ToolStripMenuItem_findDate.ShortcutKeyDisplayString = "Ctrl+Shift+F";
            this.ToolStripMenuItem_findDate.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_findDate.Text = "日期筛选器";
            this.ToolStripMenuItem_findDate.Visible = false;
            // 
            // ToolStripMenuItemDate_eq
            // 
            this.ToolStripMenuItemDate_eq.Name = "ToolStripMenuItemDate_eq";
            this.ToolStripMenuItemDate_eq.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_eq.Text = "等于";
            this.ToolStripMenuItemDate_eq.Click += new System.EventHandler(this.ToolStripMenuItemDate_eq_Click);
            // 
            // ToolStripMenuItemDate_NotEq
            // 
            this.ToolStripMenuItemDate_NotEq.Name = "ToolStripMenuItemDate_NotEq";
            this.ToolStripMenuItemDate_NotEq.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_NotEq.Text = "不等于";
            this.ToolStripMenuItemDate_NotEq.Click += new System.EventHandler(this.ToolStripMenuItemDate_NotEq_Click);
            // 
            // ToolStripMenuItemDate_less
            // 
            this.ToolStripMenuItemDate_less.Name = "ToolStripMenuItemDate_less";
            this.ToolStripMenuItemDate_less.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_less.Text = "之前";
            this.ToolStripMenuItemDate_less.Click += new System.EventHandler(this.ToolStripMenuItemDate_less_Click);
            // 
            // ToolStripMenuItemDate_greater
            // 
            this.ToolStripMenuItemDate_greater.Name = "ToolStripMenuItemDate_greater";
            this.ToolStripMenuItemDate_greater.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_greater.Text = "之后";
            this.ToolStripMenuItemDate_greater.Click += new System.EventHandler(this.ToolStripMenuItemDate_greater_Click);
            // 
            // ToolStripMenuItemDate_interval
            // 
            this.ToolStripMenuItemDate_interval.Name = "ToolStripMenuItemDate_interval";
            this.ToolStripMenuItemDate_interval.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_interval.Text = "期间";
            this.ToolStripMenuItemDate_interval.Click += new System.EventHandler(this.ToolStripMenuItemDate_interval_Click);
            // 
            // ToolStripSeparator10
            // 
            this.ToolStripSeparator10.Name = "ToolStripSeparator10";
            this.ToolStripSeparator10.Size = new System.Drawing.Size(175, 6);
            // 
            // ToolStripMenuItemDate_tomorrow
            // 
            this.ToolStripMenuItemDate_tomorrow.Name = "ToolStripMenuItemDate_tomorrow";
            this.ToolStripMenuItemDate_tomorrow.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_tomorrow.Text = "明天";
            this.ToolStripMenuItemDate_tomorrow.Click += new System.EventHandler(this.ToolStripMenuItemDate_tomorrow_Click);
            // 
            // ToolStripMenuItemDate_today
            // 
            this.ToolStripMenuItemDate_today.Name = "ToolStripMenuItemDate_today";
            this.ToolStripMenuItemDate_today.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_today.Text = "今天";
            this.ToolStripMenuItemDate_today.Click += new System.EventHandler(this.ToolStripMenuItemDate_today_Click);
            // 
            // ToolStripMenuItemDate_yesterday
            // 
            this.ToolStripMenuItemDate_yesterday.Name = "ToolStripMenuItemDate_yesterday";
            this.ToolStripMenuItemDate_yesterday.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_yesterday.Text = "昨天";
            this.ToolStripMenuItemDate_yesterday.Click += new System.EventHandler(this.ToolStripMenuItemDate_yesterday_Click);
            // 
            // ToolStripSeparator11
            // 
            this.ToolStripSeparator11.Name = "ToolStripSeparator11";
            this.ToolStripSeparator11.Size = new System.Drawing.Size(175, 6);
            // 
            // ToolStripMenuItemDate_nextWeek
            // 
            this.ToolStripMenuItemDate_nextWeek.Name = "ToolStripMenuItemDate_nextWeek";
            this.ToolStripMenuItemDate_nextWeek.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_nextWeek.Text = "下周";
            this.ToolStripMenuItemDate_nextWeek.Click += new System.EventHandler(this.ToolStripMenuItemDate_nextWeek_Click);
            // 
            // ToolStripMenuItemDate_thisWeek
            // 
            this.ToolStripMenuItemDate_thisWeek.Name = "ToolStripMenuItemDate_thisWeek";
            this.ToolStripMenuItemDate_thisWeek.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_thisWeek.Text = "本周";
            this.ToolStripMenuItemDate_thisWeek.Click += new System.EventHandler(this.ToolStripMenuItemDate_thisWeek_Click);
            // 
            // ToolStripMenuItemDate_lastWeek
            // 
            this.ToolStripMenuItemDate_lastWeek.Name = "ToolStripMenuItemDate_lastWeek";
            this.ToolStripMenuItemDate_lastWeek.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_lastWeek.Text = "上周";
            this.ToolStripMenuItemDate_lastWeek.Click += new System.EventHandler(this.ToolStripMenuItemDate_lastWeek_Click);
            // 
            // ToolStripSeparator12
            // 
            this.ToolStripSeparator12.Name = "ToolStripSeparator12";
            this.ToolStripSeparator12.Size = new System.Drawing.Size(175, 6);
            // 
            // ToolStripMenuItemDate_nextMonth
            // 
            this.ToolStripMenuItemDate_nextMonth.Name = "ToolStripMenuItemDate_nextMonth";
            this.ToolStripMenuItemDate_nextMonth.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_nextMonth.Text = "下月";
            this.ToolStripMenuItemDate_nextMonth.Click += new System.EventHandler(this.ToolStripMenuItemDate_nextMonth_Click);
            // 
            // ToolStripMenuItemDate_thisMonth
            // 
            this.ToolStripMenuItemDate_thisMonth.Name = "ToolStripMenuItemDate_thisMonth";
            this.ToolStripMenuItemDate_thisMonth.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_thisMonth.Text = "本月";
            this.ToolStripMenuItemDate_thisMonth.Click += new System.EventHandler(this.ToolStripMenuItemDate_thisMonth_Click);
            // 
            // ToolStripMenuItemDate_lastMonth
            // 
            this.ToolStripMenuItemDate_lastMonth.Name = "ToolStripMenuItemDate_lastMonth";
            this.ToolStripMenuItemDate_lastMonth.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_lastMonth.Text = "上月";
            this.ToolStripMenuItemDate_lastMonth.Click += new System.EventHandler(this.ToolStripMenuItemDate_lastMonth_Click);
            // 
            // ToolStripSeparator13
            // 
            this.ToolStripSeparator13.Name = "ToolStripSeparator13";
            this.ToolStripSeparator13.Size = new System.Drawing.Size(175, 6);
            // 
            // ToolStripMenuItemDate_nextQuarter
            // 
            this.ToolStripMenuItemDate_nextQuarter.Name = "ToolStripMenuItemDate_nextQuarter";
            this.ToolStripMenuItemDate_nextQuarter.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_nextQuarter.Text = "下季度";
            this.ToolStripMenuItemDate_nextQuarter.Click += new System.EventHandler(this.ToolStripMenuItemDate_nextQuarter_Click);
            // 
            // ToolStripMenuItemDate_thisQuarter
            // 
            this.ToolStripMenuItemDate_thisQuarter.Name = "ToolStripMenuItemDate_thisQuarter";
            this.ToolStripMenuItemDate_thisQuarter.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_thisQuarter.Text = "本季度";
            this.ToolStripMenuItemDate_thisQuarter.Click += new System.EventHandler(this.ToolStripMenuItemDate_thisQuarter_Click);
            // 
            // ToolStripMenuItemDate_lastQuarter
            // 
            this.ToolStripMenuItemDate_lastQuarter.Name = "ToolStripMenuItemDate_lastQuarter";
            this.ToolStripMenuItemDate_lastQuarter.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_lastQuarter.Text = "上季度";
            this.ToolStripMenuItemDate_lastQuarter.Click += new System.EventHandler(this.ToolStripMenuItemDate_lastQuarter_Click);
            // 
            // ToolStripSeparator14
            // 
            this.ToolStripSeparator14.Name = "ToolStripSeparator14";
            this.ToolStripSeparator14.Size = new System.Drawing.Size(175, 6);
            // 
            // ToolStripMenuItemDate_nextYear
            // 
            this.ToolStripMenuItemDate_nextYear.Name = "ToolStripMenuItemDate_nextYear";
            this.ToolStripMenuItemDate_nextYear.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_nextYear.Text = "明年";
            this.ToolStripMenuItemDate_nextYear.Click += new System.EventHandler(this.ToolStripMenuItemDate_nextYear_Click);
            // 
            // ToolStripMenuItemDate_thisYear
            // 
            this.ToolStripMenuItemDate_thisYear.Name = "ToolStripMenuItemDate_thisYear";
            this.ToolStripMenuItemDate_thisYear.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_thisYear.Text = "今年";
            this.ToolStripMenuItemDate_thisYear.Click += new System.EventHandler(this.ToolStripMenuItemDate_thisYear_Click);
            // 
            // ToolStripMenuItemDate_soFarThisYear
            // 
            this.ToolStripMenuItemDate_soFarThisYear.Name = "ToolStripMenuItemDate_soFarThisYear";
            this.ToolStripMenuItemDate_soFarThisYear.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_soFarThisYear.Text = "今年度截止到现在";
            this.ToolStripMenuItemDate_soFarThisYear.Click += new System.EventHandler(this.ToolStripMenuItemDate_soFarThisYear_Click);
            // 
            // ToolStripMenuItemDate_lastYear
            // 
            this.ToolStripMenuItemDate_lastYear.Name = "ToolStripMenuItemDate_lastYear";
            this.ToolStripMenuItemDate_lastYear.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemDate_lastYear.Text = "去年";
            this.ToolStripMenuItemDate_lastYear.Click += new System.EventHandler(this.ToolStripMenuItemDate_lastYear_Click);
            // 
            // ToolStripSeparator9
            // 
            this.ToolStripSeparator9.Name = "ToolStripSeparator9";
            this.ToolStripSeparator9.Size = new System.Drawing.Size(222, 6);
            // 
            // ToolStripMenuItem_equal
            // 
            this.ToolStripMenuItem_equal.Name = "ToolStripMenuItem_equal";
            this.ToolStripMenuItem_equal.ShortcutKeyDisplayString = "Alt+1";
            this.ToolStripMenuItem_equal.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_equal.Text = "等于";
            this.ToolStripMenuItem_equal.Click += new System.EventHandler(this.ToolStripMenuItem_equal_Click);
            // 
            // ToolStripMenuItem_NotEqual
            // 
            this.ToolStripMenuItem_NotEqual.Name = "ToolStripMenuItem_NotEqual";
            this.ToolStripMenuItem_NotEqual.ShortcutKeyDisplayString = "Alt+2";
            this.ToolStripMenuItem_NotEqual.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_NotEqual.Text = "不等于";
            this.ToolStripMenuItem_NotEqual.Click += new System.EventHandler(this.ToolStripMenuItem_NotEqual_Click);
            // 
            // ToolStripMenuItem_Like
            // 
            this.ToolStripMenuItem_Like.Name = "ToolStripMenuItem_Like";
            this.ToolStripMenuItem_Like.ShortcutKeyDisplayString = "Alt+3";
            this.ToolStripMenuItem_Like.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_Like.Text = "包含";
            this.ToolStripMenuItem_Like.Click += new System.EventHandler(this.ToolStripMenuItem_Like_Click);
            // 
            // ToolStripMenuItem_NotLike
            // 
            this.ToolStripMenuItem_NotLike.Name = "ToolStripMenuItem_NotLike";
            this.ToolStripMenuItem_NotLike.ShortcutKeyDisplayString = "Alt+4";
            this.ToolStripMenuItem_NotLike.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_NotLike.Text = "不包含";
            this.ToolStripMenuItem_NotLike.Click += new System.EventHandler(this.ToolStripMenuItem_NotLike_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(222, 6);
            // 
            // ToolStripMenuItem_query
            // 
            this.ToolStripMenuItem_query.Image = global::Erp.Toolkit.Properties.Resources.toolStripFind_Image;
            this.ToolStripMenuItem_query.Name = "ToolStripMenuItem_query";
            this.ToolStripMenuItem_query.ShortcutKeyDisplayString = "Ctrl+F";
            this.ToolStripMenuItem_query.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_query.Text = "查找";
            this.ToolStripMenuItem_query.Click += new System.EventHandler(this.ToolStripMenuItem_query_Click);
            // 
            // ToolStripMenuItem_find
            // 
            this.ToolStripMenuItem_find.Image = global::Erp.Toolkit.Properties.Resources.cancelFind;
            this.ToolStripMenuItem_find.Name = "ToolStripMenuItem_find";
            this.ToolStripMenuItem_find.ShortcutKeyDisplayString = "Ctrl+Shift+F";
            this.ToolStripMenuItem_find.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_find.Text = "筛选";
            this.ToolStripMenuItem_find.Click += new System.EventHandler(this.ToolStripMenuItem_find_Click);
            // 
            // ToolStripSeparator5
            // 
            this.ToolStripSeparator5.Name = "ToolStripSeparator5";
            this.ToolStripSeparator5.Size = new System.Drawing.Size(222, 6);
            // 
            // ToolStripMenuItem_refresh
            // 
            this.ToolStripMenuItem_refresh.Image = global::Erp.Toolkit.Properties.Resources.refresh;
            this.ToolStripMenuItem_refresh.Name = "ToolStripMenuItem_refresh";
            this.ToolStripMenuItem_refresh.ShortcutKeyDisplayString = "F5";
            this.ToolStripMenuItem_refresh.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_refresh.Text = "刷新";
            this.ToolStripMenuItem_refresh.Click += new System.EventHandler(this.ToolStripMenuItem_refresh_Click);
            // 
            // ToolStripMenuItem_CancelFind
            // 
            this.ToolStripMenuItem_CancelFind.Enabled = false;
            this.ToolStripMenuItem_CancelFind.Image = global::Erp.Toolkit.Properties.Resources.toolStripUnfilter_Image;
            this.ToolStripMenuItem_CancelFind.Name = "ToolStripMenuItem_CancelFind";
            this.ToolStripMenuItem_CancelFind.ShortcutKeyDisplayString = "Ctrl+Shift+Z";
            this.ToolStripMenuItem_CancelFind.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_CancelFind.Text = "取消所有筛选";
            this.ToolStripMenuItem_CancelFind.Click += new System.EventHandler(this.ToolStripMenuItem_CancelFind_Click);
            // 
            // ToolStripMenuItem_previousFind
            // 
            this.ToolStripMenuItem_previousFind.Enabled = false;
            this.ToolStripMenuItem_previousFind.Name = "ToolStripMenuItem_previousFind";
            this.ToolStripMenuItem_previousFind.ShortcutKeyDisplayString = "Ctrl+Z";
            this.ToolStripMenuItem_previousFind.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_previousFind.Text = "返回上次筛选";
            this.ToolStripMenuItem_previousFind.Click += new System.EventHandler(this.ToolStripMenuItem_previousFind_Click);
            // 
            // ToolStripSeparator15
            // 
            this.ToolStripSeparator15.Name = "ToolStripSeparator15";
            this.ToolStripSeparator15.Size = new System.Drawing.Size(222, 6);
            // 
            // ToolStripMenuItem_print
            // 
            this.ToolStripMenuItem_print.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_printPreview,
            this.toolStripMenuItem_printPageSetup,
            this.toolStripMenuItem_printSelectPrinter});
            this.ToolStripMenuItem_print.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_print.Image")));
            this.ToolStripMenuItem_print.Name = "ToolStripMenuItem_print";
            this.ToolStripMenuItem_print.ShortcutKeyDisplayString = "Ctrl+P";
            this.ToolStripMenuItem_print.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_print.Text = "打印";
            this.ToolStripMenuItem_print.Click += new System.EventHandler(this.ToolStripMenuItem_print_Click);
            // 
            // ToolStripMenuItem_printPreview
            // 
            this.ToolStripMenuItem_printPreview.Image = global::Erp.Toolkit.Properties.Resources.printPreview;
            this.ToolStripMenuItem_printPreview.Name = "ToolStripMenuItem_printPreview";
            this.ToolStripMenuItem_printPreview.Size = new System.Drawing.Size(141, 22);
            this.ToolStripMenuItem_printPreview.Text = "打印预览";
            this.ToolStripMenuItem_printPreview.Click += new System.EventHandler(this.ToolStripMenuItem_printPreview_Click);
            // 
            // toolStripMenuItem_printPageSetup
            // 
            this.toolStripMenuItem_printPageSetup.Name = "toolStripMenuItem_printPageSetup";
            this.toolStripMenuItem_printPageSetup.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem_printPageSetup.Text = "页面设置";
            this.toolStripMenuItem_printPageSetup.Click += new System.EventHandler(this.toolStripMenuItem_printPageSetup_Click);
            // 
            // toolStripMenuItem_printSelectPrinter
            // 
            this.toolStripMenuItem_printSelectPrinter.Image = global::Erp.Toolkit.Properties.Resources.fastPrinting_32;
            this.toolStripMenuItem_printSelectPrinter.Name = "toolStripMenuItem_printSelectPrinter";
            this.toolStripMenuItem_printSelectPrinter.ShortcutKeyDisplayString = "Ctrl+P";
            this.toolStripMenuItem_printSelectPrinter.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem_printSelectPrinter.Text = "打印";
            this.toolStripMenuItem_printSelectPrinter.Click += new System.EventHandler(this.toolStripMenuItem_printSelectPrinter_Click);
            // 
            // ToolStripMenuItem_exp
            // 
            this.ToolStripMenuItem_exp.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_exp.Image")));
            this.ToolStripMenuItem_exp.Name = "ToolStripMenuItem_exp";
            this.ToolStripMenuItem_exp.ShortcutKeyDisplayString = "Ctrl+E";
            this.ToolStripMenuItem_exp.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_exp.Text = "导出";
            this.ToolStripMenuItem_exp.Click += new System.EventHandler(this.ToolStripMenuItem_exp_Click);
            // 
            // ToolStripSeparator8
            // 
            this.ToolStripSeparator8.Name = "ToolStripSeparator8";
            this.ToolStripSeparator8.Size = new System.Drawing.Size(222, 6);
            // 
            // ToolStripMenuItem_sysTheme
            // 
            this.ToolStripMenuItem_sysTheme.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripComboBox_Language,
            this.ToolStripComboBox_Theme});
            this.ToolStripMenuItem_sysTheme.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_sysTheme.Image")));
            this.ToolStripMenuItem_sysTheme.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.ToolStripMenuItem_sysTheme.Name = "ToolStripMenuItem_sysTheme";
            this.ToolStripMenuItem_sysTheme.Size = new System.Drawing.Size(225, 22);
            this.ToolStripMenuItem_sysTheme.Text = "系统样式";
            // 
            // ToolStripComboBox_Language
            // 
            this.ToolStripComboBox_Language.Items.AddRange(new object[] {
            "英语",
            "简体中文",
            "繁体中文",
            "德语",
            "法语",
            "日语"});
            this.ToolStripComboBox_Language.Name = "ToolStripComboBox_Language";
            this.ToolStripComboBox_Language.Size = new System.Drawing.Size(150, 25);
            this.ToolStripComboBox_Language.Text = "简体中文";
            this.ToolStripComboBox_Language.SelectedIndexChanged += new System.EventHandler(this.ToolStripComboBox_Language_SelectedIndexChanged);
            // 
            // ToolStripComboBox_Theme
            // 
            this.ToolStripComboBox_Theme.Items.AddRange(new object[] {
            "深色主题",
            "浅色主题",
            "蓝色主题",
            "绿色主题",
            "橙色主题",
            "紫色主题"});
            this.ToolStripComboBox_Theme.Name = "ToolStripComboBox_Theme";
            this.ToolStripComboBox_Theme.Size = new System.Drawing.Size(150, 25);
            this.ToolStripComboBox_Theme.Text = "蓝色主题";
            this.ToolStripComboBox_Theme.SelectedIndexChanged += new System.EventHandler(this.ToolStripComboBox_Theme_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(222, 6);
            this.toolStripSeparator3.Visible = false;
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = null;
            this.bindingNavigator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(206)))), ((int)(((byte)(230)))));
            this.bindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator.DeleteItem = null;
            this.bindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bindingNavigator.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.toolStripSeparator23,
            this.bindingNavigatorDeleteItem,
            this.toolStripSeparator24,
            this.toolStripButtonReLoadList,
            this.toolStripSeparator6,
            this.toolStripTextBox_findColumnName,
            this.toolStripLabel1,
            this.toolStripTextBox_FindText,
            this.toolStripButton_find,
            this.toolStripButton_Next,
            this.toolStripButton_cancelFind2,
            this.toolStripSeparator1,
            this.toolStripLabel_tip,
            this.toolStripLabel_Statistics});
            this.bindingNavigator.Location = new System.Drawing.Point(0, 425);
            this.bindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator.Size = new System.Drawing.Size(800, 25);
            this.bindingNavigator.TabIndex = 1;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(29, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "新添";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "删除";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonReLoadList
            // 
            this.toolStripButtonReLoadList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonReLoadList.Image = global::Erp.Toolkit.Properties.Resources.refresh;
            this.toolStripButtonReLoadList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonReLoadList.Name = "toolStripButtonReLoadList";
            this.toolStripButtonReLoadList.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonReLoadList.Text = "刷新";
            this.toolStripButtonReLoadList.Click += new System.EventHandler(this.toolStripButtonReLoadList_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBox_findColumnName
            // 
            this.toolStripTextBox_findColumnName.AutoSize = false;
            this.toolStripTextBox_findColumnName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox_findColumnName.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.toolStripTextBox_findColumnName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.toolStripTextBox_findColumnName.Name = "toolStripTextBox_findColumnName";
            this.toolStripTextBox_findColumnName.ReadOnly = true;
            this.toolStripTextBox_findColumnName.Size = new System.Drawing.Size(120, 23);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(15, 22);
            this.toolStripLabel1.Text = "=";
            // 
            // toolStripTextBox_FindText
            // 
            this.toolStripTextBox_FindText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox_FindText.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.toolStripTextBox_FindText.ForeColor = System.Drawing.Color.MediumBlue;
            this.toolStripTextBox_FindText.Name = "toolStripTextBox_FindText";
            this.toolStripTextBox_FindText.Size = new System.Drawing.Size(220, 25);
            this.toolStripTextBox_FindText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox_FindText_KeyDown);
            // 
            // toolStripButton_find
            // 
            this.toolStripButton_find.Enabled = false;
            this.toolStripButton_find.Image = global::Erp.Toolkit.Properties.Resources.toolStripFind_Image;
            this.toolStripButton_find.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_find.Name = "toolStripButton_find";
            this.toolStripButton_find.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_find.Text = "查找";
            this.toolStripButton_find.Click += new System.EventHandler(this.toolStripButton_find_Click);
            // 
            // toolStripButton_Next
            // 
            this.toolStripButton_Next.Enabled = false;
            this.toolStripButton_Next.Image = global::Erp.Toolkit.Properties.Resources.toolStripFreeze_Image;
            this.toolStripButton_Next.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Next.Name = "toolStripButton_Next";
            this.toolStripButton_Next.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton_Next.Text = "下一个";
            this.toolStripButton_Next.Click += new System.EventHandler(this.toolStripButton_Next_Click);
            // 
            // toolStripButton_cancelFind2
            // 
            this.toolStripButton_cancelFind2.Enabled = false;
            this.toolStripButton_cancelFind2.Image = global::Erp.Toolkit.Properties.Resources.toolStripUnfilter_Image;
            this.toolStripButton_cancelFind2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_cancelFind2.Name = "toolStripButton_cancelFind2";
            this.toolStripButton_cancelFind2.Size = new System.Drawing.Size(79, 20);
            this.toolStripButton_cancelFind2.Text = "取消筛选";
            this.toolStripButton_cancelFind2.Click += new System.EventHandler(this.toolStripButton_cancelFind2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel_tip
            // 
            this.toolStripLabel_tip.Name = "toolStripLabel_tip";
            this.toolStripLabel_tip.Size = new System.Drawing.Size(0, 0);
            this.toolStripLabel_tip.TextChanged += new System.EventHandler(this.toolStripLabel_tip_TextChanged);
            // 
            // toolStripLabel_Statistics
            // 
            this.toolStripLabel_Statistics.Name = "toolStripLabel_Statistics";
            this.toolStripLabel_Statistics.Size = new System.Drawing.Size(0, 0);
            this.toolStripLabel_Statistics.TextChanged += new System.EventHandler(this.toolStripLabel_Statistics_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(206)))), ((int)(((byte)(230)))));
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_print,
            this.toolStripButton_asc,
            this.toolStripButton_desc,
            this.toolStripSeparator20,
            this.toolStripButton_query,
            this.toolStripButton_find1,
            this.toolStripButton_cancelFind,
            this.toolStripButton_ReLoadList,
            this.toolStripSeparator21,
            this.toolStripButton_char,
            this.toolStripButton_conditionalStyles,
            this.toolStripButton_CancelHideCol});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 3;
            // 
            // toolStripButton_print
            // 
            this.toolStripButton_print.Enabled = false;
            this.toolStripButton_print.Image = global::Erp.Toolkit.Properties.Resources.print;
            this.toolStripButton_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_print.Name = "toolStripButton_print";
            this.toolStripButton_print.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_print.Text = "打印";
            this.toolStripButton_print.Click += new System.EventHandler(this.toolStripButton_print_Click);
            // 
            // toolStripButton_asc
            // 
            this.toolStripButton_asc.Enabled = false;
            this.toolStripButton_asc.Image = global::Erp.Toolkit.Properties.Resources.toolStripAsc_Image;
            this.toolStripButton_asc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_asc.Name = "toolStripButton_asc";
            this.toolStripButton_asc.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_asc.Text = "升序";
            this.toolStripButton_asc.Click += new System.EventHandler(this.toolStripButton_asc_Click);
            // 
            // toolStripButton_desc
            // 
            this.toolStripButton_desc.Enabled = false;
            this.toolStripButton_desc.Image = global::Erp.Toolkit.Properties.Resources.toolStripDesc_Image;
            this.toolStripButton_desc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_desc.Name = "toolStripButton_desc";
            this.toolStripButton_desc.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_desc.Text = "降序";
            this.toolStripButton_desc.Click += new System.EventHandler(this.toolStripButton_desc_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_query
            // 
            this.toolStripButton_query.Enabled = false;
            this.toolStripButton_query.Image = global::Erp.Toolkit.Properties.Resources.toolStripFind_Image;
            this.toolStripButton_query.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_query.Name = "toolStripButton_query";
            this.toolStripButton_query.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_query.Text = "查找";
            this.toolStripButton_query.Click += new System.EventHandler(this.toolStripButton_query_Click);
            // 
            // toolStripButton_find1
            // 
            this.toolStripButton_find1.Enabled = false;
            this.toolStripButton_find1.Image = global::Erp.Toolkit.Properties.Resources.find;
            this.toolStripButton_find1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_find1.Name = "toolStripButton_find1";
            this.toolStripButton_find1.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_find1.Text = "筛选";
            this.toolStripButton_find1.Click += new System.EventHandler(this.toolStripButton_find1_Click);
            // 
            // toolStripButton_cancelFind
            // 
            this.toolStripButton_cancelFind.Enabled = false;
            this.toolStripButton_cancelFind.Image = global::Erp.Toolkit.Properties.Resources.toolStripUnfilter_Image;
            this.toolStripButton_cancelFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_cancelFind.Name = "toolStripButton_cancelFind";
            this.toolStripButton_cancelFind.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton_cancelFind.Text = "取消筛选";
            this.toolStripButton_cancelFind.Click += new System.EventHandler(this.toolStripButton_cancelFind_Click);
            // 
            // toolStripButton_ReLoadList
            // 
            this.toolStripButton_ReLoadList.Enabled = false;
            this.toolStripButton_ReLoadList.Image = global::Erp.Toolkit.Properties.Resources.refresh;
            this.toolStripButton_ReLoadList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_ReLoadList.Name = "toolStripButton_ReLoadList";
            this.toolStripButton_ReLoadList.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_ReLoadList.Text = "刷新";
            this.toolStripButton_ReLoadList.Click += new System.EventHandler(this.toolStripButton_ReLoadList_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_char
            // 
            this.toolStripButton_char.Enabled = false;
            this.toolStripButton_char.Image = global::Erp.Toolkit.Properties.Resources.chart;
            this.toolStripButton_char.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_char.Name = "toolStripButton_char";
            this.toolStripButton_char.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_char.Text = "统计";
            this.toolStripButton_char.Click += new System.EventHandler(this.toolStripButton_char_Click);
            // 
            // toolStripButton_conditionalStyles
            // 
            this.toolStripButton_conditionalStyles.Enabled = false;
            this.toolStripButton_conditionalStyles.Image = global::Erp.Toolkit.Properties.Resources.toolStripConditionalStyles_Image;
            this.toolStripButton_conditionalStyles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_conditionalStyles.Name = "toolStripButton_conditionalStyles";
            this.toolStripButton_conditionalStyles.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton_conditionalStyles.Text = "条件格式";
            this.toolStripButton_conditionalStyles.Click += new System.EventHandler(this.toolStripButton_conditionalStyles_Click);
            // 
            // toolStripButton_CancelHideCol
            // 
            this.toolStripButton_CancelHideCol.Enabled = false;
            this.toolStripButton_CancelHideCol.Image = global::Erp.Toolkit.Properties.Resources.toolStripPerspective_Image;
            this.toolStripButton_CancelHideCol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_CancelHideCol.Name = "toolStripButton_CancelHideCol";
            this.toolStripButton_CancelHideCol.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton_CancelHideCol.Text = "字段属性";
            this.toolStripButton_CancelHideCol.Click += new System.EventHandler(this.toolStripButton_CancelHideCol_Click);
            // 
            // RowHeaderIconList
            // 
            this.RowHeaderIconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("RowHeaderIconList.ImageStream")));
            this.RowHeaderIconList.TransparentColor = System.Drawing.Color.Transparent;
            this.RowHeaderIconList.Images.SetKeyName(0, "expand.png");
            this.RowHeaderIconList.Images.SetKeyName(1, "collapse.png");
            this.RowHeaderIconList.Images.SetKeyName(2, "collapseIcon.png");
            this.RowHeaderIconList.Images.SetKeyName(3, "expandIcon.png");
            // 
            // RowHeaderContextMenuStrip
            // 
            this.RowHeaderContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RowHeaderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_add,
            this.ToolStripMenuItem_edit,
            this.ToolStripMenuItem_del,
            this.toolStripSeparator7,
            this.ToolStripMenuItem_RowHeader_copy,
            this.ToolStripMenuItem_RowHeader_exp});
            this.RowHeaderContextMenuStrip.Name = "RowHeaderContextMenuStrip";
            this.RowHeaderContextMenuStrip.Size = new System.Drawing.Size(141, 120);
            // 
            // ToolStripMenuItem_add
            // 
            this.ToolStripMenuItem_add.Image = global::Erp.Toolkit.Properties.Resources.bindingNavigatorAddNewItem_Image;
            this.ToolStripMenuItem_add.Name = "ToolStripMenuItem_add";
            this.ToolStripMenuItem_add.Size = new System.Drawing.Size(140, 22);
            this.ToolStripMenuItem_add.Text = "新增";
            this.ToolStripMenuItem_add.Click += new System.EventHandler(this.ToolStripMenuItem_add_Click);
            // 
            // ToolStripMenuItem_edit
            // 
            this.ToolStripMenuItem_edit.Name = "ToolStripMenuItem_edit";
            this.ToolStripMenuItem_edit.ShortcutKeyDisplayString = "F2";
            this.ToolStripMenuItem_edit.Size = new System.Drawing.Size(140, 22);
            this.ToolStripMenuItem_edit.Text = "修改";
            this.ToolStripMenuItem_edit.Click += new System.EventHandler(this.ToolStripMenuItem_edit_Click);
            // 
            // ToolStripMenuItem_del
            // 
            this.ToolStripMenuItem_del.Image = global::Erp.Toolkit.Properties.Resources.bindingNavigatorDeleteItem_Image;
            this.ToolStripMenuItem_del.Name = "ToolStripMenuItem_del";
            this.ToolStripMenuItem_del.Size = new System.Drawing.Size(140, 22);
            this.ToolStripMenuItem_del.Text = "删除";
            this.ToolStripMenuItem_del.Click += new System.EventHandler(this.ToolStripMenuItem_del_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(137, 6);
            // 
            // ToolStripMenuItem_RowHeader_copy
            // 
            this.ToolStripMenuItem_RowHeader_copy.Image = global::Erp.Toolkit.Properties.Resources.CToolStripButton_Image;
            this.ToolStripMenuItem_RowHeader_copy.Name = "ToolStripMenuItem_RowHeader_copy";
            this.ToolStripMenuItem_RowHeader_copy.Size = new System.Drawing.Size(140, 22);
            this.ToolStripMenuItem_RowHeader_copy.Text = "复制";
            this.ToolStripMenuItem_RowHeader_copy.Click += new System.EventHandler(this.ToolStripMenuItem_RowHeader_copy_Click);
            // 
            // ToolStripMenuItem_RowHeader_exp
            // 
            this.ToolStripMenuItem_RowHeader_exp.Image = global::Erp.Toolkit.Properties.Resources.exp;
            this.ToolStripMenuItem_RowHeader_exp.Name = "ToolStripMenuItem_RowHeader_exp";
            this.ToolStripMenuItem_RowHeader_exp.ShortcutKeyDisplayString = "Ctrl+E";
            this.ToolStripMenuItem_RowHeader_exp.Size = new System.Drawing.Size(140, 22);
            this.ToolStripMenuItem_RowHeader_exp.Text = "导出";
            this.ToolStripMenuItem_RowHeader_exp.Click += new System.EventHandler(this.ToolStripMenuItem_RowHeader_exp_Click);
            // 
            // ColHeaderContextMenuStrip
            // 
            this.ColHeaderContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ColHeaderContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ColHeader_asc,
            this.ToolStripMenuItem_ColHeader_desc,
            this.toolStripSeparator16,
            this.ToolStripMenuItem_ColHeader_query,
            this.ToolStripMenuItem_ColHeader_find,
            this.ToolStripMenuItem_ColHeader_CancelFind,
            this.ToolStripMenuItem_ColHeader_previousFind,
            this.toolStripSeparator17,
            this.ToolStripMenuItem_ColHeader_copy,
            this.ToolStripMenuItem_ColHeader_exp,
            this.toolStripSeparator18,
            this.toolStripMenuItem_colWidth,
            this.ToolStripMenuItem_ColHeader_HideCol,
            this.ToolStripMenuItem_ColHeader_CancelHideCol,
            this.ToolStripMenuItem_ColHeader_FreezeCol,
            this.ToolStripMenuItem_ColHeader_CancelFreezeCol,
            this.toolStripSeparator19,
            this.ToolStripMenuItem_ColHeader_addCol,
            this.ToolStripMenuItem_ColHeader_ConditionalStyles,
            this.ToolStripMenuItem_ColHeader_Att});
            this.ColHeaderContextMenuStrip.Name = "ColHeaderContextMenuStrip";
            this.ColHeaderContextMenuStrip.Size = new System.Drawing.Size(194, 380);
            // 
            // ToolStripMenuItem_ColHeader_asc
            // 
            this.ToolStripMenuItem_ColHeader_asc.Image = global::Erp.Toolkit.Properties.Resources.toolStripAsc_Image;
            this.ToolStripMenuItem_ColHeader_asc.Name = "ToolStripMenuItem_ColHeader_asc";
            this.ToolStripMenuItem_ColHeader_asc.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_asc.Text = "升序";
            this.ToolStripMenuItem_ColHeader_asc.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_asc_Click);
            // 
            // ToolStripMenuItem_ColHeader_desc
            // 
            this.ToolStripMenuItem_ColHeader_desc.Image = global::Erp.Toolkit.Properties.Resources.toolStripDesc_Image;
            this.ToolStripMenuItem_ColHeader_desc.Name = "ToolStripMenuItem_ColHeader_desc";
            this.ToolStripMenuItem_ColHeader_desc.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_desc.Text = "降序";
            this.ToolStripMenuItem_ColHeader_desc.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_desc_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(190, 6);
            // 
            // ToolStripMenuItem_ColHeader_query
            // 
            this.ToolStripMenuItem_ColHeader_query.Image = global::Erp.Toolkit.Properties.Resources.toolStripFind_Image;
            this.ToolStripMenuItem_ColHeader_query.Name = "ToolStripMenuItem_ColHeader_query";
            this.ToolStripMenuItem_ColHeader_query.ShortcutKeyDisplayString = "";
            this.ToolStripMenuItem_ColHeader_query.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_query.Text = "查找";
            this.ToolStripMenuItem_ColHeader_query.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_query_Click);
            // 
            // ToolStripMenuItem_ColHeader_find
            // 
            this.ToolStripMenuItem_ColHeader_find.Image = global::Erp.Toolkit.Properties.Resources.find;
            this.ToolStripMenuItem_ColHeader_find.Name = "ToolStripMenuItem_ColHeader_find";
            this.ToolStripMenuItem_ColHeader_find.ShortcutKeyDisplayString = "";
            this.ToolStripMenuItem_ColHeader_find.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_find.Text = "筛选";
            this.ToolStripMenuItem_ColHeader_find.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_find_Click);
            // 
            // ToolStripMenuItem_ColHeader_CancelFind
            // 
            this.ToolStripMenuItem_ColHeader_CancelFind.Enabled = false;
            this.ToolStripMenuItem_ColHeader_CancelFind.Image = global::Erp.Toolkit.Properties.Resources.toolStripUnfilter_Image;
            this.ToolStripMenuItem_ColHeader_CancelFind.Name = "ToolStripMenuItem_ColHeader_CancelFind";
            this.ToolStripMenuItem_ColHeader_CancelFind.ShortcutKeyDisplayString = "";
            this.ToolStripMenuItem_ColHeader_CancelFind.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_CancelFind.Text = "取消所有筛选";
            this.ToolStripMenuItem_ColHeader_CancelFind.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_CancelFind_Click);
            // 
            // ToolStripMenuItem_ColHeader_previousFind
            // 
            this.ToolStripMenuItem_ColHeader_previousFind.Enabled = false;
            this.ToolStripMenuItem_ColHeader_previousFind.Name = "ToolStripMenuItem_ColHeader_previousFind";
            this.ToolStripMenuItem_ColHeader_previousFind.ShortcutKeyDisplayString = "Ctrl+Z";
            this.ToolStripMenuItem_ColHeader_previousFind.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_previousFind.Text = "返回上次筛选";
            this.ToolStripMenuItem_ColHeader_previousFind.Click += new System.EventHandler(this.ToolStripMenuItem_previousFind_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(190, 6);
            // 
            // ToolStripMenuItem_ColHeader_copy
            // 
            this.ToolStripMenuItem_ColHeader_copy.Image = global::Erp.Toolkit.Properties.Resources.CToolStripButton_Image;
            this.ToolStripMenuItem_ColHeader_copy.Name = "ToolStripMenuItem_ColHeader_copy";
            this.ToolStripMenuItem_ColHeader_copy.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_copy.Text = "复制";
            this.ToolStripMenuItem_ColHeader_copy.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_copy_Click);
            // 
            // ToolStripMenuItem_ColHeader_exp
            // 
            this.ToolStripMenuItem_ColHeader_exp.Image = global::Erp.Toolkit.Properties.Resources.exp;
            this.ToolStripMenuItem_ColHeader_exp.Name = "ToolStripMenuItem_ColHeader_exp";
            this.ToolStripMenuItem_ColHeader_exp.ShortcutKeyDisplayString = "Ctrl+E";
            this.ToolStripMenuItem_ColHeader_exp.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_exp.Text = "导出";
            this.ToolStripMenuItem_ColHeader_exp.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_exp_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(190, 6);
            // 
            // toolStripMenuItem_colWidth
            // 
            this.toolStripMenuItem_colWidth.Image = global::Erp.Toolkit.Properties.Resources.toolStripWidth_Image;
            this.toolStripMenuItem_colWidth.Name = "toolStripMenuItem_colWidth";
            this.toolStripMenuItem_colWidth.Size = new System.Drawing.Size(193, 22);
            this.toolStripMenuItem_colWidth.Text = "字段宽度";
            this.toolStripMenuItem_colWidth.Click += new System.EventHandler(this.toolStripMenuItem_colWidth_Click);
            // 
            // ToolStripMenuItem_ColHeader_HideCol
            // 
            this.ToolStripMenuItem_ColHeader_HideCol.Name = "ToolStripMenuItem_ColHeader_HideCol";
            this.ToolStripMenuItem_ColHeader_HideCol.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_HideCol.Text = "隐藏字段";
            this.ToolStripMenuItem_ColHeader_HideCol.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_HideCol_Click);
            // 
            // ToolStripMenuItem_ColHeader_CancelHideCol
            // 
            this.ToolStripMenuItem_ColHeader_CancelHideCol.Image = global::Erp.Toolkit.Properties.Resources.toolStripPerspective_Image;
            this.ToolStripMenuItem_ColHeader_CancelHideCol.Name = "ToolStripMenuItem_ColHeader_CancelHideCol";
            this.ToolStripMenuItem_ColHeader_CancelHideCol.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_CancelHideCol.Text = "取消隐藏字段";
            this.ToolStripMenuItem_ColHeader_CancelHideCol.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_CancelHideCol_Click);
            // 
            // ToolStripMenuItem_ColHeader_FreezeCol
            // 
            this.ToolStripMenuItem_ColHeader_FreezeCol.Image = global::Erp.Toolkit.Properties.Resources.toolStripFreeze_Image;
            this.ToolStripMenuItem_ColHeader_FreezeCol.Name = "ToolStripMenuItem_ColHeader_FreezeCol";
            this.ToolStripMenuItem_ColHeader_FreezeCol.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_FreezeCol.Text = "冻结字段";
            this.ToolStripMenuItem_ColHeader_FreezeCol.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_FreezeCol_Click);
            // 
            // ToolStripMenuItem_ColHeader_CancelFreezeCol
            // 
            this.ToolStripMenuItem_ColHeader_CancelFreezeCol.Name = "ToolStripMenuItem_ColHeader_CancelFreezeCol";
            this.ToolStripMenuItem_ColHeader_CancelFreezeCol.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_CancelFreezeCol.Text = "取消冻结所有字段";
            this.ToolStripMenuItem_ColHeader_CancelFreezeCol.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_CancelFreezeCol_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(190, 6);
            // 
            // ToolStripMenuItem_ColHeader_addCol
            // 
            this.ToolStripMenuItem_ColHeader_addCol.Enabled = false;
            this.ToolStripMenuItem_ColHeader_addCol.Name = "ToolStripMenuItem_ColHeader_addCol";
            this.ToolStripMenuItem_ColHeader_addCol.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_addCol.Text = "添加自定义列";
            this.ToolStripMenuItem_ColHeader_addCol.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_addCol_Click);
            // 
            // ToolStripMenuItem_ColHeader_ConditionalStyles
            // 
            this.ToolStripMenuItem_ColHeader_ConditionalStyles.Image = global::Erp.Toolkit.Properties.Resources.toolStripConditionalStyles_Image;
            this.ToolStripMenuItem_ColHeader_ConditionalStyles.Name = "ToolStripMenuItem_ColHeader_ConditionalStyles";
            this.ToolStripMenuItem_ColHeader_ConditionalStyles.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_ConditionalStyles.Text = "条件格式";
            this.ToolStripMenuItem_ColHeader_ConditionalStyles.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_ConditionalStyles_Click);
            // 
            // ToolStripMenuItem_ColHeader_Att
            // 
            this.ToolStripMenuItem_ColHeader_Att.Image = global::Erp.Toolkit.Properties.Resources.toolStripAtt_Image;
            this.ToolStripMenuItem_ColHeader_Att.Name = "ToolStripMenuItem_ColHeader_Att";
            this.ToolStripMenuItem_ColHeader_Att.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItem_ColHeader_Att.Text = "属性";
            this.ToolStripMenuItem_ColHeader_Att.Click += new System.EventHandler(this.ToolStripMenuItem_ColHeader_Att_Click);
            // 
            // VToolStripMenuItem
            // 
            this.VToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VToolStripMenuItem.Name = "VToolStripMenuItem";
            this.VToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.VToolStripMenuItem.Text = "打印预览(&V)";
            // 
            // Dgv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(206)))), ((int)(((byte)(230)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.bindingNavigator);
            this.Name = "Dgv";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.DgvContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).EndInit();
            this.bindingNavigator.ResumeLayout(false);
            this.bindingNavigator.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.RowHeaderContextMenuStrip.ResumeLayout(false);
            this.ColHeaderContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_asc;
        private System.Windows.Forms.ToolStripButton toolStripButton_desc;
        private System.Windows.Forms.ToolStripButton toolStripButton_query;
        public Erp.Toolkit.Controls.OptimizedDataGridView dataGridView;
        public System.Windows.Forms.BindingNavigator bindingNavigator;
        internal System.Windows.Forms.ImageList RowHeaderIconList;
        public System.Windows.Forms.ToolStrip toolStrip1;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_copy;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_asc;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_desc;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_findText;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemText_eq;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemText_NotEq;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemText_LikeBf;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemText_NotLikeBf;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemText_Like;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemText_NotLike;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemText_EndLike;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemText_EndNotLike;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_findNubmer;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNumber_eq;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNumber_NotEq;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNumber_less;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNumber_greater;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNumber_interval;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_findDate;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_eq;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_NotEq;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_less;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_greater;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_interval;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator10;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_tomorrow;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_today;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_yesterday;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator11;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_nextWeek;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_thisWeek;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_lastWeek;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator12;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_nextMonth;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_thisMonth;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_lastMonth;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator13;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_nextQuarter;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_thisQuarter;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_lastQuarter;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator14;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_nextYear;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_thisYear;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_soFarThisYear;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDate_lastYear;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator9;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_equal;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_NotEqual;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Like;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_NotLike;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_refresh;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_CancelFind;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_previousFind;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator15;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_print;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_exp;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator8;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_sysTheme;
        internal System.Windows.Forms.ToolStripComboBox ToolStripComboBox_Theme;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_query;
        internal System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_find;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_findColumnName;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_FindText;
        private System.Windows.Forms.ToolStripButton toolStripButton_find;
        private System.Windows.Forms.ContextMenuStrip RowHeaderContextMenuStrip;
        private System.Windows.Forms.ContextMenuStrip ColHeaderContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_add;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_edit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_del;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RowHeader_copy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_asc;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_desc;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_find;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_query;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_copy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_exp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RowHeader_exp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_HideCol;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_CancelHideCol;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_CancelFind;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_FreezeCol;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_CancelFreezeCol;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_addCol;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_ConditionalStyles;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_Att;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripButton toolStripButton_find1;
        private System.Windows.Forms.ToolStripButton toolStripButton_cancelFind;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripButton toolStripButton_char;
        private System.Windows.Forms.ToolStripButton toolStripButton_conditionalStyles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_tip;
        private System.Windows.Forms.ToolStripButton toolStripButton_CancelHideCol;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_colWidth;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ColHeader_previousFind;
        private System.Windows.Forms.ToolStripButton toolStripButton_Next;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripButton toolStripButtonReLoadList;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_Statistics;
        private System.Windows.Forms.ToolStripButton toolStripButton_ReLoadList;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_printPreview;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_printPageSetup;
        private System.Windows.Forms.ToolStripMenuItem VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_printSelectPrinter;
        private System.Windows.Forms.ToolStripButton toolStripButton_print;
        public System.Windows.Forms.ContextMenuStrip DgvContextMenuStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton_cancelFind2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripComboBox ToolStripComboBox_Language;
    }
}
