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
            ViewComponentManager.RegisterViewComponent("按钮",typeof(ButtonComponentView));
            ViewComponentManager.RegisterViewComponent("表格", typeof(TableComponentView));
            ViewComponentManager.RegisterViewComponent("列表", typeof(ListComponentView));
            ViewComponents = new ObservableCollection<string>(ViewComponentManager.GetViewComponentDescriptions().Keys);
        }
    }
}
