using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.BehaviorDemo.Controls;

namespace WPF.BehaviorDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowModel();
    }

    private void OpenEdit_Click(object sender, RoutedEventArgs e)
    {
      var lsit=  ViewComponentCanvas.Children.Cast<UIElement>().Where(P => P is ViewComponentControl).Cast<ViewComponentControl>().ToList();
        foreach (var item in lsit)
        {
            item.OpenEdit();
        }
    }

    private void CloseEdit_Click(object sender, RoutedEventArgs e)
    {
        var lsit = ViewComponentCanvas.Children.Cast<UIElement>().Where(P => P is ViewComponentControl).Cast<ViewComponentControl>().ToList();
        foreach (var item in lsit)
        {
            item.CloseEdit();
        }
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contextMenu = menuItem.Parent as ContextMenu;
        var viewComponentControl = (contextMenu.PlacementTarget as FrameworkElement).DataContext as ViewComponentControl;
        ViewComponentCanvas.Children.Remove(viewComponentControl);
    }

    private void EditOption_Click(object sender, RoutedEventArgs e)
    {
        var menuItem = sender as MenuItem;
        var contextMenu = menuItem.Parent as ContextMenu;
        var viewComponentControl = (contextMenu.PlacementTarget as FrameworkElement).DataContext as ViewComponentControl;
        viewComponentControl?.ShowEditOptionView();
    }
}