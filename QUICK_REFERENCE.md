# Quick Reference Guide - Laboratory Management System
# 快速参考指南 - 实验室管理系统

## Quick Start (快速开始)

### Build & Run
```bash
# Build the solution
dotnet build LaboratoryManagementSystem.sln

# Run the application
dotnet run --project LaboratoryManagementSystem/LaboratoryManagementSystem.csproj
```

### Project Structure
```
Models/           # Domain models
ViewModels/       # MVVM ViewModels  
Views/            # XAML views
Controls/         # Custom controls
Services/         # Business services
Protos/           # gRPC definitions
```

## Key Classes (关键类)

### Models
- **LaboratoryConfig** - Root configuration
- **Module** - Functional modules (Warehouse, Robot, AGV, etc.)
- **PlatformTask** - Executable workflows
- **ProductionLine** - Production processes

### ViewModels
- **MainViewModel** - Application state
- **ViewModelBase** - INotifyPropertyChanged base
- **RelayCommand** - ICommand implementation

### Services
- **ILaboratoryConfigService** - Configuration persistence
- **IPlatformTaskService** - Task execution
- **JsonConfigurationService** - JSON implementation

## Common Tasks (常用任务)

### Add New Module Type
```csharp
// 1. Update enum in Models/LaboratoryModels.cs
public enum ModuleType
{
    // ...
    NewType
}

// 2. Create module instance
var module = new Module
{
    Name = "New Module",
    Type = ModuleType.NewType
};

// 3. Add to configuration
LaboratoryConfig.Modules.Add(module);
```

### Add New Command
```csharp
// 1. In ViewModel, declare command property
public ICommand? MyCommand { get; private set; }

// 2. Initialize in constructor
MyCommand = new RelayCommand(_ => ExecuteMyCommand());

// 3. Implement method
private void ExecuteMyCommand()
{
    // Command logic
}

// 4. Bind in XAML
<Button Command="{Binding MyCommand}"/>
```

### Add New View
```xml
<!-- 1. Create XAML file in Views/ -->
<UserControl x:Class="LaboratoryManagementSystem.Views.MyView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <!-- View content -->
</UserControl>
```

```csharp
// 2. Create code-behind
namespace LaboratoryManagementSystem.Views
{
    public partial class MyView : UserControl
    {
        public MyView()
        {
            InitializeComponent();
        }
    }
}
```

### Save Configuration
```csharp
// Automatic on changes
await _configService.SaveConfigAsync(LaboratoryConfig, _defaultConfigPath);

// Via menu command
Command="{Binding SaveConfigCommand}"

// Keyboard shortcut
Ctrl+S
```

## Data Binding Patterns (数据绑定模式)

### Simple Property Binding
```xml
<TextBlock Text="{Binding Name}"/>
```

### Collection Binding
```xml
<ListBox ItemsSource="{Binding LaboratoryConfig.Modules}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <TextBlock Text="{Binding Name}"/>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

### Command Binding
```xml
<Button Content="Save" Command="{Binding SaveCommand}"/>
```

### Two-Way Binding
```xml
<TextBox Text="{Binding Name, Mode=TwoWay}"/>
```

## Color Scheme (配色方案)

```xml
<!-- VS Code Dark Theme -->
<SolidColorBrush x:Key="BackgroundBrush" Color="#1E1E1E"/>
<SolidColorBrush x:Key="SidebarBrush" Color="#252526"/>
<SolidColorBrush x:Key="ActivityBarBrush" Color="#333333"/>
<SolidColorBrush x:Key="ForegroundBrush" Color="#CCCCCC"/>
<SolidColorBrush x:Key="AccentBrush" Color="#007ACC"/>
<SolidColorBrush x:Key="BorderBrush" Color="#3E3E42"/>
```

## Configuration Files (配置文件)

### Default Location
```
%APPDATA%/LaboratoryManagementSystem/config.json
```

### Configuration Structure
```json
{
  "id": "guid",
  "name": "Lab Config",
  "projects": [],
  "modules": [
    {
      "id": "guid",
      "name": "Module Name",
      "type": "warehouse",
      "actions": []
    }
  ],
  "resources": [],
  "platformTasks": [],
  "productionLines": []
}
```

## gRPC Service (gRPC服务)

### Protocol Definition
```protobuf
service PlatformCallService {
  rpc ExecutePlatformTask (PlatformTaskRequest) returns (PlatformTaskResponse);
  rpc GetTaskStatus (TaskStatusRequest) returns (TaskStatusResponse);
  rpc CancelTask (CancelTaskRequest) returns (CancelTaskResponse);
}
```

### Usage Example
```csharp
// In production, connect to actual gRPC service
var channel = GrpcChannel.ForAddress("https://localhost:5001");
var client = new PlatformCallService.PlatformCallServiceClient(channel);

