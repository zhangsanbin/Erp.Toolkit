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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace Erp.Toolkit.Localization
{
    /// <summary>
    /// 本地化服务核心类，管理应用程序的多语言支持
    /// </summary>
    public class Localizer : INotifyPropertyChanged
    {
        private static readonly Lazy<Localizer> _instance = new Lazy<Localizer>(() => new Localizer());

        /// <summary>
        /// 获取本地化服务单例实例
        /// </summary>
        public static Localizer Instance => _instance.Value;

        private ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        /// <summary>
        /// 属性变更事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 文化变更事件
        /// </summary>
        public event EventHandler<CultureChangedEventArgs> CultureChanged;

        /// <summary>
        /// 获取或设置当前文化
        /// </summary>
        public CultureInfo CurrentCulture
        {
            get => _currentCulture ?? CultureInfo.CurrentCulture;
            set
            {
                if (_currentCulture?.Name != value?.Name)
                {
                    var oldCulture = _currentCulture;
                    _currentCulture = value ?? DefaultCulture;

                    // 更新线程文化设置
                    CultureInfo.CurrentCulture = _currentCulture;
                    CultureInfo.CurrentUICulture = _currentCulture;

                    OnCultureChanged(oldCulture, _currentCulture);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentCulture)));
                }
            }
        }

        /// <summary>
        /// 获取默认文化（英文）
        /// </summary>
        public CultureInfo DefaultCulture => new CultureInfo("en-US");

        private Localizer()
        {
            _resourceManager = new ResourceManager("Erp.Toolkit.Resources.Strings", typeof(Localizer).Assembly);

            // 初始时使用默认文化
            // _currentCulture = DefaultCulture;

            // 初始化时智能检测系统语言
            SmartDetectSystemLanguage();
        }

        /// <summary>
        /// 索引器，通过键获取本地化字符串
        /// </summary>
        public string this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                    return key;

                // 优先使用当前文化，然后回退到默认英文
                string result = _resourceManager.GetString(key, CurrentCulture) ??
                               _resourceManager.GetString(key, DefaultCulture);

                return result ?? $"[{key}]";
            }
        }

        /// <summary>
        /// 获取本地化字符串
        /// </summary>
        public string GetString(string key) => this[key];

        /// <summary>
        /// 获取格式化本地化字符串
        /// </summary>
        public string GetString(string key, params object[] args)
        {
            var format = this[key];
            return string.Format(CurrentCulture, format, args);
        }

        /// <summary>
        /// 检查键是否存在
        /// </summary>
        public bool HasKey(string key)
        {
            try
            {
                var value = _resourceManager.GetString(key, DefaultCulture);
                return value != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取支持的文化列表
        /// </summary>
        public IEnumerable<CultureInfo> GetSupportedCultures()
        {
            return new[]
            {
                new CultureInfo("en-US"),   // 英语 (默认)
                new CultureInfo("zh-CN"),   // 简体中文
                new CultureInfo("zh-TW"),   // 繁体中文
                new CultureInfo("de-DE"),   // 德语
                new CultureInfo("fr-FR"),   // 法语
                new CultureInfo("ja-JP"),   // 日语
            };
        }

        /// <summary>
        /// 文化变更处理
        /// </summary>
        /// <param name="oldCulture"></param>
        /// <param name="newCulture"></param>
        private void OnCultureChanged(CultureInfo oldCulture, CultureInfo newCulture)
        {
            CultureChanged?.Invoke(this, new CultureChangedEventArgs(oldCulture, newCulture));
        }

        /// <summary>
        /// 重新检测系统语言并应用
        /// </summary>
        public void RefreshSystemLanguage()
        {
            SmartDetectSystemLanguage();
        }

        /// <summary>
        /// 获取系统当前UI语言信息
        /// </summary>
        public CultureInfo GetSystemCulture()
        {
            return CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// 系统语言检测
        /// </summary>
        public void SmartDetectSystemLanguage()
        {
            var systemCulture = CultureInfo.CurrentUICulture;
            var supportedCultures = GetSupportedCultures().ToList();

            Console.WriteLine($"检测到系统语言: {systemCulture.Name} ({systemCulture.EnglishName})");

            // 匹配策略
            var match = supportedCultures.FirstOrDefault(c => c.Name == systemCulture.Name) ?? // 精确匹配
                        supportedCultures.FirstOrDefault(c => c.Name.StartsWith(systemCulture.TwoLetterISOLanguageName + "-")) ?? // 同语言区域变体
                        supportedCultures.FirstOrDefault(c => c.TwoLetterISOLanguageName == systemCulture.TwoLetterISOLanguageName) ?? // 同语言
                        DefaultCulture; // 默认回退

            CurrentCulture = match;
            Console.WriteLine($"使用语言: {CurrentCulture.Name} ({CurrentCulture.EnglishName})");
        }
    }
}