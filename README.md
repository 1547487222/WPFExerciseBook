# Laboratory Management System (实验室管理系统)

A WPF-based laboratory management system with a VS Code-like interface for managing laboratory projects, modules, resources, platform tasks, and production lines.

## Features (功能特性)

### VS Code-like UI (仿VS Code界面)
- Left activity bar with icons for quick navigation
- Sidebar panel for context-specific content
- Main content area for detailed views
- Dark theme inspired by VS Code

### Core Functionality (核心功能)

1. **Laboratory Projects (实验室项目)**
   - Create and manage laboratory projects
   - Reference modules and resources
   - Track project status and timeline

2. **Modules (模块)**
   - Define and manage functional modules
   - Support for different module types:
     - Standard modules
     - Warehouse modules (仓库)
     - Robot modules (机器人)
     - AGV modules
     - Transfer modules (中转)
   - Module actions and parameters

3. **Resources (资源)**
   - Manage laboratory resources
   - Track resource status and availability
   - Support for equipment, materials, tools, and facilities

4. **Platform Tasks (平台任务)**
   - Define executable platform tasks
   - Workflow composition with multiple steps
   - Integration with gRPC PlatformCallService
   - Task execution and monitoring

5. **Production Lines (产线工艺流程)**
   - Create production line workflows
   - Compose processes from:
     - Platform tasks (平台任务)
     - Transfer operations (中转)
     - Module actions (模块动作)
   - Multiple processes per production line

## Architecture (架构)

### Project Structure
```
LaboratoryManagementSystem/
├── Models/              # Data models
│   └── LaboratoryModels.cs
├── ViewModels/          # MVVM ViewModels
│   ├── ViewModelBase.cs
│   ├── RelayCommand.cs
│   └── MainViewModel.cs
├── Views/               # WPF Views
│   └── MainWindow.xaml
├── Services/            # Business logic services
├── Controls/            # Custom WPF controls
└── Protos/              # gRPC protocol definitions
    └── platformcall.proto
```

### Technology Stack
- .NET 8.0 (Windows)
- WPF (Windows Presentation Foundation)
- MVVM (Model-View-ViewModel) pattern
- gRPC for platform communication
- Protocol Buffers

## LaboratoryConfig Structure (实验室配置结构)

The `LaboratoryConfig` class is the main configuration structure containing:
- Laboratory projects
- Available modules
- Resources
- Platform tasks
- Production lines

Each production line can contain multiple production processes, and each process is composed of steps that reference platform tasks, transfers, or module actions.

## gRPC Interface (gRPC接口)

The system includes a `PlatformCallService` gRPC interface for:
- Executing platform tasks
- Getting task status
- Canceling running tasks

See `Protos/platformcall.proto` for the complete interface definition.

## Building and Running (构建和运行)

### Prerequisites
- .NET 8.0 SDK or later
- Windows operating system (for WPF)

### Build
```bash
dotnet build LaboratoryManagementSystem.sln
```

### Run
```bash
dotnet run --project LaboratoryManagementSystem/LaboratoryManagementSystem.csproj
```

## Concept: "Everything is a Module" (万物皆是模块)

The system is built on the philosophy that everything in the laboratory can be represented as a module. This includes:
- Standard operational modules
- Facility modules (warehouses, robots, AGVs)
- Transfer operations
- Custom modules

This modular approach allows for flexible composition of workflows where platform tasks and production processes can be built by combining these modules in different ways.

## License

MIT License - See LICENSE file for details