var request = new PlatformTaskRequest
{
    TaskId = task.Id,
    TaskName = task.Name
};

var response = await client.ExecutePlatformTaskAsync(request);
```

## Keyboard Shortcuts (键盘快捷键)

| Shortcut | Action |
|----------|--------|
| Ctrl+S | Save Configuration |
| Ctrl+O | Load Configuration |
| F1 | User Guide (planned) |

## Icons Reference (图标参考)

| Icon | Meaning | Usage |
|------|---------|-------|
| 📁 | Projects | Laboratory projects view |
| 🧩 | Modules | Module management |
| 📦 | Resources | Resource tracking |
| ⚙️ | Tasks | Platform tasks |
| 🏭 | Lines | Production lines |
| 💾 | Save | Save action |
| ▶️ | Execute | Run task |
| 🗑️ | Delete | Remove item |

## Module Types (模块类型)

| Type | Chinese | Icon | Purpose |
|------|---------|------|---------|
| Standard | 标准 | - | General purpose |
| Warehouse | 仓库 | 🏢 | Storage operations |
| Robot | 机器人 | 🤖 | Manipulation |
| AGV | AGV | 🚗 | Transport |
| Transfer | 中转 | ↔️ | Transfer operations |

## Process Step Types (流程步骤类型)

| Type | Chinese | Color | Usage |
|------|---------|-------|-------|
| PlatformTask | 平台任务 | Blue (#4EC9B0) | Complete task |
| Transfer | 中转 | Orange (#CE9178) | Material transfer |
| ModuleAction | 模块动作 | Various | Direct action |

## Common Patterns (常用模式)

### Adding Item to Collection
```csharp
// Create new item
var item = new Module
{
    Name = "New Module",
    Description = "Description"
};

// Add to observable collection
LaboratoryConfig.Modules.Add(item);
// UI updates automatically
```

### Handling Async Operations
```csharp
private async void SaveConfiguration()
{
    try
    {
        await _configService.SaveConfigAsync(config, path);
        // Success
    }
    catch (Exception ex)
    {
        // Handle error
        MessageBox.Show(ex.Message);
    }
}
```

### Observable Collection Pattern
```csharp
// In model/ViewModel
public ObservableCollection<Module> Modules { get; set; } = new();

// In XAML
<ListBox ItemsSource="{Binding Modules}"/>
// Auto-updates when Modules changes
```

## Debugging Tips (调试技巧)

### Check Data Binding
```xml
<!-- Add Diagnostics namespace -->
xmlns:diag="clr-namespace:System.Diagnostics;assembly=WindowsBase"

<!-- Enable trace output -->
<TextBlock Text="{Binding Name, diag:PresentationTraceSources.TraceLevel=High}"/>
```

### ViewModel Inspection
- Set breakpoints in command methods
- Check property values in watch window
- Verify ObservableCollections are not null

### XAML Hot Reload
- Make XAML changes while debugging
- See changes immediately without restart
- Only for UI changes, not code-behind

## Performance Tips (性能提示)

1. **Use VirtualizingStackPanel** for large lists
2. **Avoid excessive property change notifications**
3. **Use async/await** for long operations
4. **Dispose** of resources properly
5. **Cache** repeated calculations

## Error Handling (错误处理)

```csharp
// Service layer
public async Task<LaboratoryConfig> LoadConfigAsync(string path)
{
    try
    {
        // Load logic
    }
    catch (FileNotFoundException ex)
    {
        // Return default
        return CreateDefaultConfiguration();
    }
    catch (Exception ex)
    {
        // Log and re-throw
        Console.WriteLine($"Error: {ex.Message}");
        throw;
    }
}

// ViewModel layer
private async void LoadConfiguration()
{
    try
    {
        var config = await _configService.LoadConfigAsync(path);
        LaboratoryConfig = config;
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Failed to load: {ex.Message}");
    }
}
```

## Documentation Links (文档链接)

- **README.md** - Project overview
- **ARCHITECTURE.md** - Architecture details
- **USER_GUIDE.md** - User manual
- **IMPLEMENTATION_SUMMARY.md** - Implementation details

## Support (支持)

For issues or questions:
1. Check USER_GUIDE.md
2. Review ARCHITECTURE.md
3. Check implementation code
4. Create GitHub issue

---

*Quick Reference Version 1.0*
