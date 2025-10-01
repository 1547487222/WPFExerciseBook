using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LaboratoryManagementSystem.Models;

namespace LaboratoryManagementSystem.Services
{
    /// <summary>
    /// JSON-based configuration persistence service
    /// </summary>
    public class JsonConfigurationService : ILaboratoryConfigService
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public JsonConfigurationService()
        {
            _serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
        }

        public async Task<LaboratoryConfig> LoadConfigAsync(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    // Return default configuration if file doesn't exist
                    return CreateDefaultConfiguration();
                }

                using var fileStream = File.OpenRead(path);
                var config = await JsonSerializer.DeserializeAsync<LaboratoryConfig>(fileStream, _serializerOptions);
                
                return config ?? CreateDefaultConfiguration();
            }
            catch (Exception ex)
            {
                // Log error (in production, use proper logging)
                Console.WriteLine($"Error loading configuration: {ex.Message}");
                return CreateDefaultConfiguration();
            }
        }

        public async Task SaveConfigAsync(LaboratoryConfig config, string path)
        {
            try
            {
                // Ensure directory exists
                var directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Update modified date
                config.ModifiedDate = DateTime.Now;

                // Serialize to JSON
                using var fileStream = File.Create(path);
                await JsonSerializer.SerializeAsync(fileStream, config, _serializerOptions);
            }
            catch (Exception ex)
            {
                // Log error (in production, use proper logging)
                Console.WriteLine($"Error saving configuration: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ExportConfigAsync(LaboratoryConfig config, string path, ExportFormat format)
        {
            try
            {
                switch (format)
                {
                    case ExportFormat.Json:
                        await SaveConfigAsync(config, path);
                        return true;

                    case ExportFormat.Xml:
                        // Future implementation for XML export
                        throw new NotImplementedException("XML export not yet implemented");

                    default:
                        throw new ArgumentException($"Unsupported export format: {format}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error exporting configuration: {ex.Message}");
                return false;
            }
        }

        public async Task<LaboratoryConfig?> ImportConfigAsync(string path, ExportFormat format)
        {
            try
            {
                switch (format)
                {
                    case ExportFormat.Json:
                        return await LoadConfigAsync(path);

                    case ExportFormat.Xml:
                        // Future implementation for XML import
                        throw new NotImplementedException("XML import not yet implemented");

                    default:
                        throw new ArgumentException($"Unsupported import format: {format}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing configuration: {ex.Message}");
                return null;
            }
        }

        private LaboratoryConfig CreateDefaultConfiguration()
        {
            var config = new LaboratoryConfig
            {
                Name = "Default Laboratory Configuration",
                Description = "Default configuration with sample data"
            };

            // Add sample modules
            config.Modules.Add(new Module
            {
                Name = "Warehouse Module",
                Description = "Storage and retrieval operations",
                Type = ModuleType.Warehouse,
                Version = "1.0.0"
            });

            config.Modules.Add(new Module
            {
                Name = "Robot Arm Module",
                Description = "Robotic manipulation operations",
                Type = ModuleType.Robot,
                Version = "1.0.0"
            });

            config.Modules.Add(new Module
            {
                Name = "AGV Transport Module",
                Description = "Automated guided vehicle for transport",
                Type = ModuleType.AGV,
                Version = "1.0.0"
            });

            config.Modules.Add(new Module
            {
                Name = "Transfer Station",
                Description = "Material transfer operations",
                Type = ModuleType.Transfer,
                Version = "1.0.0"
            });

            // Add sample resources
            config.Resources.Add(new Resource
            {
                Name = "Test Equipment A",
                Description = "Quality testing equipment",
                Type = ResourceType.Equipment,
                Status = ResourceStatus.Available,
                Location = "Lab Room 101"
            });

            config.Resources.Add(new Resource
            {
                Name = "Material Storage",
                Description = "Raw material storage area",
                Type = ResourceType.Facility,
                Status = ResourceStatus.Available,
                Location = "Warehouse A"
            });

            // Add sample platform task
            var sampleTask = new PlatformTask
            {
                Name = "Quality Inspection Task",
                Description = "Perform quality inspection on products",
                Priority = 5
            };
            config.PlatformTasks.Add(sampleTask);

            // Add sample production line
            var sampleLine = new ProductionLine
            {
                Name = "Assembly Line 1",
                Description = "Main assembly production line"
            };
            config.ProductionLines.Add(sampleLine);

            return config;
        }
    }

    /// <summary>
    /// Export/Import format enumeration
    /// </summary>
    public enum ExportFormat
    {
        Json,
        Xml
    }
}
