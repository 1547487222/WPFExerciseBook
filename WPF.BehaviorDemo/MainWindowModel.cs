using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPF.BehaviorDemo.ComponentViews;
using WPF.BehaviorDemo.Models;
using WPF.BehaviorDemo.ViewComponents;

namespace WPF.BehaviorDemo
{
  public  class MainWindowModel
    {
        public ObservableCollection<string> ViewComponents { get; set; }
        public MainWindowModel()
        {
            ViewComponentManager.RegisterViewComponent("按钮",typeof(ButtonComponentView),typeof(ButtonComponentViewModel));
            ViewComponentManager.RegisterViewComponent("表格", typeof(TableComponentView), typeof(ButtonComponentViewModel));
            ViewComponentManager.RegisterViewComponent("列表", typeof(ListComponentView), typeof(ButtonComponentViewModel));
            ViewComponentManager.Build();
            ViewComponents = [.. ViewComponentManager.GetViewComponentDescriptions().Keys];
        }
    }
}
