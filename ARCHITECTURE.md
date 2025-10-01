# Laboratory Management System - Architecture Documentation
# 实验室管理系统 - 架构文档

## System Overview (系统概述)

The Laboratory Management System is a WPF desktop application designed to manage laboratory workflows, including projects, modules, resources, platform tasks, and production line processes. The system follows a modular architecture inspired by VS Code's UI design.

## Core Philosophy: "Everything is a Module" (万物皆是模块)

The system is built on the principle that all components in the laboratory can be represented as modules. This enables flexible composition of workflows where complex processes are built by combining simple, reusable modules.

### Module Types

1. **Standard Modules** - General-purpose modules
2. **Warehouse Modules (仓库)** - Storage and retrieval operations
3. **Robot Modules (机器人)** - Robotic manipulation operations
4. **AGV Modules** - Automated guided vehicle operations
5. **Transfer Modules (中转)** - Material transfer operations
6. **Custom Modules** - User-defined modules

## Architecture Layers

### 1. Presentation Layer (Views)

#### MainWindow
- VS Code-like interface with three-column layout:
  - Activity Bar (50px) - Icon-based navigation
  - Sidebar (300px) - Context-specific content
  - Main Content Area - Primary workspace

#### Specialized Views
- **PlatformTaskEditor** - Visual workflow composer for platform tasks
- **ProductionLineEditor** - Production line process designer

### 2. ViewModel Layer (MVVM Pattern)

#### MainViewModel
- Central ViewModel managing application state
- Coordinates between different views
- Implements INotifyPropertyChanged for data binding

#### Command Pattern
- **RelayCommand** - Generic command implementation
- Supports CanExecute and Execute patterns

### 3. Model Layer

#### Core Data Models

**LaboratoryConfig** (Root Configuration)
```
LaboratoryConfig
├── Projects: ObservableCollection<LaboratoryProject>
├── Modules: ObservableCollection<Module>
├── Resources: ObservableCollection<Resource>
├── PlatformTasks: ObservableCollection<PlatformTask>
└── ProductionLines: ObservableCollection<ProductionLine>
```

**LaboratoryProject**
- Represents a laboratory project
- Contains module and resource references
- Tracks project status and timeline

**Module**
- Defines functional modules
- Contains actions and parameters
- Supports different module types

**PlatformTask**
- Executable task composed of workflow steps
- Each step references a module action
- Can be executed via gRPC service

**ProductionLine**
- Contains multiple production processes
- Each process is composed of steps
- Steps can be:
  - Platform Tasks (平台任务)
  - Transfer Operations (中转)
  - Module Actions (模块动作)

### 4. Service Layer

#### IPlatformTaskService
- Interface for executing platform tasks
- Methods:
  - `ExecuteTaskAsync(PlatformTask, parameters)` - Execute a task
  - `GetTaskStatusAsync(executionId)` - Query task status
  - `CancelTaskAsync(executionId)` - Cancel running task

#### ILaboratoryConfigService
- Interface for configuration persistence
- Methods:
  - `LoadConfigAsync(path)` - Load configuration
  - `SaveConfigAsync(config, path)` - Save configuration

### 5. Communication Layer (gRPC)

#### PlatformCallService
Protocol Buffers definition in `Protos/platformcall.proto`

**Service Methods:**
1. `ExecutePlatformTask` - Execute a platform task
2. `GetTaskStatus` - Get execution status
3. `CancelTask` - Cancel running task

**Message Types:**
- `PlatformTaskRequest` - Task execution request
- `ModuleAction` - Individual module action
- `TaskStatusResponse` - Status information
- More defined in the proto file

## Data Flow

### Platform Task Execution Flow

```
User Action (UI)
    ↓
MainViewModel Command
    ↓
PlatformTaskService.ExecuteTaskAsync()
    ↓
gRPC Client (PlatformCallService)
    ↓
Backend Service
    ↓
Status Updates via GetTaskStatusAsync()
    ↓
UI Updates via INotifyPropertyChanged
```

### Production Line Composition Flow

```
User Drags Module/Task (UI)
    ↓
Drop Event Handler
    ↓
ProductionProcess.Steps.Add(ProcessStep)
    ↓
UI Updates (ObservableCollection)
    ↓
Can be saved via ILaboratoryConfigService
```

## UI/UX Design

### Color Scheme (VS Code Dark Theme)
- Background: `#1E1E1E`
- Sidebar: `#252526`
- Activity Bar: `#333333`
- Foreground: `#CCCCCC`
- Accent: `#007ACC`
- Border: `#3E3E42`

### Navigation Flow
1. User selects icon from Activity Bar
2. Sidebar updates with relevant content
3. Main content area shows detailed view
4. Action buttons appear in sidebar footer

### Drag-and-Drop Support
- Custom `DraggableItem` control for source items
- Custom `DropTarget` control for drop zones
- Visual feedback during drag operations

## Extension Points

### Adding New Module Types
1. Add enum value to `ModuleType`
2. Create module definition with actions
3. Add to LaboratoryConfig.Modules
4. Module automatically appears in editors

### Adding New Process Step Types
1. Add enum value to `ProcessStepType`
2. Implement step execution logic
3. Add UI representation in editor
4. Step can be used in production lines

### Custom gRPC Services
1. Define service in `.proto` file
2. Generate client code via Grpc.Tools
3. Implement service interface
4. Register service in dependency injection

## Performance Considerations

### Observable Collections
- Used throughout for automatic UI updates
- Efficient add/remove operations
- Minimal overhead for WPF data binding

### Async/Await Pattern
- All service calls are asynchronous
- Prevents UI blocking
- Proper cancellation token support

### Memory Management
- ViewModels implement INotifyPropertyChanged
- Proper event handler cleanup
- No circular references in data models

## Security Considerations

### Configuration Data
- Sensitive data should be encrypted at rest
- Consider using secure storage for credentials
- Implement access control for configuration changes

### gRPC Communication
- Should use TLS in production
- Implement authentication/authorization
- Validate all input data

## Future Enhancements

1. **Real-time Collaboration**
   - Multiple users editing workflows
   - Conflict resolution
   - Change tracking

2. **Advanced Workflow Features**
   - Conditional branching
   - Parallel execution
   - Error handling and retry logic

3. **Monitoring and Analytics**
   - Task execution metrics
   - Resource utilization tracking
   - Performance optimization

4. **Plugin System**
   - Third-party module integration
   - Custom UI extensions
   - External service connectors

## Testing Strategy

### Unit Tests
- ViewModel logic testing
- Model validation
- Command execution

### Integration Tests
- Service layer testing
- gRPC communication
- Configuration persistence

### UI Tests
- View rendering
- User interaction flows
- Drag-and-drop functionality

## Deployment

### Prerequisites
- .NET 8.0 Runtime (Windows)
- Windows 10/11
- Network access for gRPC services

### Installation
1. Install .NET 8.0 Runtime
2. Extract application files
3. Configure gRPC endpoints
4. Run LaboratoryManagementSystem.exe

### Configuration
- Edit app.config for endpoints
- Set up initial laboratory configuration
- Configure module definitions
