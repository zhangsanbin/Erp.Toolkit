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

namespace Erp.Toolkit.Example
{
    /// <summary>
    /// Erp.Toolkit WinForm Usage Example
    /// </summary>
    public partial class WinFormExampleFull : Form
    {
        // Create control
        private Erp.Toolkit.Controls.Dgv dgv = new Erp.Toolkit.Controls.Dgv();

        // Sample data source (projects)
        private List<ProjectData> _allProjectData;

        public WinFormExampleFull()
        {
            InitializeComponent();

            // Sample data
            var sampleData = GenerateSampleData();
            _allProjectData = GenerateProjectData(sampleData);

            // Render at UI level
            Controls.Add(dgv);
            dgv.Dock = DockStyle.Fill;

            // Fill data for the top-level main view
            dgv.FillList(sampleData, this.Name);

            // Enable subview and initialize (without filling data)
            dgv.SubviewsEnable();

            // Enable, percentage progress bar display mode
            dgv.subview.ProgressColumnName = "Progress";

            // Set theme
            dgv.ThemeStyle = ThemeStyle.blue;

            // Custom user menu or toolbar
            List<DgvUserContextMenuStripConfig> menuConfigs = new List<DgvUserContextMenuStripConfig>
            {
                new DgvUserContextMenuStripConfig
                {
                    MenuText = "Detailed Profile",
                    Target = MenuShowTarget.ToolStrip | MenuShowTarget.ContextMenuStrip,
                    Group = 1,
                    ClickHandler = (senders, es) => {
                        var winFrom = new WinFormExampleFull();
                        winFrom.Text = $"View detailed profile for employee {dgv.GetSelectedItemIds()}";
                        winFrom.ShowDialog();
                    }
                }
            };

            // Build user menu configuration
            dgv.SetUserContextMenu(menuConfigs);

            // Subscribe to events
            SubscribeEvent();
        }

        /// <summary>
        /// Subscribe to required events
        /// </summary>
        private void SubscribeEvent()
        {
            // Expand event
            dgv.MasterSlaveDataExpand += Dgv_MasterSlaveDataExpand;

            // Double-click event
            dgv.DoubleClickDgv += Dgv_DoubleClickDgv;

            // Add event
            dgv.AddDgv += Dgv_AddClickDgv;

            // Delete event
            dgv.DeleteDgv += Dgv_DeleteDgv;

            // Refresh button state based on event
            dgv.RefreshButtonState();
        }

        /// <summary>
        /// Master-detail data expand event
        /// </summary>
        private void Dgv_MasterSlaveDataExpand(object sender, DataGridViewCellMouseEventArgs e, string id, Rectangle rect)
        {
            if (int.TryParse(id, out int userId))
            {
                // Get corresponding project data based on user ID
                var userProjects = _allProjectData
                    .Where(p => p.UserId == userId)
                    .ToList();

                // Fill subview data
                dgv.FillSubviewWithList(userProjects);
            }
        }

        /// <summary>
        /// Double-click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Dgv_DoubleClickDgv(object sender, EventArgs e, string id)
        {
            // Call API interface, or other operations
            Console.WriteLine($"Double-clicked row with ID {id}");
        }

        /// <summary>
        /// Add event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Dgv_AddClickDgv(object sender, EventArgs e)
        {
            // Call API interface, or other operations
            Console.WriteLine("Clicked the Add button");
        }

        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Dgv_DeleteDgv(object sender, DgvDeleteEventArgs e, string id)
        {
            // Call API interface, or other operations
            Console.WriteLine("Clicked the Delete button, pending deletion ID list: " + id);
        }

