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
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    /// <summary>
    /// 管理打印预览窗体
    /// </summary>
    /// <remarks>
    /// 兼容框架：
    /// - .NET Framework 4.6.2+
    /// - .NET Core 3.1+ (Windows)
    /// - .NET 5/6/7/8+ (Windows)
    ///
    /// 注意：此窗体依赖于 Windows Forms，仅支持 Windows 平台
    /// </remarks>
    public partial class FrmPrintPreview : Form
    {
        private readonly Localization.Localizer localizer = Localization.Localizer.Instance;

        public FrmPrintPreview()
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
            this.Text = localizer.GetString("PrintPreview");

            this.toolStripButton_SelectPrinter.SetLocalizationKey("SwitchPrinter");
            this.toolStripButton_PageSetup.SetLocalizationKey("PageSetup");
            this.toolStripButton_Print.SetLocalizationKey("Print");
            this.toolStripButton_VerticalPage.SetLocalizationKey("Portrait");
            this.toolStripButton_HorizontalPage.SetLocalizationKey("Landscape");
            this.toolStripDropDown_displayScale.SetLocalizationKey("ZoomLevel");
            this.toolStripMenuItem_displayScaleAuto.SetLocalizationKey("FitWindowSize");
            this.toolStripButton_OnePage.SetLocalizationKey("SinglePage");
            this.toolStripButton_TwoPage.SetLocalizationKey("TwoPages");
            this.toolStripButton_FourPage.SetLocalizationKey("FourPages");
            this.toolStripButton_SixPage.SetLocalizationKey("SixPages");
            this.toolStripButton_PreviousPage.SetLocalizationKey("PreviousPage");
            this.toolStripButton_NextPage.SetLocalizationKey("NextPage");
            this.toolStripButton_Close.SetLocalizationKey("Close");
            this.toolStripLabel_PageNumber.SetLocalizationKey("PageNumber");

            // 应用本地化到整个窗体
            this.ApplyLocalization();
        }

        private void FrmPrintPreview_Load(object sender, EventArgs e)
        {
            printPreviewControl.Document = printDocument;
            pageSetupDialog.Document = printDocument;
            printDialog.Document = printDocument;
            Text = printDocument.DocumentName.ToString() + "（" + localizer.GetString("PrintPreview") + "）";
        }

        private void toolStripButton_SelectPrinter_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                // 强制刷新
                printPreviewControl.Document = null;
                printPreviewControl.Document = printDocument;
            }
        }

        private void toolStripButton_PageSetup_Click(object sender, EventArgs e)
        {
            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取并显示用户设置的页边距
                Margins margins = pageSetupDialog.PageSettings.Margins;
                printDocument.DefaultPageSettings.Margins.Top = ConvertHundredthsOfAnInchToMM(margins.Top);
                printDocument.DefaultPageSettings.Margins.Bottom = ConvertHundredthsOfAnInchToMM(margins.Bottom);
                printDocument.DefaultPageSettings.Margins.Left = ConvertHundredthsOfAnInchToMM(margins.Left);
                printDocument.DefaultPageSettings.Margins.Right = ConvertHundredthsOfAnInchToMM(margins.Right);

                // 强制刷新
                printPreviewControl.Document = null;
                printPreviewControl.Document = printDocument;
            }
        }

        private void toolStripButton_Print_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void toolStripButton_OnePage_Click(object sender, EventArgs e)
        {
            printPreviewControl.Rows = 1;
            printPreviewControl.Columns = 1;
        }

        private void toolStripButton_TwoPage_Click(object sender, EventArgs e)
        {
            printPreviewControl.Rows = 1;
            printPreviewControl.Columns = 2;
        }

        private void toolStripButton_FourPage_Click(object sender, EventArgs e)
        {
            printPreviewControl.Rows = 2;
            printPreviewControl.Columns = 2;
        }

        private void toolStripButton_SixPage_Click(object sender, EventArgs e)
        {
            printPreviewControl.Rows = 2;
            printPreviewControl.Columns = 3;
        }

        private void toolStripButton_PreviousPage_Click(object sender, EventArgs e)
        {
            if (printPreviewControl.StartPage > 0)
            {
                printPreviewControl.StartPage--;
                toolStripTextBox_PageNumber.Text = (printPreviewControl.StartPage + 1).ToString();
                toolStripTextBox_PageNumber.Visible = true;
                toolStripLabel_PageNumber.Visible = true;
            }
        }

        private void toolStripButton_NextPage_Click(object sender, EventArgs e)
        {
            printPreviewControl.StartPage++;
            toolStripTextBox_PageNumber.Text = (printPreviewControl.StartPage + 1).ToString();
            toolStripTextBox_PageNumber.Visible = true;
            toolStripLabel_PageNumber.Visible = true;
        }

        /// <summary>
        /// 将以百分之一英寸（hundredths of an inch）为单位的长度转换为毫米（mm）
        /// </summary>
        /// <param name="hundredthsOfAnInch"></param>
        /// <returns></returns>
        private int ConvertHundredthsOfAnInchToMM(int hundredthsOfAnInch)
        {
            return (int)(hundredthsOfAnInch / 10.0 * 25.4);
        }

        private void toolStripButton_VerticalPage_Click(object sender, EventArgs e)
        {
            // 竖向
            printDocument.DefaultPageSettings.Landscape = false;
            // 强制刷新
            printPreviewControl.Document = null;
            printPreviewControl.Document = printDocument;
        }

        private void toolStripButton_HorizontalPage_Click(object sender, EventArgs e)
        {
            // 横向
            printDocument.DefaultPageSettings.Landscape = true;
            // 强制刷新
            printPreviewControl.Document = null;
            printPreviewControl.Document = printDocument;
        }

        private void toolStripMenuItem_displayScale10_Click(object sender, EventArgs e)
        {
            printPreviewControl.AutoZoom = false;
            printPreviewControl.Zoom = 0.1;
            toolStripMenuItem_displayScale10.Checked = true;
            toolStripMenuItem_displayScale25.Checked = false;
            toolStripMenuItem_displayScale50.Checked = false;
            toolStripMenuItem_displayScale75.Checked = false;
            toolStripMenuItem_displayScale100.Checked = false;
            toolStripMenuItem_displayScaleAuto.Checked = false;
            toolStripMenuItem_displayScale150.Checked = false;
            toolStripMenuItem_displayScale200.Checked = false;
        }

        private void toolStripMenuItem_displayScale25_Click(object sender, EventArgs e)
        {
            printPreviewControl.AutoZoom = false;
            printPreviewControl.Zoom = 0.25;
            toolStripMenuItem_displayScale10.Checked = false;
            toolStripMenuItem_displayScale25.Checked = true;
            toolStripMenuItem_displayScale50.Checked = false;
            toolStripMenuItem_displayScale75.Checked = false;
            toolStripMenuItem_displayScale100.Checked = false;
            toolStripMenuItem_displayScaleAuto.Checked = false;
            toolStripMenuItem_displayScale150.Checked = false;
            toolStripMenuItem_displayScale200.Checked = false;
        }

        private void toolStripMenuItem_displayScale50_Click(object sender, EventArgs e)
        {
            printPreviewControl.AutoZoom = false;
            printPreviewControl.Zoom = 0.5;
            toolStripMenuItem_displayScale10.Checked = false;
            toolStripMenuItem_displayScale25.Checked = false;
            toolStripMenuItem_displayScale50.Checked = true;
            toolStripMenuItem_displayScale75.Checked = false;
            toolStripMenuItem_displayScale100.Checked = false;
            toolStripMenuItem_displayScaleAuto.Checked = false;
            toolStripMenuItem_displayScale150.Checked = false;
            toolStripMenuItem_displayScale200.Checked = false;
        }

        private void toolStripMenuItem_displayScale75_Click(object sender, EventArgs e)
        {
            printPreviewControl.AutoZoom = false;
            printPreviewControl.Zoom = 0.75;
            toolStripMenuItem_displayScale10.Checked = false;
            toolStripMenuItem_displayScale25.Checked = false;
            toolStripMenuItem_displayScale50.Checked = false;
            toolStripMenuItem_displayScale75.Checked = true;
            toolStripMenuItem_displayScale100.Checked = false;
            toolStripMenuItem_displayScaleAuto.Checked = false;
            toolStripMenuItem_displayScale150.Checked = false;
            toolStripMenuItem_displayScale200.Checked = false;
        }

        private void toolStripMenuItem_displayScale100_Click(object sender, EventArgs e)
        {
            printPreviewControl.AutoZoom = false;
            printPreviewControl.Zoom = 1.0;
            toolStripMenuItem_displayScale10.Checked = false;
            toolStripMenuItem_displayScale25.Checked = false;
            toolStripMenuItem_displayScale50.Checked = false;
            toolStripMenuItem_displayScale75.Checked = false;
            toolStripMenuItem_displayScale100.Checked = true;
            toolStripMenuItem_displayScaleAuto.Checked = false;
            toolStripMenuItem_displayScale150.Checked = false;
            toolStripMenuItem_displayScale200.Checked = false;
        }

        private void toolStripMenuItem_displayScaleAuto_Click(object sender, EventArgs e)
        {
            printPreviewControl.AutoZoom = true;
            toolStripMenuItem_displayScale10.Checked = false;
            toolStripMenuItem_displayScale25.Checked = false;
            toolStripMenuItem_displayScale50.Checked = false;
            toolStripMenuItem_displayScale75.Checked = false;
            toolStripMenuItem_displayScale100.Checked = false;
            toolStripMenuItem_displayScaleAuto.Checked = true;
            toolStripMenuItem_displayScale150.Checked = false;
            toolStripMenuItem_displayScale200.Checked = false;
        }

        private void toolStripMenuItem_displayScale150_Click(object sender, EventArgs e)
        {
            printPreviewControl.AutoZoom = false;
            printPreviewControl.Zoom = 1.5;
            toolStripMenuItem_displayScale10.Checked = false;
            toolStripMenuItem_displayScale25.Checked = false;
            toolStripMenuItem_displayScale50.Checked = false;
            toolStripMenuItem_displayScale75.Checked = false;
            toolStripMenuItem_displayScale100.Checked = false;
            toolStripMenuItem_displayScaleAuto.Checked = false;
            toolStripMenuItem_displayScale150.Checked = true;
            toolStripMenuItem_displayScale200.Checked = false;
        }

        private void toolStripMenuItem_displayScale200_Click(object sender, EventArgs e)
        {
            printPreviewControl.AutoZoom = false;
            printPreviewControl.Zoom = 2.0;
            toolStripMenuItem_displayScale10.Checked = false;
            toolStripMenuItem_displayScale25.Checked = false;
            toolStripMenuItem_displayScale50.Checked = false;
            toolStripMenuItem_displayScale75.Checked = false;
            toolStripMenuItem_displayScale100.Checked = false;
            toolStripMenuItem_displayScaleAuto.Checked = false;
            toolStripMenuItem_displayScale150.Checked = false;
            toolStripMenuItem_displayScale200.Checked = true;
        }

        private void toolStripButton_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}