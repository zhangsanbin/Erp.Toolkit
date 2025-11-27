/*
 * Copyright (c) 2010-2025, doipc.com Development Team
 *
 * SPDX-License-Identifier: Apache-2.0
 *
 * Change Logs:
 * Date                 Author      Notes
 * 2025-11-23           Andy        the first version
 */

using Erp.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Erp.Toolkit.Sample
{
    public partial class WinFormSample : Form
    {
        // 创建控件
        private Erp.Toolkit.Controls.Dgv dgv = new Erp.Toolkit.Controls.Dgv();

        public WinFormSample()
        {
            InitializeComponent();

            // 示例数据
            var sampleData = GenerateSampleData();

            // 呈现在UI层
            Controls.Add(dgv);
            dgv.Dock = DockStyle.Fill;

            // 填充顶级主视图的数据
            dgv.FillList(sampleData, this.Name);

            // 设置主题
            dgv.ThemeStyle = ThemeStyle.blue;

            // 自定义用户菜单或工具条
            List<DgvUserContextMenuStripConfig> menuConfigs = new List<DgvUserContextMenuStripConfig>
            {
                new DgvUserContextMenuStripConfig
                {
                    MenuText = "详细档案",
                    Target = MenuShowTarget.ToolStrip | MenuShowTarget.ContextMenuStrip,
                    Group = 1,
                    ClickHandler = (senders, es) => {
                        // 简单测试，弹出窗口显示选中项ID
                        var winFrom = new WinFormSample();
                        winFrom.Text = $"查看员工 {dgv.GetSelectedItemIds()} 的详细档案";
                        winFrom.ShowDialog();
                    }
                },
            };

            // 构建用户菜单配置
            dgv.SetUserContextMenu(menuConfigs);
        }

        /// <summary>
        /// 生成示例数据
        /// </summary>
        /// <returns></returns>
        private List<SampleData> GenerateSampleData()
        {
            return new List<SampleData>
            {
                new SampleData
                {
                    Id = 1,
                    Name = "张明",
                    Age = 28,
                    Department = "技术部",
                    Position = "高级软件工程师",
                    Salary = 15000m,
                    JoinDate = new DateTime(2020, 3, 15),
                    Email = "zhangming@doipc.com",
                    Phone = "13800138001",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 2,
                    Name = "李芳",
                    Age = 32,
                    Department = "人力资源部",
                    Position = "HR经理",
                    Salary = 12000m,
                    JoinDate = new DateTime(2019, 7, 22),
                    Email = "lifang@doipc.com",
                    Phone = "13900139002",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 3,
                    Name = "王强",
                    Age = 25,
                    Department = "市场部",
                    Position = "市场专员",
                    Salary = 8000m,
                    JoinDate = new DateTime(2022, 1, 10),
                    Email = "wangqiang@doipc.com",
                    Phone = "13700137003",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 4,
                    Name = "刘小红",
                    Age = 35,
                    Department = "财务部",
                    Position = "财务主管",
                    Salary = 18000m,
                    JoinDate = new DateTime(2018, 5, 6),
                    Email = "liuxiaohong@doipc.com",
                    Phone = "13600136004",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 5,
                    Name = "陈建国",
                    Age = 45,
                    Department = "管理层",
                    Position = "技术总监",
                    Salary = 25000m,
                    JoinDate = new DateTime(2015, 11, 30),
                    Email = "chenjianguo@doipc.com",
                    Phone = "13500135005",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 6,
                    Name = "赵婷婷",
                    Age = 29,
                    Department = "设计部",
                    Position = "UI设计师",
                    Salary = 11000m,
                    JoinDate = new DateTime(2021, 8, 14),
                    Email = "zhaotingting@doipc.com",
                    Phone = "13400134006",
                    IsActive = false
                },
                new SampleData
                {
                    Id = 7,
                    Name = "孙伟",
                    Age = 31,
                    Department = "技术部",
                    Position = "后端开发工程师",
                    Salary = 14000m,
                    JoinDate = new DateTime(2020, 9, 3),
                    Email = "sunwei@doipc.com",
                    Phone = "13300133007",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 8,
                    Name = "周丽",
                    Age = 27,
                    Department = "客服部",
                    Position = "客服主管",
                    Salary = 9000m,
                    JoinDate = new DateTime(2021, 12, 1),
                    Email = "zhouli@doipc.com",
                    Phone = "13200132008",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 9,
                    Name = "吴刚",
                    Age = 38,
                    Department = "运维部",
                    Position = "系统运维工程师",
                    Salary = 13000m,
                    JoinDate = new DateTime(2017, 4, 18),
                    Email = "wugang@doipc.com",
                    Phone = "13100131009",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 10,
                    Name = "郑美丽",
                    Age = 26,
                    Department = "市场部",
                    Position = "市场策划",
                    Salary = 8500m,
                    JoinDate = new DateTime(2023, 2, 28),
                    Email = "zhengmeili@doipc.com",
                    Phone = "13000130010",
                    IsActive = true
                }
            };
        }
    }

    /// <summary>
    /// 示例数据模型
    /// </summary>
    internal class SampleData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string Status => IsActive ? "在职" : "离职";
        public int WorkYears => DateTime.Now.Year - JoinDate.Year;
    }
}