/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2025-12-01           Andy        the first version
 */

using System;
using System.Windows.Forms;

namespace Erp.Toolkit.Example
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // 切换到英文界面
            Erp.Toolkit.Localization.LanguageService.SwitchToEnglish();

            // 启动WinForm示例
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinFormExampleFull());
        }
    }
}