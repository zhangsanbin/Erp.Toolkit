# Erp.Toolkit

[![Docker Image Size](https://img.shields.io/docker/image-size/doipc/erpwebapi?color=0db7ed)](https://hub.docker.com/r/doipc/erpwebapi)
[![License](https://img.shields.io/github/license/zhangsanbin/my-project-assets?color=97ca00)](./LICENSE)
[![Docker](https://img.shields.io/badge/docker-latest-0db7ed?logo=docker)](https://hub.docker.com/r/doipc/erpwebapi)
[![GitHub commit activity](https://img.shields.io/github/commit-activity/y/zhangsanbin/my-project-assets?color=fe7d37)](https://github.com/zhangsanbin/my-project-assets)
[![Pulls](https://img.shields.io/docker/pulls/doipc/erpwebapi?color=6f42c1)](https://hub.docker.com/r/doipc/erpwebapi)
[![NuGet Version](https://img.shields.io/nuget/v/Erp.Toolkit?color=005cc5)](https://www.nuget.org/packages/Erp.Toolkit)

[English](README.md) | [中文](README_zh.md)

一个开源的企业级，深度定制用户控件组件库，提供丰富的 WinForms UI 组件和工具。

此包是 ERP 2.0 项目的扩展，其目的是为了简化项目中主从结构数据的展示，并提供常用基础功能。

## 特性

- 高性能的 DataGridView 控件
- 多目标框架支持 (.NET Framework 4.6.2+ 和 .NET Core 3.1+)
- 丰富的主题系统
- 条件样式和格式化
- 主从表格视图
- 自定义列配置
- 统计、打印和导出
- 内置搜索和过滤功能
- 更多企业级 UI 组件

## 支持的框架

- .NET Framework 4.6.2, 4.7.2, 4.8
- .NET Core 3.1
- .NET 6.0, 7.0, 8.0 (Windows)

## 预览
![示例图](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/images/Erp.Toolkit/erp.toolkit.sample.png)

## 如何打包

```powershell
# 打包项目
dotnet pack --configuration Release

# 指定输出目录
dotnet pack --configuration Release --output ./nupkgs

# 使用 dotnet CLI 上传到 NuGet 服务器
dotnet nuget push ./nupkgs/Erp.Toolkit.0.3.6.nupkg --api-key API_KEY --source https://api.nuget.org/v3/index.json
```

## 安装

使用NuGet安装：

```powershell
Install-Package Erp.Toolkit
```

## 如何使用

| [快速入门](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/samples/Erp.Toolkit/WinFormSample.cs) | [完整示例](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/samples/Erp.Toolkit/WinFormExampleFull.cs) | [NuGet程序包](https://www.nuget.org/packages/Erp.Toolkit) |
| -- | -- | -- |

```csharp
// 创建控件
private Erp.Toolkit.Controls.Dgv dgv = new Erp.Toolkit.Controls.Dgv();

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
            var winFrom = new WinFormSample();
            winFrom.Text = $"查看员工 {dgv.GetSelectedItemIds()} 的详细档案";
            winFrom.ShowDialog();
        }
    },
};

// 构建用户菜单配置
dgv.SetUserContextMenu(menuConfigs);
```

### 示例数据

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
    public string Status => IsActive ? "在职" : "离职";
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
        }
    };
}
```

## 示例代码

- [快速入门](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/samples/Erp.Toolkit/WinFormSample.cs)
- [完整示例](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/samples/Erp.Toolkit/WinFormExampleFull.cs)
- [条件样式配置](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/scripts/Erp.Toolkit/WINFORMSAMPLE.CFG.JSON)
- [自定义列配置](https://raw.githubusercontent.com/zhangsanbin/my-project-assets/refs/heads/main/scripts/Erp.Toolkit/WINFORMSAMPLE.COL.JSON)
- [NuGet程序包](https://www.nuget.org/packages/Erp.Toolkit)
- [英文自述](https://github.com/zhangsanbin/my-project-assets/blob/main/docs/Erp.Toolkit/README.md)
- [中文自述](https://github.com/zhangsanbin/my-project-assets/blob/main/docs/Erp.Toolkit/README_zh.md)

> [!TIP]
> Erp.Toolkit - 让企业级开发不再复杂！
> - 专注 ERP 场景的工具集合
> - 基于真实企业需求开发
> - 经过生产环境验证
> - 持续更新维护
> - 免费开源，遵循 Apache-2.0 license 许可证，欢迎商用
> - 若有问题请发 `Issues` 上报，对于已知的错误欢迎 `PR`
>> *Erp.Toolkit: A powerful set of tools and components for building ERP systems efficiently.*

---

> @zhangsanbin :+1: This project looks great - it's ready to pull!
> - [x] I have reviewed the code changes.