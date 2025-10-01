using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using LaboratoryManagementSystem.Models;
using LaboratoryManagementSystem.Services;

namespace LaboratoryManagementSystem.ViewModels
{
    /// <summary>
    /// Main ViewModel for the Laboratory Management System
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private LaboratoryConfig _laboratoryConfig;
        private object? _selectedMenuItem;
        private object? _currentView;
        private readonly ILaboratoryConfigService _configService;
        private readonly string _defaultConfigPath;

        public MainViewModel()
        {
            _laboratoryConfig = new LaboratoryConfig
            {
                Name = "Laboratory Management System"
            };

            _configService = new JsonConfigurationService();
            _defaultConfigPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "LaboratoryManagementSystem",
                "config.json"
            );

            InitializeCommands();
            InitializeSampleData();
            LoadConfigurationAsync();
        }

        public LaboratoryConfig LaboratoryConfig
        {
            get => _laboratoryConfig;
            set => SetProperty(ref _laboratoryConfig, value);
        }

        public object? SelectedMenuItem
        {
            get => _selectedMenuItem;
            set
            {
                if (SetProperty(ref _selectedMenuItem, value))
                {
                    OnMenuItemSelected();
                }
            }
        }

        public object? CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        // Commands
        public ICommand? AddProjectCommand { get; private set; }
        public ICommand? AddModuleCommand { get; private set; }
        public ICommand? AddResourceCommand { get; private set; }
        public ICommand? AddPlatformTaskCommand { get; private set; }
        public ICommand? AddProductionLineCommand { get; private set; }
        public ICommand? SaveConfigCommand { get; private set; }
        public ICommand? LoadConfigCommand { get; private set; }
        public ICommand? ExportConfigCommand { get; private set; }

        private void InitializeCommands()
        {
            AddProjectCommand = new RelayCommand(_ => AddProject());
            AddModuleCommand = new RelayCommand(_ => AddModule());
            AddResourceCommand = new RelayCommand(_ => AddResource());
            AddPlatformTaskCommand = new RelayCommand(_ => AddPlatformTask());
            AddProductionLineCommand = new RelayCommand(_ => AddProductionLine());
            SaveConfigCommand = new RelayCommand(_ => SaveConfigurationAsync());
            LoadConfigCommand = new RelayCommand(_ => LoadConfigurationAsync());
            ExportConfigCommand = new RelayCommand(_ => ExportConfigurationAsync());
        }

        private void InitializeSampleData()
        {
            // Add sample modules
            LaboratoryConfig.Modules.Add(new Module
            {
                Name = "Warehouse Module",
                Description = "Warehouse storage and retrieval module",
                Type = ModuleType.Warehouse
            });

            LaboratoryConfig.Modules.Add(new Module
            {
                Name = "Robot Arm Module",
                Description = "Robotic arm for handling operations",
                Type = ModuleType.Robot
            });

            LaboratoryConfig.Modules.Add(new Module
            {
                Name = "AGV Transport Module",
                Description = "Automated guided vehicle for material transport",
                Type = ModuleType.AGV
            });

            // Add sample resources
            LaboratoryConfig.Resources.Add(new Resource
            {
                Name = "Test Equipment A",
                Description = "Testing equipment for quality control",
                Type = ResourceType.Equipment,
                Status = ResourceStatus.Available
            });

            // Add sample platform task
            var task = new PlatformTask
            {
                Name = "Sample Platform Task",
                Description = "A sample platform task for demonstration"
            };
            LaboratoryConfig.PlatformTasks.Add(task);

            // Add sample production line
            var productionLine = new ProductionLine
            {
                Name = "Sample Production Line",
                Description = "A sample production line workflow"
            };
            LaboratoryConfig.ProductionLines.Add(productionLine);
        }

        private void AddProject()
        {
            var project = new LaboratoryProject
            {
                Name = $"New Project {LaboratoryConfig.Projects.Count + 1}",
                Description = "New laboratory project"
            };
            LaboratoryConfig.Projects.Add(project);
        }

        private void AddModule()
        {
            var module = new Module
            {
                Name = $"New Module {LaboratoryConfig.Modules.Count + 1}",
                Description = "New module"
            };
            LaboratoryConfig.Modules.Add(module);
        }

        private void AddResource()
        {
            var resource = new Resource
            {
                Name = $"New Resource {LaboratoryConfig.Resources.Count + 1}",
                Description = "New resource"
            };
            LaboratoryConfig.Resources.Add(resource);
        }

        private void AddPlatformTask()
        {
            var task = new PlatformTask
            {
                Name = $"New Task {LaboratoryConfig.PlatformTasks.Count + 1}",
                Description = "New platform task"
            };
            LaboratoryConfig.PlatformTasks.Add(task);
        }

        private void AddProductionLine()
        {
            var line = new ProductionLine
            {
                Name = $"New Production Line {LaboratoryConfig.ProductionLines.Count + 1}",
                Description = "New production line"
            };
            LaboratoryConfig.ProductionLines.Add(line);
        }

        private void OnMenuItemSelected()
        {
            // Handle menu item selection and change current view
            // This will be implemented based on the selected menu item
        }

        private async void SaveConfigurationAsync()
        {
            try
            {
                await _configService.SaveConfigAsync(LaboratoryConfig, _defaultConfigPath);
                MessageBox.Show("Configuration saved successfully!", "Save Configuration", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save configuration: {ex.Message}", "Save Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LoadConfigurationAsync()
        {
            try
            {
                var config = await _configService.LoadConfigAsync(_defaultConfigPath);
                LaboratoryConfig = config;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load configuration: {ex.Message}", "Load Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ExportConfigurationAsync()
        {
            try
            {
                var exportPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    $"LabConfig_{DateTime.Now:yyyyMMdd_HHmmss}.json"
                );

                await _configService.SaveConfigAsync(LaboratoryConfig, exportPath);
                MessageBox.Show($"Configuration exported to:\n{exportPath}", "Export Complete", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export configuration: {ex.Message}", "Export Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
