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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Erp.Toolkit.ExampleFull
{
    /// <summary>
    /// Erp.Toolkit WinForm 使用示例
    /// </summary>
    public partial class WinFormExampleFull : Form
    {
        // 创建控件
        private Erp.Toolkit.Controls.Dgv dgv = new Erp.Toolkit.Controls.Dgv();

        // 示例数据源（项目）
        private List<ProjectData> _allProjectData;

        public WinFormExampleFull()
        {
            InitializeComponent();

            // 示例数据
            var sampleData = GenerateSampleData();
            _allProjectData = GenerateProjectData(sampleData);

            // 呈现在UI层
            Controls.Add(dgv);
            dgv.Dock = DockStyle.Fill;

            // 填充顶级主视图的数据
            dgv.FillList(sampleData, this.Name);

            // 启用子视图并初始化（不填充数据）
            dgv.SubviewsEnable();

            // 开启，百分比进度条显示模式
            dgv.subview.ProgressColumnsName = "Progress";

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
                        var winFrom = new WinFormExampleFull();
                        winFrom.Text = $"查看员工 {dgv.GetSelectedItemIds()} 的详细档案";
                        winFrom.ShowDialog();
                    }
                }
            };

            // 构建用户菜单配置
            dgv.SetUserContextMenu(menuConfigs);

            // 订阅事件
            SubscribeEvent();
        }

        /// <summary>
        /// 订阅所需的事件
        /// </summary>
        private void SubscribeEvent()
        {
            // 展开事件
            dgv.MasterSlaveDataExpand += Dgv_MasterSlaveDataExpand;

            // 双击事件
            dgv.DoubleClickDgv += Dgv_DoubleClickDgv;

            // 新增事件
            dgv.AddDgv += Dgv_AddClickDgv;

            // 删除事件
            dgv.DeleteDgv += Dgv_DeleteDgv;

            // 根据事件重新刷新按钮状态
            dgv.RefreshButtonState();
        }

        /// <summary>
        /// 主从数据展开事件
        /// </summary>
        private void Dgv_MasterSlaveDataExpand(object sender, DataGridViewCellMouseEventArgs e, string id, Rectangle rect)
        {
            if (int.TryParse(id, out int userId))
            {
                // 根据用户ID获取对应的项目数据
                var userProjects = _allProjectData
                    .Where(p => p.UserId == userId)
                    .ToList();

                // 填充子数据
                dgv.FillSubviewWithList(userProjects);

                // 设置二级子视图的工具条不可见
                dgv.subview.ToolStripVisible = false;
            }
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Dgv_DoubleClickDgv(object sender, EventArgs e, string id)
        {
            // 调用 API 接口，或其他操作
            Console.WriteLine($"双击了 ID 为 {id} 的行");
        }

        /// <summary>
        /// 新增事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Dgv_AddClickDgv(object sender, EventArgs e)
        {
            // 调用 API 接口，或其他操作
            Console.WriteLine("点击了新增按钮");
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Dgv_DeleteDgv(object sender, DgvDeleteEventArgs e, string id)
        {
            // 调用 API 接口，或其他操作
            Console.WriteLine("点击了删除按钮，待删除 ID 列表：" + id);
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

        /// <summary>
        /// 生成项目数据
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        private List<ProjectData> GenerateProjectData(List<SampleData> employees)
        {
            var projects = new List<ProjectData>();
            var random = new Random();

            foreach (var employee in employees)
            {
                // 每个员工分配2-5个项目
                int projectCount = random.Next(2, 6);

                for (int i = 0; i < projectCount; i++)
                {
                    var startDate = DateTime.Now.AddDays(-random.Next(30, 180));
                    var endDate = startDate.AddDays(random.Next(30, 180));
                    var progress = random.Next(0, 101);

                    string status;
                    if (progress == 0)
                        status = "未开始";
                    else if (progress == 100)
                        status = "已完成";
                    else if (progress < 50)
                        status = "进行中";
                    else
                        status = "延期";

                    projects.Add(new ProjectData
                    {
                        Id = projects.Count + 1,
                        UserId = employee.Id,
                        ProjectName = $"项目-{employee.Name}-{i + 1}",
                        Progress = progress,
                        Status = status,
                        StartDate = startDate,
                        EndDate = endDate,
                        CurrentMilestone = GetMilestoneByProgress(progress)
                    });
                }
            }

            return projects;
        }

        /// <summary>
        /// 根据进度获取当前节点
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        private string GetMilestoneByProgress(int progress)
        {
            if (progress < 20)
                return "需求分析";
            else if (progress < 40)
                return "设计阶段";
            else if (progress < 60)
                return "开发阶段";
            else if (progress < 80)
                return "测试阶段";
            else if (progress < 100)
                return "上线准备";
            else
                return "项目完成";
        }
    }

    /// <summary>
    /// 示例数据类
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

    /// <summary>
    /// 项目数据类
    /// </summary>
    internal class ProjectData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProjectName { get; set; }
        public int Progress { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CurrentMilestone { get; set; }
    }
}