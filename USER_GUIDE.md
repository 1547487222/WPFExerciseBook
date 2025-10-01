# Laboratory Management System - User Guide
# 实验室管理系统 - 用户指南

## Getting Started (开始使用)

### Application Overview

The Laboratory Management System provides a unified interface for managing all aspects of laboratory operations, including:
- Laboratory Projects (实验室项目)
- Modules (模块)
- Resources (资源)
- Platform Tasks (平台任务)
- Production Lines (产线工艺流程)

### Main Interface

The application uses a VS Code-inspired interface with three main areas:

1. **Activity Bar** (Left-most column, 50px wide)
   - 📁 Projects - Manage laboratory projects
   - 🧩 Modules - Define and manage modules
   - 📦 Resources - Track laboratory resources
   - ⚙️ Platform Tasks - Create executable tasks
   - 🏭 Production Lines - Design production workflows

2. **Sidebar** (Second column, 300px wide)
   - Shows context-specific content based on selected activity
   - Displays lists of items relevant to current view
   - Contains "Add New" button for creating new items

3. **Main Content Area** (Right side, expandable)
   - Displays detailed information
   - Provides editing interfaces
   - Shows welcome screen by default

## Working with Projects (项目管理)

### Creating a New Project

1. Click the 📁 icon in the Activity Bar
2. Click the "+ Add Project" button in the sidebar
3. A new project will be created with default name
4. Click on the project to view/edit details

### Project Details

Each project contains:
- Name and Description
- Creation and modification dates
- Status (Planning, In Progress, Completed, Suspended, Cancelled)
- Module References - Modules used in this project
- Resource References - Resources allocated to this project

## Working with Modules (模块管理)

### Module Types

The system supports several module types:
- **Standard** - General-purpose modules
- **Warehouse (仓库)** - Storage and retrieval operations
- **Robot (机器人)** - Robotic manipulation operations
- **AGV** - Automated guided vehicle operations
- **Transfer (中转)** - Material transfer operations
- **Custom** - User-defined modules

### Creating a Module

1. Click the 🧩 icon in the Activity Bar
2. Click "+ Add Module" in the sidebar
3. Configure module properties:
   - Name
   - Description
   - Type
   - Version
   - Actions (what the module can do)
   - Parameters (module configuration)

### Module Actions

Each module can have multiple actions:
- Action name and description
- Input parameters
- Expected behavior

Example: A Warehouse module might have actions like:
- Store Item
- Retrieve Item
- Check Inventory
- Reserve Location

## Working with Resources (资源管理)

### Resource Types

Resources can be:
- Equipment - Testing and manufacturing equipment
- Material - Raw materials and consumables
- Tool - Hand tools and instruments
- Facility - Rooms, storage areas, etc.
- Other - Miscellaneous resources

### Resource Status

Each resource has a status:
- Available - Ready for use
- In Use - Currently being used
- Maintenance - Under maintenance or repair
- Offline - Not available

### Creating a Resource

1. Click the 📦 icon in the Activity Bar
2. Click "+ Add Resource" in the sidebar
3. Fill in resource details:
   - Name and Description
   - Type
   - Status
   - Location
   - Properties (custom key-value pairs)

## Platform Tasks (平台任务)

Platform tasks are executable workflows composed of multiple steps. Each step performs a module action.

### Creating a Platform Task

1. Click the ⚙️ icon in the Activity Bar
2. Click "+ Add Platform Task" in the sidebar
3. The Platform Task Editor opens

### Platform Task Editor

The editor has two main areas:

**Toolbox (Left side)**
- Lists available modules
- Grouped by type
- Shows transfer operations

**Workflow Canvas (Right side)**
- Drag modules from toolbox
- Drop onto canvas to add steps
- Configure each step's parameters
- Reorder steps as needed

### Workflow Steps

Each step in a platform task:
- References a specific module
- Specifies an action to perform
- Contains parameters for the action
- Has a sequence number

### Executing Platform Tasks

1. Design the workflow in the editor
2. Save the task
3. Click "▶️ Execute" to run the task
4. Monitor execution status
5. View results when complete

Platform tasks are executed via the gRPC PlatformCallService.

## Production Lines (产线工艺流程)

Production lines represent complete manufacturing or testing workflows. They are more complex than platform tasks and can include:
- Platform Tasks (平台任务)
- Transfer Operations (中转)
- Module Actions (模块动作)

### Creating a Production Line

