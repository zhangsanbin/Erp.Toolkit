/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2024-04-03           Andy        the first version
 * 2025-07-13           Andy        Split, restructure
 * 2025-12-10           Andy        Remove binary serialization and replace it with JSON.
 * 2025-12-14           Andy        Merging, multilingual localization initialization
 */

using Erp.Toolkit.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Erp.Toolkit.Controls
{
    public partial class Dgv
    {
        #region 本地文化切换

        /// <summary>
        /// 初始化用户界面元素的本地化设置
        /// </summary>
        private void InitializeLocalization()
        {
            // 打印
            this.toolStripButton_print.SetLocalizationKey("Print");
            this.ToolStripMenuItem_print.SetLocalizationKey("Print");
            this.toolStripMenuItem_printSelectPrinter.SetLocalizationKey("Print");
            this.ToolStripMenuItem_printPreview.SetLocalizationKey("PrintPreview");
            this.toolStripMenuItem_printPageSetup.SetLocalizationKey("PageSetup");

            // 升序
            this.toolStripButton_asc.SetLocalizationKey("Ascending");
            this.ToolStripMenuItem_asc.SetLocalizationKey("Ascending");
            this.ToolStripMenuItem_ColHeader_asc.SetLocalizationKey("Ascending");

            // 降序
            this.toolStripButton_desc.SetLocalizationKey("Descending");
            this.ToolStripMenuItem_desc.SetLocalizationKey("Descending");
            this.ToolStripMenuItem_ColHeader_desc.SetLocalizationKey("Descending");

            // 查找
            this.toolStripButton_query.SetLocalizationKey("Find");
            this.toolStripButton_find.SetLocalizationKey("Find");
            this.ToolStripMenuItem_query.SetLocalizationKey("Find");
            this.ToolStripMenuItem_ColHeader_query.SetLocalizationKey("Find");

            // 筛选
            this.toolStripButton_find1.SetLocalizationKey("Filter");
            this.ToolStripMenuItem_find.SetLocalizationKey("Filter");
            this.ToolStripMenuItem_ColHeader_find.SetLocalizationKey("Filter");

            // 取消筛选
            this.toolStripButton_cancelFind.SetLocalizationKey("CancelFilter");
            this.toolStripButton_cancelFind2.SetLocalizationKey("CancelFilter");
            this.ToolStripMenuItem_CancelFind.SetLocalizationKey("CancelAllFilters");
            this.ToolStripMenuItem_ColHeader_CancelFind.SetLocalizationKey("CancelAllFilters");
            this.ToolStripMenuItem_previousFind.SetLocalizationKey("RestoreLastFilter");
            this.ToolStripMenuItem_ColHeader_previousFind.SetLocalizationKey("RestoreLastFilter");

            // 下一个
            this.toolStripButton_Next.SetLocalizationKey("Next");

            // 刷新
            this.toolStripButton_ReLoadList.SetLocalizationKey("Refresh");
            this.toolStripButtonReLoadList.SetLocalizationKey("Refresh");
            this.ToolStripMenuItem_refresh.SetLocalizationKey("Refresh");

            // 统计
            this.toolStripButton_char.SetLocalizationKey("Statistics");

            // 条件格式
            this.toolStripButton_conditionalStyles.SetLocalizationKey("ConditionalFormat");
            this.ToolStripMenuItem_ColHeader_ConditionalStyles.SetLocalizationKey("ConditionalFormat");

            // 字段属性
            this.toolStripButton_CancelHideCol.SetLocalizationKey("FieldProperties");

            // 复制
            this.ToolStripMenuItem_copy.SetLocalizationKey("Copy");
            this.ToolStripMenuItem_RowHeader_copy.SetLocalizationKey("Copy");
            this.ToolStripMenuItem_ColHeader_copy.SetLocalizationKey("Copy");

            // 导出
            this.ToolStripMenuItem_exp.SetLocalizationKey("Export");
            this.ToolStripMenuItem_RowHeader_exp.SetLocalizationKey("Export");
            this.ToolStripMenuItem_ColHeader_exp.SetLocalizationKey("Export");

            // 文本筛选器
            this.ToolStripMenuItem_findText.SetLocalizationKey("TextFilter");
            this.ToolStripMenuItemText_eq.SetLocalizationKey("Equal");
            this.ToolStripMenuItemText_NotEq.SetLocalizationKey("NotEqual");
            this.ToolStripMenuItemText_LikeBf.SetLocalizationKey("StartsWith");
            this.ToolStripMenuItemText_NotLikeBf.SetLocalizationKey("DoesNotStartWith");
            this.ToolStripMenuItemText_Like.SetLocalizationKey("Contains");
            this.ToolStripMenuItemText_NotLike.SetLocalizationKey("DoesNotContain");
            this.ToolStripMenuItemText_EndLike.SetLocalizationKey("EndsWith");
            this.ToolStripMenuItemText_EndNotLike.SetLocalizationKey("DoesNotEndWith");

            // 数字筛选器
            this.ToolStripMenuItem_findNubmer.SetLocalizationKey("NumberFilter");
            this.ToolStripMenuItemNumber_eq.SetLocalizationKey("Equal");
            this.ToolStripMenuItemNumber_NotEq.SetLocalizationKey("NotEqual");
            this.ToolStripMenuItemNumber_less.SetLocalizationKey("LessThan");
            this.ToolStripMenuItemNumber_greater.SetLocalizationKey("GreaterThan");
            this.ToolStripMenuItemNumber_interval.SetLocalizationKey("Period");

            // 日期筛选器
            this.ToolStripMenuItem_findDate.SetLocalizationKey("DateFilter");
            this.ToolStripMenuItemDate_eq.SetLocalizationKey("Equal");
            this.ToolStripMenuItemDate_NotEq.SetLocalizationKey("NotEqual");
            this.ToolStripMenuItemDate_less.SetLocalizationKey("Before");
            this.ToolStripMenuItemDate_greater.SetLocalizationKey("After");
            this.ToolStripMenuItemDate_interval.SetLocalizationKey("Period");
            this.ToolStripMenuItemDate_tomorrow.SetLocalizationKey("Tomorrow");
            this.ToolStripMenuItemDate_today.SetLocalizationKey("Today");
            this.ToolStripMenuItemDate_yesterday.SetLocalizationKey("Yesterday");
            this.ToolStripMenuItemDate_nextWeek.SetLocalizationKey("NextWeek");
            this.ToolStripMenuItemDate_thisWeek.SetLocalizationKey("ThisWeek");
            this.ToolStripMenuItemDate_lastWeek.SetLocalizationKey("LastWeek");
            this.ToolStripMenuItemDate_nextMonth.SetLocalizationKey("NextMonth");
            this.ToolStripMenuItemDate_thisMonth.SetLocalizationKey("ThisMonth");
            this.ToolStripMenuItemDate_lastMonth.SetLocalizationKey("LastMonth");
            this.ToolStripMenuItemDate_nextQuarter.SetLocalizationKey("NextQuarter");
            this.ToolStripMenuItemDate_thisQuarter.SetLocalizationKey("ThisQuarter");
            this.ToolStripMenuItemDate_lastQuarter.SetLocalizationKey("LastQuarter");
            this.ToolStripMenuItemDate_nextYear.SetLocalizationKey("NextYear");
            this.ToolStripMenuItemDate_thisYear.SetLocalizationKey("ThisYear");
            this.ToolStripMenuItemDate_soFarThisYear.SetLocalizationKey("YearToDate");
            this.ToolStripMenuItemDate_lastYear.SetLocalizationKey("LastYear");

            // 操作菜单
            this.ToolStripMenuItem_add.SetLocalizationKey("New");
            this.ToolStripMenuItem_edit.SetLocalizationKey("Modify");
            this.ToolStripMenuItem_del.SetLocalizationKey("Delete");
            this.bindingNavigatorAddNewItem.SetLocalizationKey("New");
            this.bindingNavigatorDeleteItem.SetLocalizationKey("Delete");

            // 系统样式
            this.ToolStripMenuItem_sysTheme.SetLocalizationKey("SystemStyle");

            // 字段宽度
            this.toolStripMenuItem_colWidth.SetLocalizationKey("FieldWidth");
            this.ToolStripMenuItem_ColHeader_HideCol.SetLocalizationKey("HideField");
            this.ToolStripMenuItem_ColHeader_CancelHideCol.SetLocalizationKey("UnhideField");
            this.ToolStripMenuItem_ColHeader_FreezeCol.SetLocalizationKey("FreezeField");
            this.ToolStripMenuItem_ColHeader_CancelFreezeCol.SetLocalizationKey("UnfreezeAllFields");
            this.ToolStripMenuItem_ColHeader_addCol.SetLocalizationKey("AddCustomColumn");
            this.ToolStripMenuItem_ColHeader_ConditionalStyles.SetLocalizationKey("ConditionalFormat");
            this.ToolStripMenuItem_ColHeader_Att.SetLocalizationKey("Properties");

            // 记录控制
            this.bindingNavigatorMoveFirstItem.SetLocalizationKey("GoToFirstRecord");
            this.bindingNavigatorMovePreviousItem.SetLocalizationKey("GoToPreviousRecord");
            this.bindingNavigatorMoveNextItem.SetLocalizationKey("GoToNextRecord");
            this.bindingNavigatorMoveLastItem.SetLocalizationKey("GoToLastRecord");

            // 输入框提示
            this.toolStripTextBox_FindText.SetLocalizationKey("InputTips");

            // 语言
            this.ToolStripComboBox_Language.Items.Clear();
            this.ToolStripComboBox_Language.Items.AddRange(new object[]
            {
                Localizer.Instance.GetString("English"),
                Localizer.Instance.GetString("SimplifiedChinese"),
                Localizer.Instance.GetString("TraditionalChinese"),
                Localizer.Instance.GetString("German"),
                Localizer.Instance.GetString("French"),
                Localizer.Instance.GetString("Japanese"),
            });
            this.ToolStripComboBox_Language.Text = LanguageService.GetCurrentCulture().DisplayName;

            // 主题
            this.ToolStripComboBox_Theme.Items.Clear();
            this.ToolStripComboBox_Theme.Items.AddRange(new object[]
            {
                Localizer.Instance.GetString("DarkTheme"),
                Localizer.Instance.GetString("LightTheme"),
                Localizer.Instance.GetString("BlueTheme"),
                Localizer.Instance.GetString("GreenTheme"),
                Localizer.Instance.GetString("OrangeTheme"),
                Localizer.Instance.GetString("PurpleTheme"),
            });
            this.ToolStripComboBox_Theme.SetLocalizationKey(_themeStyle.ToString());

            // 应用本地化到主控件
            this.ApplyLocalization();

            // 单独应用本地化到 ContextMenuStrip
            if (this.DgvContextMenuStrip != null)
            {
                this.DgvContextMenuStrip.ApplyLocalization();
            }

            if (this.RowHeaderContextMenuStrip != null)
            {
                this.RowHeaderContextMenuStrip.ApplyLocalization();
            }

            if (this.ColHeaderContextMenuStrip != null)
            {
                this.ColHeaderContextMenuStrip.ApplyLocalization();
            }

            // 文化变更事件订阅
            Localizer.Instance.CultureChanged -= OnCultureChanged; // 先取消订阅，避免重复
            Localizer.Instance.CultureChanged += OnCultureChanged;
        }

        /// <summary>
        /// 处理文化变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCultureChanged(object sender, CultureChangedEventArgs e)
        {
            // 确保在 UI 线程上执行
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnCultureChanged(sender, e)));
                return;
            }

            // 更新语言下拉框显示文本
            this.ToolStripComboBox_Language.Items.Clear();
            this.ToolStripComboBox_Language.Items.AddRange(new object[]
            {
                Localizer.Instance.GetString("English"),
                Localizer.Instance.GetString("SimplifiedChinese"),
                Localizer.Instance.GetString("TraditionalChinese"),
                Localizer.Instance.GetString("German"),
                Localizer.Instance.GetString("French"),
                Localizer.Instance.GetString("Japanese"),
            });
            this.ToolStripComboBox_Language.Text = e.NewCulture.DisplayName;

            // 更新主题下拉框
            this.ToolStripComboBox_Theme.Items.Clear();
            this.ToolStripComboBox_Theme.Items.AddRange(new object[]
            {
                Localizer.Instance.GetString("DarkTheme"),
                Localizer.Instance.GetString("LightTheme"),
                Localizer.Instance.GetString("BlueTheme"),
                Localizer.Instance.GetString("GreenTheme"),
                Localizer.Instance.GetString("OrangeTheme"),
                Localizer.Instance.GetString("PurpleTheme"),
            });

            // 重要：重新应用本地化
            this.ApplyLocalization();

            // 重新应用菜单本地化
            if (this.DgvContextMenuStrip != null)
            {
                this.DgvContextMenuStrip.ApplyLocalization();
            }
            if (this.RowHeaderContextMenuStrip != null)
            {
                this.RowHeaderContextMenuStrip.ApplyLocalization();
            }
            if (this.ColHeaderContextMenuStrip != null)
            {
                this.ColHeaderContextMenuStrip.ApplyLocalization();
            }
        }

        #endregion 本地文化切换

        #region 配置文件持久化

        /// <summary>
        /// 保存配置到JSON文件
        /// </summary>
        private void SaveConfigAsJson<T>(T config, string fileSuffix, string configTypeName)
        {
            try
            {
                // 确保配置目录存在
                MkdirConfigurationFilePath();

                // 构建JSON文件路径
                string jsonFilePath = Path.Combine("Configuration", $"{Guid}.{fileSuffix}.json").ToUpper();

                // 验证参数有效性
                if (config == null)
                {
                    Console.WriteLine($"无法保存{configTypeName}配置，配置对象为空");
                    return;
                }

                // 序列化并写入JSON文件
                string json = JsonConvert.SerializeObject(config, Formatting.Indented);
                File.WriteAllText(jsonFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存{configTypeName}配置时出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 读取JSON配置
        /// </summary>
        private T LoadConfigFromJson<T>(string fileSuffix, string configTypeName)
        {
            try
            {
                // 构建JSON文件路径
                string jsonFilePath = Path.Combine("Configuration", $"{_guid}.{fileSuffix}.json").ToUpper();

                // 检查文件是否存在
                if (!File.Exists(jsonFilePath))
                {
                    return default(T);
                }

                // 读取并反序列化JSON文件
                string json = File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载{configTypeName}配置失败: {ex.Message}");
                return default(T);
            }
        }

        /// <summary>
        /// 检查配置文件是否存在
        /// </summary>
        private bool ConfigFileExists(string fileSuffix)
        {
            string jsonFilePath = Path.Combine("Configuration", $"{_guid}.{fileSuffix}.json").ToUpper();
            return File.Exists(jsonFilePath);
        }

        // 类成员变量声明
        private System.Windows.Forms.Timer _saveTimer;

        /// <summary>
        /// 延迟保存配置（防抖机制）
        /// </summary>
        private void ScheduleConfigSave()
        {
            // 清除已有定时器
            _saveTimer?.Stop();

            // 创建新定时器（500ms延迟）
            _saveTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _saveTimer.Tick += (s, e) =>
            {
                _saveTimer.Stop();
                SaveGuiConfigs();
                _saveTimer.Dispose();
                _saveTimer = null;
            };
            _saveTimer.Start();
        }

        /// <summary>
        /// 保存 UI 布局配置信息到本地磁盘
        /// </summary>
        public void SaveGuiConfigs()
        {
            if (_columnInfos != null)
            {
                SaveConfigAsJson(_columnInfos, "COL", "UI布局");
            }
            else
            {
                Console.WriteLine("无法保存 GUI 配置，列信息为空");
            }
        }

        /// <summary>
        /// 保存条件样式配置信息到本地磁盘
        /// </summary>
        public void SaveConditionalStylesConfigs(List<DgvConditionalConfig> configs)
        {
            if (configs != null)
            {
                SaveConfigAsJson(configs, "CFG", "条件样式");
            }
            else
            {
                Console.WriteLine("无法保存条件样式配置，配置为空");
            }
        }

        /// <summary>
        /// 保存自定义列配置信息到本地磁盘
        /// </summary>
        public void SaveCustomColumnsConfigs()
        {
            if (_customColumnsConfigs != null)
            {
                SaveConfigAsJson(_customColumnsConfigs, "CCL", "自定义字段");
            }
            else
            {
                Console.WriteLine("无法保存自定义字段配置，配置为空");
            }
        }

        /// <summary>
        /// 保存统计表达式配置信息到本地磁盘
        /// </summary>
        public void SaveStatisticsExpressionConfigs(DgvAggregatorConfig aggregatorConfig)
        {
            if (aggregatorConfig != null)
            {
                SaveConfigAsJson(aggregatorConfig, "EXP", "统计表达式");
            }
            else
            {
                Console.WriteLine("无法保存统计表达式配置，配置为空");
            }
        }

        /// <summary>
        /// 保存自定义筛选配置信息到本地磁盘
        /// </summary>
        public void SaveFilterConfigs(List<DgvCustomFilterConfig> configs)
        {
            if (configs != null)
            {
                SaveConfigAsJson(configs, "CFC", "自定义筛选");
            }
            else
            {
                Console.WriteLine("无法保存自定义筛选配置，配置为空");
            }
        }

        /// <summary>
        /// 创建存储配置文件夹目录
        /// </summary>
        private void MkdirConfigurationFilePath()
        {
            try
            {
                if (!Directory.Exists("Configuration"))
                {
                    Directory.CreateDirectory("Configuration");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建文件夹时发生错误: {ex.Message}");
            }
        }

        /// <summary>
        /// dgv调整列宽
        /// </summary>
        private void DataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            // 使用 LINQ 查找匹配的配置项
            var config = _columnInfos?.FirstOrDefault(c => c.Name == e.Column.Name);

            if (config != null)
            {
                config.RowWidth = e.Column.Width;
                ScheduleConfigSave();
            }
        }

        /// <summary>
        /// 列头显示索引改变事件
        /// </summary>
        private void DataGridView_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_isLoadingData || _isSettingDisplayIndex) return;

            // 只更新当前改变的列
            var config = _columnInfos?.FirstOrDefault(c => c.Name == e.Column.Name);
            if (config != null)
            {
                config.DisplayIndex = e.Column.DisplayIndex;
            }

            // 保存配置
            ScheduleConfigSave();
        }

        /// <summary>
        /// subview dgv调整列宽
        /// </summary>
        private void Subview_dataGridView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (subview == null) return;

            // 使用 LINQ 查找匹配的配置项
            var config = subview._columnInfos?.FirstOrDefault(c => c.Name == e.Column.Name);

            if (config != null)
            {
                config.RowWidth = e.Column.Width;
                subview.ScheduleConfigSave();
            }
        }

        /// <summary>
        /// subview dgv 列头显示索引改变事件
        /// </summary>
        private void Subview_dataGridView_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (subview == null || subview._isLoadingData) return;

            // 使用 LINQ 查找匹配的配置项
            var config = subview._columnInfos?.FirstOrDefault(c => c.Name == e.Column.Name);

            if (config != null)
            {
                config.DisplayIndex = e.Column.DisplayIndex;
                subview.ScheduleConfigSave();
            }
        }

        #endregion 配置文件持久化

        #region 配置文件读取

        /// <summary>
        /// 加载UI布局配置
        /// </summary>
        private void LoadGuiConfigs()
        {
            var openColumnInfos = LoadConfigFromJson<List<DgvColumnInfoConfig>>("COL", "UI布局");
            if (openColumnInfos != null)
            {
                // 恢复字段属性的配置信息
                ColumnInfos = openColumnInfos;
                // 更新字段属性配置
                SetColumnInfo(ColumnInfos);
            }
        }

        /// <summary>
        /// 加载条件样式配置
        /// </summary>
        private void LoadConditionalStylesConfigs()
        {
            var cfg = LoadConfigFromJson<List<DgvConditionalConfig>>("CFG", "条件样式");
            if (cfg != null)
            {
                // 构建条件样式并应用配置
                BuildConditionalFormatting(cfg);
            }
        }

        /// <summary>
        /// 加载自定义字段配置
        /// </summary>
        private void LoadCustomColumnsConfigs()
        {
            var cfg = LoadConfigFromJson<List<DgvCustomColumnsConfig>>("CCL", "自定义字段");
            if (cfg != null)
            {
                // 恢复自定义字段的配置信息
                CustomColumnsConfigs = cfg;
                // 显示自定义字段
                SetCustomColumnsConfig();
            }
        }

        /// <summary>
        /// 加载统计表达式配置
        /// </summary>
        private void LoadStatisticsExpressionConfigs()
        {
            var cfg = LoadConfigFromJson<DgvAggregatorConfig>("EXP", "统计表达式");
            if (cfg != null)
            {
                // 存储表达式对象
                _dgvAggregatorConfig = cfg;
            }
        }

        /// <summary>
        /// 加载自定义筛选配置
        /// </summary>
        private void LoadFilterConfigs()
        {
            var cfg = LoadConfigFromJson<List<DgvCustomFilterConfig>>("CFC", "自定义筛选");
            if (cfg != null)
            {
                // 增加一层验证，防止重复构建筛选菜单
                // 首次载入有效，后续刷新操作跳过
                if (DgvCustomFilterConfig == null)
                {
                    // 保持以便于修改
                    DgvCustomFilterConfig = cfg;
                    // 存储表达式对象
                    SetUserToolStripMenuForFilter(DgvCustomFilterConfig);
                }
            }
        }

        #endregion 配置文件读取
    }
}