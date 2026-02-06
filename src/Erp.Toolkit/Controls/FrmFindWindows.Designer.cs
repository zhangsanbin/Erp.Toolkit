namespace Erp.Toolkit.Controls
{
    partial class FrmFindWindows
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFindWindows));
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.CheckBox3 = new System.Windows.Forms.CheckBox();
            this.CheckBox2 = new System.Windows.Forms.CheckBox();
            this.CheckBox1 = new System.Windows.Forms.CheckBox();
            this.ButtonClose1 = new System.Windows.Forms.Button();
            this.ButtonFind1 = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.ComboBox3 = new System.Windows.Forms.ComboBox();
            this.ComboBox2 = new System.Windows.Forms.ComboBox();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtcznr = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.txtsxnr = new System.Windows.Forms.TextBox();
            this.txtsxnr2 = new System.Windows.Forms.TextBox();
            this.CheckBox4 = new System.Windows.Forms.CheckBox();
            this.CheckBox_Find_Or = new System.Windows.Forms.CheckBox();
            this.CheckBox6 = new System.Windows.Forms.CheckBox();
            this.ButtonClose2 = new System.Windows.Forms.Button();
            this.ButtonFind2 = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.ComboBox4 = new System.Windows.Forms.ComboBox();
            this.ComboBox5 = new System.Windows.Forms.ComboBox();
            this.ComboBox6 = new System.Windows.Forms.ComboBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ButtonAddCFG = new System.Windows.Forms.Button();
            this.ButtonSaveFiltersConfig = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.FiltersContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_add = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_del = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_moveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_moveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl1.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.TabPage3.SuspendLayout();
            this.FiltersContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl1
            // 
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TabPage1);
            this.TabControl1.Controls.Add(this.TabPage2);
            this.TabControl1.Controls.Add(this.TabPage3);
            this.TabControl1.Location = new System.Drawing.Point(12, 12);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.Size = new System.Drawing.Size(524, 187);
            this.TabControl1.TabIndex = 0;
            this.TabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.White;
            this.TabPage1.Controls.Add(this.ComboBox1);
            this.TabPage1.Controls.Add(this.ComboBox2);
            this.TabPage1.Controls.Add(this.ComboBox3);
            this.TabPage1.Controls.Add(this.CheckBox3);
            this.TabPage1.Controls.Add(this.CheckBox2);
            this.TabPage1.Controls.Add(this.CheckBox1);
            this.TabPage1.Controls.Add(this.ButtonClose1);
            this.TabPage1.Controls.Add(this.ButtonFind1);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.txtcznr);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(516, 161);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "查找";
            // 
            // CheckBox3
            // 
            this.CheckBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CheckBox3.AutoSize = true;
            this.CheckBox3.Checked = true;
            this.CheckBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox3.Location = new System.Drawing.Point(320, 132);
            this.CheckBox3.Name = "CheckBox3";
            this.CheckBox3.Size = new System.Drawing.Size(108, 16);
            this.CheckBox3.TabIndex = 6;
            this.CheckBox3.Text = "首字母模糊搜索";
            this.CheckBox3.UseVisualStyleBackColor = true;
            // 
            // CheckBox2
            // 
            this.CheckBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CheckBox2.AutoSize = true;
            this.CheckBox2.Checked = true;
            this.CheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox2.Location = new System.Drawing.Point(192, 132);
            this.CheckBox2.Name = "CheckBox2";
            this.CheckBox2.Size = new System.Drawing.Size(108, 16);
            this.CheckBox2.TabIndex = 5;
            this.CheckBox2.Text = "按格式搜索字段";
            this.CheckBox2.UseVisualStyleBackColor = true;
            // 
            // CheckBox1
            // 
            this.CheckBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CheckBox1.AutoSize = true;
            this.CheckBox1.Location = new System.Drawing.Point(90, 132);
            this.CheckBox1.Name = "CheckBox1";
            this.CheckBox1.Size = new System.Drawing.Size(84, 16);
            this.CheckBox1.TabIndex = 4;
            this.CheckBox1.Text = "区分大小写";
            this.CheckBox1.UseVisualStyleBackColor = true;
            // 
            // ButtonClose1
            // 
            this.ButtonClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose1.Location = new System.Drawing.Point(382, 43);
            this.ButtonClose1.Name = "ButtonClose1";
            this.ButtonClose1.Size = new System.Drawing.Size(122, 23);
            this.ButtonClose1.TabIndex = 8;
            this.ButtonClose1.Text = "取消(&C)";
            this.ButtonClose1.UseVisualStyleBackColor = true;
            // 
            // ButtonFind1
            // 
            this.ButtonFind1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFind1.Location = new System.Drawing.Point(382, 13);
            this.ButtonFind1.Name = "ButtonFind1";
            this.ButtonFind1.Size = new System.Drawing.Size(122, 23);
            this.ButtonFind1.TabIndex = 7;
            this.ButtonFind1.Text = "搜索下一个(&F)";
            this.ButtonFind1.UseVisualStyleBackColor = true;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(13, 103);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(65, 12);
            this.Label4.TabIndex = 6;
            this.Label4.Text = "搜索方向：";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(13, 75);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(65, 12);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "匹配模式：";
            // 
            // ComboBox3
            // 
            this.ComboBox3.FormattingEnabled = true;
            this.ComboBox3.Items.AddRange(new object[] {
            "全部",
            "向上",
            "向下"});
            this.ComboBox3.Location = new System.Drawing.Point(90, 100);
            this.ComboBox3.Name = "ComboBox3";
            this.ComboBox3.Size = new System.Drawing.Size(140, 20);
            this.ComboBox3.TabIndex = 3;
            this.ComboBox3.Text = "全部";
            // 
            // ComboBox2
            // 
            this.ComboBox2.FormattingEnabled = true;
            this.ComboBox2.Items.AddRange(new object[] {
            "字段任何部分",
            "整个字段",
            "字段开头",
            "字段结尾",
            "不等于",
            "不包含",
            "开头不是",
            "结尾不是",
            "小于",
            "大于",
            "期间"});
            this.ComboBox2.Location = new System.Drawing.Point(90, 72);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(140, 20);
            this.ComboBox2.TabIndex = 2;
            this.ComboBox2.Text = "字段任何部分";
            this.ComboBox2.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            // 
            // ComboBox1
            // 
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Items.AddRange(new object[] {
            "当前字段",
            "全局搜索"});
            this.ComboBox1.Location = new System.Drawing.Point(90, 44);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(140, 20);
            this.ComboBox1.TabIndex = 1;
            this.ComboBox1.Text = "当前字段";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(13, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(65, 12);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "查找范围：";
            // 
            // txtcznr
            // 
            this.txtcznr.Location = new System.Drawing.Point(90, 15);
            this.txtcznr.Name = "txtcznr";
            this.txtcznr.Size = new System.Drawing.Size(282, 21);
            this.txtcznr.TabIndex = 0;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(65, 12);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "查找内容：";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.White;
            this.TabPage2.Controls.Add(this.ComboBox6);
            this.TabPage2.Controls.Add(this.ComboBox5);
            this.TabPage2.Controls.Add(this.ComboBox4);
            this.TabPage2.Controls.Add(this.txtsxnr);
            this.TabPage2.Controls.Add(this.txtsxnr2);
            this.TabPage2.Controls.Add(this.CheckBox4);
            this.TabPage2.Controls.Add(this.CheckBox_Find_Or);
            this.TabPage2.Controls.Add(this.CheckBox6);
            this.TabPage2.Controls.Add(this.ButtonClose2);
            this.TabPage2.Controls.Add(this.ButtonFind2);
            this.TabPage2.Controls.Add(this.Label5);
            this.TabPage2.Controls.Add(this.Label6);
            this.TabPage2.Controls.Add(this.Label7);
            this.TabPage2.Controls.Add(this.Label8);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(516, 161);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "筛选";
            // 
            // txtsxnr
            // 
            this.txtsxnr.Location = new System.Drawing.Point(90, 15);
            this.txtsxnr.Name = "txtsxnr";
            this.txtsxnr.Size = new System.Drawing.Size(282, 21);
            this.txtsxnr.TabIndex = 0;
            // 
            // txtsxnr2
            // 
            this.txtsxnr2.Location = new System.Drawing.Point(232, 15);
            this.txtsxnr2.Name = "txtsxnr2";
            this.txtsxnr2.Size = new System.Drawing.Size(140, 21);
            this.txtsxnr2.TabIndex = 1;
            this.txtsxnr2.Visible = false;
            // 
            // CheckBox4
            // 
            this.CheckBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CheckBox4.AutoSize = true;
            this.CheckBox4.Checked = true;
            this.CheckBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox4.Location = new System.Drawing.Point(320, 132);
            this.CheckBox4.Name = "CheckBox4";
            this.CheckBox4.Size = new System.Drawing.Size(108, 16);
            this.CheckBox4.TabIndex = 7;
            this.CheckBox4.Text = "首字母模糊搜索";
            this.CheckBox4.UseVisualStyleBackColor = true;
            // 
            // CheckBox_Find_Or
            // 
            this.CheckBox_Find_Or.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CheckBox_Find_Or.AutoSize = true;
            this.CheckBox_Find_Or.Location = new System.Drawing.Point(192, 132);
            this.CheckBox_Find_Or.Name = "CheckBox_Find_Or";
            this.CheckBox_Find_Or.Size = new System.Drawing.Size(96, 16);
            this.CheckBox_Find_Or.TabIndex = 6;
            this.CheckBox_Find_Or.Text = "满足任意条件";
            this.CheckBox_Find_Or.UseVisualStyleBackColor = true;
            // 
            // CheckBox6
            // 
            this.CheckBox6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CheckBox6.AutoSize = true;
            this.CheckBox6.Checked = true;
            this.CheckBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox6.Location = new System.Drawing.Point(90, 132);
            this.CheckBox6.Name = "CheckBox6";
            this.CheckBox6.Size = new System.Drawing.Size(84, 16);
            this.CheckBox6.TabIndex = 5;
            this.CheckBox6.Text = "区分大小写";
            this.CheckBox6.UseVisualStyleBackColor = true;
            // 
            // ButtonClose2
            // 
            this.ButtonClose2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose2.Location = new System.Drawing.Point(382, 43);
            this.ButtonClose2.Name = "ButtonClose2";
            this.ButtonClose2.Size = new System.Drawing.Size(122, 23);
            this.ButtonClose2.TabIndex = 9;
            this.ButtonClose2.Text = "取消(&C)";
            this.ButtonClose2.UseVisualStyleBackColor = true;
            // 
            // ButtonFind2
            // 
            this.ButtonFind2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFind2.Location = new System.Drawing.Point(382, 13);
            this.ButtonFind2.Name = "ButtonFind2";
            this.ButtonFind2.Size = new System.Drawing.Size(122, 23);
            this.ButtonFind2.TabIndex = 8;
            this.ButtonFind2.Text = "筛选(&F)";
            this.ButtonFind2.UseVisualStyleBackColor = true;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(13, 103);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(65, 12);
            this.Label5.TabIndex = 19;
            this.Label5.Text = "筛选方向：";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(13, 75);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(65, 12);
            this.Label6.TabIndex = 17;
            this.Label6.Text = "匹配模式：";
            // 
            // ComboBox4
            // 
            this.ComboBox4.FormattingEnabled = true;
            this.ComboBox4.Items.AddRange(new object[] {
            "全部",
            "向上",
            "向下"});
            this.ComboBox4.Location = new System.Drawing.Point(90, 100);
            this.ComboBox4.Name = "ComboBox4";
            this.ComboBox4.Size = new System.Drawing.Size(140, 20);
            this.ComboBox4.TabIndex = 4;
            this.ComboBox4.Text = "全部";
            // 
            // ComboBox5
            // 
            this.ComboBox5.FormattingEnabled = true;
            this.ComboBox5.Items.AddRange(new object[] {
            "字段任何部分",
            "整个字段",
            "字段开头",
            "字段结尾",
            "不等于",
            "不包含",
            "开头不是",
            "结尾不是",
            "小于",
            "大于",
            "期间"});
            this.ComboBox5.Location = new System.Drawing.Point(90, 72);
            this.ComboBox5.Name = "ComboBox5";
            this.ComboBox5.Size = new System.Drawing.Size(140, 20);
            this.ComboBox5.TabIndex = 3;
            this.ComboBox5.Text = "字段任何部分";
            this.ComboBox5.SelectedIndexChanged += new System.EventHandler(this.ComboBox5_SelectedIndexChanged);
            // 
            // ComboBox6
            // 
            this.ComboBox6.FormattingEnabled = true;
            this.ComboBox6.Items.AddRange(new object[] {
            "当前字段",
            "全局搜索"});
            this.ComboBox6.Location = new System.Drawing.Point(90, 44);
            this.ComboBox6.Name = "ComboBox6";
            this.ComboBox6.Size = new System.Drawing.Size(140, 20);
            this.ComboBox6.TabIndex = 2;
            this.ComboBox6.Text = "当前字段";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(13, 48);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(65, 12);
            this.Label7.TabIndex = 15;
            this.Label7.Text = "筛选范围：";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(13, 20);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(65, 12);
            this.Label8.TabIndex = 13;
            this.Label8.Text = "筛选内容：";
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.White;
            this.TabPage3.Controls.Add(this.textBox1);
            this.TabPage3.Controls.Add(this.label9);
            this.TabPage3.Controls.Add(this.ButtonAddCFG);
            this.TabPage3.Controls.Add(this.ButtonSaveFiltersConfig);
            this.TabPage3.Controls.Add(this.listView1);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage3.Size = new System.Drawing.Size(516, 161);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "管理器";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(429, 134);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(75, 21);
            this.textBox1.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(382, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "菜单名:";
            // 
            // ButtonAddCFG
            // 
            this.ButtonAddCFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonAddCFG.Location = new System.Drawing.Point(382, 13);
            this.ButtonAddCFG.Name = "ButtonAddCFG";
            this.ButtonAddCFG.Size = new System.Drawing.Size(122, 23);
            this.ButtonAddCFG.TabIndex = 1;
            this.ButtonAddCFG.Text = "新增(&A)";
            this.ButtonAddCFG.UseVisualStyleBackColor = true;
            // 
            // ButtonSaveFiltersConfig
            // 
            this.ButtonSaveFiltersConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSaveFiltersConfig.Location = new System.Drawing.Point(382, 43);
            this.ButtonSaveFiltersConfig.Name = "ButtonSaveFiltersConfig";
            this.ButtonSaveFiltersConfig.Size = new System.Drawing.Size(122, 23);
            this.ButtonSaveFiltersConfig.TabIndex = 2;
            this.ButtonSaveFiltersConfig.Text = "应用(&A)";
            this.ButtonSaveFiltersConfig.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.ContextMenuStrip = this.FiltersContextMenuStrip;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(6, 6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(370, 149);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // FiltersContextMenuStrip
            // 
            this.FiltersContextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FiltersContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_add,
            this.ToolStripMenuItem_edit,
            this.ToolStripMenuItem_del,
            this.toolStripSeparator7,
            this.toolStripMenuItem_moveUp,
            this.toolStripMenuItem_moveDown});
            this.FiltersContextMenuStrip.Name = "RowHeaderContextMenuStrip";
            this.FiltersContextMenuStrip.Size = new System.Drawing.Size(120, 120);
            // 
            // ToolStripMenuItem_add
            // 
            this.ToolStripMenuItem_add.Enabled = false;
            this.ToolStripMenuItem_add.Image = global::Erp.Toolkit.Properties.Resources.bindingNavigatorAddNewItem_Image;
            this.ToolStripMenuItem_add.Name = "ToolStripMenuItem_add";
            this.ToolStripMenuItem_add.Size = new System.Drawing.Size(119, 22);
            this.ToolStripMenuItem_add.Text = "新增";
            // 
            // ToolStripMenuItem_edit
            // 
            this.ToolStripMenuItem_edit.Enabled = false;
            this.ToolStripMenuItem_edit.Name = "ToolStripMenuItem_edit";
            this.ToolStripMenuItem_edit.ShortcutKeyDisplayString = "F2";
            this.ToolStripMenuItem_edit.Size = new System.Drawing.Size(119, 22);
            this.ToolStripMenuItem_edit.Text = "修改";
            // 
            // ToolStripMenuItem_del
            // 
            this.ToolStripMenuItem_del.Image = global::Erp.Toolkit.Properties.Resources.bindingNavigatorDeleteItem_Image;
            this.ToolStripMenuItem_del.Name = "ToolStripMenuItem_del";
            this.ToolStripMenuItem_del.Size = new System.Drawing.Size(119, 22);
            this.ToolStripMenuItem_del.Text = "删除";
            this.ToolStripMenuItem_del.Click += new System.EventHandler(this.ToolStripMenuItem_del_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(116, 6);
            // 
            // toolStripMenuItem_moveUp
            // 
            this.toolStripMenuItem_moveUp.Name = "toolStripMenuItem_moveUp";
            this.toolStripMenuItem_moveUp.Size = new System.Drawing.Size(119, 22);
            this.toolStripMenuItem_moveUp.Text = "上移";
            this.toolStripMenuItem_moveUp.Click += new System.EventHandler(this.toolStripMenuItem_moveUp_Click);
            // 
            // toolStripMenuItem_moveDown
            // 
            this.toolStripMenuItem_moveDown.Name = "toolStripMenuItem_moveDown";
            this.toolStripMenuItem_moveDown.Size = new System.Drawing.Size(119, 22);
            this.toolStripMenuItem_moveDown.Text = "下移";
            this.toolStripMenuItem_moveDown.Click += new System.EventHandler(this.toolStripMenuItem_moveDown_Click);
            // 
            // FrmFindWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.ButtonClose1;
            this.ClientSize = new System.Drawing.Size(549, 211);
            this.Controls.Add(this.TabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFindWindows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找与筛选";
            this.Load += new System.EventHandler(this.FindWindows_Load);
            this.Shown += new System.EventHandler(this.FindWindows_Shown);
            this.TabControl1.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.FiltersContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabControl TabControl1;
        internal System.Windows.Forms.TabPage TabPage1;
        internal System.Windows.Forms.CheckBox CheckBox3;
        internal System.Windows.Forms.CheckBox CheckBox2;
        internal System.Windows.Forms.CheckBox CheckBox1;
        internal System.Windows.Forms.Button ButtonClose1;
        internal System.Windows.Forms.Button ButtonFind1;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.ComboBox ComboBox3;
        internal System.Windows.Forms.ComboBox ComboBox2;
        internal System.Windows.Forms.ComboBox ComboBox1;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtcznr;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TabPage TabPage2;
        internal System.Windows.Forms.CheckBox CheckBox4;
        internal System.Windows.Forms.CheckBox CheckBox_Find_Or;
        internal System.Windows.Forms.CheckBox CheckBox6;
        internal System.Windows.Forms.Button ButtonClose2;
        internal System.Windows.Forms.Button ButtonFind2;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.ComboBox ComboBox4;
        internal System.Windows.Forms.ComboBox ComboBox5;
        internal System.Windows.Forms.ComboBox ComboBox6;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox txtsxnr;
        internal System.Windows.Forms.Label Label8;
        private System.Windows.Forms.TextBox txtsxnr2;
        private System.Windows.Forms.TabPage TabPage3;
        private System.Windows.Forms.ListView listView1;
        internal System.Windows.Forms.Button ButtonAddCFG;
        internal System.Windows.Forms.Button ButtonSaveFiltersConfig;
        private System.Windows.Forms.ContextMenuStrip FiltersContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_add;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_edit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_del;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_moveUp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_moveDown;
    }
}