1. Click the 🏭 icon in the Activity Bar
2. Click "+ Add Production Line" in the sidebar
3. The Production Line Editor opens

### Production Line Editor

**Toolbox (Left side)**
Three expandable sections:
- Platform Tasks (平台任务) - Existing platform tasks
- Transfer Operations (中转) - Transfer points
- Modules (模块) - Individual module actions

**Workflow Canvas (Right side)**
- Visual workflow designer
- Shows process flow with arrows
- Each step shows type and details
- Drag-and-drop composition

### Production Process Steps

Steps in a production process can be one of three types:

1. **Platform Task** - Execute a complete platform task
   - Shows as blue/teal colored step
   - Includes all actions from the task

2. **Transfer** - Material transfer operation
   - Shows as orange colored step
   - Represents movement between locations

3. **Module Action** - Direct module action
   - Shows with module-specific color
   - Single action execution

### Workflow Composition

To build a production workflow:

1. **Name your production line and process**
   - Set production line name
   - Set current process name

2. **Add steps by dragging**
   - Drag platform task to add complete task
   - Drag transfer operation to add transfer
   - Drag module to add module action

3. **Configure each step**
   - Click ⚙️ icon to configure
   - Set parameters
   - Adjust timing

4. **Arrange steps**
   - Steps execute in order
   - Use arrows to show flow
   - Can add branching (future feature)

5. **Add multiple processes**
   - Click "+ Add Process" to add more
   - Multiple processes in one production line
   - Each process is independent

6. **Save and validate**
   - Click "💾 Save" to save changes
   - Click "✓ Validate" to check workflow

### Example Production Line

```
Production Line: "Assembly Line 1"
  ├─ Process: "Quality Inspection"
  │   ├─ Step 1: Platform Task (Quality Check)
  │   ├─ Step 2: Transfer (To Assembly)
  │   └─ Step 3: Module (Robot - Pick and Place)
  │
  └─ Process: "Final Assembly"
      ├─ Step 1: Module (Warehouse - Retrieve Parts)
      ├─ Step 2: Module (Robot - Assemble)
      ├─ Step 3: Platform Task (Final Test)
      └─ Step 4: Transfer (To Packaging)
```

## Tips and Best Practices

### Organizing Modules

1. **Use clear naming conventions**
   - Include module type in name
   - Version numbers for tracking
   - Descriptive action names

2. **Group related modules**
   - Organize by function
   - Keep similar types together
   - Document dependencies

3. **Define reusable actions**
   - Make actions generic
   - Use parameters for flexibility
   - Test thoroughly before use

### Designing Platform Tasks

1. **Keep tasks focused**
   - Each task should have a clear purpose
   - Avoid overly complex workflows
   - Break down into smaller tasks if needed

2. **Handle errors gracefully**
   - Consider failure scenarios
   - Plan for retry logic
   - Document expected behavior

3. **Test incrementally**
   - Test each step individually
   - Combine into full workflow
   - Monitor execution carefully

### Building Production Lines

1. **Plan the workflow**
   - Map out entire process first
   - Identify required modules
   - Note transfer points

2. **Use platform tasks for common sequences**
   - Reuse tested task workflows
   - Maintain consistency
   - Easier to update

3. **Document the process**
   - Add clear descriptions
   - Note special requirements
   - Record any assumptions

### Managing Resources

1. **Keep status updated**
   - Mark resources in use
   - Note maintenance schedules
   - Track location changes

2. **Add relevant properties**
   - Specifications
   - Calibration dates
   - Contact information

3. **Regular audits**
   - Verify resource availability
   - Check condition
   - Update documentation

## Keyboard Shortcuts (Future Enhancement)

- `Ctrl+N` - New item
- `Ctrl+S` - Save
- `Ctrl+E` - Execute task
- `Delete` - Remove selected item
- `F2` - Rename item

## Troubleshooting

### Task Execution Fails

1. Check module availability
2. Verify parameters are correct
3. Check gRPC service connection
4. Review error messages

### Drag-and-Drop Not Working

1. Ensure item is draggable
2. Check drop target is enabled
3. Verify no modal dialogs are open
4. Restart application if needed

### Items Not Appearing

1. Check filter settings
2. Verify data is saved
3. Reload configuration
4. Check for errors in log

## Support and Documentation

For additional help:
- See ARCHITECTURE.md for technical details
- Check README.md for setup instructions
- Review code comments for specific features
- Contact system administrator for support
