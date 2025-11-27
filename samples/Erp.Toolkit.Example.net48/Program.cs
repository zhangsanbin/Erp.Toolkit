/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2025-11-23           Andy        the first version
 */

using Erp.Toolkit.ExampleFull;
using System;
using System.Windows.Forms;

namespace Erp.Toolkit.Sample
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new WinFormSample());// 简单示例
            Application.Run(new WinFormExampleFull());// 完整示例
        }
    }
}