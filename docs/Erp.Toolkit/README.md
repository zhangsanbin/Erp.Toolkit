# Erp.Toolkit

[![Docker Image Size](https://img.shields.io/docker/image-size/doipc/erpwebapi?color=0db7ed)](https://hub.docker.com/r/doipc/erpwebapi)
[![License](https://img.shields.io/github/license/zhangsanbin/my-project-assets?color=97ca00)](./LICENSE)
[![Docker](https://img.shields.io/badge/docker-latest-0db7ed?logo=docker)](https://hub.docker.com/r/doipc/erpwebapi)
[![GitHub commit activity](https://img.shields.io/github/commit-activity/y/zhangsanbin/my-project-assets?color=fe7d37)](https://github.com/zhangsanbin/my-project-assets)
[![Pulls](https://img.shields.io/docker/pulls/doipc/erpwebapi?color=6f42c1)](https://hub.docker.com/r/doipc/erpwebapi)
[![NuGet Version](https://img.shields.io/nuget/v/Erp.Toolkit?color=005cc5)](https://www.nuget.org/packages/Erp.Toolkit)

[English](README.md) | [中文](https://github.com/zhangsanbin/my-project-assets/blob/main/docs/Erp.Toolkit/README_zh.md)

An open-source, enterprise-grade, deeply customizable user control component library, providing a rich set of WinForms UI components and tools.

This package is an extension for the ERP 2.0 project, aimed at simplifying the display of master-detail structured data within projects and providing common basic functionalities.

## Features

- High-performance DataGridView control
- Multi-target framework support (.NET Framework 4.6.2+ and .NET Core 3.1+)
- Rich theme system
- Conditional styling and formatting
- Master-detail grid views
- Customizable column configuration
- Statistics, printing, and exporting
- Built-in search and filtering capabilities
- More enterprise-level UI components

## Supported Frameworks

- .NET Framework 4.6.2, 4.7.2, 4.8
- .NET Core 3.1
- .NET 6.0, 7.0, 8.0 (Windows)

## Preview
![Preview](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/images/Erp.Toolkit/erp.toolkit.sample.png)

## How to Pack

```powershell
# Pack the project
dotnet pack --configuration Release

# Specify output directory
dotnet pack --configuration Release --output ./nupkgs

# Upload to NuGet server using dotnet CLI
dotnet nuget push ./nupkgs/Erp.Toolkit.0.3.6.nupkg --api-key API_KEY --source https://api.nuget.org/v3/index.json
```

## Installation

Install via NuGet:

```powershell
Install-Package Erp.Toolkit
```

## How to Use

| [Quick Start](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/samples/Erp.Toolkit/WinFormSample.cs) | [Full Example](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/samples/Erp.Toolkit/WinFormExampleFull.cs) | [NuGet Package](https://www.nuget.org/packages/Erp.Toolkit) |
| -- | -- | -- |

```csharp
// Create control
private Erp.Toolkit.Controls.Dgv dgv = new Erp.Toolkit.Controls.Dgv();

// Sample data
var sampleData = GenerateSampleData();

// Render on UI layer
Controls.Add(dgv);
dgv.Dock = DockStyle.Fill;

// Populate data for the top-level master view
dgv.FillList(sampleData, this.Name);

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
            var winFrom = new WinFormSample();
            winFrom.Text = $"View detailed profile for employee {dgv.GetSelectedItemIds()}";
            winFrom.ShowDialog();
        }
    },
};

// Build user menu configuration
dgv.SetUserContextMenu(menuConfigs);
```

### Sample Data

```csharp
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
```

```csharp
private List<SampleData> GenerateSampleData()
{
    return new List<SampleData>
    {
        new SampleData
        {
            Id = 1,
            Name = "Zhang Ming",
            Age = 28,
            Department = "Technology Department",
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
        }
    };
}
```

## Sample Code

- [Quick Start](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/samples/Erp.Toolkit/WinFormSample.cs)
- [Full Example](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/samples/Erp.Toolkit/WinFormExampleFull.cs)
- [Conditional Style Configuration](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/scripts/Erp.Toolkit/WINFORMSAMPLE.CFG.JSON)
- [Custom Column Configuration](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/scripts/Erp.Toolkit/WINFORMSAMPLE.COL.JSON)
- [NuGet Package](https://www.nuget.org/packages/Erp.Toolkit)
- [English Readme](https://github.com/zhangsanbin/my-project-assets/blob/main/docs/Erp.Toolkit/README.md)
- [Chinese Readme](https://github.com/zhangsanbin/my-project-assets/blob/main/docs/Erp.Toolkit/README_zh.md)

> [!TIP]
> Erp.Toolkit - Making enterprise development less complex!
> - A toolset focused on ERP scenarios
> - Developed based on real enterprise requirements
> - Proven in production environments
> - Continuously updated and maintained
> - Free and open source, licensed under Apache-2.0 license, commercial use welcome
> - Please submit `Issues` for any problems; `PRs` are welcome for known errors
>> *Erp.Toolkit: A powerful set of tools and components for building ERP systems efficiently.*

---

> @zhangsanbin :+1: This project looks great - it's ready to pull!
> - [x] I have reviewed the code changes.