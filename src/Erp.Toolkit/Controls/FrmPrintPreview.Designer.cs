namespace Erp.Toolkit.Controls
{
    partial class FrmPrintPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintPreview));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_SelectPrinter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_PageSetup = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Print = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_VerticalPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_HorizontalPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDown_displayScale = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem_displayScale10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_displayScale25 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_displayScale50 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_displayScale75 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_displayScale100 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_displayScaleAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_displayScale150 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_displayScale200 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_OnePage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_TwoPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_FourPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_SixPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_PreviousPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_NextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox_PageNumber = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton_Close = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_PageNumber = new System.Windows.Forms.ToolStripLabel();
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(206)))), ((int)(((byte)(230)))));
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_SelectPrinter,
            this.toolStripButton_PageSetup,
            this.toolStripButton_Print,
            this.toolStripSeparator1,
            this.toolStripButton_VerticalPage,
            this.toolStripButton_HorizontalPage,
            this.toolStripDropDown_displayScale,
            this.toolStripSeparator4,
            this.toolStripButton_OnePage,
            this.toolStripButton_TwoPage,
            this.toolStripButton_FourPage,
            this.toolStripButton_SixPage,
            this.toolStripSeparator2,
            this.toolStripButton_PreviousPage,
            this.toolStripButton_NextPage,
            this.toolStripSeparator3,
            this.toolStripTextBox_PageNumber,
            this.toolStripButton_Close,
            this.toolStripLabel_PageNumber});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1264, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButton_SelectPrinter
            // 
            this.toolStripButton_SelectPrinter.Image = global::Erp.Toolkit.Properties.Resources.print_32;
            this.toolStripButton_SelectPrinter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_SelectPrinter.Name = "toolStripButton_SelectPrinter";
            this.toolStripButton_SelectPrinter.Size = new System.Drawing.Size(92, 22);
            this.toolStripButton_SelectPrinter.Text = "切换打印机";
            this.toolStripButton_SelectPrinter.Click += new System.EventHandler(this.toolStripButton_SelectPrinter_Click);
            // 
            // toolStripButton_PageSetup
            // 
            this.toolStripButton_PageSetup.Image = global::Erp.Toolkit.Properties.Resources.printPreview;
            this.toolStripButton_PageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_PageSetup.Name = "toolStripButton_PageSetup";
            this.toolStripButton_PageSetup.Size = new System.Drawing.Size(79, 22);
            this.toolStripButton_PageSetup.Text = "页面设置";
            this.toolStripButton_PageSetup.Click += new System.EventHandler(this.toolStripButton_PageSetup_Click);
            // 
            // toolStripButton_Print
            // 
            this.toolStripButton_Print.Image = global::Erp.Toolkit.Properties.Resources.print;
            this.toolStripButton_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Print.Name = "toolStripButton_Print";
            this.toolStripButton_Print.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_Print.Text = "打印";
            this.toolStripButton_Print.Click += new System.EventHandler(this.toolStripButton_Print_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_VerticalPage
            // 
            this.toolStripButton_VerticalPage.Image = global::Erp.Toolkit.Properties.Resources.Vertical;
            this.toolStripButton_VerticalPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_VerticalPage.Name = "toolStripButton_VerticalPage";
            this.toolStripButton_VerticalPage.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_VerticalPage.Text = "纵向";
            this.toolStripButton_VerticalPage.Click += new System.EventHandler(this.toolStripButton_VerticalPage_Click);
            // 
            // toolStripButton_HorizontalPage
            // 
            this.toolStripButton_HorizontalPage.Image = global::Erp.Toolkit.Properties.Resources.Horizontal;
            this.toolStripButton_HorizontalPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_HorizontalPage.Name = "toolStripButton_HorizontalPage";
            this.toolStripButton_HorizontalPage.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_HorizontalPage.Text = "横向";
            this.toolStripButton_HorizontalPage.Click += new System.EventHandler(this.toolStripButton_HorizontalPage_Click);
            // 
            // toolStripDropDown_displayScale
            // 
            this.toolStripDropDown_displayScale.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_displayScale10,
            this.toolStripMenuItem_displayScale25,
            this.toolStripMenuItem_displayScale50,
            this.toolStripMenuItem_displayScale75,
            this.toolStripMenuItem_displayScale100,
            this.toolStripMenuItem_displayScaleAuto,
            this.toolStripMenuItem_displayScale150,
            this.toolStripMenuItem_displayScale200});
            this.toolStripDropDown_displayScale.Image = global::Erp.Toolkit.Properties.Resources.preview_32;
            this.toolStripDropDown_displayScale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDown_displayScale.Name = "toolStripDropDown_displayScale";
            this.toolStripDropDown_displayScale.Size = new System.Drawing.Size(88, 22);
            this.toolStripDropDown_displayScale.Text = "显示比例";
            // 
            // toolStripMenuItem_displayScale10
            // 
            this.toolStripMenuItem_displayScale10.Name = "toolStripMenuItem_displayScale10";
            this.toolStripMenuItem_displayScale10.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_displayScale10.Text = "10%";
            this.toolStripMenuItem_displayScale10.Click += new System.EventHandler(this.toolStripMenuItem_displayScale10_Click);
            // 
            // toolStripMenuItem_displayScale25
            // 
            this.toolStripMenuItem_displayScale25.Name = "toolStripMenuItem_displayScale25";
            this.toolStripMenuItem_displayScale25.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_displayScale25.Text = "25%";
            this.toolStripMenuItem_displayScale25.Click += new System.EventHandler(this.toolStripMenuItem_displayScale25_Click);
            // 
            // toolStripMenuItem_displayScale50
            // 
            this.toolStripMenuItem_displayScale50.Name = "toolStripMenuItem_displayScale50";
            this.toolStripMenuItem_displayScale50.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_displayScale50.Text = "50%";
            this.toolStripMenuItem_displayScale50.Click += new System.EventHandler(this.toolStripMenuItem_displayScale50_Click);
            // 
            // toolStripMenuItem_displayScale75
            // 
            this.toolStripMenuItem_displayScale75.Name = "toolStripMenuItem_displayScale75";
            this.toolStripMenuItem_displayScale75.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_displayScale75.Text = "75%";
            this.toolStripMenuItem_displayScale75.Click += new System.EventHandler(this.toolStripMenuItem_displayScale75_Click);
            // 
            // toolStripMenuItem_displayScale100
            // 
            this.toolStripMenuItem_displayScale100.Name = "toolStripMenuItem_displayScale100";
            this.toolStripMenuItem_displayScale100.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_displayScale100.Text = "100%";
            this.toolStripMenuItem_displayScale100.Click += new System.EventHandler(this.toolStripMenuItem_displayScale100_Click);
            // 
            // toolStripMenuItem_displayScaleAuto
            // 
            this.toolStripMenuItem_displayScaleAuto.Checked = true;
            this.toolStripMenuItem_displayScaleAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem_displayScaleAuto.Name = "toolStripMenuItem_displayScaleAuto";
            this.toolStripMenuItem_displayScaleAuto.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_displayScaleAuto.Text = "适应窗口尺寸";
            this.toolStripMenuItem_displayScaleAuto.Click += new System.EventHandler(this.toolStripMenuItem_displayScaleAuto_Click);
            // 
            // toolStripMenuItem_displayScale150
            // 
            this.toolStripMenuItem_displayScale150.Name = "toolStripMenuItem_displayScale150";
            this.toolStripMenuItem_displayScale150.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_displayScale150.Text = "150%";
            this.toolStripMenuItem_displayScale150.Click += new System.EventHandler(this.toolStripMenuItem_displayScale150_Click);
            // 
            // toolStripMenuItem_displayScale200
            // 
            this.toolStripMenuItem_displayScale200.Name = "toolStripMenuItem_displayScale200";
            this.toolStripMenuItem_displayScale200.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_displayScale200.Text = "200%";
            this.toolStripMenuItem_displayScale200.Click += new System.EventHandler(this.toolStripMenuItem_displayScale200_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_OnePage
            // 
            this.toolStripButton_OnePage.Image = global::Erp.Toolkit.Properties.Resources.Page1;
            this.toolStripButton_OnePage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_OnePage.Name = "toolStripButton_OnePage";
            this.toolStripButton_OnePage.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_OnePage.Text = "单页";
            this.toolStripButton_OnePage.Click += new System.EventHandler(this.toolStripButton_OnePage_Click);
            // 
            // toolStripButton_TwoPage
            // 
            this.toolStripButton_TwoPage.Image = global::Erp.Toolkit.Properties.Resources.Page2;
            this.toolStripButton_TwoPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_TwoPage.Name = "toolStripButton_TwoPage";
            this.toolStripButton_TwoPage.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_TwoPage.Text = "双页";
            this.toolStripButton_TwoPage.Click += new System.EventHandler(this.toolStripButton_TwoPage_Click);
            // 
            // toolStripButton_FourPage
            // 
            this.toolStripButton_FourPage.Image = global::Erp.Toolkit.Properties.Resources.Page4;
            this.toolStripButton_FourPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_FourPage.Name = "toolStripButton_FourPage";
            this.toolStripButton_FourPage.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_FourPage.Text = "四页";
            this.toolStripButton_FourPage.Click += new System.EventHandler(this.toolStripButton_FourPage_Click);
            // 
            // toolStripButton_SixPage
            // 
            this.toolStripButton_SixPage.Image = global::Erp.Toolkit.Properties.Resources.Page6;
            this.toolStripButton_SixPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_SixPage.Name = "toolStripButton_SixPage";
            this.toolStripButton_SixPage.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_SixPage.Text = "六页";
            this.toolStripButton_SixPage.Click += new System.EventHandler(this.toolStripButton_SixPage_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_PreviousPage
            // 
            this.toolStripButton_PreviousPage.Image = global::Erp.Toolkit.Properties.Resources.PreviousPage;
            this.toolStripButton_PreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_PreviousPage.Name = "toolStripButton_PreviousPage";
            this.toolStripButton_PreviousPage.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton_PreviousPage.Text = "上一页";
            this.toolStripButton_PreviousPage.Click += new System.EventHandler(this.toolStripButton_PreviousPage_Click);
            // 
            // toolStripButton_NextPage
            // 
            this.toolStripButton_NextPage.Image = global::Erp.Toolkit.Properties.Resources.NextPage;
            this.toolStripButton_NextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_NextPage.Name = "toolStripButton_NextPage";
            this.toolStripButton_NextPage.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton_NextPage.Text = "下一页";
            this.toolStripButton_NextPage.Click += new System.EventHandler(this.toolStripButton_NextPage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBox_PageNumber
            // 
            this.toolStripTextBox_PageNumber.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox_PageNumber.Enabled = false;
            this.toolStripTextBox_PageNumber.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.toolStripTextBox_PageNumber.Name = "toolStripTextBox_PageNumber";
            this.toolStripTextBox_PageNumber.Size = new System.Drawing.Size(50, 25);
            this.toolStripTextBox_PageNumber.Text = "0";
            this.toolStripTextBox_PageNumber.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolStripTextBox_PageNumber.ToolTipText = "页码";
            this.toolStripTextBox_PageNumber.Visible = false;
            // 
            // toolStripButton_Close
            // 
            this.toolStripButton_Close.Image = global::Erp.Toolkit.Properties.Resources.bindingNavigatorDeleteItem_Image;
            this.toolStripButton_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Close.Name = "toolStripButton_Close";
            this.toolStripButton_Close.Size = new System.Drawing.Size(53, 22);
            this.toolStripButton_Close.Text = "关闭";
            this.toolStripButton_Close.Click += new System.EventHandler(this.toolStripButton_Close_Click);
            // 
            // toolStripLabel_PageNumber
            // 
            this.toolStripLabel_PageNumber.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_PageNumber.Name = "toolStripLabel_PageNumber";
            this.toolStripLabel_PageNumber.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel_PageNumber.Text = "页码";
            this.toolStripLabel_PageNumber.Visible = false;
            // 
            // printPreviewControl
            // 
            this.printPreviewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.printPreviewControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(206)))), ((int)(((byte)(230)))));
            this.printPreviewControl.Location = new System.Drawing.Point(1, 26);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(1262, 832);
            this.printPreviewControl.TabIndex = 1;
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // printDocument
            // 
            this.printDocument.DocumentName = "打印预览";
            // 
            // FrmPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(206)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(1264, 861);
            this.Controls.Add(this.printPreviewControl);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmPrintPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印预览";
            this.Load += new System.EventHandler(this.FrmPrintPreview_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.PrintPreviewControl printPreviewControl;
        private System.Windows.Forms.ToolStripButton toolStripButton_SelectPrinter;
        private System.Windows.Forms.ToolStripButton toolStripButton_PageSetup;
        private System.Windows.Forms.ToolStripButton toolStripButton_Print;
        private System.Windows.Forms.ToolStripButton toolStripButton_OnePage;
        private System.Windows.Forms.ToolStripButton toolStripButton_TwoPage;
        private System.Windows.Forms.ToolStripButton toolStripButton_FourPage;
        private System.Windows.Forms.ToolStripButton toolStripButton_SixPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton_PreviousPage;
        private System.Windows.Forms.ToolStripButton toolStripButton_NextPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox_PageNumber;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        public System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.ToolStripButton toolStripButton_Close;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_PageNumber;
        private System.Windows.Forms.ToolStripButton toolStripButton_VerticalPage;
        private System.Windows.Forms.ToolStripButton toolStripButton_HorizontalPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDown_displayScale;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_displayScale10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_displayScale25;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_displayScale50;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_displayScale75;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_displayScale100;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_displayScaleAuto;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_displayScale150;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_displayScale200;
    }
}