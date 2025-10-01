using System.Windows;
using System.Windows.Controls;

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
    }
}
