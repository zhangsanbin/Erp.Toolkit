/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2025-11-27           Andy        the first version
 * 2025-12-14           Andy        Refactoring, splitting files
 */

using System.Collections.Generic;
using System.Globalization;

namespace Erp.Toolkit.Localization
{
    /// <summary>
    /// 语言服务，提供语言切换功能
    /// </summary>
    public static class LanguageService
    {
        /// <summary>
        /// 切换到英语
        /// </summary>
        public static void SwitchToEnglish() => SwitchToCulture("en-US");

        /// <summary>
        /// 切换到简体中文
        /// </summary>
        public static void SwitchToSimplifiedChinese() => SwitchToCulture("zh-CN");

        /// <summary>
        /// 切换到繁体中文（台湾）
        /// </summary>
        public static void SwitchToTraditionalChinese() => SwitchToCulture("zh-TW");

        /// <summary>
        /// 切换到德语
        /// </summary>
        public static void SwitchToGerman() => SwitchToCulture("de-DE");

        /// <summary>
        /// 切换到法语
        /// </summary>
        public static void SwitchToFrench() => SwitchToCulture("fr-FR");

        /// <summary>
        /// 切换到日语
        /// </summary>
        public static void SwitchToJapanese() => SwitchToCulture("ja-JP");

        /// <summary>
        /// 获取当前文化
        /// </summary>
        public static CultureInfo GetCurrentCulture() => Localizer.Instance.CurrentCulture;

        /// <summary>
        /// 获取支持的所有文化
        /// </summary>
        public static IEnumerable<CultureInfo> GetSupportedCultures() => Localizer.Instance.GetSupportedCultures();

        /// <summary>
        /// 切换到指定文化
        /// </summary>
        /// <param name="cultureName"></param>
        private static void SwitchToCulture(string cultureName)
        {
            try
            {
                // 尝试切换到指定文化
                Localizer.Instance.CurrentCulture = new CultureInfo(cultureName);
            }
            catch (CultureNotFoundException)
            {
                // 回退到默认文化
                Localizer.Instance.CurrentCulture = Localizer.Instance.DefaultCulture;
            }
        }
    }
}