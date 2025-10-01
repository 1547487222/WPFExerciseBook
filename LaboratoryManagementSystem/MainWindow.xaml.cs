using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.IO;

namespace LaboratoryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowProjectsView(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            ProjectsPanel.Visibility = Visibility.Visible;
            ContentTitle.Text = "Laboratory Projects";
            SidebarTitle.Text = "PROJECTS";
            SidebarActionButton.Content = "+ Add Project";
            SidebarActionButton.Command = (DataContext as ViewModels.MainViewModel)?.AddProjectCommand;
        }

        private void ShowModulesView(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            ModulesPanel.Visibility = Visibility.Visible;
            ContentTitle.Text = "Available Modules";
            SidebarTitle.Text = "MODULES";
            SidebarActionButton.Content = "+ Add Module";
            SidebarActionButton.Command = (DataContext as ViewModels.MainViewModel)?.AddModuleCommand;
        }

        private void ShowResourcesView(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            ResourcesPanel.Visibility = Visibility.Visible;
            ContentTitle.Text = "Laboratory Resources";
            SidebarTitle.Text = "RESOURCES";
            SidebarActionButton.Content = "+ Add Resource";
            SidebarActionButton.Command = (DataContext as ViewModels.MainViewModel)?.AddResourceCommand;
        }

        private void ShowPlatformTasksView(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            PlatformTasksPanel.Visibility = Visibility.Visible;
            ContentTitle.Text = "Platform Tasks";
            SidebarTitle.Text = "PLATFORM TASKS";
            SidebarActionButton.Content = "+ Add Platform Task";
            SidebarActionButton.Command = (DataContext as ViewModels.MainViewModel)?.AddPlatformTaskCommand;
        }

        private void ShowProductionLinesView(object sender, RoutedEventArgs e)
        {
            HideAllPanels();
            ProductionLinesPanel.Visibility = Visibility.Visible;
            ContentTitle.Text = "Production Lines (工艺流程)";
            SidebarTitle.Text = "PRODUCTION LINES";
            SidebarActionButton.Content = "+ Add Production Line";
            SidebarActionButton.Command = (DataContext as ViewModels.MainViewModel)?.AddProductionLineCommand;
        }

        private void HideAllPanels()
        {
            WelcomePanel.Visibility = Visibility.Collapsed;
            ProjectsPanel.Visibility = Visibility.Collapsed;
            ModulesPanel.Visibility = Visibility.Collapsed;
            ResourcesPanel.Visibility = Visibility.Collapsed;
            PlatformTasksPanel.Visibility = Visibility.Collapsed;
            ProductionLinesPanel.Visibility = Visibility.Collapsed;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UserGuide_Click(object sender, RoutedEventArgs e)
        {
            var userGuidePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "USER_GUIDE.md");
            if (File.Exists(userGuidePath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = userGuidePath,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show($"User Guide location:\n{Path.GetFullPath(userGuidePath)}", 
                        "User Guide", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please see USER_GUIDE.md in the repository root.", 
                    "User Guide", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Laboratory Management System\n" +
                "Version 1.0.0\n\n" +
                "A comprehensive laboratory management system with VS Code-like interface.\n\n" +
                "Features:\n" +
                "• Laboratory project management\n" +
                "• Module and resource tracking\n" +
                "• Platform task composition\n" +
                "• Production line workflow designer\n\n" +
                "Built with .NET 8.0 and WPF\n" +
                "Following MVVM pattern",
                "About Laboratory Management System",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
