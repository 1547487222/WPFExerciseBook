using System;
using System.Collections.ObjectModel;

namespace LaboratoryManagementSystem.Models
{
    /// <summary>
    /// Laboratory Configuration - Main configuration structure
    /// </summary>
    public class LaboratoryConfig
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        
        // Projects in the laboratory
        public ObservableCollection<LaboratoryProject> Projects { get; set; } = new();
        
        // Available modules
        public ObservableCollection<Module> Modules { get; set; } = new();
        
        // Available resources
        public ObservableCollection<Resource> Resources { get; set; } = new();
        
        // Platform tasks
        public ObservableCollection<PlatformTask> PlatformTasks { get; set; } = new();
        
        // Production lines
        public ObservableCollection<ProductionLine> ProductionLines { get; set; } = new();
    }

    /// <summary>
    /// Laboratory Project
    /// </summary>
    public class LaboratoryProject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ProjectStatus Status { get; set; } = ProjectStatus.Planning;
        
        // Module references used in this project
        public ObservableCollection<ModuleReference> ModuleReferences { get; set; } = new();
        
        // Resource references used in this project
        public ObservableCollection<ResourceReference> ResourceReferences { get; set; } = new();
    }

    /// <summary>
    /// Module - Represents a functional module in the system
    /// </summary>
    public class Module
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ModuleType Type { get; set; } = ModuleType.Standard;
        public string Version { get; set; } = "1.0.0";
        
        // Available actions for this module
        public ObservableCollection<ModuleAction> Actions { get; set; } = new();
        
        // Module parameters
        public ObservableCollection<ModuleParameter> Parameters { get; set; } = new();
    }

    /// <summary>
    /// Module Action - Actions that can be performed by a module
    /// </summary>
    public class ModuleAction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ObservableCollection<ActionParameter> Parameters { get; set; } = new();
    }

    /// <summary>
    /// Resource - Represents a resource in the laboratory
    /// </summary>
    public class Resource
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ResourceType Type { get; set; } = ResourceType.Equipment;
        public ResourceStatus Status { get; set; } = ResourceStatus.Available;
        public string Location { get; set; } = string.Empty;
        public ObservableCollection<ResourceProperty> Properties { get; set; } = new();
    }

    /// <summary>
    /// Platform Task - A task that can be executed on the platform
    /// </summary>
    public class PlatformTask
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; } = 0;
        
        // Workflow steps for this task
        public ObservableCollection<WorkflowStep> WorkflowSteps { get; set; } = new();
        
        // Parameters for the task
        public ObservableCollection<TaskParameter> Parameters { get; set; } = new();
    }

    /// <summary>
    /// Production Line - Represents a production line workflow
    /// </summary>
    public class ProductionLine
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        // Production line processes (工艺流程)
        public ObservableCollection<ProductionProcess> Processes { get; set; } = new();
    }

    /// <summary>
    /// Production Process - A workflow process in a production line
    /// Composed of platform tasks, transfers, and modules
    /// </summary>
    public class ProductionProcess
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Sequence { get; set; } = 0;
        
        // Process steps - can be platform tasks, transfers, or module actions
        public ObservableCollection<ProcessStep> Steps { get; set; } = new();
    }

    /// <summary>
    /// Process Step - A step in a production process
    /// </summary>
    public class ProcessStep
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public ProcessStepType Type { get; set; } = ProcessStepType.PlatformTask;
        public int Sequence { get; set; } = 0;
        
        // Reference to the actual item (platform task, module, etc.)
        public string ReferenceId { get; set; } = string.Empty;
        
        // Step parameters
        public ObservableCollection<StepParameter> Parameters { get; set; } = new();
    }

    /// <summary>
    /// Workflow Step - A step in a platform task workflow
    /// </summary>
    public class WorkflowStep
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public int Sequence { get; set; } = 0;
        public string ModuleId { get; set; } = string.Empty;
        public string ActionId { get; set; } = string.Empty;
        public ObservableCollection<StepParameter> Parameters { get; set; } = new();
    }

    // Reference types
    public class ModuleReference
    {
        public string ModuleId { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
    }

    public class ResourceReference
    {
        public string ResourceId { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
    }

    // Parameter types
    public class ModuleParameter
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "string";
        public string DefaultValue { get; set; } = string.Empty;
        public bool Required { get; set; } = false;
    }

    public class ActionParameter
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "string";
        public string DefaultValue { get; set; } = string.Empty;
        public bool Required { get; set; } = false;
    }

    public class TaskParameter
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class StepParameter
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class ResourceProperty
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    // Enums
    public enum ProjectStatus
    {
        Planning,
        InProgress,
        Completed,
        Suspended,
        Cancelled
    }

    public enum ModuleType
    {
        Standard,
        Warehouse,      // 仓库
        Robot,          // 机器人
        AGV,            // AGV
        Transfer,       // 中转
        Custom
    }

    public enum ResourceType
    {
        Equipment,
        Material,
        Tool,
        Facility,
        Other
    }

    public enum ResourceStatus
    {
        Available,
        InUse,
        Maintenance,
        Offline
    }

    public enum ProcessStepType
    {
        PlatformTask,   // 平台任务
        Transfer,       // 中转
        ModuleAction    // 模块动作
    }
}