        /// <summary>
        /// Generate sample data
        /// </summary>
        /// <returns></returns>
        private List<SampleData> GenerateSampleData()
        {
            return new List<SampleData>
            {
                new SampleData
                {
                    Id = 1,
                    Name = "Zhang Ming",
                    Age = 28,
                    Department = "Technical Department",
                    Position = "Senior Software Engineer",
                    Salary = 15000m,
                    JoinDate = new DateTime(2020, 3, 15),
                    Email = "zhangming@doipc.com",
                    Phone = "13800138001",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 2,
                    Name = "Li Fang",
                    Age = 32,
                    Department = "Human Resources Department",
                    Position = "HR Manager",
                    Salary = 12000m,
                    JoinDate = new DateTime(2019, 7, 22),
                    Email = "lifang@doipc.com",
                    Phone = "13900139002",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 3,
                    Name = "Wang Qiang",
                    Age = 25,
                    Department = "Marketing Department",
                    Position = "Marketing Specialist",
                    Salary = 8000m,
                    JoinDate = new DateTime(2022, 1, 10),
                    Email = "wangqiang@doipc.com",
                    Phone = "13700137003",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 4,
                    Name = "Liu Xiaohong",
                    Age = 35,
                    Department = "Finance Department",
                    Position = "Finance Supervisor",
                    Salary = 18000m,
                    JoinDate = new DateTime(2018, 5, 6),
                    Email = "liuxiaohong@doipc.com",
                    Phone = "13600136004",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 5,
                    Name = "Chen Jianguo",
                    Age = 45,
                    Department = "Management",
                    Position = "Technical Director",
                    Salary = 25000m,
                    JoinDate = new DateTime(2015, 11, 30),
                    Email = "chenjianguo@doipc.com",
                    Phone = "13500135005",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 6,
                    Name = "Zhao Tingting",
                    Age = 29,
                    Department = "Design Department",
                    Position = "UI Designer",
                    Salary = 11000m,
                    JoinDate = new DateTime(2021, 8, 14),
                    Email = "zhaotingting@doipc.com",
                    Phone = "13400134006",
                    IsActive = false
                },
                new SampleData
                {
                    Id = 7,
                    Name = "Sun Wei",
                    Age = 31,
                    Department = "Technical Department",
                    Position = "Backend Development Engineer",
                    Salary = 14000m,
                    JoinDate = new DateTime(2020, 9, 3),
                    Email = "sunwei@doipc.com",
                    Phone = "13300133007",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 8,
                    Name = "Zhou Li",
                    Age = 27,
                    Department = "Customer Service Department",
                    Position = "Customer Service Supervisor",
                    Salary = 9000m,
                    JoinDate = new DateTime(2021, 12, 1),
                    Email = "zhouli@doipc.com",
                    Phone = "13200132008",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 9,
                    Name = "Wu Gang",
                    Age = 38,
                    Department = "Operations Department",
                    Position = "System Operations Engineer",
                    Salary = 13000m,
                    JoinDate = new DateTime(2017, 4, 18),
                    Email = "wugang@doipc.com",
                    Phone = "13100131009",
                    IsActive = true
                },
                new SampleData
                {
                    Id = 10,
                    Name = "Zheng Meili",
                    Age = 26,
                    Department = "Marketing Department",
                    Position = "Marketing Planner",
                    Salary = 8500m,
                    JoinDate = new DateTime(2023, 2, 28),
                    Email = "zhengmeili@doipc.com",
                    Phone = "13000130010",
                    IsActive = true
                }
            };
        }

        /// <summary>
        /// Generate project data
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        private List<ProjectData> GenerateProjectData(List<SampleData> employees)
        {
            var projects = new List<ProjectData>();
            var random = new Random();

            foreach (var employee in employees)
            {
                // Assign 2-5 projects to each employee
                int projectCount = random.Next(2, 6);

                for (int i = 0; i < projectCount; i++)
                {
                    var startDate = DateTime.Now.AddDays(-random.Next(30, 180));
                    var endDate = startDate.AddDays(random.Next(30, 180));
                    var progress = random.Next(0, 101);

                    string status;
                    if (progress == 0)
                        status = "Not Started";
                    else if (progress == 100)
                        status = "Completed";
                    else if (progress < 50)
                        status = "In Progress";
                    else
                        status = "Delayed";

                    projects.Add(new ProjectData
                    {
                        Id = projects.Count + 1,
                        UserId = employee.Id,
                        ProjectName = $"Project-{employee.Name}-{i + 1}",
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
        /// Get current milestone based on progress
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        private string GetMilestoneByProgress(int progress)
        {
            if (progress < 20)
                return "Requirements Analysis";
            else if (progress < 40)
                return "Design Phase";
            else if (progress < 60)
                return "Development Phase";
            else if (progress < 80)
                return "Testing Phase";
            else if (progress < 100)
                return "Launch Preparation";
            else
                return "Project Completed";
        }
    }

    /// <summary>
    /// Sample data class
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
        public string Status => IsActive ? "Active" : "Inactive";
        public int WorkYears => DateTime.Now.Year - JoinDate.Year;
    }

    /// <summary>
    /// Project data class
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